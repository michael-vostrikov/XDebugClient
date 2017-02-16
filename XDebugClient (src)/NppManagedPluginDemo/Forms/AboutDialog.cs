using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NppPluginNET.Forms
{
	public partial class AboutDialog : Form
	{
		public AboutDialog()
		{
			InitializeComponent();
		}

		private void AboutDialog_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) this.Close();
		}
	}
}