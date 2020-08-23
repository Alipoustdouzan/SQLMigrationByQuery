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
            objRequest.strConnectionString = txtConnectionString.Text;
            objRequest.strCallerProjectName = "WinAppTester";
            objRequest.strMigrationMark = "Migration-";
            SQLMigrationByQuery.resultMigration objResult = SQLMigrationByQuery.clsMigration.getApplyMigration(objRequest);
            if (objResult.blnSuccess == true)
            {
                MessageBox.Show("Migration was successful");
            }
            else
            {
                MessageBox.Show(objResult.strError);
            }
        }
    }
}
