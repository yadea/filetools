using File_Tools.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Tools
{
    public partial class frmMain : Form
    {
        Logging logging;
        public frmMain()
        {
            InitializeComponent();
            logging = new Logging();
            logging.start();
            Logging.Log("========== Log Started ==========");
        }

        private void cmdAddGroup1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                try
                {

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void FrmMain_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            logging.stop();
        }
    }
}
