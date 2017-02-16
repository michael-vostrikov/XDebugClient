namespace NppPluginNET.Forms
{
	partial class SettingsDialog
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
			this.bnCancel = new System.Windows.Forms.Button();
			this.tb_ip = new System.Windows.Forms.TextBox();
			this.tb_port = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tb_path_mapping_from = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tb_path_mapping_to = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// bnOK
			// 
			this.bnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnOK.Location = new System.Drawing.Point(74, 135);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(75, 23);
			this.bnOK.TabIndex = 0;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// bnCancel
			// 
			this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(204, 135);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(75, 23);
			this.bnCancel.TabIndex = 1;
			this.bnCancel.Text = "Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			// 
			// tb_ip
			// 
			this.tb_ip.Location = new System.Drawing.Point(74, 12);
			this.tb_ip.Name = "tb_ip";
			this.tb_ip.Size = new System.Drawing.Size(100, 20);
			this.tb_ip.TabIndex = 2;
			// 
			// tb_port
			// 
			this.tb_port.Location = new System.Drawing.Point(246, 12);
			this.tb_port.Name = "tb_port";
			this.tb_port.Size = new System.Drawing.Size(100, 20);
			this.tb_port.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "IP address";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(214, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Port";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(38, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "From";
			// 
			// tb_path_mapping_from
			// 
			this.tb_path_mapping_from.Location = new System.Drawing.Point(74, 69);
			this.tb_path_mapping_from.Name = "tb_path_mapping_from";
			this.tb_path_mapping_from.Size = new System.Drawing.Size(272, 20);
			this.tb_path_mapping_from.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(71, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Path mapping:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(48, 98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(20, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "To";
			// 
			// tb_path_mapping_to
			// 
			this.tb_path_mapping_to.Location = new System.Drawing.Point(74, 95);
			this.tb_path_mapping_to.Name = "tb_path_mapping_to";
			this.tb_path_mapping_to.Size = new System.Drawing.Size(272, 20);
			this.tb_path_mapping_to.TabIndex = 8;
			// 
			// SettingsDialog
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(363, 170);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tb_path_mapping_to);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tb_path_mapping_from);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tb_port);
			this.Controls.Add(this.tb_ip);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.bnOK);
			this.Name = "SettingsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bnOK;
		private System.Windows.Forms.Button bnCancel;
		private System.Windows.Forms.TextBox tb_ip;
		private System.Windows.Forms.TextBox tb_port;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tb_path_mapping_from;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tb_path_mapping_to;
	}
}