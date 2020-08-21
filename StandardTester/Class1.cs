using System;

namespace StandardTester
{
    public class Class1
    {
        public static bool getTest(string strConnection)
        {
            bool blnResult = SQLMigrationByQuery.clsMigration.getApplyMigration(strConnection, "WinAppTester", "Migration-");
            return blnResult;
        }
    }
}
