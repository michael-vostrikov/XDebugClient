using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace NppPluginNET
{
	// settings
	public class PluginSettings
	{
		public string server_ip = "127.0.0.1";
		public string server_port = "9000";
		public string path_mapping_from = "";
		public string path_mapping_to = "";
	}

    partial class PluginBase
    {
        #region " Fields "
        internal const string PluginName = "XDebugClient";
        static string iniFilePath = null;
		public static PluginSettings settings = new PluginSettings();
        
        static XDCDialog xdcDlg = null;
        static internal int idXDCDlgItem = -1;
        static Bitmap tbBmp = Properties.Resources.circle;
        static Bitmap tbBmp_tbTab = Properties.Resources.circle_bmp;
        static Icon tbIcon = null;
        #endregion

        #region " Startup/CleanUp "
        static internal void CommandMenuInit()
        {
            // Initialization of your plugin commands
            // You should fill your plugins commands here
 
        	//
	        // Firstly we get the parameters from your plugin config file (if any)
	        //

	        // get path of plugin configuration
            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            iniFilePath = sbIniFilePath.ToString();

	        // if config path doesn't exist, we create it
            if (!Directory.Exists(iniFilePath))
	        {
                Directory.CreateDirectory(iniFilePath);
	        }

	        // make your plugin config file full file path name
            iniFilePath = Path.Combine(iniFilePath, PluginName + ".xml");

	        // read plugin settings
			try
			{
				using (Stream stream = new FileStream(iniFilePath, FileMode.Open))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(PluginSettings));
					settings = (PluginSettings)serializer.Deserialize(stream);
				}
			}
			catch(Exception)
			{
				
			}

            // with function :
            // SetCommand(int index,                            // zero based number to indicate the order of command
            //            string commandName,                   // the command name that you want to see in plugin menu
            //            NppFuncItemDelegate functionPointer,  // the symbol of function (function pointer) associated with this command. The body should be defined below. See Step 4.
            //            ShortcutKey *shortcut,                // optional. Define a shortcut to trigger this command
            //            bool check0nInit                      // optional. Make this menu item be checked visually
            //            );

			SetCommand(0, "Debugger", DockableDlg); idXDCDlgItem = 0;
			SetCommand(1, "Settings", SettingsDialog);
			SetCommand(2, "About", AboutMsg);
		}

        static internal void SetToolBarIcon()
        {
            toolbarIcons tbIcons = new toolbarIcons();
            tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_ADDTOOLBARICON, _funcItems.Items[idXDCDlgItem]._cmdID, pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }

        static internal void PluginCleanUp()
        {
			try
			{
				using (Stream stream = new FileStream(iniFilePath, FileMode.Create))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(PluginSettings));
					serializer.Serialize(stream, settings);
				}
			}
			catch (Exception)
			{
				
			}
        }
        #endregion

        #region " Menu functions "


		static void AboutMsg()
		{
			NppPluginNET.Forms.AboutDialog dlg = new NppPluginNET.Forms.AboutDialog();
			dlg.ShowDialog();
			dlg = null;
		}

		static void SettingsDialog()
		{
			NppPluginNET.Forms.SettingsDialog dlg = new NppPluginNET.Forms.SettingsDialog();
			dlg.ShowDialog();
			dlg = null;
		}
		

        static void DockableDlg()
        {
            // Dockable Dialog Demo
            // 
            // This demonstration shows you how to do a dockable dialog.
            // You can create your own non dockable dialog - in this case you don't nedd this demonstration.
            if (xdcDlg == null)
            {
                xdcDlg = new XDCDialog();

                using (Bitmap newBmp = new Bitmap(16, 16))
                {
					Graphics g = Graphics.FromImage(newBmp);
					ColorMap[] colorMap = new ColorMap[1];
					colorMap[0] = new ColorMap();
					colorMap[0].OldColor = Color.FromArgb(0xFF, 0xEC, 0xE9, 0xD8);		// 0xECE9D8 (KnownColor.ButtonFace on Windows XP)
					colorMap[0].NewColor = Color.FromKnownColor(KnownColor.ButtonFace);
					ImageAttributes attr = new ImageAttributes();
					attr.SetRemapTable(colorMap);
					g.DrawImage(tbBmp_tbTab, new Rectangle(0, 0, 16, 16), 0, 0, 16, 16, GraphicsUnit.Pixel, attr);
					//g.DrawImage(tbBmp_tbTab, new Rectangle(0, 0, 16, 16), 0, 0, 16, 16, GraphicsUnit.Pixel);
					tbIcon = Icon.FromHandle(newBmp.GetHicon());
                }
                
                NppTbData _nppTbData = new NppTbData();
                _nppTbData.hClient = xdcDlg.Handle;
				_nppTbData.pszName = xdcDlg.Text;
                // the dlgDlg should be the index of funcItem where the current function pointer is in
                // this case is 15.. so the initial value of funcItem[15]._cmdID - not the updated internal one !
                _nppTbData.dlgID = idXDCDlgItem;
                // define the default docking behaviour
				_nppTbData.uMask = NppTbMsg.DWS_DF_CONT_BOTTOM | NppTbMsg.DWS_ICONTAB | NppTbMsg.DWS_ICONBAR;
                _nppTbData.hIconTab = (uint)tbIcon.Handle;
                _nppTbData.pszModuleName = PluginName;
                IntPtr _ptrNppTbData = Marshal.AllocHGlobal(Marshal.SizeOf(_nppTbData));
                Marshal.StructureToPtr(_nppTbData, _ptrNppTbData, false);
				xdcDlg.__nppTbData = _nppTbData;

                Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_DMMREGASDCKDLG, 0, _ptrNppTbData);
            }
            else
            {
            	if (!xdcDlg.Visible)
            	{
	                Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_DMMSHOW, 0, xdcDlg.Handle);
					xdcDlg.Start();
            	}
            	else
            	{
	                Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_DMMHIDE, 0, xdcDlg.Handle);
					xdcDlg.Stop();
            	}
            }
        }
        #endregion
    }
}   
