using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace SQLMigrationByQuery
{
    internal class clsSQL
    {
        public static string strConnectionString;

        public static IDbConnection Connection { get; internal set; }

        public static resultRead<T> getSQLRead<T>(string strQuery, object objParameter)
        {
            var objResult = new resultRead<T>();
            try
            {
                IDbConnection objConnection;
                if (strConnectionString == "")
                {
                    objConnection = Connection;
                    if (objConnection.State == ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }


                }
                else
                {
                    objConnection = new SqlConnection(strConnectionString);
                    objConnection.Open();
                }
                try
                {
                    List<T> lstResult = objConnection.Query<T>(strQuery, objParameter).ToList();
                    objResult.lstResult = lstResult;
                    if (lstResult.Count > 0)
                    {
                        objResult.Result = enmSQLReadResult.HaveData;
                    }
                    else
                    {
                        objResult.Result = enmSQLReadResult.Null;
                    }
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                    return objResult;

                }
                catch (Exception ex)
                {

                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }

                    objResult.strError = ex.Message;
                    return objResult;

                }
            }
            catch (Exception ex)
            {
                objResult.Result = enmSQLReadResult.Fail;
                objResult.strError = ex.Message;
                return objResult;
            }
        }

        public static resultExecute getExecute(string strQuery, object objData)
        {
            var objResult = new resultExecute();
            try
            {
                IDbConnection objConnection;
                if (strConnectionString == "")
                {
                    objConnection = Connection;
                    if (objConnection.State == ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }


                }
                else
                {
                    objConnection = new SqlConnection(strConnectionString);
                    objConnection.Open();
                }
                IDbTransaction objTransaction = objConnection.BeginTransaction();
                try
                {
                    int intRowEffected = objConnection.Execute(strQuery, objData, objTransaction, 600);
                    objTransaction.Commit();
                    objResult.intRowEffected = intRowEffected;
                    objResult.blnSuccess = true;
                    objConnection.Close();
                    return objResult;
                }
                catch (Exception ex)
                {
                    if (objTransaction != null)
                    {
                        objTransaction.Rollback();
                    }

                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }

                    objResult.strError = ex.Message;
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                objResult.strError = ex.Message;
                return objResult;
            }
        }
        public static resultExecute getExecute(List<string> lstQuery)
        {
            var objResult = new resultExecute();
            try
            {
                IDbConnection objConnection;
                if (strConnectionString=="")
                {
                    objConnection = Connection;
                    if (objConnection.State==ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    

                }
                else
                {
                    objConnection = new SqlConnection(strConnectionString);
                    objConnection.Open();
                }
                IDbTransaction objTransaction = objConnection.BeginTransaction();
                try
                {
                    objResult.intRowEffected = 0;
                    foreach (string strQuery in lstQuery)
                    {
                        objConnection.Execute(strQuery, null, objTransaction, 600);
                    }
                    objTransaction.Commit();
                    objResult.blnSuccess = true;
                    objConnection.Close();
                    return objResult;
                }
                catch (Exception ex)
                {
                    if (objTransaction != null)
                    {
                        objTransaction.Rollback();
                    }

                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }

                    objResult.strError = ex.Message;
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                objResult.strError = ex.Message;
                return objResult;
            }
        }

        public enum enmSQLReadResult
        {
            HaveData = 0,
            Null = 1,
            Fail = 2
        }

        public partial class resultRead<T>
        {
            public enmSQLReadResult Result { get; set; }
            public List<T> lstResult { get; set; }
            public string strError { get; set; }

            public T objFirstRow
            {
                get
                {
                    if (lstResult is object)
                    {
                        return lstResult.First();
                    }
                    else
                    {
                        return default;
                    }
                }
            }

            public resultRead()
            {
                Result = enmSQLReadResult.Fail;
                lstResult = null;
                strError = "";
            }
        }

        public partial class resultExecute
        {
            public bool blnSuccess { get; set; }
            public string strError { get; set; }
            public int intRowEffected { get; set; }
            public object lstResult { get; set; }

            public resultExecute()
            {
                blnSuccess = false;
                strError = "";
                intRowEffected = 0;
                lstResult = null;
            }

        }
    }
}
