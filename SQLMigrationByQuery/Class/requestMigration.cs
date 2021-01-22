using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLMigrationByQuery
{
    public class requestMigration
    {
        /// <summary>
        /// Connection string of migration target database
        /// </summary>
        public string ConnectionString { get; set; } = "";
        /// <summary>
        /// CallerProjectName should not change in future or the migration will execute again in database
        /// </summary>
        public string CallerProjectName { get; set; } = "";
        /// <summary>
        /// The text in begin of your .sql files, Example : "MyScript-"
        /// </summary>
        public string MigrationMark { get; set; } = "";
        /// <summary>
        /// True = All query string with ReplaceTextSource will be replace to ReplaceTextTarget, False = Nothing will be replace
        /// </summary>
        public bool ReplaceTextInQuery { get; set; } = false;
        /// <summary>
        /// ReplaceTextSource will be replace to ReplaceTextTarget if ReplaceTextInQuery be true
        /// </summary>
        public string ReplaceTextSource { get; set; } = "";
        /// <summary>
        /// ReplaceTextSource will be replace to ReplaceTextTarget if ReplaceTextInQuery be true
        /// </summary>
        public string ReplaceTextTarget { get; set; } = "";
    }
}
