using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CISOFTDataOperations.Employees
{
    internal static class DataOperations
    {
        /// <summary>
        /// Ejecuta un SELECT y devuelve un DataSet.
        /// </summary>
        public static DataSet GetData(
            SqlConnection connection,
            string sql)
        {
            return Database.DataOperations.GetData(
                connection,
                sql);
        }

        /// <summary>
        /// Ejecuta INSERT, UPDATE y DELETE.
        /// </summary>
        public static int Execute(
            SqlConnection connection,
            string sql)
        {
            return Database.DataOperations.Execute(
                connection,
                sql);
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un único valor.
        /// </summary>
        public static object ExecuteScalar(
            SqlConnection connection,
            string sql)
        {
            return Database.DataOperations.ExecuteScalar(
                connection,
                sql);
        }

        /// <summary>
        /// Ejecuta un Stored Procedure y devuelve un DataSet.
        /// </summary>
        public static DataSet ExecuteProcedure(
            SqlConnection connection,
            string procedureName,
            params SqlParameter[] parameters)
        {
            return Database.DataOperations.ExecuteProcedure(
                connection,
                procedureName,
                null,
                parameters);
        }
    }
}
