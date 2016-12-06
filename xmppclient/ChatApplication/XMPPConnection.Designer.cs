namespace ChatApplication
{
    partial class XMPPConnection
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Groupscombobox = new System.Windows.Forms.ComboBox();
            this.AddContactbtn = new System.Windows.Forms.Button();
            this.Searchtxb = new System.Windows.Forms.TextBox();
            this.Searchbtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.refreshContactsbtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.sndMsgbtn = new System.Windows.Forms.Button();
            this.sendMsgtbx = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.convHistorytxb = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newGrpConv = new System.Windows.Forms.ToolStripMenuItem();
            this.addContact = new System.Windows.Forms.ToolStripMenuItem();
            this.removeContact = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.panel1.Controls.Add(this.Groupscombobox);
            this.panel1.Controls.Add(this.AddContactbtn);
            this.panel1.Controls.Add(this.Searchtxb);
            this.panel1.Controls.Add(this.Searchbtn);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.refreshContactsbtn);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 693);
            this.panel1.TabIndex = 0;
            // 
            // Groupscombobox
            // 
            this.Groupscombobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Groupscombobox.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Groupscombobox.FormattingEnabled = true;
            this.Groupscombobox.Location = new System.Drawing.Point(139, 280);
            this.Groupscombobox.Name = "Groupscombobox";
            this.Groupscombobox.Size = new System.Drawing.Size(200, 29);
            this.Groupscombobox.TabIndex = 10;
            // 
            // AddContactbtn
            // 
            this.AddContactbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.AddContactbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddContactbtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.AddContactbtn.Location = new System.Drawing.Point(22, 272);
            this.AddContactbtn.Name = "AddContactbtn";
            this.AddContactbtn.Size = new System.Drawing.Size(109, 40);
            this.AddContactbtn.TabIndex = 0;
            this.AddContactbtn.Text = "Add Contact";
            this.AddContactbtn.UseVisualStyleBackColor = false;
            this.AddContactbtn.Click += new System.EventHandler(this.AddContactbtn_Click);
            // 
            // Searchtxb
            // 
            this.Searchtxb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Searchtxb.Location = new System.Drawing.Point(139, 212);
            this.Searchtxb.Name = "Searchtxb";
            this.Searchtxb.Size = new System.Drawing.Size(200, 19);
            this.Searchtxb.TabIndex = 3;
            this.Searchtxb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Searchtxb.Leave += new System.EventHandler(this.Searchtxb_leave);
            // 
            // Searchbtn
            // 
            this.Searchbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.Searchbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Searchbtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Searchbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Searchbtn.Location = new System.Drawing.Point(22, 199);
            this.Searchbtn.Name = "Searchbtn";
            this.Searchbtn.Size = new System.Drawing.Size(100, 40);
            this.Searchbtn.TabIndex = 4;
            this.Searchbtn.Text = "Search";
            this.Searchbtn.UseVisualStyleBackColor = false;
            this.Searchbtn.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.AutoSize = true;
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.panel4.Location = new System.Drawing.Point(22, 341);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(304, 369);
            this.panel4.TabIndex = 14;
            // 
            // refreshContactsbtn
            // 
            this.refreshContactsbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.refreshContactsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshContactsbtn.Location = new System.Drawing.Point(22, 139);
            this.refreshContactsbtn.Name = "refreshContactsbtn";
            this.refreshContactsbtn.Size = new System.Drawing.Size(194, 30);
            this.refreshContactsbtn.TabIndex = 6;
            this.refreshContactsbtn.Text = "Refresh Contacts";
            this.refreshContactsbtn.UseVisualStyleBackColor = false;
            this.refreshContactsbtn.Click += new System.EventHandler(this.refreshContactsbtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(366, 593);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.lblUsername);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(366, 100);
            this.panel2.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Available",
            "Away",
            "Offline",
            "Busy",
            "Do Not Disturb",
            "Invisible"});
            this.comboBox1.Location = new System.Drawing.Point(123, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(216, 29);
            this.comboBox1.TabIndex = 2;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(119, 13);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(71, 30);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "label1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ChatApplication.Properties.Resources.UserIcon;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 91);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(366, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(766, 693);
            this.panel3.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(766, 693);
            this.tabControl.TabIndex = 1;
            // 
            // sndMsgbtn
            // 
            this.sndMsgbtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.sndMsgbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sndMsgbtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sndMsgbtn.Location = new System.Drawing.Point(685, 3);
            this.sndMsgbtn.Name = "sndMsgbtn";
            this.sndMsgbtn.Size = new System.Drawing.Size(78, 197);
            this.sndMsgbtn.TabIndex = 3;
            this.sndMsgbtn.Text = "Send";
            this.sndMsgbtn.UseVisualStyleBackColor = true;
            this.sndMsgbtn.Click += new System.EventHandler(this.sndMsgbtn_Click);
            // 
            // sendMsgtbx
            // 
            this.sendMsgtbx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.sendMsgtbx.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMsgtbx.Location = new System.Drawing.Point(3, 3);
            this.sendMsgtbx.Name = "sendMsgtbx";
            this.sendMsgtbx.Size = new System.Drawing.Size(676, 197);
            this.sendMsgtbx.TabIndex = 4;
            this.sendMsgtbx.Text = "";
            this.sendMsgtbx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendMsgtbx_KeyUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.16449F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.83551F));
            this.tableLayoutPanel1.Controls.Add(this.sndMsgbtn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.sendMsgtbx, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(366, 490);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(766, 203);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // convHistorytxb
            // 
            this.convHistorytxb.AcceptsTab = true;
            this.convHistorytxb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.convHistorytxb.Location = new System.Drawing.Point(3, 3);
            this.convHistorytxb.Name = "convHistorytxb";
            this.convHistorytxb.Size = new System.Drawing.Size(752, 654);
            this.convHistorytxb.TabIndex = 0;
            this.convHistorytxb.Text = "";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGrpConv,
            this.addContact,
            this.removeContact});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(297, 94);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.MyPopupEventHandler);
            // 
            // newGrpConv
            // 
            this.newGrpConv.Enabled = false;
            this.newGrpConv.Name = "newGrpConv";
            this.newGrpConv.Size = new System.Drawing.Size(296, 30);
            this.newGrpConv.Text = "New Group Conversation";
            this.newGrpConv.ToolTipText = "Group Conversation is enabled only when atleaast when 1 chat is active";
            this.newGrpConv.Click += new System.EventHandler(this.NewGroupConv_Click);
            // 
            // addContact
            // 
            this.addContact.Name = "addContact";
            this.addContact.Size = new System.Drawing.Size(296, 30);
            this.addContact.Text = "Add Contact";
            // 
            // removeContact
            // 
            this.removeContact.Name = "removeContact";
            this.removeContact.Size = new System.Drawing.Size(296, 30);
            this.removeContact.Text = "Remove Contact";
            // 
            // XMPPConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1132, 693);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "XMPPConnection";
            this.ShowIcon = false;
            this.Text = " Chat Application - XMPP ";
            this.Load += new System.EventHandler(this.XMPPConnection_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.PictureBox pictureBox1;
        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.Panel panel3;
        protected System.Windows.Forms.Button refreshContactsbtn;
        protected System.Windows.Forms.BindingSource bindingSource1;
        protected System.Windows.Forms.Panel panel4;
        protected System.Windows.Forms.Button sndMsgbtn;
        protected System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        protected System.Windows.Forms.TextBox Searchtxb;
        protected System.Windows.Forms.Button AddContactbtn;
        protected System.Windows.Forms.Button Searchbtn;
       // protected System.Windows.Forms.Button button;
        protected System.Windows.Forms.ComboBox Groupscombobox;
       public System.Windows.Forms.TabControl tabControl;
       public System.Windows.Forms.RichTextBox convHistorytxb;
        protected System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        protected System.Windows.Forms.ToolStripMenuItem newGrpConv;
        protected System.Windows.Forms.ToolStripMenuItem addContact;
        protected System.Windows.Forms.ToolStripMenuItem removeContact;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.RichTextBox sendMsgtbx;
        //private System.Windows.Forms.ContextMenuStrip RosterActionscontextMenu;
        //private System.Windows.Forms.ToolStrip toolStrip1;
        //private System.Windows.Forms.ToolStripDropDownButton RosterActtoolStripDropDownbtn;
        //private System.Windows.Forms.MenuStrip menuStrip1;
        //private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

