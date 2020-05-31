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
using System.Collections;
using System.IO;

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
                    ToggleControl(true);
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
                prgBar.Invoke((MethodInvoker)delegate { prgBar.Maximum = max; prgBar.Value = 0; });
            else
            {
                prgBar.Maximum = max;
                prgBar.Value = 0;
            }
        }

        public void SetProgressBarValue(int value)
        {
            if (prgBar.InvokeRequired)
                prgBar.Invoke((MethodInvoker)delegate { prgBar.Value = value; });
            else
                prgBar.Value = value;
        }

        public void AddFileToList(string filepath)
        {
            if (chkFileList.InvokeRequired)
                chkFileList.Invoke((MethodInvoker)delegate { chkFileList.Items.Add(filepath); });
            else
                chkFileList.Items.Add(filepath);
        }

        public void ShowMessagebox(string message, string title = "Error", MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { MessageBox.Show(message, title, buttons, icon); });
            else
                MessageBox.Show(message, title, buttons, icon);
        }

        public void ToggleControl(bool enabled)
        {
            if (cmdCopy.InvokeRequired)
                cmdCopy.Invoke((MethodInvoker)delegate { cmdCopy.Enabled = enabled; });
            else
                cmdCopy.Enabled = enabled;
        }

        //todo need to do handling for cancelling
        private void cmdCopy_Click(object sender, EventArgs e)
        {
            Logging.Log("Copy selected", Logging.LogType.Debug);
            ArrayList list = new ArrayList();
            //foreach (var file in chkFileList.CheckedItems)
            //{
            //    list.Add(file as string);
            //}
            //string[] fileList = (string[])list.ToArray(typeof(string));

            FileManager fileManager = new FileManager(this);
            foreach (var chkFile in chkFileList.CheckedItems)
            {
                string file = chkFile.ToString();
                bool overwriteFile = false;
                if (fileManager.FileExists(PATH2, file))
                {
                    if (MessageBox.Show("The file " + file + " already exist in the destination folder " + PATH2 + ". Do you want to overwrite it?") == DialogResult.No)
                        continue;
                    else
                        overwriteFile = true;
                }

                if (fileManager.CopyFile(file, PATH1, PATH2, overwriteFile))
                    list.Add(chkFile);
            }

            foreach (object file in list)
            {
                chkFileList.Items.Remove(file);
            }
        }
    }
}
