using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Sql
{
    public sealed class SqlDBAccess : SQLManager
    {
        private CommandType CommandType { get; }
        private SqlConnection SqlConnectionObject { get; set; }
        public ConnectionState SqlConnectionState { get; private set; }

        public SqlDBAccess(object ConnectionString) 
            : base(ConnectionString)
        {
            CommandType = CommandType.StoredProcedure;
            SqlConnectionObject = null;
        }

        #region Select Query
        public DataRow GetFirstOrDefaultRow(string Command, nameValuePairs NameValuePairObject)
        {
            try
            {
                using (DataSet ds = Get(Command, NameValuePairObject))
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

        public DataTable GetData(string Command, nameValuePairs NameValuePairObject)
        {
            try
            {
                using (DataSet ds = Get(Command, NameValuePairObject))
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
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetDataSet(string Command, nameValuePairs NameValuePairObject)
        {
            try
            {
                using (DataSet ds = Get(Command, NameValuePairObject))
                {
                    if (ds != null)
                        return ds;
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet Get(string Command, nameValuePairs NameValuePairObject)
        {
            try
            {
                GetConnection();
                SqlCommand cmd = CreateSqlCommand(Command, NameValuePairObject);

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

        public object InsertUpdateDeleteReturnObject(string Command, 
            nameValuePairs NameValuePairObject, 
            string outParameterName)
        {
            try
            {
                GetConnection();

                SqlCommand cmdObject = CreateSqlCommand(Command, NameValuePairObject);

                cmdObject.Parameters.Add(outParameterName, SqlDbType.VarChar, int.MaxValue);
                cmdObject.Parameters[outParameterName].Direction = ParameterDirection.Output;


                cmdObject.ExecuteNonQuery();
                return cmdObject.Parameters[outParameterName].Value;
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

        private SqlCommand CreateSqlCommand(string _Command, nameValuePairs _NameValuePairObject = null)
        {
            try
            {
                if (this.SqlConnectionObject.State == ConnectionState.Open)
                {
                    SqlCommand cmdObject = new SqlCommand(_Command)
                    {
                        Connection = this.SqlConnectionObject,
                        CommandType = CommandType
                    };
                    cmdObject.Parameters.Clear();
                    if (_NameValuePairObject != null)
                    {
                        foreach (nameValuePair objList in _NameValuePairObject)
                        {
                            cmdObject.Parameters.Add(new SqlParameter(objList.getName, objList.getValue));
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

        private void GetConnection()
        {
            try
            {
                SqlConnectionObject = new SqlConnection(ConnectionString.ToString());

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CloseConnection()
        {
            try
            {
                if (SqlConnectionObject != null)
                {
                    SqlConnectionObject.Close();
                    SqlConnectionState = ConnectionState.Closed;
                    SqlConnectionObject.Dispose();
                    SqlConnectionObject = null;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}