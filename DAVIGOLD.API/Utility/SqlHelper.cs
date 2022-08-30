using System.Data.SqlClient;

namespace DAVIGOLD.API.Utility
{
    public static class SqlHelper
    {
        /// <summary>
        /// Execute Command for Sql
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool ExecuteCommandReturnBool(string connectionString, string commandText, params SqlParameter[] parameters)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = commandText;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                var response = cmd.ExecuteScalar();
                if (response != null)
                    result = !string.IsNullOrEmpty(Convert.ToString(response));
            }


            return result;
                

                
        }

        /// <summary>
        /// Execute procedure and Return string 
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="procName"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>

        public static string ExecuteProcedureReturnString(string connString, string commandText,
            params SqlParameter[] paramters)
        {
            string result = "";
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = commandText;
                    if (paramters != null)
                    {
                        command.Parameters.AddRange(paramters);
                    }
                    sqlConnection.Open();
                    var ret = command.ExecuteScalar();
                    if (ret != null)
                        result = Convert.ToString(ret);
                }
            }
            return result;
        }


        /// <summary>
        /// Execute SQl Command with Return Data
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="connString"></param>
        /// <param name="commandText"></param>
        /// <param name="translator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static TData ExtecuteCommandReturnData<TData>(string connString, string commandText,
           Func<SqlDataReader, TData> translator,
           params SqlParameter[] parameters)
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandText = commandText;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        TData elements;
                        try
                        {
                            elements = translator(reader);
                        }
                        finally
                        {
                            while (reader.NextResult())
                            { }
                        }
                        return elements;
                    }
                }
            }
        }

        ///Methods to get values of 
        ///individual columns from sql data reader
        #region Get Values from Sql Data Reader
        public static string GetNullableString(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? null : Convert.ToString(reader[colName]);
        }

        public static int GetNullableInt32(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToInt32(reader[colName]);
        }

        public static bool GetBoolean(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? default(bool) : Convert.ToBoolean(reader[colName]);
        }

        //this method is to check wheater column exists or not in data reader
        public static bool IsColumnExists(this System.Data.IDataRecord dr, string colName)
        {
            try
            {
                return (dr.GetOrdinal(colName) >= 0);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

    }
}
