using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace WebService.DBAccess
{
    public class DBHelper
    {
        #region "Variable Declaration"
        string _ConnectionString;
        string _ProviderName;
        #endregion

        #region "Constructors"
        public DBHelper() { }
        #endregion

        #region "Properties"
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }
        public bool IsErrorFound { get; set; }

        string _ErrorMessage;
        public string ErrorMessage
        {
            get { return "<span style='color: Red'>" + _ErrorMessage + "</span>"; }
            set { _ErrorMessage = value; }
        }
        #endregion

        #region "Other Functions"
        public int ExecuteNonQuery(string Query)
        {
            string Provider = this.ProviderName;
            string Connection = this.ConnectionString;

            if (string.IsNullOrEmpty(this.ProviderName))
                this.ProviderName = "System.Data.SqlClient";

            if (string.IsNullOrEmpty(Provider))
                Provider = "System.Data.SqlClient";

            int iResult = 0;
            using (DbConnection dbconnection = DbProviderFactories.GetFactory(Provider).CreateConnection())
            {
                dbconnection.ConnectionString = Connection;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandText = Query;

                try
                {
                    dbcommand.Connection.Open();
                    iResult = dbcommand.ExecuteNonQuery();
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return iResult;
        }
        public int ExecuteNonQuery(string Connection, string Query)
        {
            string Provider = this.ProviderName;

            if (string.IsNullOrEmpty(this.ProviderName))
                this.ProviderName = "System.Data.SqlClient";

            if (string.IsNullOrEmpty(Provider))
                Provider = "System.Data.SqlClient";

            int iResult = 0;
            using (DbConnection dbconnection = DbProviderFactories.GetFactory(Provider).CreateConnection())
            {
                dbconnection.ConnectionString = Connection;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandText = Query;

                try
                {
                    dbcommand.Connection.Open();
                    iResult = dbcommand.ExecuteNonQuery();
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return iResult;
        }

        public object ExecuteScalar(string Query)
        {
            return ExecuteScalar(this.ProviderName, this.ConnectionString, Query);
        }
        public object ExecuteScalar(string Provider, string Connection, string Query)
        {
            object oResult = null;
            if (string.IsNullOrEmpty(this.ProviderName))
                this.ProviderName = "System.Data.SqlClient";

            if (string.IsNullOrEmpty(Provider))
                Provider = "System.Data.SqlClient";

            using (DbConnection dbconnection = DbProviderFactories.GetFactory(Provider).CreateConnection())
            {
                dbconnection.ConnectionString = Connection;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandText = Query;
                try
                {
                    dbcommand.Connection.Open();
                    oResult = dbcommand.ExecuteScalar();
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return oResult;
        }
        public object ExecuteScalar(DbCommand dbcommand)
        {
            object oResult = null;
            using (DbConnection dbconnection = DbProviderFactories.GetFactory(ProviderName).CreateConnection())
            {
                dbconnection.ConnectionString = this.ConnectionString;
                dbcommand.Connection = dbconnection;
                try
                {
                    dbcommand.Connection.Open();
                    oResult = dbcommand.ExecuteScalar();
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return oResult;
        }

        public DataTable FillTable(string Query)
        {
            return FillTable(this.ProviderName, this.ConnectionString, Query);
        }
        public DataTable FillTable(string Connection, string Query)
        {
            return FillTable(this.ProviderName, Connection, Query);
        }
        public DataTable FillTable(string Provider, string Connection, string Query)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(this.ProviderName))
                this.ProviderName = "System.Data.SqlClient";

            if (string.IsNullOrEmpty(Provider))
                Provider = "System.Data.SqlClient";

            using (DbConnection dbconnection = DbProviderFactories.GetFactory(Provider).CreateConnection())
            {
                dbconnection.ConnectionString = Connection;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandText = Query;
                try
                {
                    dbcommand.Connection.Open();
                    DbDataAdapter dbdataadapter = DbProviderFactories.GetFactory(ProviderName).CreateDataAdapter();
                    dbdataadapter.SelectCommand = dbcommand;
                    dbdataadapter.Fill(dt);
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                    throw ex;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return dt;
        }
        public DataTable FillTable(String ProcName, System.Collections.Hashtable ParaValues, bool IncludeReturnPara)
        {
            DataTable result = new DataTable();

            using (DbConnection dbconnection = DbProviderFactories.GetFactory(ProviderName).CreateConnection())
            {
                dbconnection.ConnectionString = this.ConnectionString;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandType = CommandType.StoredProcedure;
                dbcommand.CommandText = ProcName;
                try
                {
                    dbcommand.Connection.Open();
                    if ((ParaValues != null) && (ParaValues.Count > 0))
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                            {
                                p.Value = DBNull.Value;
                            }
                            else
                            {
                                p.Value = ParaValues[p.ParameterName];

                                if (ParaValues[p.ParameterName] == null)
                                    p.Value = DBNull.Value;
                            }
                        }
                    }
                    else
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.ParameterName == "@OperationType") { p.Value = 'S'; }
                            else p.Value = DBNull.Value;
                        }
                    }

                    DbDataAdapter adp = DbProviderFactories.GetFactory(ProviderName).CreateDataAdapter();
                    adp.SelectCommand = dbcommand;
                    adp.Fill(result);
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return result;
        }
        public DataTable FillTable(String ProcName, System.Collections.Hashtable ParaValues, bool IncludeReturnPara, string sConnection)
        {
            DataTable result = new DataTable();

            using (DbConnection dbconnection = DbProviderFactories.GetFactory(ProviderName).CreateConnection())
            {
                dbconnection.ConnectionString = sConnection;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandType = CommandType.StoredProcedure;
                dbcommand.CommandText = ProcName;
                try
                {
                    dbcommand.Connection.Open();
                    if ((ParaValues != null) && (ParaValues.Count > 0))
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                            {
                                p.Value = DBNull.Value;
                            }
                            else
                            {
                                p.Value = ParaValues[p.ParameterName];

                                if (ParaValues[p.ParameterName] == null)
                                    p.Value = DBNull.Value;
                            }
                        }
                    }
                    else
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.ParameterName == "@OperationType") { p.Value = 'S'; }
                            else p.Value = DBNull.Value;
                        }
                    }

                    DbDataAdapter adp = DbProviderFactories.GetFactory(ProviderName).CreateDataAdapter();
                    adp.SelectCommand = dbcommand;
                    adp.Fill(result);
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return result;
        }

        public DataSet FillDataset(string Query)
        {
            return FillDataset(this.ProviderName, this.ConnectionString, Query);
        }
        public DataSet FillDataset(string Provider, string Connection, string Query)
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(this.ProviderName))
                this.ProviderName = "System.Data.SqlClient";

            if (string.IsNullOrEmpty(Provider))
                Provider = "System.Data.SqlClient";

            using (DbConnection dbconnection = DbProviderFactories.GetFactory(Provider).CreateConnection())
            {
                dbconnection.ConnectionString = Connection;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandText = Query;
                try
                {
                    dbcommand.Connection.Open();
                    DbDataAdapter dbdataadapter = DbProviderFactories.GetFactory(ProviderName).CreateDataAdapter();
                    dbdataadapter.SelectCommand = dbcommand;
                    dbdataadapter.Fill(ds);
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return ds;
        }

        public DataTable FillTableSchema(string Query)
        {
            return FillTableSchema(this.ProviderName, this.ConnectionString, Query);
        }
        public DataTable FillTableSchema(string Provider, string Connection, string Query)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(this.ProviderName))
                this.ProviderName = "System.Data.SqlClient";

            if (string.IsNullOrEmpty(Provider))
                Provider = "System.Data.SqlClient";

            using (DbConnection dbconnection = DbProviderFactories.GetFactory(Provider).CreateConnection())
            {
                dbconnection.ConnectionString = Connection;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandText = Query;
                try
                {
                    dbcommand.Connection.Open();
                    DbDataAdapter dbdataadapter = DbProviderFactories.GetFactory(ProviderName).CreateDataAdapter();
                    dbdataadapter.SelectCommand = dbcommand;
                    dbdataadapter.FillSchema(dt, SchemaType.Mapped);
                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return dt;
        }

        public DataTable GetSchema(string TableName)
        {
            return FillTable("execute sp_pkeys " + TableName);
        }
        public DataRow GetDataRow(string Query)
        {
            return FillTable(this.ProviderName, this.ConnectionString, Query).Rows[0];
        }
        public DataRow GetDataRow(string Connection, string Query)
        {
            return FillTable(this.ProviderName, Connection, Query).Rows[0];
        }
        public DataRow GetDataRow(string Query, int RowIndex)
        {

            if (RowIndex >= 0)
            {
                string sError = string.Empty;
                DataTable dt = FillTable(this.ProviderName, this.ConnectionString, Query);
                if (RowIndex <= dt.Rows.Count - 1)
                    return dt.Rows[RowIndex];
            }
            return null;
        }
        public DataRow GetDataRow(string Provider, string Connection, string Query)
        {
            return FillTable(Provider, Connection, Query).Rows[0];
        }
        public DataRow GetDataRow(string Provider, string Connection, string Query, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                DataTable dt = FillTable(Provider, Connection, Query);
                if (RowIndex <= dt.Rows.Count - 1)
                    return dt.Rows[RowIndex];
            }
            return null;
        }
        #endregion

        #region "Stored Procedure Functions"
        public int ExecuteStoredProcedure(String ProcName, System.Collections.Hashtable ParaValues, bool IncludeReturnPara, String Provider, String ConString)
        {
            int result = -1;
            if (string.IsNullOrEmpty(this.ProviderName))
                this.ProviderName = "System.Data.SqlClient";

            if (string.IsNullOrEmpty(Provider))
                Provider = "System.Data.SqlClient";

            using (DbConnection dbconnection = DbProviderFactories.GetFactory(Provider).CreateConnection())
            {
                dbconnection.ConnectionString = ConString;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandType = CommandType.StoredProcedure;
                dbcommand.CommandText = ProcName;
                try
                {
                    dbcommand.Connection.Open();
                    //string OutputParaName = "";
                    if ((ParaValues != null) && (ParaValues.Count > 0))
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                            {
                                p.Value = DBNull.Value;
                            }
                            else
                            {
                                p.Value = ParaValues[p.ParameterName];

                                if (ParaValues[p.ParameterName] == null)
                                    p.Value = DBNull.Value;
                            }
                        }
                    }
                    else
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.ParameterName == "@OperationType") { p.Value = 'S'; }
                            else p.Value = DBNull.Value;
                        }
                    }
                    result = dbcommand.ExecuteNonQuery();

                    if (IncludeReturnPara)
                    {
                        result = int.Parse(dbcommand.Parameters["@ReturnValue"].Value.ToString());
                    }

                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return result;
        }
        public int ExecuteStoredProcedure(String ProcName, System.Collections.Hashtable ParaValues, bool IncludeReturnPara)
        {
            int result = -1;
            using (DbConnection dbconnection = DbProviderFactories.GetFactory(ProviderName).CreateConnection())
            {
                dbconnection.ConnectionString = this.ConnectionString;
                DbCommand dbcommand = dbconnection.CreateCommand();
                dbcommand.CommandType = CommandType.StoredProcedure;
                dbcommand.CommandText = ProcName;

                try
                {
                    dbcommand.Connection.Open();
                    //  string OutputParaName = "";
                    if ((ParaValues != null) && (ParaValues.Count > 0))
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                            {
                                p.Value = DBNull.Value;
                            }
                            else
                            {
                                p.Value = ParaValues[p.ParameterName];

                                if (ParaValues[p.ParameterName] == null)
                                    p.Value = DBNull.Value;
                            }
                        }
                    }
                    else
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbcommand);
                        foreach (DbParameter p in dbcommand.Parameters)
                        {
                            if (p.ParameterName == "@OperationType") { p.Value = 'S'; }
                            else p.Value = DBNull.Value;
                        }
                    }
                    result = dbcommand.ExecuteNonQuery();

                    if (IncludeReturnPara)
                    {
                        result = int.Parse(dbcommand.Parameters["@ReturnValue"].Value.ToString());
                    }

                    IsErrorFound = false;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (dbcommand.Connection.State != ConnectionState.Closed)
                    {
                        dbcommand.Connection.Close();
                        dbcommand.Dispose();
                    }
                }
            }
            return result;
        }
        public int ExecuteCommand(DbCommand cmd)
        {
            int result = -1;
            using (DbConnection dbconnection = DbProviderFactories.GetFactory(ProviderName).CreateConnection())
            {
                try
                {
                    dbconnection.ConnectionString = this.ConnectionString;

                    cmd.Connection = dbconnection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbconnection.Open();

                    result = cmd.ExecuteNonQuery();

                    IsErrorFound = false;
                    return result;
                }
                catch (Exception ex)
                {
                    IsErrorFound = true;
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    if (cmd.Connection.State != ConnectionState.Closed)
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }
            }

            return result;
        }
        #endregion
    }
}