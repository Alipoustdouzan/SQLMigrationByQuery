using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLMigrationByQuery
{
    public class requestMigration
    {
        public string strConnectionString { get; set; }
        public string strCallerProjectName { get; set; }
        public string strMigrationMark { get; set; }
    }
}
