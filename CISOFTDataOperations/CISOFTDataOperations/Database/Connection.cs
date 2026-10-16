using System;
using System.IO;
using System.Xml;
using Microsoft.Data.SqlClient;

namespace CISOFTDataOperations.Database
{
    public static class Connection
    {
        private static SqlConnection GetConnection(string certificate)
        {
            string filePath = Path.Combine(
                Constants.ConfigFolder,
                certificate);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo de configuración.",
                    filePath);
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePath);

            XmlNodeList conn = xDoc.GetElementsByTagName("Config");

            if (conn.Count == 0)
            {
                throw new InvalidOperationException(
                    "No se encontró el elemento <Config> en el archivo de configuración.");
            }

            string connectionString =
                Encrypt.Decrypt(conn[0]!.InnerText.Trim(), true);

            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(connectionString);

            builder.TrustServerCertificate = true;

            return new SqlConnection(builder.ConnectionString);
        }

        public static SqlConnection Principal()
        {
            return GetConnection("Certificate.xml");
        }

        public static SqlConnection Reportes()
        {
            return GetConnection("CertificateR.xml");
        }

        public static SqlConnection Inventario()
        {
            return GetConnection("CertificateInventario.xml");
        }

        public static SqlConnection InventarioHistorico()
        {
            return GetConnection("CertificateInventarioHistorico.xml");
        }

        public static SqlConnection Contabilidad()
        {
            return GetConnection("CertificateContabilidad.xml");
        }

        public static SqlConnection ContabilidadHistorico()
        {
            return GetConnection("CertificateContabilidadHistorico.xml");
        }

        public static SqlConnection Nomina()
        {
            return GetConnection("CertificateN.xml");
        }

        public static SqlConnection FondoAhorro()
        {
            return GetConnection("CertificateFondoAhorro.xml");
        }

        public static SqlConnection CFDI()
        {
            return GetConnection("CertificateCFDI.xml");
        }

        public static SqlConnection Infotronica()
        {
            return GetConnection("CertificateInfotronica.xml");
        }

        public static SqlConnection VisionSolutions()
        {
            return GetConnection("CertificateVisionSolutions.xml");
        }

        public static SqlConnection CiSoft()
        {
            return GetConnection("CertificateCiSoft.xml");
        }

        public static SqlConnection CiSoft_Prueba()
        {
            return GetConnection("CertificateCiSoft_Prueba.xml");
        }

        public static SqlConnection NewCiSoft()
        {
            return GetConnection("CertificateNewCiSoft.xml");
        }

        public static string GetConnectionString(string certificate)
        {
            string filePath = Path.Combine(
                Constants.ConfigFolder,
                certificate);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo de configuración.",
                    filePath);
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePath);

            XmlNodeList conn = xDoc.GetElementsByTagName("Config");

            if (conn.Count == 0)
            {
                throw new InvalidOperationException(
                    "No se encontró el elemento <Config> en el archivo de configuración.");
            }

            string connectionString =
                Encrypt.Decrypt(
                    conn[0]!.InnerText.Trim(),
                    true);

            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(connectionString);

            builder.TrustServerCertificate = true;

            return builder.ConnectionString;
        }
    }
}
