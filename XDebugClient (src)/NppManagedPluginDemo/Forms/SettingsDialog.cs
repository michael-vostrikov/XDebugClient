using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NppPluginNET.Forms
{
	public partial class SettingsDialog : Form
	{
		public SettingsDialog()
		{
			InitializeComponent();
			tb_ip.Text = PluginBase.settings.server_ip;
			tb_port.Text = PluginBase.settings.server_port;
			tb_path_mapping_from.Text = PluginBase.settings.path_mapping_from;
			tb_path_mapping_to.Text = PluginBase.settings.path_mapping_to;
		}

		private void bnOK_Click(object sender, EventArgs e)
		{
			PluginBase.settings.server_ip = tb_ip.Text;
			PluginBase.settings.server_port = tb_port.Text;
			PluginBase.settings.path_mapping_from = tb_path_mapping_from.Text;
			PluginBase.settings.path_mapping_to = tb_path_mapping_to.Text;
			this.Close();
		}
	}
}