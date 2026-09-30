using System;
using System.Security.Cryptography;
using System.Text;

namespace VisionSolutionsDataOperations.Database
{
    public static class Encrypt
    {
        private const string EncryptionPassword = "SomosTuGente";

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                keyArray = MD5.HashData(
                    Encoding.UTF8.GetBytes(EncryptionPassword));
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(EncryptionPassword);
            }

            using (TripleDES tdes = TripleDES.Create())
            {
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = tdes.CreateDecryptor())
                {
                    byte[] resultArray = decryptor.TransformFinalBlock(
                        toEncryptArray,
                        0,
                        toEncryptArray.Length);

                    return Encoding.UTF8.GetString(resultArray);
                }
            }
        }

        public static string EncryptString(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray =
                Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                keyArray = MD5.HashData(
                    Encoding.UTF8.GetBytes(EncryptionPassword));
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(EncryptionPassword);
            }

            using (TripleDES tdes = TripleDES.Create())
            {
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = tdes.CreateEncryptor())
                {
                    byte[] resultArray = encryptor.TransformFinalBlock(
                        toEncryptArray,
                        0,
                        toEncryptArray.Length);

                    return Convert.ToBase64String(resultArray);
                }
            }
        }
    }
}
