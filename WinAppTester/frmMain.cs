using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppTester
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            SQLMigrationByQuery.requestMigration objRequest = new SQLMigrationByQuery.requestMigration();
            objRequest.ConnectionString = txtConnectionString.Text;
            objRequest.CallerProjectName = "WinAppTester";
            objRequest.MigrationMark = "Migration-";
            objRequest.ReplaceTextInQuery = true;
            objRequest.ReplaceTextSource = "User";
            objRequest.ReplaceTextTarget = "People";
            SQLMigrationByQuery.resultMigration objResult = SQLMigrationByQuery.helperMigration.getApplyMigration(objRequest);
            if (objResult.Success == true)
            {
                MessageBox.Show("Migration was successful");
            }
            else
            {
                MessageBox.Show(objResult.ResultMessage);
            }
        }
    }
}
