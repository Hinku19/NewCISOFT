using System;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using CISOFTDataOperations.Database;

namespace CISOFTDataOperations.Security
{
    public static class UserAuthentication
    {
        private const int Iterations = 10000;
        private const int HashSize = 32;

        public static UserSession Authenticate(
            string nick,
            string password)
        {
            if (string.IsNullOrWhiteSpace(nick))
            {
                return null;
            }

            if (string.IsNullOrEmpty(password))
            {
                return null;
            }

            using (SqlConnection connection = Connection.VisionSolutions())
            {
                connection.Open();

                string sql = @"
                    SELECT TOP 1
                        U.UserId,
                        U.Name,
                        U.Password,
                        U.Salt,
                        A.CompanyId,
                        A.DivisionId,
                        A.SubsidiaryId
                    FROM VisionSolutions_Users U
                    LEFT JOIN VisionSolutions_UserAccess A
                        ON A.UserId = U.UserId
                        AND A.DefaultStart = 1
                    WHERE U.Nick = @Nick
                      AND U.IsActive = 1;";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.CommandTimeout = 300;

                    command.Parameters.Add(
                        "@Nick",
                        System.Data.SqlDbType.VarChar, 50).Value =
                        nick.Trim();

                    using (SqlDataReader reader =
                        command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        int userId =
                            Convert.ToInt32(reader["UserId"]);
                        string dbUserName =
                            reader["Name"] == DBNull.Value
                                ? string.Empty
                                : reader["Name"].ToString();        

                        string storedPassword =
                            reader["Password"] == DBNull.Value
                                ? string.Empty
                                : reader["Password"].ToString();

                        string storedSalt =
                            reader["Salt"] == DBNull.Value
                                ? string.Empty
                                : reader["Salt"].ToString();

                        int companyId = 0;
                        int divisionId = 0;
                        int subsidiaryId = 0;

                        if (reader["CompanyId"] != DBNull.Value)
                        {
                            companyId =
                                Convert.ToInt32(reader["CompanyId"]);
                        }

                        if (reader["DivisionId"] != DBNull.Value)
                        {
                            divisionId =
                                Convert.ToInt32(reader["DivisionId"]);
                        }

                        if (reader["SubsidiaryId"] != DBNull.Value)
                        {
                            subsidiaryId =
                                Convert.ToInt32(reader["SubsidiaryId"]);
                        }

                        bool passwordValid =
                            VerifyPassword(
                                password,
                                storedPassword,
                                storedSalt);

                        if (!passwordValid)
                        {
                            return null;
                        }

                        return new UserSession(
                            userId,
                            dbUserName,
                            companyId,
                            divisionId,
                            subsidiaryId);
                    }
                }
            }
        }

        private static bool VerifyPassword(
            string password,
            string storedHash,
            string storedSalt)
        {
            if (string.IsNullOrEmpty(storedHash) ||
                string.IsNullOrEmpty(storedSalt))
            {
                return false;
            }

            byte[] salt;

            try
            {
                salt = Convert.FromBase64String(storedSalt);
            }
            catch
            {
                return false;
            }

            byte[] expectedHash;

            try
            {
                expectedHash =
                    Convert.FromBase64String(storedHash);
            }
            catch
            {
                return false;
            }

            byte[] calculatedHash;

            using (Rfc2898DeriveBytes pbkdf2 =
                new Rfc2898DeriveBytes(
                    password,
                    salt,
                    Iterations,
                    HashAlgorithmName.SHA256))
            {
                calculatedHash =
                    pbkdf2.GetBytes(HashSize);
            }

            return CryptographicOperations.FixedTimeEquals(
                calculatedHash,
                expectedHash);
        }
    }
}
