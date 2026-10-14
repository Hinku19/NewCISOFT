using System.Data;
using Microsoft.Data.SqlClient;

namespace VisionSolutionsDataOperations.Security
{
    public static class DataOperations
    {
        /// <summary>
        /// Obtiene un usuario por UserId.
        /// </summary>
        public static DataSet GetUser(int userId)
        {
            string sql = @"
                SELECT
                    UserId,
                    Name,
                    Nick,
                    Password,
                    IsActive,
                    EmployeeId,
                    Salt
                FROM VisionSolutions_Users
                WHERE UserId = @UserId";

            using (SqlConnection connection =
                Database.Connection.VisionSolutions())
            {
                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(
                        new SqlParameter("@UserId", userId));

                    return ExecuteQuery(command);
                }
            }
        }

        /// <summary>
        /// Obtiene un usuario por Nick.
        /// </summary>
        public static DataSet GetUserByNick(string nick)
        {
            string sql = @"
                SELECT
                    UserId,
                    Name,
                    Nick,
                    IsActive,
                    EmployeeId,
                    Salt
                FROM VisionSolutions_Users
                WHERE Nick = @Nick";

            using (SqlConnection connection =
                Database.Connection.VisionSolutions())
            {
                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(
                        new SqlParameter("@Nick", nick));

                    return ExecuteQuery(command);
                }
            }
        }

        /// <summary>
        /// Obtiene los accesos organizacionales del usuario.
        /// </summary>
        public static DataSet GetUserAccess(int userId)
        {
            string sql = @"
                SELECT
                    UA.UserId,
                    UA.CompanyId,
                    C.CompanyName,
                    UA.DivisionId,
                    D.DivisionName,
                    UA.SubsidiaryId,
                    S.SubsidiaryName,
                    UA.DefaultStart
                FROM VisionSolutions_UserAccess UA
                LEFT JOIN VisionSolutions_Companies C
                    ON C.CompanyId = UA.CompanyId
                LEFT JOIN VisionSolutions_Divisions D
                    ON D.DivisionId = UA.DivisionId
                LEFT JOIN VisionSolutions_Subsidiaries S
                    ON S.SubsidiaryId = UA.SubsidiaryId
                WHERE UA.UserId = @UserId
                ORDER BY
                    UA.CompanyId,
                    UA.DivisionId,
                    UA.SubsidiaryId";

            using (SqlConnection connection =
                Database.Connection.VisionSolutions())
            {
                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(
                        new SqlParameter("@UserId", userId));

                    return ExecuteQuery(command);
                }
            }
        }

        /// <summary>
        /// Obtiene la configuración de seguridad del usuario.
        /// </summary>
        public static DataSet GetSecurityUser(int userId)
        {
            string sql = @"
                SELECT
                    UserId,
                    IdentifiedBy,
                    SubsidiaryId
                FROM VisionSolutions_SecurityUsers
                WHERE UserId = @UserId";

            using (SqlConnection connection =
                Database.Connection.VisionSolutions())
            {
                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(
                        new SqlParameter("@UserId", userId));

                    return ExecuteQuery(command);
                }
            }
        }

        /// <summary>
        /// Obtiene los permisos asignados al usuario.
        /// </summary>
        public static DataSet GetUserPermissions(int userId)
        {
            string sql = @"
                SELECT
                    D.UserId,
                    D.MenuId,
                    M.Description,
                    M.Module,
                    M.Aplication,
                    M.OptionMenu,
                    D.Enabled
                FROM VisionSolutions_SecurityUsersDetails D
                LEFT JOIN VisionSolutions_SecurityMenuOptions M
                    ON M.MenuId = D.MenuId
                WHERE D.UserId = @UserId
                ORDER BY
                    M.Module,
                    M.OptionMenu,
                    D.MenuId";

            using (SqlConnection connection =
                Database.Connection.VisionSolutions())
            {
                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(
                        new SqlParameter("@UserId", userId));

                    return ExecuteQuery(command);
                }
            }
        }

        /// <summary>
        /// Obtiene las opciones de menú de VisionSolutions.
        /// </summary>
        public static DataSet GetMenuOptions()
        {
            string sql = @"
                SELECT
                    MenuId,
                    Description,
                    Module,
                    Aplication,
                    OptionMenu
                FROM VisionSolutions_SecurityMenuOptions
                ORDER BY
                    Module,
                    OptionMenu,
                    MenuId";

            using (SqlConnection connection =
                Database.Connection.VisionSolutions())
            {
                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    return ExecuteQuery(command);
                }
            }
        }

        /// <summary>
        /// Ejecuta una consulta y devuelve un DataSet.
        /// </summary>
        private static DataSet ExecuteQuery(
            SqlCommand command)
        {
            DataSet dataSet = new DataSet();

            command.Connection.Open();
            command.CommandTimeout = 300;

            using (SqlDataAdapter adapter =
                new SqlDataAdapter(command))
            {
                adapter.Fill(dataSet);
            }

            return dataSet;
        }

        /// <summary>
        /// Obtiene el acceso marcado como predeterminado para el usuario.
        /// </summary>
        public static DataSet GetDefaultAccess(int userId)
        {
            string sql = @"
        SELECT
            UA.UserId,
            UA.CompanyId,
            C.CompanyName,
            UA.DivisionId,
            D.DivisionName,
            UA.SubsidiaryId,
            S.SubsidiaryName,
            UA.DefaultStart
        FROM VisionSolutions_UserAccess UA
        LEFT JOIN VisionSolutions_Companies C
            ON C.CompanyId = UA.CompanyId
        LEFT JOIN VisionSolutions_Divisions D
            ON D.DivisionId = UA.DivisionId
        LEFT JOIN VisionSolutions_Subsidiaries S
            ON S.SubsidiaryId = UA.SubsidiaryId
        WHERE UA.UserId = @UserId
          AND UA.DefaultStart = 1
        ORDER BY
            UA.CompanyId,
            UA.DivisionId,
            UA.SubsidiaryId";

            using (SqlConnection connection =
                Database.Connection.VisionSolutions())
            {
                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(
                        new SqlParameter("@UserId", userId));

                    return ExecuteQuery(command);
                }
            }
        }
    }
}