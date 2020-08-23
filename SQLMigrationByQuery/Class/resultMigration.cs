using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLMigrationByQuery
{
    public class resultMigration
    {
        public bool blnSuccess { get; set; }
        public string strError { get; set; }
        public resultMigration()
        {
            blnSuccess = false;
            strError = "";
        }
    }
}
