using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLMigrationByQuery
{
    public class resultMigration
    {
        /// <summary>
        /// True = All migrations execute successful, False = There is an error in execution
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Success and fail string message
        /// </summary>
        public string ResultMessage { get; set; }
        public resultMigration()
        {
            Success = false;
            ResultMessage = "";
        }
    }
}
