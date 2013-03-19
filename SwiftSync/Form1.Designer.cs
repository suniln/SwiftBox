namespace SwiftSync
{
    partial class Form1
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
            this.btnUploadFiles = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnUploadFiles
            // 
            this.btnUploadFiles.Location = new System.Drawing.Point(99, 300);
            this.btnUploadFiles.Name = "btnUploadFiles";
            this.btnUploadFiles.Size = new System.Drawing.Size(75, 23);
            this.btnUploadFiles.TabIndex = 2;
            this.btnUploadFiles.Text = "Upload";
            this.btnUploadFiles.UseVisualStyleBackColor = true;
            this.btnUploadFiles.Click += new System.EventHandler(this.btnUploadFiles_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(374, 300);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(17, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(515, 270);
            this.txtLog.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 331);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUploadFiles);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadFiles;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtLog;
    }
}

