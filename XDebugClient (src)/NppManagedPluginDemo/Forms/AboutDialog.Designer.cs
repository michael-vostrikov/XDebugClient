namespace NppPluginNET.Forms
{
	partial class AboutDialog
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
			this.bnOK = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// bnOK
			// 
			this.bnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bnOK.Location = new System.Drawing.Point(89, 87);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(75, 23);
			this.bnOK.TabIndex = 1;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(228, 63);
			this.label3.TabIndex = 3;
			this.label3.Text = "User interface for XDebug extention\r\n\r\nVostrikov Michael\r\narchangel87@mail.ru";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(252, 122);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.bnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutDialog_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bnOK;
		protected internal System.Windows.Forms.Label label3;
	}
}