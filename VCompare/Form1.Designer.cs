namespace VCompare
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.butSaveIndex = new System.Windows.Forms.Button();
            this.butLoadIF = new System.Windows.Forms.Button();
            this.butLoadFS = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butCompare = new System.Windows.Forms.Button();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.lblProcStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // butSaveIndex
            // 
            this.butSaveIndex.Location = new System.Drawing.Point(12, 103);
            this.butSaveIndex.Name = "butSaveIndex";
            this.butSaveIndex.Size = new System.Drawing.Size(113, 23);
            this.butSaveIndex.TabIndex = 0;
            this.butSaveIndex.Text = "Save IndexFile";
            this.butSaveIndex.UseVisualStyleBackColor = true;
            this.butSaveIndex.Click += new System.EventHandler(this.butSaveIndex_Click);
            // 
            // butLoadIF
            // 
            this.butLoadIF.Location = new System.Drawing.Point(6, 45);
            this.butLoadIF.Name = "butLoadIF";
            this.butLoadIF.Size = new System.Drawing.Size(107, 23);
            this.butLoadIF.TabIndex = 1;
            this.butLoadIF.Text = "Load Index File";
            this.butLoadIF.UseVisualStyleBackColor = true;
            this.butLoadIF.Click += new System.EventHandler(this.butLoadIF_Click);
            // 
            // butLoadFS
            // 
            this.butLoadFS.Location = new System.Drawing.Point(6, 16);
            this.butLoadFS.Name = "butLoadFS";
            this.butLoadFS.Size = new System.Drawing.Size(107, 23);
            this.butLoadFS.TabIndex = 2;
            this.butLoadFS.Text = "Load FileSystem";
            this.butLoadFS.UseVisualStyleBackColor = true;
            this.butLoadFS.Click += new System.EventHandler(this.butLoadFS_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butLoadFS);
            this.groupBox1.Controls.Add(this.butLoadIF);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(147, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 74);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FileSystem Info";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // butCompare
            // 
            this.butCompare.Location = new System.Drawing.Point(12, 132);
            this.butCompare.Name = "butCompare";
            this.butCompare.Size = new System.Drawing.Size(112, 41);
            this.butCompare.TabIndex = 6;
            this.butCompare.Text = "Compare with FileSystem";
            this.butCompare.UseVisualStyleBackColor = true;
            this.butCompare.Click += new System.EventHandler(this.butCompare_Click);
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(156, 92);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(191, 23);
            this.pbStatus.TabIndex = 1;
            // 
            // lblProcStatus
            // 
            this.lblProcStatus.AutoSize = true;
            this.lblProcStatus.Location = new System.Drawing.Point(153, 118);
            this.lblProcStatus.Name = "lblProcStatus";
            this.lblProcStatus.Size = new System.Drawing.Size(35, 13);
            this.lblProcStatus.TabIndex = 1;
            this.lblProcStatus.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 199);
            this.Controls.Add(this.lblProcStatus);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.butCompare);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butSaveIndex);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butSaveIndex;
        private System.Windows.Forms.Button butLoadIF;
        private System.Windows.Forms.Button butLoadFS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button butCompare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Label lblProcStatus;
    }
}

