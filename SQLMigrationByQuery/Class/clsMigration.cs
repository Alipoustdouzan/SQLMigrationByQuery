﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SQLMigrationByQuery
{
    public class clsMigration
    {
        /// <summary>
        /// Execute all .sql file in caller project which their name start with MigrationMark
        /// </summary>
        /// <param name="strConnectionString">Connection string for connect the target database</param>
        /// <param name="strCallerProjectName">Project name for checking if this query execute on this project or not</param>
        /// <param name="strMigrationMark">Static words which all .sql files start with (Example : "Migration-")</param>
        /// <returns>Return a class which contain a bool and a string</returns>
        public static resultMigration getApplyMigration(requestMigration objRequest)
        {
            resultMigration objResult = new resultMigration();

            //Get caller and executer assemply info
            Assembly objCallerAssembly = Assembly.GetCallingAssembly();
            Assembly objExecuterAssembly = Assembly.GetExecutingAssembly();
            string strExecuterProjectName = objExecuterAssembly.ManifestModule.Name.Replace(".dll", "");

            //Create migration table
            clsSQL.strConnectionString = objRequest.strConnectionString;
            string strQuery = getReadResourceQuery(objExecuterAssembly, strExecuterProjectName + @".Query.CreateMigrationTable.sql");
            clsSQL.resultExecute objExecuteResult = clsSQL.getExecute(strQuery, null);
            if (objExecuteResult.blnSuccess != true)
            {
                objResult.strError = objExecuteResult.strError;
                return objResult;
            }

            //Get migration query list
            List<string> lstResourceString = objCallerAssembly.GetManifestResourceNames().ToList();
            List<___DatabaseMigration> lstAllMigration = new List<___DatabaseMigration>();
            lstResourceString = lstResourceString.FindAll(x => x.Contains(objRequest.strMigrationMark) && x.Contains(".sql"));
            if (lstResourceString.Count > 0)
            {
                lstResourceString.Sort();

                for (int intCounter = 0; intCounter < lstResourceString.Count; intCounter++)
                {
                    ___DatabaseMigration objItem = new ___DatabaseMigration();
                    objItem.strMigrationProject = objRequest.strCallerProjectName;
                    string strMarkAddress = lstResourceString[intCounter].Substring(0, lstResourceString[intCounter].IndexOf(objRequest.strMigrationMark) + objRequest.strMigrationMark.Length);
                    objItem.strMigrationName = lstResourceString[intCounter].Replace(strMarkAddress, "").Replace(".sql", "");
                    objItem.strPath = lstResourceString[intCounter];
                    objItem.strMigrationDesc = "";
                    objItem.datMigrationApply = DateTime.Now;
                    objItem.blnMigrationSuccess = true;
                    lstAllMigration.Add(objItem);
                }
                strQuery = getReadResourceQuery(objExecuterAssembly, strExecuterProjectName + @".Query.ReadAllSuccess.sql");
                List<___DatabaseMigration> lstExistsMigration;
                clsSQL.resultRead<___DatabaseMigration> objReadResult = clsSQL.getSQLRead<___DatabaseMigration>(strQuery, lstAllMigration.First());
                if (objReadResult.Result != clsSQL.enmSQLReadResult.Fail)
                {
                    int intExecuted = 0;
                    lstExistsMigration = objReadResult.lstResult;
                    foreach (___DatabaseMigration objItem in lstAllMigration)
                    {
                        ___DatabaseMigration objTemp = lstExistsMigration.Find(x => x.strMigrationName == objItem.strMigrationName);
                        if (objTemp == null)
                        {
                            intExecuted = intExecuted + 1;
                            strQuery = getReadResourceQuery(objCallerAssembly, objItem.strPath);
                            string strDesc = strQuery.Substring(0, strQuery.IndexOf(Environment.NewLine));
                            strDesc = strDesc.Replace("--@strMigrationDesc=", "");
                            objExecuteResult = clsSQL.getExecute(strQuery, null);
                            strQuery = getReadResourceQuery(objExecuterAssembly, strExecuterProjectName + @".Query.InsertUpdate.sql");
                            ___DatabaseMigration objMigration = new ___DatabaseMigration();
                            objMigration = objItem;
                            if (objExecuteResult.blnSuccess == true)
                            {
                                objMigration.strMigrationDesc = strDesc;
                                objMigration.blnMigrationSuccess = true;
                                objExecuteResult = clsSQL.getExecute(strQuery, objItem);
                            }
                            else
                            {
                                objMigration.strMigrationDesc = objExecuteResult.strError;
                                objMigration.blnMigrationSuccess = false;
                                objResult.strError = objExecuteResult.strError;
                                objExecuteResult = clsSQL.getExecute(strQuery, objItem);
                                return objResult;
                            }
                        }
                    }
                    objResult.blnSuccess = true;
                    if (intExecuted>0)
                    {
                        objResult.strError = "All migration execute succesfuly. Executed files : "+ intExecuted.ToString ();
                    }
                    else
                    {
                        objResult.strError = "All migration were execute before";
                    }

                    return objResult;
                }
                else
                {
                    objResult.strError = objReadResult.strError;
                    return objResult;
                }
            }
            else
            {
                objResult.blnSuccess = true;
                objResult.strError = "No .sql file found in " + objCallerAssembly.ManifestModule.Name;
                return objResult;
            }

        }
        private static string getReadResourceQuery(Assembly objAssembly, string strPath)
        {
            string strReadData = "";
            using (Stream objStream = objAssembly.GetManifestResourceStream(strPath))
            {
                using (StreamReader objStreamReader = new StreamReader(objStream))
                {
                    strReadData = objStreamReader.ReadToEnd();
                }
            }
            return strReadData;
        }

    }
}
