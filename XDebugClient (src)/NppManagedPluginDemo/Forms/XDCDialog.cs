using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace NppPluginNET
{
    partial class XDCDialog : Form
    {
		public NppTbData __nppTbData;
        public XDCDialog()
        {
			InitializeComponent();
			//SetDoubleBuffer(this, true);
			//SetDoubleBuffer(tv_context, true);
		}

		public static void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs args)
		{
			Exception e = args.Exception;
			MessageBox.Show("Произошло исключение: " + e.Message);
		}

		public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e0)
		{
			Exception e = ((Exception)(e0.ExceptionObject));
			MessageBox.Show("Произошло необработанное исключение: " + e.Message);
		}


		public static void SetDoubleBuffer(Control ctrl, bool value)
		{
			ControlStyles ctrlStyles = ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer;

			Type ctrlType = ctrl.GetType();
			System.Reflection.MethodInfo methodInfo = ctrlType.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			methodInfo.Invoke(ctrl, new object[] { ctrlStyles, value });
		}

		// -----------------------------------------------------------------------------
		
		[DllImport("user32.dll")]
		static extern void FlashWindow(IntPtr a, bool b);
		[DllImport("user32.dll")]
		internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

		void EnableButtons(bool f_enable)
		{
			bnStepInto.Enabled = f_enable;
			bnStepOver.Enabled = f_enable;
			bnStepOut.Enabled = f_enable;
			bnRun.Enabled = f_enable;
			bnStop.Enabled = f_enable;

			if (f_enable == false)
			{
				SetNodesColor(tv_context.Nodes, System.Drawing.Color.Black);
			}
		}

		void SetNodesColor(TreeNodeCollection tnc, System.Drawing.Color clr)
		{
			foreach (TreeNode tn in tnc)
			{
				tn.ForeColor = clr;
				if (tn.Nodes.Count != 0) SetNodesColor(tn.Nodes, clr);
			}
		}



		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
			out uint lpdwProcessId);

		// When you don't want the ProcessId, use this overload and pass 
		// IntPtr.Zero for the second parameter
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
			IntPtr ProcessId);

		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		/// The GetForegroundWindow function returns a handle to the 
		/// foreground window.
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach,
			uint idAttachTo, bool fAttach);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(HandleRef hWnd);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

		private void ForceForegroundWindow(IntPtr hWnd)
		{
			uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
			uint appThread = GetCurrentThreadId();
			const uint SW_SHOW = 5;

			if (foreThread != appThread)
			{
				AttachThreadInput(foreThread, appThread, true);
				BringWindowToTop(hWnd);
				ShowWindow(hWnd, SW_SHOW);
				AttachThreadInput(foreThread, appThread, false);
			}
			else
			{
				BringWindowToTop(hWnd);
				ShowWindow(hWnd, SW_SHOW);
			}
		}

		Thread state_thread = null;
		void receive_handler(string msg)
		{
			try
			{
				if (InvokeRequired)
				{
					this.Invoke(new ld(receive_handler), new object[] { msg });
					return;
				}

				string[] str_array = msg.Split('\0');
				if (str_array.Length < 2) return;	// unknown string

				for (int i = 0; i < str_array.Length; i += 2)
				{
					string len = str_array[i];			// skip
					if (len.Length == 0) continue;
					string xml_str = str_array[i+1];
					if (xml_str.Length == 0) continue;

					XmlDocument doc = new XmlDocument();
					doc.LoadXml(xml_str);

					switch (doc.DocumentElement.Name)
					{
						case "init":
							FlashWindow(PluginBase.nppData._nppHandle, true);

							transaction_id = 1;
							SendCommand("feature_set -n max_depth -v 4");
							SendCommand("feature_set -n max_children -v 65536");
							SendCommand("feature_set -n max_data -v 65536");
							SendCommand("breakpoints");
							SendCommand("breakpoint_list");
							SendCommand("run");
							EnableButtons(true);
							break;

						case "response":
							switch (doc.DocumentElement.Attributes["command"].Value)
							{
								case "run":
									switch (doc.DocumentElement.Attributes["status"].Value)
									{
										case "stopping":
											SendCommand("run");
											EnableButtons(false);
											server.CloseSocket();
											break;

										case "break":
											lb_state.Text = "break";

											if (doc.DocumentElement.ChildNodes.Count > 0)
											{
												XmlNode nd = doc.DocumentElement.ChildNodes[0];
												string fn = nd.Attributes["filename"].Value;
												string ln = nd.Attributes["lineno"].Value;
												fn = fn.Replace("file:///", "");
												lb_state.Text += ": " + ln + " (" + System.IO.Path.GetFileName(fn) + ")";
												DoOpenFile(fn);
												GoToLine(ln);

												ForceForegroundWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
												//SetForegroundWindow(PluginBase.nppData._nppHandle);

												SendCommand("stack_get");
												//SendCommand("context_get");
												SendCommand("watch_get");
											}
											break;
									}
									break;

								case "stop":
									SendCommand("run");
									EnableButtons(false);
									server.CloseSocket();
									break;

								case "stack_get":
									dgr_stack.Rows.Clear();
									foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
									{
										if (nd.Name == "stack")
										{
											string file = (string)nd.Attributes["filename"].Value;
											file = file.Replace("file:///", "");
											string fn = System.IO.Path.GetFileName(file);
											string[] row_data = {
												(string)nd.Attributes["level"].Value + ": " + (string)nd.Attributes["where"].Value,
												fn, (string)nd.Attributes["lineno"].Value, file
											};
											dgr_stack.Rows.Add(row_data);
										}
									}
									break;

								case "context_get":
									//FillTree(doc.DocumentElement, null);
									break;

								case "eval":
								case "property_get":
									if (doc.DocumentElement.ChildNodes.Count != 1) break;
									UpdateWatch(doc.DocumentElement.ChildNodes[0], null);
								break;

								case "breakpoint_list":
									foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
									{
										if (nd.Name == "breakpoint")
										{
											string file = (string)nd.Attributes["filename"].Value;
											file = file.Replace("file:///", "");
											string line = (string)nd.Attributes["lineno"].Value;
											string id   = (string)nd.Attributes["id"].Value;

											foreach(DataGridViewRow row in dgr_breakpoints.Rows)
												if ((string)row.Cells["path"].Value == file && (string)row.Cells["line"].Value == line)
												{
													row.Cells["id"].Value = id;
													break;
												}
										}
									}
									break;

								case "step_into":
								case "step_over":
								case "step_out":
									lb_state.Text = "break";
									if (state_thread != null)
									{
										state_thread.Abort();
										state_thread = null;
									}
									if (doc.DocumentElement.Attributes["status"].Value == "stopping")
									{
										SendCommand("run");
										EnableButtons(false);
										server.CloseSocket();
										break;
									}

									if (doc.DocumentElement.ChildNodes.Count > 0)
									{
										XmlNode nd = doc.DocumentElement.ChildNodes[0];
										string fn = nd.Attributes["filename"].Value;
										string ln = nd.Attributes["lineno"].Value;
										fn = fn.Replace("file:///", "");
										lb_state.Text += ": " + ln + " (" + System.IO.Path.GetFileName(fn) + ")";
										DoOpenFile(fn);
										GoToLine(ln);

										SendCommand("stack_get");
										//SendCommand("context_get");
										SendCommand("watch_get");
									}
									break;
							}
							break;
					}
					doc = null;
				}
			}
			catch (Exception e)
			{
				log(e.ToString());
			}
		}

		public void SendCommand(string cmd)
		{
			try
			{
				string msg = "";
				switch (cmd)
				{
					case "context_get":
						/*msg = "context_get -i " + transaction_id.ToString() + " -c 0";
						server.Send(msg);
						transaction_id++;

						msg = "context_get -i " + transaction_id.ToString() + " -c 1";
						server.Send(msg);
						transaction_id++;*/

						return;

					case "watch_get":
						foreach (TreeNode nd in tv_context.Nodes)
						{
							AddWatch(nd.Text, nd);
						}
						return;

					case "breakpoints":
						string file, line;
						for (int i = 0; i < dgr_breakpoints.Rows.Count; i++)
						{
							file = (string)dgr_breakpoints.Rows[i].Cells["path"].Value;
							line = (string)dgr_breakpoints.Rows[i].Cells["line"].Value;
							SetBreakpoint(file, line);
						}
						return;

					default:
						msg = cmd + " -i " + transaction_id.ToString();
						break;
				}

				if (msg.Length != 0 && server != null) server.Send(msg);
				if (cmd == "run")
				{
					lb_state.Text = "ready";
				}
				else if(cmd == "step_into" || cmd == "step_over" || cmd == "step_out")
				{
					state_thread = new Thread(SetLabelState);
					state_thread.IsBackground = true;
					state_thread.Start("ready");
				}
				transaction_id++;
			}
			catch (Exception e)
			{
				log(e.ToString());
			}
		}

		delegate void ssd(object s);
		void SetLabelState(object state)
		{
			if (InvokeRequired)
			{
				this.Invoke(new ssd(SetLabelState2), new object[] { state });
				return;
			}
			else
			{
				SetLabelState2(state);
			}
		}

		void SetLabelState2(object state)
		{
			Thread.Sleep(400);
			lb_state.Text = (string)state;
		}

		void RemoveBreakpoint(string breakpoint_id)
		{
			if (server != null && server.socket != null)
			{
				server.Send("breakpoint_remove -i " + transaction_id + " -d " + breakpoint_id);
				transaction_id++;
			}
		}

		void SetBreakpoint(string file, string line)
		{
			if (server.socket != null)
			{
				string msg = "breakpoint_set -i " + transaction_id.ToString()
					+ " -t line -f file:///" + file
					+ " -n " + line + " -s enabled -h 0";
				server.Send(msg);
				transaction_id++;
			}
		}

		public void AddBreakpoint(string file, string line)
		{
			try
			{
				string filename = System.IO.Path.GetFileName(file);
				file = file.Replace('\\', '/');

				StringBuilder sb = new StringBuilder(file);
				string[] from = PluginBase.settings.path_mapping_from.Split(';');
				string[] to = PluginBase.settings.path_mapping_to.Split(';');
				for (int i = 0; i < from.Length; i++)
				{
					if (i >= to.Length) break;
					if (from[i].Length == 0) continue;

					from[i] = from[i].Trim();
					to[i] = to[i].Trim();
					sb.Replace(from[i], to[i], 0, from[i].Length);
					// replace only one time
					if (sb.ToString() != file) { file = sb.ToString(); break; }
				}
				sb = null;

				foreach (DataGridViewRow row in dgr_breakpoints.Rows)
				{
					if ((string)row.Cells["path"].Value == file && (string)row.Cells["line"].Value == line)
					{
						if ((string)row.Cells["id"].Value != "") RemoveBreakpoint((string)row.Cells["id"].Value);
						dgr_breakpoints.Rows.Remove(row);

						dgr_breakpoints.ClearSelection();
						return;
					}
				}

				string[] row_data = { "", filename, line, file };

				dgr_breakpoints.Rows.Add(row_data);
				SetBreakpoint(file, line);
				SendCommand("breakpoint_list");

				dgr_breakpoints.ClearSelection();
			}
			catch (Exception e)
			{
				log(e.ToString());
			}
		}

		// класс для хранения значений в TreeNode.Tag
		class WatchInfo
		{
			// transaction_id используется в TreeNode для идентификации родительских выражений, потому что в ответе название выражения не приходит
			public string transaction_id = "";
			public XmlNode xml_node = null;
		}

		TreeNode find_node(string transaction_id)
		{
			foreach (TreeNode nd in tv_context.Nodes)
			{
				/*if (((WatchInfo)nd.Tag).xml_node != null)
				{
					string tr_id = "";
					XmlNode property_node = ((WatchInfo)nd.Tag).xml_node;
					if (property_node.Attributes["transaction_id"] != null)
						tr_id = property_node.Attributes["transaction_id"].Value;
					if (tr_id == transaction_id) return nd;
				}
				else*/
				{
					if (((WatchInfo)nd.Tag).transaction_id == transaction_id) return nd;
				}
			}
			return null;
		}

		string get_watch_value(XmlNode watch_node)
		{
			string watch_value = "";
			if (watch_node != null && watch_node.ChildNodes.Count > 0)
			{
				XmlCDataSection cdata = watch_node.ChildNodes[0] as XmlCDataSection;
				if (cdata != null)
				{
					if (watch_node.Attributes["encoding"] != null && watch_node.Attributes["encoding"].Value == "base64")
						watch_value = Encoding.UTF8.GetString(Convert.FromBase64String(cdata.Value));
					else
						watch_value = cdata.Value;
				}
			}
			return watch_value;
		}

		delegate void uw(XmlNode a, TreeNode b);
		public void UpdateWatch(XmlNode property_node, TreeNode tn_root)
		{
			try
			{
				if (InvokeRequired)
				{
					this.Invoke(new uw(UpdateWatch), new object[] { property_node, tn_root });
					return;
				}


				TreeNode tree_node = tn_root;
				if (tree_node == null)
				{
					// первый уровень, название выражения
					// ищем узел дерева по transaction_id
					string transaction_id = "";
					if (property_node.ParentNode != null)
					{
						// опеределяем transaction_id
						if (property_node.ParentNode.Attributes["transaction_id"] != null)
						{
							transaction_id = property_node.ParentNode.Attributes["transaction_id"].Value;
							// добавляем атрибут в property_node, чтобы не лазить в ParentNode
							property_node.Attributes.Append(property_node.ParentNode.Attributes["transaction_id"]);
						}
					}

					tree_node = find_node(transaction_id);
					if (tree_node == null) return;
				}
				tree_node.ForeColor = System.Drawing.Color.Black;


				if (property_node.Name == "error")
				{
					if (tree_node.Nodes.Count != 0) { tree_node.ForeColor = System.Drawing.Color.Red; tree_node.Nodes.Clear(); }
					else tree_node.ForeColor = System.Drawing.Color.Black;
					return;
				}


				// отображаем property_node в tree_node

				// добавляем все узлы в список для удаления
				Dictionary<string, TreeNode> del_list = new Dictionary<string, TreeNode>();
				foreach (TreeNode nd in tree_node.Nodes)
				{
					del_list.Add(nd.Text, nd);
				}


				int cnt = 0;
				if (property_node.Attributes["numchildren"] != null)
					cnt = Convert.ToInt32(property_node.Attributes["numchildren"].Value);

				if (cnt > 0)
				{
					// есть дочерние элементы, добавляем рекурсивно
					foreach (XmlNode xn in property_node.ChildNodes)
					{
						string name = xn.Attributes["name"].Value;
						
						// если узел с таким названием есть, удалять его не будем
						TreeNode child_tree_node = null;
						del_list.TryGetValue(name, out child_tree_node);
						if (child_tree_node != null)
						{
							child_tree_node.ForeColor = System.Drawing.Color.Black;
							del_list.Remove(name);
							UpdateWatch(xn, child_tree_node);
						}
						else
						{
							// если такого узла нет, добавляем
							child_tree_node = tree_node.Nodes.Add(name);
							child_tree_node.Tag = new WatchInfo();
							UpdateWatch(xn, child_tree_node);
							HighlightChange(child_tree_node);
						}
					}
				}
				else
				{
					// дочерних элемнтов нет, отображаем значение узла

					string watch_value = get_watch_value(property_node);
					
					// ограничиваем длину текста до 80 символов
					string node_name = watch_value;
					node_name = node_name.Replace('\r', ' ');
					node_name = node_name.Replace('\n', ' ');
					node_name = node_name.Replace('\t', ' ');
					if (node_name.Length > 83)
					{
						node_name = node_name.Substring(0, 80);
						node_name += "...";
					}

					string prev_watch_value = "";
					bool f_keep_node = true;
					if(((WatchInfo)tree_node.Tag).xml_node != null)
					{
						int cnt_child = 0;
						if (((WatchInfo)tree_node.Tag).xml_node.Attributes["numchildren"] != null)
							cnt_child = Convert.ToInt32(((WatchInfo)tree_node.Tag).xml_node.Attributes["numchildren"].Value);

						if (cnt_child > 0)
						{
							// раньше было несколько дочерних элементов, значение не определяем
							prev_watch_value = "";
							// сохранять узел не надо, надо удалить
							f_keep_node = false;
						}
						else
						{
							prev_watch_value = get_watch_value(((WatchInfo)tree_node.Tag).xml_node);
						}
					}

					// если значения равны и узел с таким названием есть, удалять его не будем
					// значения проверяем, потому что они могут быть больше 80 символов
					TreeNode child_tree_node = null;
					del_list.TryGetValue(node_name, out child_tree_node);
					if (f_keep_node && watch_value == prev_watch_value && child_tree_node != null)
					{
						child_tree_node.ForeColor = System.Drawing.Color.Black;
						del_list.Remove(node_name);
					}
					else
					{
						// если такого узла нет, добавляем, кроме значения null
						if (property_node.Attributes["type"] != null && property_node.Attributes["type"].Value == "null")
						{
							
						}
						else if (tree_node.Nodes.Count == 1 && tree_node.Nodes[0].Nodes.Count == 0)
						{
							// если есть только один дочерний узел со значением переменной, обновляем значение
							child_tree_node = tree_node.Nodes[0];
							del_list.Remove(child_tree_node.Text);
							child_tree_node.Text = node_name;
							HighlightChange(child_tree_node);
						}
						else
						{
							child_tree_node = tree_node.Nodes.Add(node_name);
							child_tree_node.Tag = new WatchInfo();
							HighlightChange(child_tree_node);
						}
					}
				}


				// удаляем лишние узлы
				foreach (KeyValuePair<string, TreeNode> p in del_list)
				{
					HighlightChange(p.Value);
					p.Value.Remove();
				}


				// сохраняем xml информацию
				((WatchInfo)tree_node.Tag).xml_node = property_node.Clone();

				// если выделен текущий узел или его дочерний узел со значением, обновляем вкладку с полным текстом
				if(tree_node.IsSelected || tree_node.Nodes.Count == 1 && tree_node.Nodes[0].IsSelected)
					UpdateTextTab(tv_context.SelectedNode);

				return;
			}
			catch (Exception e)
			{
				log(e.ToString());
			}
			
		}

		void HighlightChange(TreeNode node)
		{
			TreeNode tn = node;
			do
			{
				tn.ForeColor = System.Drawing.Color.Red;
				tn = tn.Parent;
			}
			while (tn != null);
		}

		public void AddWatch(string s, TreeNode node)
		{
			if (node.Tag == null)
			{
				WatchInfo info = new WatchInfo();
				node.Tag = info;
			}
			((WatchInfo)node.Tag).transaction_id = transaction_id.ToString();

			string str64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
			string msg = "eval -i " + transaction_id.ToString() + " -- " +  str64;
			server.Send(msg);
			transaction_id++;
		}

		void GoToLine(string line_str)
		{
			int line = Convert.ToInt32(line_str);
			IntPtr curScintilla = PluginBase.GetCurrentScintilla();
			Win32.SendMessage(curScintilla, SciMsg.SCI_ENSUREVISIBLE, line - 1, 0);
			Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOLINE, line - 1, 0);
			Win32.SendMessage(curScintilla, SciMsg.SCI_ENSUREVISIBLE, line - 1, 0);
			Win32.SendMessage(curScintilla, SciMsg.SCI_GRABFOCUS, 0, 0);
		}

		void DoOpenFile(string filename)
		{
			//Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETFULLCURRENTPATH, 0, &s);
			//if (s == filename) return;

			StringBuilder sb = new StringBuilder(filename);
			string[] from = PluginBase.settings.path_mapping_from.Split(';');
			string[] to = PluginBase.settings.path_mapping_to.Split(';');
			for (int i = 0; i < from.Length; i++)
			{
				if (i >= to.Length) break;
				if (from[i].Length == 0) continue;

				from[i] = from[i].Trim();
				to[i] = to[i].Trim();
				sb.Replace(to[i], from[i], 0, to[i].Length);
				// replace only one time
				if (sb.ToString() != filename) { filename = sb.ToString(); break; }
			}
			sb = null;


			IntPtr strPtr = Marshal.StringToHGlobalUni(filename);
			Win32.SendMessage(PluginBase.nppData._nppHandle, SciMsg.WM_DOOPEN, 0, strPtr);
			Marshal.FreeHGlobal(strPtr);
		}

		public string GetCurrentFile()
		{
			StringBuilder path = new StringBuilder(Win32.MAX_PATH);
			Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETFULLCURRENTPATH, 0, path);
			return path.ToString();
		}

		public int GetCurrentLine()
		{
			int n = -1;
			IntPtr n_ptr = Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTLINE, 0, 0);
			n = n_ptr.ToInt32();
			return n+1;
		}

		
		public HookDemoHelper m = null;
		Server server;
		public bool f_started = true;
		int transaction_id = 0;

		public void Start()
		{
			f_started = true;
			bnTurn.Text = "Turn OFF";
			lb_state.Text = "ready";

			transaction_id = 1;

			server = new Server(receive_handler, log);
			m = new HookDemoHelper(this);
		}

		public void Stop()
		{
			if (server != null) SendCommand("run");
			f_started = false;
			m = null;

			if (server != null) server.Stop();
			server = null;
			EnableButtons(false);

			lb_state.Text = "ready";
			bnTurn.Text = "Turn ON";
		}

		delegate void ld(string s);
		void log(string data)
		{
			if (InvokeRequired)
			{
				this.Invoke(new ld(log), new object[] { data });
				return;
			}

			if (cbLog.Checked == false) return;

			data += "\n";
			data = data.Replace('\0', ' ');
			tb_log.Text += data;

			tb_log.SelectionStart = tb_log.TextLength;
			tb_log.ScrollToCaret();
		}

		private void bnTurn_Click(object sender, EventArgs e)
		{
			try
			{
				if (f_started == false) Start();
				else Stop();
			}
			catch (Exception ex)
			{
				log(ex.ToString());
			}
		}

		private void bnStepInto_Click(object sender, EventArgs e)
		{
			SendCommand("step_into");
		}

		private void bnStepOver_Click(object sender, EventArgs e)
		{
			SendCommand("step_over");
		}

		private void bnStepOut_Click(object sender, EventArgs e)
		{
			SendCommand("step_out");
		}

		private void bnRun_Click(object sender, EventArgs e)
		{
			SendCommand("run");
		}

		private void bnStop_Click(object sender, EventArgs e)
		{
			SendCommand("stop");
		}

		private void bnBreak_Click(object sender, EventArgs e)
		{
			SendCommand("break");
		}

		private void bnBreakpoint_Click(object sender, EventArgs e)
		{
			AddBreakpoint(GetCurrentFile(), GetCurrentLine().ToString());
		}

		private void bnClearAllBreakpoints_Click(object sender, EventArgs e)
		{
			while (dgr_breakpoints.Rows.Count > 0)
			{
				dgr_breakpoints.Rows[0].Selected = true;
				bnDelBreakpoint_Click(sender, e);
			}
				
		}

		private void bn_clear_log_Click(object sender, EventArgs e)
		{
			tb_log.Clear();
		}

		private void bnDelBreakpoint_Click(object sender, EventArgs e)
		{
			if (dgr_breakpoints.SelectedRows.Count != 0)
			{
				DataGridViewRow row = dgr_breakpoints.SelectedRows[0];

				if ((string)row.Cells["id"].Value != "") RemoveBreakpoint((string)row.Cells["id"].Value);
				dgr_breakpoints.Rows.Remove(row);

				dgr_breakpoints.ClearSelection();
				return;
			}
		}

		private void XDCDialog_VisibleChanged(object sender, EventArgs e)
		{
			//if (this.Visible) Start();
			//else Stop();
		}


		[StructLayout(LayoutKind.Sequential)]
		struct NMHDR
		{
			public IntPtr hwndFrom;
			public IntPtr idFrom;
			public int code;
		}

		protected override void WndProc(ref Message msg)
		{

			const int WM_NOTIFY = 0x004E;

			if (msg.Msg == WM_NOTIFY)
			{
				NMHDR nmh = (NMHDR)msg.GetLParam(typeof(NMHDR));
				if (nmh.hwndFrom == PluginBase.nppData._nppHandle)
				{
					switch ((DockMgrMsg)(nmh.code & 0x0000FFFF))	// LOWORD
					{
						case DockMgrMsg.DMN_DOCK:
							Start();
						break;

						case DockMgrMsg.DMN_CLOSE:
							Stop();
						break;
					}
				}
			}

			base.WndProc(ref msg);
		}

		public void tv_context_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Insert)
				{
					TreeNode nd = tv_context.Nodes.Add("");
					nd.BeginEdit();
				}

				if (e.KeyCode == Keys.Delete)
				{
					TreeNode nd = tv_context.SelectedNode;
					if (nd != null && nd.Parent == null)
					{
						nd.Remove();
					}
				}

				if (e.KeyCode == Keys.F2)
				{
					if (!this.Visible)
					{
						Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DMMSHOW, 0, this.Handle);
						//this.Visible = true;
						tabControl1.Refresh();
					}
					else
					{

						if (tv_context.SelectedNode != null) tv_context.SelectedNode.BeginEdit();
						if (tv_context.Nodes.Count == 0)
						{
							TreeNode nd = tv_context.Nodes.Add("");
							nd.BeginEdit();
						}
					}
				}

				if (e.Control && e.KeyCode == Keys.C && tv_context.SelectedNode != null)
				{
					UpdateTextTab(tv_context.SelectedNode);
					string str = rt.Text;
					str = str.Replace("\r\n", "\n");
					str = str.Replace("\n", "\r\n");
					Clipboard.SetText(str);
				}
			}
			catch (Exception ex)
			{
				log(ex.ToString());
			}
		}

		private void tv_context_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Nodes.Count == 1) e.Node.Nodes[0].Expand();
			tv_context.SelectedNode = e.Node;
		}

		private void tv_context_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			string name = "";

			if (e.Label == null)
			{
				if (e.Node.Text == "") e.Node.Remove();
				e.CancelEdit = true;
				return;
			}
			
			name = e.Label;
			if (e.Node.Nodes.Count == 0)
			{
				e.Node.Nodes.Add("tmp"); e.Node.Expand(); e.Node.Nodes.Clear();
			}
			AddWatch(name, e.Node);
		}

		private void tv_context_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			// редактируем только названия переменных (узлов первого уровня)
			if (e.Node.Parent != null) e.CancelEdit = true;
		}

		private void dgr_breakpoints_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex < 0 || dgr_breakpoints.Rows.Count == 0) return;

				string path = (string)dgr_breakpoints.Rows[e.RowIndex].Cells["path"].Value;
				string line = (string)dgr_breakpoints.Rows[e.RowIndex].Cells["line"].Value;
				DoOpenFile(path);
				GoToLine(line);
			}
			catch (Exception) { }
		}

		private void dgr_stack_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex < 0 || dgr_stack.Rows.Count == 0) return;

				string path = (string)dgr_stack.Rows[e.RowIndex].Cells["_path"].Value;
				string line = (string)dgr_stack.Rows[e.RowIndex].Cells["_line"].Value;
				DoOpenFile(path);
				GoToLine(line);
			}
			catch (Exception) { }
		}

		private void lb_state_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dgr_stack.Rows.Count <= 0) return;

			string path = (string)dgr_stack.Rows[0].Cells["_path"].Value;
			string line = (string)dgr_stack.Rows[0].Cells["_line"].Value;
			DoOpenFile(path);
			GoToLine(line);
		}

		private void XDCDialog_Resize(object sender, EventArgs e)
		{
			tabControl1.Refresh();
		}

		void UpdateTextTab(TreeNode tn)
		{
			rt.Text = "";
			if (tn == null) return;

			string str = "";
			
			if (tn.Tag != null && ((WatchInfo)tn.Tag).xml_node == null && tn.Parent != null)
			{
				// выделен узел со значением
				object tag = tn.Parent.Tag;
				XmlNode xn = null;
				if (tag != null) xn = ((WatchInfo)tag).xml_node;

				if (xn != null) str = get_watch_value(xn);
				else str = tn.Text;
			}
			else str = tn.Text;
			
			
			rt.Text = str;
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl1.SelectedIndex == 1)
			{
				UpdateTextTab(tv_context.SelectedNode);
			}
			else if (tabControl1.SelectedIndex == 0)
			{
				tv_context.Focus();
			}
		}

		private void tv_context_AfterSelect(object sender, TreeViewEventArgs e)
		{
			UpdateTextTab(tv_context.SelectedNode);
		}
    }


	// -----------------------------------------------------------------------------
	// -----------------------------------------------------------------------------


	internal class HookDemoHelper
	{
		private const int WH_KEYBOARD_LL = 13;
		private const int WH_KEYBOARD = 2;
		private LowLevelKeyboardProcDelegate m_callback;
		private IntPtr m_hHook;

		[DllImport("user32.dll", SetLastError = true)]

		private static extern IntPtr SetWindowsHookEx(
			int idHook,
			LowLevelKeyboardProcDelegate lpfn,
			IntPtr hMod, int dwThreadId);
		
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);
		
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr CallNextHookEx(
			IntPtr hhk,
			int nCode, IntPtr wParam, IntPtr lParam);


		private IntPtr LowLevelKeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode < 0)
			{
				return CallNextHookEx(m_hHook, nCode, wParam, lParam);
			}
			else
			{
				// не обрабатываем в ситуациях, когда открыто другое окно (например, диалог открытия файла)
				if (XDCDialog.GetForegroundWindow() != PluginBase.nppData._nppHandle)
					return IntPtr.Zero;

				if (xdcDlg.f_started == true)
				{
					// keydown: lParam & 0x40000000 == 0
					if (wParam.ToInt32() == 116)
					{
						if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
						{
							// Shift+F5
							if (((int)lParam & 0x40000000) == 0) xdcDlg.SendCommand("stop");
						}
						else
						{
							// F5
							if (((int)lParam & 0x40000000) == 0) xdcDlg.SendCommand("run");
						}
						IntPtr val = new IntPtr(1);
						return val;
					}
					else if (wParam.ToInt32() == 120)
					{
						// F9
						if (((int)lParam & 0x40000000) == 0) xdcDlg.AddBreakpoint(xdcDlg.GetCurrentFile(), xdcDlg.GetCurrentLine().ToString());
						IntPtr val = new IntPtr(1);
						return val;
					}
					else if (wParam.ToInt32() == 121)
					{
						// F10
						if (((int)lParam & 0x40000000) == 0) xdcDlg.SendCommand("step_over");

						IntPtr val = new IntPtr(1);
						return val;
					}
					else if (wParam.ToInt32() == 122)
					{
						if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
						{
							// Shift+F11
							if (((int)lParam & 0x40000000) == 0) xdcDlg.SendCommand("step_out");
						}
						else
						{
							// F11
							if (((int)lParam & 0x40000000) == 0) xdcDlg.SendCommand("step_into");
						}
						IntPtr val = new IntPtr(1);
						return val;
					}
					else if(wParam.ToInt32() == 113)
					{
						// F2
						if (((int)lParam & 0x40000000) == 0)
						{
							KeyEventArgs e = new KeyEventArgs(Keys.F2);
							xdcDlg.tv_context_KeyDown(xdcDlg, e);
						}
						IntPtr val = new IntPtr(1);
						return val;
					}
					else
					{
						return CallNextHookEx(m_hHook, nCode, wParam, lParam);
					}
				}
				else return CallNextHookEx(m_hHook, nCode, wParam, lParam);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct KeyboardHookStruct
		{
			public readonly int VirtualKeyCode;
			public readonly int ScanCode;
			public readonly int Flags;
			public readonly int Time;
			public readonly IntPtr ExtraInfo;
		}

		private delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);
		public void SetHook()
		{
			m_callback = LowLevelKeyboardHookProc;
			int id = AppDomain.GetCurrentThreadId();
			m_hHook = SetWindowsHookEx(WH_KEYBOARD, m_callback, GetModuleHandle(IntPtr.Zero), id);	// WH_KEYBOARD
			int e = Marshal.GetLastWin32Error();
		}

		public void Unhook()
		{
			UnhookWindowsHookEx(m_hHook);
		}

		XDCDialog xdcDlg;
		public HookDemoHelper(XDCDialog dlg)
		{
			xdcDlg = dlg;
			SetHook();
		}

		~HookDemoHelper()
		{
			Unhook();
		}
	}


	public delegate void ReceiveSocketHandler(string s);
	public delegate void LogHandler(string s);



	public class Server
	{
		string server_ip;
		int server_port;

		public Socket socket;
		Socket listener;
		ReceiveSocketHandler receive_handler;
		LogHandler log_handler;
		Thread connection_thread;

		public Server(ReceiveSocketHandler h, LogHandler lg)
		{
			try
			{
				server_ip = PluginBase.settings.server_ip;
				server_port = Convert.ToInt32(PluginBase.settings.server_port);
			}
			catch(Exception)
			{
				server_ip = "127.0.0.1";
				server_port = 9000;
			}

			receive_handler = h;
			log_handler = lg;
			connection_thread = new Thread(Start);
			connection_thread.IsBackground = true;
			connection_thread.Start();
		}

		~Server()
		{
			//Stop();
		}

		public void Stop()
		{
			if (socket != null)
			{
				try
				{
					socket.Shutdown(SocketShutdown.Both);
				}
				catch (Exception e)
				{
					log_handler(e.ToString());
				}
				socket.Close();
				Thread.Sleep(100);
				socket = null;
			}

			if (listener != null)
			{
				listener.Close();
				listener = null;
			}

			Thread.Sleep(100);
			//connection_thread.Abort();
		}

		public void CloseSocket()
		{
			if (socket != null)
			{
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
				Thread.Sleep(100);
				socket = null;
			}
		}

		public void Send(string data)
		{
			if (socket == null) return;
			log_handler("Send: " + data);

			data += "\0";
			byte[] msg = Encoding.UTF8.GetBytes(data);
			socket.Send(msg);
		}

		void Start()
		{
			// Create a TCP/IP socket.
			listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and 
			// listen for incoming connections.
			try
			{
				listener.Bind(new IPEndPoint(IPAddress.Parse(server_ip), server_port));
				listener.Listen(10);

				// Start listening for connections.
				while (listener != null)
				{
					// Program is suspended while waiting for an incoming connection.
					socket = listener.Accept();
					log_handler("Accept: " + socket.RemoteEndPoint.ToString());

					string data = null;
					byte[] bytes = new byte[1024];

					// An incoming connection needs to be processed.
					int bytesRec = 0;
					while (socket != null && socket.Connected)
					{
						data = "";
						while (socket.Available > 0)
						{
							bytesRec = socket.Receive(bytes);
							data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
						}

						if (data.Length != 0)
						{
							log_handler("Receive: " + data);
							receive_handler(data);
						}
						Thread.Sleep(100);
					}

					log_handler("Close");
					if (socket != null)
					{
						socket.Shutdown(SocketShutdown.Both);
						socket.Close();
						socket = null;
					}

					Thread.Sleep(100);
				}
			}
			catch (SocketException e)
			{
				if (e.ErrorCode != 10004)
					log_handler(e.ToString());
			}
			catch (Exception e)
			{ log_handler(e.ToString()); }
		}


	}







}

