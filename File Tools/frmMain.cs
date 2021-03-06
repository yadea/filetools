﻿using File_Tools.Modules;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;

namespace File_Tools
{
    public partial class frmMain : Form
    {
        Logging logging;
        string group1InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
        string group2InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

        public frmMain()
        {
            InitializeComponent();
            logging = new Logging();
            logging.start();
            Logging.Log("========== Log Started ==========");
        }

        private bool AddFolder(CheckedListBox listBox, string showPath, out string filepath)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                try
                {
                    dialog.IsFolderPicker = true;
                    dialog.InitialDirectory = showPath;
                    dialog.Multiselect = true;
                    CommonFileDialogResult result = dialog.ShowDialog();
                    if (result == CommonFileDialogResult.Ok)
                    {
                        string basefolder = null;
                        foreach (string folder in dialog.FileNames)
                        {
                            if (!listBox.Items.Contains(folder))
                            {
                                listBox.Items.Add(folder);
                                listBox.SetItemChecked(listBox.Items.Count - 1, true);
                            }
                            basefolder = folder.Substring(0, folder.LastIndexOf('\\'));
                        }
                        filepath = basefolder;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log("[AddFolder] " + ex, Logging.LogType.Error);
                }
                filepath = null;
                return false;
            }
        }

        #region Form Components
        private void cmdAddGroup1_Click(object sender, EventArgs e)
        {
            string path;
            if (AddFolder(chkGroup1, group1InitialDirectory, out path))
            {
                group1InitialDirectory = path;
            }
        }

        private void cmdAddGroup2_Click(object sender, EventArgs e)
        {
            string path;
            if (AddFolder(chkGroup2, group2InitialDirectory, out path))
            {
                group2InitialDirectory = path;
            }
        }

        private void cmdClearGroup1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm clear group 1 list?", "Confirm",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                chkGroup1.Items.Clear();
            }
        }

        private void cmdClearGroup2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm clear group 2 list?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                chkGroup2.Items.Clear();
            }
        }

        private void FrmMain_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            logging.stop();
        }
        #endregion
    }
}
