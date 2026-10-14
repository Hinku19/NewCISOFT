using System;
using System.Data;

namespace VisionSolutionsDataOperations.Database
{
    public class Utilities
    {
        public static int UserParameterIdentify(int value, string cadena)
        {
            int p1 = 0;
            int p2 = 0;
            int p3 = 0;
            int p4 = 0;

            // ==========================================
            // VALIDAR CADENA
            // ==========================================

            if (string.IsNullOrEmpty(cadena))
            {
                return 0;
            }

            // ==========================================
            // ENCONTRAR LOS SEPARADORES |
            // ==========================================

            for (int p = 0; p < cadena.Length; p++)
            {
                if (cadena.Substring(p, 1) == "|")
                {
                    if (p1 == 0)
                    {
                        p1 = p;
                    }
                    else if (p2 == 0)
                    {
                        p2 = p;
                    }
                    else if (p3 == 0)
                    {
                        p3 = p;
                    }
                    else
                    {
                        p4 = p;
                        break;
                    }
                }
            }

            // ==========================================
            // VALIDAR FORMATO
            // ==========================================

            if (p1 == 0 ||
                p2 == 0 ||
                p3 == 0 ||
                p4 == 0)
            {
                return 0;
            }

            // ==========================================
            // OBTENER VALORES
            // ==========================================

            string usuario =
                cadena.Substring(
                    p1 + 2,
                    p2 - p1 - 3
                ).Trim();

            string company =
                cadena.Substring(
                    p2 + 2,
                    p3 - p2 - 3
                ).Trim();

            string division =
                cadena.Substring(
                    p3 + 2,
                    p4 - p3 - 3
                ).Trim();

            string subsidiary =
                cadena.Substring(
                    p4 + 2
                ).Trim();

            // ==========================================
            // VALUE 1 - USUARIO
            // ==========================================

            if (value == 1)
            {
                string sql =
                    "SELECT UserId " +
                    "FROM VisionSolutions_Users " +
                    "WHERE Nick = '" +
                    usuario.Replace("'", "''") +
                    "'";

                DataSet ds =
                    DataOperations.GetData(
                        Connection.VisionSolutions(),
                        sql);

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(
                        ds.Tables[0].Rows[0]["UserId"]);
                }

                return 0;
            }

            // ==========================================
            // VALUE 2 - COMPAÑÍA
            // ==========================================

            if (value == 2)
            {
                string sql =
                    "SELECT CompanyId " +
                    "FROM VisionSolutions_Companies " +
                    "WHERE CompanyName = N'" +
                    company.Replace("'", "''") +
                    "'";

                DataSet ds =
                    DataOperations.GetData(
                        Connection.VisionSolutions(),
                        sql);

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(
                        ds.Tables[0].Rows[0]["CompanyId"]);
                }

                return 0;
            }

            // ==========================================
            // VALUE 3 - DIVISIÓN
            // VALIDANDO COMPAÑÍA
            // ==========================================

            if (value == 3)
            {
                string sql =
                    "SELECT D.DivisionId " +
                    "FROM VisionSolutions_Divisions D " +
                    "INNER JOIN VisionSolutions_Companies C " +
                    "    ON C.CompanyId = D.CompanyId " +
                    "WHERE D.DivisionName = N'" +
                    division.Replace("'", "''") +
                    "' " +
                    "AND C.CompanyName = N'" +
                    company.Replace("'", "''") +
                    "'";

                DataSet ds =
                    DataOperations.GetData(
                        Connection.VisionSolutions(),
                        sql);

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(
                        ds.Tables[0].Rows[0]["DivisionId"]);
                }

                return 0;
            }

            // ==========================================
            // VALUE 4 - SUBSIDIARIA
            // VALIDANDO COMPAÑÍA + DIVISIÓN
            // ==========================================

            if (value == 4)
            {
                string sql =
                    "SELECT S.SubsidiaryId " +
                    "FROM VisionSolutions_Subsidiaries S " +
                    "INNER JOIN VisionSolutions_Divisions D " +
                    "    ON D.DivisionId = S.DivisionId " +
                    "INNER JOIN VisionSolutions_Companies C " +
                    "    ON C.CompanyId = D.CompanyId " +
                    "WHERE S.SubsidiaryName = N'" +
                    subsidiary.Replace("'", "''") +
                    "' " +
                    "AND D.DivisionName = N'" +
                    division.Replace("'", "''") +
                    "' " +
                    "AND C.CompanyName = N'" +
                    company.Replace("'", "''") +
                    "'";

                DataSet ds =
                    DataOperations.GetData(
                        Connection.VisionSolutions(),
                        sql);

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(
                        ds.Tables[0].Rows[0]["SubsidiaryId"]);
                }

                return 0;
            }

            // ==========================================
            // VALUE NO VÁLIDO
            // ==========================================

            return 0;
        }

        public static int GetUserActionPermits(
            int User,
            string ActionPermit)
        {
            string sql =
                "SELECT Enabled " +
                "FROM VisionSolutions_SecurityUsersDetails " +
                "WHERE UserId = " + User +
                " AND MenuId = '" +
                ActionPermit.Replace("'", "''") +
                "'";

            DataSet ds =
                DataOperations.GetData(
                    Connection.VisionSolutions(),
                    sql);

            if (ds != null &&
                ds.Tables.Count > 0 &&
                ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(
                    ds.Tables[0].Rows[0]["Enabled"]);
            }

            return 0;
        }
    }
}