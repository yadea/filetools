using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using File_Tools.Modules;
using File_Tools.Tasks;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Tools
{
    public partial class frmResults : Form
    {
        readonly string TASK;
        readonly string PATH1;
        readonly string PATH2;

        public frmResults(string task, string path1, string path2)
        {
            InitializeComponent();
            TASK = task;
            PATH1 = path1;
            PATH2 = path2;

            switch (task)
            {
                case "Fill":
                    Fill fillTask = new Fill(this, PATH1, PATH2);
                    break;
                default:
                    Logging.Log("Task " + task + " not defined. Exiting results view.", Logging.LogType.Warn);
                    MessageBox.Show("Task " + task + " has not been created yet. Please wait for future release.", "Task not yet created", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    break;
            }
        }

        public void UpdateStatus(string status, int progressbar = -1)
        {
            if (lblStatus.InvokeRequired)
                lblStatus.Invoke((MethodInvoker)delegate { lblStatus.Text = status; });
            else
                lblStatus.Text = status;

            if (progressbar > -1)
                if (prgBar.InvokeRequired)
                    prgBar.Invoke((MethodInvoker)delegate { prgBar.Value = progressbar; });
                else
                    prgBar.Value = progressbar;
        }

        public void SetProgressBarMax(int max)
        {
            if (prgBar.InvokeRequired)
                prgBar.Invoke((MethodInvoker)delegate { prgBar.Maximum = max; });
            else
                prgBar.Maximum = max;
        }


        public void AddFileToList(string filepath)
        {
            if (chkFileList.InvokeRequired)
                chkFileList.Invoke((MethodInvoker)delegate { chkFileList.Items.Add(filepath); });
            else
                chkFileList.Items.Add(filepath);
        }
    }
}
