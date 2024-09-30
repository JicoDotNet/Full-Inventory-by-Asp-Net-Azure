using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Sql.Entity;

namespace DataAccess.Sql
{
    public sealed class SqlDBAccess : SQLManager, ISqlDBAccess
    {
        private CommandType CommandType { get; }
        private SqlConnection SqlConnectionObject { get; set; }
        public ConnectionState SqlConnectionState { get; private set; }

        public SqlDBAccess(string connectionString) 
            : base(connectionString)
        {
            CommandType = CommandType.StoredProcedure;
            SqlConnectionObject = null;
        }

        #region Select Query
        public DataRow GetFirstOrDefaultData(string spName, INameValuePairs nameValuePairObject)
        {
            try
            {
                using (DataSet ds = Get(spName, nameValuePairObject))
                {
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                                return ds.Tables[0].Rows[0];
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetData(string spName, INameValuePairs nameValuePairObject)
        {
            try
            {
                using (DataSet ds = Get(spName, nameValuePairObject))
                {
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            return ds.Tables[0];
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDataSet(string spName, INameValuePairs nameValuePairObject)
        {
            try
            {
                using (DataSet ds = Get(spName, nameValuePairObject))
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet Get(string spName, INameValuePairs nameValuePairObject)
        {
            try
            {
                GetConnection();

                SqlCommand cmd = WriteSqlCommand(spName, nameValuePairObject);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        public object DataManipulation(string spName, 
            INameValuePairs nameValuePairObject, 
            string outParameterName)
        {
            try
            {
                IList<string> outParameter = null;
                if (!string.IsNullOrEmpty(outParameterName))
                {
                    outParameter = new List<string>
                    {
                        outParameterName
                    };
                }

                INameValuePairs keyValuePairs = DataManipulation(spName, nameValuePairObject, outParameter);

                return keyValuePairs?.FirstOrDefault(a => a.getName == outParameterName)?.getValue;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public INameValuePairs DataManipulation(string spName,
            INameValuePairs inParameterName,
            IList<string> outParameterName = null)
        {
            try
            {
                GetConnection();

                SqlCommand cmdObject = WriteSqlCommand(spName, inParameterName, outParameterName);
                
                cmdObject.ExecuteNonQuery();

                return ReadSqlCommand(cmdObject, outParameterName);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                CloseConnection();
            }
        }

        private SqlCommand WriteSqlCommand(string command, 
            INameValuePairs inParameterName = null,
            IList<string> outParameterName = null)
        {
            try
            {
                if (SqlConnectionObject.State == ConnectionState.Open)
                {
                    SqlCommand cmdObject = new SqlCommand(command)
                    {
                        Connection = this.SqlConnectionObject,
                        CommandType = CommandType
                    };
                    cmdObject.Parameters.Clear();
                    if (inParameterName != null)
                    {
                        foreach (INameValuePair inParam in inParameterName)
                        {
                            cmdObject.Parameters.Add(new SqlParameter(inParam.getName, inParam.getValue));
                        }
                    }

                    if (outParameterName != null)
                    {
                        foreach (string outParam in outParameterName)
                        {
                            cmdObject.Parameters.Add(outParam, SqlDbType.VarChar, int.MaxValue);
                            cmdObject.Parameters[outParam].Direction = ParameterDirection.Output;
                        }
                    }

                    return cmdObject;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private INameValuePairs ReadSqlCommand(SqlCommand command, 
            IList<string> outParamDictionary = null)
        {
            if (outParamDictionary != null)
            {
                INameValuePairs nvps = new NameValuePairs();
                foreach (string outParam in outParamDictionary)
                {
                    nvps.Add(new NameValuePair(outParam, command.Parameters[outParam].Value));
                }
                return nvps;
            }

            return null;
        }

        #region Connection
        private void GetConnection()
        {
            try
            {
                SqlConnectionObject = new SqlConnection(SqlConnectionString);

                if (SqlConnectionObject.State == ConnectionState.Open ||
                    SqlConnectionObject.State == ConnectionState.Broken ||
                    SqlConnectionObject.State == ConnectionState.Connecting)
                {
                    SqlConnectionObject.Close();
                    SqlConnectionObject.Open();
                }
                else if (SqlConnectionObject.State == ConnectionState.Closed)
                {
                    SqlConnectionObject.Open();
                }
                else
                {
                    SqlConnectionObject.Close();
                    SqlConnectionObject.Open();
                }

                SqlConnectionState = ConnectionState.Open;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CloseConnection()
        {
            try
            {
                if (SqlConnectionObject == null) return;

                SqlConnectionObject.Close();
                SqlConnectionState = ConnectionState.Closed;
                SqlConnectionObject.Dispose();
                SqlConnectionObject = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}