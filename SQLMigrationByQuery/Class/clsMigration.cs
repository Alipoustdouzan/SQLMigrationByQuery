using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SQLMigrationByQuery
{
    public class clsMigration
    {
        public static bool getApplyMigration(string strConnectionString, string strMigrationMark)
        {
            //Get caller and executer assemply info
            Assembly objCallerAssembly = Assembly.GetCallingAssembly();
            Assembly objExecuterAssembly = Assembly.GetExecutingAssembly();
            string strCallerProjectName = objCallerAssembly.ManifestModule.Name.Replace(".dll", "");
            string strExecuterProjectName = objExecuterAssembly.ManifestModule.Name.Replace(".dll", "");

            //Create migration table
            clsSQL.strConnectionString = strConnectionString;
            string strQuery = getReadResourceQuery(objExecuterAssembly, strExecuterProjectName + @".Query.CreateMigrationTable.sql");
            clsSQL.resultExecute objExecuteResult = clsSQL.getExecute(strQuery, null);

            //Get migration query list
            List<string> lstResourceString = objCallerAssembly.GetManifestResourceNames().ToList();
            List<___DatabaseMigration> lstAllMigration = new List<___DatabaseMigration>();
            lstResourceString = lstResourceString.FindAll(x => x.Contains(strMigrationMark) && x.Contains(".sql"));
            if (lstResourceString.Count > 0)
            {
                lstResourceString.Sort();

                for (int intCounter = 0; intCounter < lstResourceString.Count; intCounter++)
                {
                    ___DatabaseMigration objItem = new ___DatabaseMigration();
                    objItem.strMigrationProject = strCallerProjectName;
                    string strMarkAddress = lstResourceString[intCounter].Substring(0, lstResourceString[intCounter].IndexOf(strMigrationMark) + strMigrationMark.Length);
                    objItem.strMigrationName = lstResourceString[intCounter].Replace(strMarkAddress, "").Replace(".sql", "");
                    objItem.strPath = lstResourceString[intCounter];
                    objItem.strMigrationDesc = "";
                    objItem.datMigrationApply = DateTime.Now;
                    objItem.blnMigrationSuccess = true;
                    lstAllMigration.Add(objItem);
                }
                strQuery = getReadResourceQuery(objExecuterAssembly, strExecuterProjectName + @".Query.ReadAllSuccess.sql");
                List<___DatabaseMigration> lstExistsMigration;
                clsSQL.resultRead<___DatabaseMigration> objResult = clsSQL.getSQLRead<___DatabaseMigration>(strQuery, lstAllMigration.First());
                if (objResult.Result != clsSQL.enmSQLReadResult.Fail)
                {
                    lstExistsMigration = objResult.lstResult;
                    foreach (___DatabaseMigration objItem in lstAllMigration)
                    {
                        ___DatabaseMigration objTemp = lstExistsMigration.Find(x => x.strMigrationName == objItem.strMigrationName);
                        if (objTemp == null)
                        {
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
                                objExecuteResult = clsSQL.getExecute(strQuery, objItem);
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        private static string getReadResourceQuery(Assembly objAssembly, string strPath)
        {
            string strReadData = "";
            using (Stream objStream = objAssembly.GetManifestResourceStream(strPath))
            {
                using (StreamReader sr = new StreamReader(objStream))
                {
                    strReadData = sr.ReadToEnd();
                }
            }
            return strReadData;
        }

    }
}
