namespace File_Tools
{
    partial class frmResults
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSelectAll = new System.Windows.Forms.Button();
            this.cmdAutoScroll = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdMove = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.chkFileList = new System.Windows.Forms.CheckedListBox();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(12, 36);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(759, 23);
            this.prgBar.Step = 1;
            this.prgBar.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStatus.Location = new System.Drawing.Point(13, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(759, 23);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSelectAll);
            this.groupBox1.Controls.Add(this.cmdAutoScroll);
            this.groupBox1.Controls.Add(this.cmdDelete);
            this.groupBox1.Controls.Add(this.cmdMove);
            this.groupBox1.Controls.Add(this.cmdCopy);
            this.groupBox1.Controls.Add(this.chkFileList);
            this.groupBox1.Location = new System.Drawing.Point(12, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 449);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File";
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Enabled = false;
            this.cmdSelectAll.Location = new System.Drawing.Point(87, 420);
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Size = new System.Drawing.Size(75, 23);
            this.cmdSelectAll.TabIndex = 5;
            this.cmdSelectAll.Text = "Select &All";
            this.cmdSelectAll.UseVisualStyleBackColor = true;
            // 
            // cmdAutoScroll
            // 
            this.cmdAutoScroll.Enabled = false;
            this.cmdAutoScroll.Location = new System.Drawing.Point(6, 420);
            this.cmdAutoScroll.Name = "cmdAutoScroll";
            this.cmdAutoScroll.Size = new System.Drawing.Size(75, 23);
            this.cmdAutoScroll.TabIndex = 4;
            this.cmdAutoScroll.Text = "Auto Scroll";
            this.cmdAutoScroll.UseVisualStyleBackColor = true;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Enabled = false;
            this.cmdDelete.Location = new System.Drawing.Point(517, 420);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Delete(Del)";
            this.cmdDelete.UseVisualStyleBackColor = true;
            // 
            // cmdMove
            // 
            this.cmdMove.Enabled = false;
            this.cmdMove.Location = new System.Drawing.Point(598, 420);
            this.cmdMove.Name = "cmdMove";
            this.cmdMove.Size = new System.Drawing.Size(75, 23);
            this.cmdMove.TabIndex = 2;
            this.cmdMove.Text = "Move(X)";
            this.cmdMove.UseVisualStyleBackColor = true;
            // 
            // cmdCopy
            // 
            this.cmdCopy.Enabled = false;
            this.cmdCopy.Location = new System.Drawing.Point(679, 420);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdCopy.TabIndex = 1;
            this.cmdCopy.Text = "&Copy";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // chkFileList
            // 
            this.chkFileList.CheckOnClick = true;
            this.chkFileList.FormattingEnabled = true;
            this.chkFileList.Location = new System.Drawing.Point(6, 19);
            this.chkFileList.Name = "chkFileList";
            this.chkFileList.Size = new System.Drawing.Size(748, 379);
            this.chkFileList.TabIndex = 0;
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(691, 526);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(75, 23);
            this.cmdExit.TabIndex = 3;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(610, 526);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // frmResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.prgBar);
            this.Name = "frmResults";
            this.Text = "frmResults";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSelectAll;
        private System.Windows.Forms.Button cmdAutoScroll;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdMove;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.CheckedListBox chkFileList;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdCancel;
    }
}