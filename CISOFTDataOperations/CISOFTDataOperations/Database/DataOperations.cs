using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CISOFTDataOperations.Database
{
    public static class DataOperations
    {
        /// <summary>
        /// Ejecuta un SELECT y devuelve un DataSet.
        /// </summary>
        public static DataSet GetData(SqlConnection connection, string sql)
        {
            DataSet ds = new DataSet();

            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, connection))
                    {
                        da.SelectCommand.CommandTimeout = 300;
                        da.Fill(ds);
                    }
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }

        /// <summary>
        /// Ejecuta INSERT, UPDATE y DELETE.
        /// </summary>
        public static int Execute(SqlConnection connection, string sql)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.CommandTimeout = 300;

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un único valor.
        /// </summary>
        public static object ExecuteScalar(SqlConnection connection, string sql)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.CommandTimeout = 300;

                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ejecuta un Stored Procedure y devuelve un DataSet.
        /// </summary>
        public static DataSet ExecuteProcedure(
            SqlConnection connection,
            string procedureName,
            Action<string> messageHandler,
            params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();

            try
            {
                connection.InfoMessage += delegate (
                    object sender,
                    SqlInfoMessageEventArgs e)
                {
                    if (messageHandler != null)
                    {
                        messageHandler(e.Message);
                    }
                };

                using (connection)
                {
                    connection.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;

                        if (parameters != null &&
                            parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter da =
                            new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }
        public static DataSet ExecuteProcedure(
        SqlConnection connection,
        string procedureName,
        params SqlParameter[] parameters)
        {
            return ExecuteProcedure(
                connection,
                procedureName,
                null,
                parameters);
        }
    }
}
