namespace NppPluginNET
{
    partial class XDCDialog
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
			this.bnTurn = new System.Windows.Forms.Button();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.bnDelBreakpoint = new System.Windows.Forms.Button();
			this.bnBreakpoint = new System.Windows.Forms.Button();
			this.bnClearAllBreakpoints = new System.Windows.Forms.Button();
			this.dgr_breakpoints = new System.Windows.Forms.DataGridView();
			this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.file = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.dgr_stack = new System.Windows.Forms.DataGridView();
			this._where = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._file = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._line = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._path = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bnStepInto = new System.Windows.Forms.Button();
			this.bnStepOver = new System.Windows.Forms.Button();
			this.bnStepOut = new System.Windows.Forms.Button();
			this.bnRun = new System.Windows.Forms.Button();
			this.bnStop = new System.Windows.Forms.Button();
			this.lb_state_text = new System.Windows.Forms.Label();
			this.lb_state = new System.Windows.Forms.Label();
			this.tpLog = new System.Windows.Forms.TabPage();
			this.cbLog = new System.Windows.Forms.CheckBox();
			this.bn_clear_log = new System.Windows.Forms.Button();
			this.tb_log = new System.Windows.Forms.RichTextBox();
			this.tpText = new System.Windows.Forms.TabPage();
			this.rt = new System.Windows.Forms.RichTextBox();
			this.tpWatch = new System.Windows.Forms.TabPage();
			this.tv_context = new System.Windows.Forms.TreeView();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabControl2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgr_breakpoints)).BeginInit();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgr_stack)).BeginInit();
			this.tpLog.SuspendLayout();
			this.tpText.SuspendLayout();
			this.tpWatch.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bnTurn
			// 
			this.bnTurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnTurn.Location = new System.Drawing.Point(465, 27);
			this.bnTurn.Name = "bnTurn";
			this.bnTurn.Size = new System.Drawing.Size(75, 23);
			this.bnTurn.TabIndex = 0;
			this.bnTurn.Text = "Turn ON";
			this.bnTurn.UseVisualStyleBackColor = true;
			this.bnTurn.Click += new System.EventHandler(this.bnTurn_Click);
			// 
			// tabControl2
			// 
			this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl2.Controls.Add(this.tabPage3);
			this.tabControl2.Controls.Add(this.tabPage4);
			this.tabControl2.Location = new System.Drawing.Point(567, 5);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(420, 200);
			this.tabControl2.TabIndex = 4;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.bnDelBreakpoint);
			this.tabPage3.Controls.Add(this.bnBreakpoint);
			this.tabPage3.Controls.Add(this.bnClearAllBreakpoints);
			this.tabPage3.Controls.Add(this.dgr_breakpoints);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(412, 174);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Breakpoints";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// bnDelBreakpoint
			// 
			this.bnDelBreakpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnDelBreakpoint.Location = new System.Drawing.Point(250, 144);
			this.bnDelBreakpoint.Name = "bnDelBreakpoint";
			this.bnDelBreakpoint.Size = new System.Drawing.Size(75, 23);
			this.bnDelBreakpoint.TabIndex = 12;
			this.bnDelBreakpoint.Text = "Delete";
			this.bnDelBreakpoint.UseVisualStyleBackColor = true;
			this.bnDelBreakpoint.Click += new System.EventHandler(this.bnDelBreakpoint_Click);
			// 
			// bnBreakpoint
			// 
			this.bnBreakpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnBreakpoint.Location = new System.Drawing.Point(6, 144);
			this.bnBreakpoint.Name = "bnBreakpoint";
			this.bnBreakpoint.Size = new System.Drawing.Size(75, 23);
			this.bnBreakpoint.TabIndex = 10;
			this.bnBreakpoint.Text = "Breakpoint";
			this.bnBreakpoint.UseVisualStyleBackColor = true;
			this.bnBreakpoint.Click += new System.EventHandler(this.bnBreakpoint_Click);
			// 
			// bnClearAllBreakpoints
			// 
			this.bnClearAllBreakpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnClearAllBreakpoints.Location = new System.Drawing.Point(331, 144);
			this.bnClearAllBreakpoints.Name = "bnClearAllBreakpoints";
			this.bnClearAllBreakpoints.Size = new System.Drawing.Size(75, 23);
			this.bnClearAllBreakpoints.TabIndex = 11;
			this.bnClearAllBreakpoints.Text = "Clear all";
			this.bnClearAllBreakpoints.UseVisualStyleBackColor = true;
			this.bnClearAllBreakpoints.Click += new System.EventHandler(this.bnClearAllBreakpoints_Click);
			// 
			// dgr_breakpoints
			// 
			this.dgr_breakpoints.AllowUserToAddRows = false;
			this.dgr_breakpoints.AllowUserToDeleteRows = false;
			this.dgr_breakpoints.AllowUserToResizeRows = false;
			this.dgr_breakpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dgr_breakpoints.BackgroundColor = System.Drawing.Color.White;
			this.dgr_breakpoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgr_breakpoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgr_breakpoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.file,
            this.Line,
            this.Path});
			this.dgr_breakpoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgr_breakpoints.Location = new System.Drawing.Point(6, 6);
			this.dgr_breakpoints.Name = "dgr_breakpoints";
			this.dgr_breakpoints.RowHeadersVisible = false;
			this.dgr_breakpoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgr_breakpoints.ShowEditingIcon = false;
			this.dgr_breakpoints.Size = new System.Drawing.Size(400, 132);
			this.dgr_breakpoints.TabIndex = 0;
			this.dgr_breakpoints.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgr_breakpoints_CellMouseDoubleClick);
			// 
			// id
			// 
			this.id.HeaderText = "ID";
			this.id.Name = "id";
			this.id.Visible = false;
			// 
			// file
			// 
			this.file.HeaderText = "File";
			this.file.Name = "file";
			this.file.Width = 180;
			// 
			// Line
			// 
			this.Line.HeaderText = "Line";
			this.Line.Name = "Line";
			this.Line.Width = 60;
			// 
			// Path
			// 
			this.Path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Path.HeaderText = "Path";
			this.Path.Name = "Path";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.dgr_stack);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(412, 174);
			this.tabPage4.TabIndex = 1;
			this.tabPage4.Text = "Stack";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// dgr_stack
			// 
			this.dgr_stack.AllowUserToAddRows = false;
			this.dgr_stack.AllowUserToDeleteRows = false;
			this.dgr_stack.AllowUserToResizeRows = false;
			this.dgr_stack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dgr_stack.BackgroundColor = System.Drawing.Color.White;
			this.dgr_stack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgr_stack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgr_stack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._where,
            this._file,
            this._line,
            this._path});
			this.dgr_stack.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgr_stack.Location = new System.Drawing.Point(6, 6);
			this.dgr_stack.Name = "dgr_stack";
			this.dgr_stack.RowHeadersVisible = false;
			this.dgr_stack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgr_stack.ShowEditingIcon = false;
			this.dgr_stack.Size = new System.Drawing.Size(393, 157);
			this.dgr_stack.TabIndex = 1;
			this.dgr_stack.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgr_stack_CellMouseDoubleClick);
			// 
			// _where
			// 
			this._where.HeaderText = "Where";
			this._where.Name = "_where";
			this._where.Width = 120;
			// 
			// _file
			// 
			this._file.HeaderText = "File";
			this._file.Name = "_file";
			this._file.Width = 120;
			// 
			// _line
			// 
			this._line.HeaderText = "Line";
			this._line.Name = "_line";
			this._line.Width = 60;
			// 
			// _path
			// 
			this._path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this._path.HeaderText = "Path";
			this._path.Name = "_path";
			// 
			// bnStepInto
			// 
			this.bnStepInto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnStepInto.Enabled = false;
			this.bnStepInto.Location = new System.Drawing.Point(371, 27);
			this.bnStepInto.Name = "bnStepInto";
			this.bnStepInto.Size = new System.Drawing.Size(75, 23);
			this.bnStepInto.TabIndex = 5;
			this.bnStepInto.Text = "StepInto";
			this.bnStepInto.UseVisualStyleBackColor = true;
			this.bnStepInto.Click += new System.EventHandler(this.bnStepInto_Click);
			// 
			// bnStepOver
			// 
			this.bnStepOver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnStepOver.Enabled = false;
			this.bnStepOver.Location = new System.Drawing.Point(371, 56);
			this.bnStepOver.Name = "bnStepOver";
			this.bnStepOver.Size = new System.Drawing.Size(75, 23);
			this.bnStepOver.TabIndex = 6;
			this.bnStepOver.Text = "StepOver";
			this.bnStepOver.UseVisualStyleBackColor = true;
			this.bnStepOver.Click += new System.EventHandler(this.bnStepOver_Click);
			// 
			// bnStepOut
			// 
			this.bnStepOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnStepOut.Enabled = false;
			this.bnStepOut.Location = new System.Drawing.Point(371, 85);
			this.bnStepOut.Name = "bnStepOut";
			this.bnStepOut.Size = new System.Drawing.Size(75, 23);
			this.bnStepOut.TabIndex = 7;
			this.bnStepOut.Text = "StepOut";
			this.bnStepOut.UseVisualStyleBackColor = true;
			this.bnStepOut.Click += new System.EventHandler(this.bnStepOut_Click);
			// 
			// bnRun
			// 
			this.bnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnRun.Enabled = false;
			this.bnRun.Location = new System.Drawing.Point(371, 114);
			this.bnRun.Name = "bnRun";
			this.bnRun.Size = new System.Drawing.Size(75, 23);
			this.bnRun.TabIndex = 8;
			this.bnRun.Text = "Run";
			this.bnRun.UseVisualStyleBackColor = true;
			this.bnRun.Click += new System.EventHandler(this.bnRun_Click);
			// 
			// bnStop
			// 
			this.bnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnStop.Enabled = false;
			this.bnStop.Location = new System.Drawing.Point(371, 143);
			this.bnStop.Name = "bnStop";
			this.bnStop.Size = new System.Drawing.Size(75, 23);
			this.bnStop.TabIndex = 9;
			this.bnStop.Text = "Stop";
			this.bnStop.UseVisualStyleBackColor = true;
			this.bnStop.Click += new System.EventHandler(this.bnStop_Click);
			// 
			// lb_state_text
			// 
			this.lb_state_text.AutoSize = true;
			this.lb_state_text.BackColor = System.Drawing.SystemColors.Control;
			this.lb_state_text.Location = new System.Drawing.Point(258, 6);
			this.lb_state_text.Name = "lb_state_text";
			this.lb_state_text.Size = new System.Drawing.Size(35, 13);
			this.lb_state_text.TabIndex = 10;
			this.lb_state_text.Text = "State:";
			// 
			// lb_state
			// 
			this.lb_state.AutoSize = true;
			this.lb_state.BackColor = System.Drawing.SystemColors.Control;
			this.lb_state.Location = new System.Drawing.Point(293, 6);
			this.lb_state.Name = "lb_state";
			this.lb_state.Size = new System.Drawing.Size(13, 13);
			this.lb_state.TabIndex = 11;
			this.lb_state.Text = "_";
			this.lb_state.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lb_state_MouseDoubleClick);
			// 
			// tpLog
			// 
			this.tpLog.Controls.Add(this.cbLog);
			this.tpLog.Controls.Add(this.bn_clear_log);
			this.tpLog.Controls.Add(this.tb_log);
			this.tpLog.Location = new System.Drawing.Point(4, 22);
			this.tpLog.Name = "tpLog";
			this.tpLog.Padding = new System.Windows.Forms.Padding(3);
			this.tpLog.Size = new System.Drawing.Size(352, 174);
			this.tpLog.TabIndex = 2;
			this.tpLog.Text = "Log";
			this.tpLog.UseVisualStyleBackColor = true;
			// 
			// cbLog
			// 
			this.cbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbLog.AutoSize = true;
			this.cbLog.Location = new System.Drawing.Point(96, 148);
			this.cbLog.Name = "cbLog";
			this.cbLog.Size = new System.Drawing.Size(44, 17);
			this.cbLog.TabIndex = 9;
			this.cbLog.Text = "Log";
			this.cbLog.UseVisualStyleBackColor = true;
			// 
			// bn_clear_log
			// 
			this.bn_clear_log.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bn_clear_log.Location = new System.Drawing.Point(6, 144);
			this.bn_clear_log.Name = "bn_clear_log";
			this.bn_clear_log.Size = new System.Drawing.Size(75, 23);
			this.bn_clear_log.TabIndex = 8;
			this.bn_clear_log.Text = "Clear log";
			this.bn_clear_log.UseVisualStyleBackColor = true;
			this.bn_clear_log.Click += new System.EventHandler(this.bn_clear_log_Click);
			// 
			// tb_log
			// 
			this.tb_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_log.Location = new System.Drawing.Point(4, 6);
			this.tb_log.Name = "tb_log";
			this.tb_log.Size = new System.Drawing.Size(342, 134);
			this.tb_log.TabIndex = 7;
			this.tb_log.Text = "";
			this.tb_log.WordWrap = false;
			// 
			// tpText
			// 
			this.tpText.Controls.Add(this.rt);
			this.tpText.Location = new System.Drawing.Point(4, 22);
			this.tpText.Name = "tpText";
			this.tpText.Padding = new System.Windows.Forms.Padding(3);
			this.tpText.Size = new System.Drawing.Size(352, 174);
			this.tpText.TabIndex = 3;
			this.tpText.Text = "Text";
			this.tpText.UseVisualStyleBackColor = true;
			// 
			// rt
			// 
			this.rt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rt.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rt.Location = new System.Drawing.Point(4, 6);
			this.rt.Name = "rt";
			this.rt.Size = new System.Drawing.Size(342, 161);
			this.rt.TabIndex = 2;
			this.rt.Text = "";
			this.rt.WordWrap = false;
			// 
			// tpWatch
			// 
			this.tpWatch.Controls.Add(this.tv_context);
			this.tpWatch.Location = new System.Drawing.Point(4, 22);
			this.tpWatch.Name = "tpWatch";
			this.tpWatch.Padding = new System.Windows.Forms.Padding(3);
			this.tpWatch.Size = new System.Drawing.Size(352, 174);
			this.tpWatch.TabIndex = 0;
			this.tpWatch.Text = "Watch";
			this.tpWatch.UseVisualStyleBackColor = true;
			// 
			// tv_context
			// 
			this.tv_context.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tv_context.LabelEdit = true;
			this.tv_context.Location = new System.Drawing.Point(4, 6);
			this.tv_context.Name = "tv_context";
			this.tv_context.Size = new System.Drawing.Size(342, 162);
			this.tv_context.TabIndex = 0;
			this.tv_context.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_context_AfterLabelEdit);
			this.tv_context.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_context_AfterSelect);
			this.tv_context.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_context_BeforeLabelEdit);
			this.tv_context.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tv_context_KeyDown);
			this.tv_context.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_context_AfterExpand);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tpWatch);
			this.tabControl1.Controls.Add(this.tpText);
			this.tabControl1.Controls.Add(this.tpLog);
			this.tabControl1.Location = new System.Drawing.Point(5, 5);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(360, 200);
			this.tabControl1.TabIndex = 3;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// XDCDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 210);
			this.Controls.Add(this.lb_state);
			this.Controls.Add(this.lb_state_text);
			this.Controls.Add(this.bnStop);
			this.Controls.Add(this.bnRun);
			this.Controls.Add(this.bnStepOut);
			this.Controls.Add(this.bnStepOver);
			this.Controls.Add(this.bnStepInto);
			this.Controls.Add(this.bnTurn);
			this.Controls.Add(this.tabControl2);
			this.Controls.Add(this.tabControl1);
			this.DoubleBuffered = true;
			this.Name = "XDCDialog";
			this.Text = "XDebugClient";
			this.VisibleChanged += new System.EventHandler(this.XDCDialog_VisibleChanged);
			this.Resize += new System.EventHandler(this.XDCDialog_Resize);
			this.tabControl2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgr_breakpoints)).EndInit();
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgr_stack)).EndInit();
			this.tpLog.ResumeLayout(false);
			this.tpLog.PerformLayout();
			this.tpText.ResumeLayout(false);
			this.tpWatch.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button bnTurn;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button bnStepInto;
		private System.Windows.Forms.Button bnStepOver;
		private System.Windows.Forms.Button bnStepOut;
		private System.Windows.Forms.Button bnRun;
		private System.Windows.Forms.Button bnStop;
		private System.Windows.Forms.Button bnBreakpoint;
		private System.Windows.Forms.DataGridView dgr_breakpoints;
		private System.Windows.Forms.Button bnClearAllBreakpoints;
		private System.Windows.Forms.DataGridView dgr_stack;
		private System.Windows.Forms.Button bnDelBreakpoint;
		private System.Windows.Forms.Label lb_state_text;
		private System.Windows.Forms.Label lb_state;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
		private System.Windows.Forms.DataGridViewTextBoxColumn file;
		private System.Windows.Forms.DataGridViewTextBoxColumn Line;
		private System.Windows.Forms.DataGridViewTextBoxColumn Path;
		private System.Windows.Forms.DataGridViewTextBoxColumn _where;
		private System.Windows.Forms.DataGridViewTextBoxColumn _file;
		private System.Windows.Forms.DataGridViewTextBoxColumn _line;
		private System.Windows.Forms.DataGridViewTextBoxColumn _path;
		private System.Windows.Forms.TabPage tpLog;
		private System.Windows.Forms.CheckBox cbLog;
		private System.Windows.Forms.Button bn_clear_log;
		private System.Windows.Forms.RichTextBox tb_log;
		private System.Windows.Forms.TabPage tpText;
		private System.Windows.Forms.RichTextBox rt;
		private System.Windows.Forms.TabPage tpWatch;
		private System.Windows.Forms.TreeView tv_context;
		private System.Windows.Forms.TabControl tabControl1;




    }
}