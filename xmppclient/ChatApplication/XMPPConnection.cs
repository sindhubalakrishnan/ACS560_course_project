using System;
using System.Drawing;
using System.Windows.Forms;
using S22.Xmpp;
using S22.Xmpp.Client;
using S22.Xmpp.Im;
using System.Net.Security;
using System.Linq;

namespace ChatApplication
{
    public partial class XMPPConnection : Form
    {
        public XmppClient client;
        public XmppIm im;
        public string sendTo, receipientName,sentbyJID;
        public TabPage userTabPage;

        // True if the instance -im has been disposed of.
        private bool disposed;

        delegate void SetConverstionLog(string text, string sentby, string sentto);
        public delegate void SetTabCntrls(string receipientName);

        public XmppIm Im
        {
            get
            {
                return im;
            }
        }

        public int Port
        {
            get
            {
                return im.Port;
            }

            set
            {
                im.Port = value;
            }
        }

        public string Username
        {
            get
            {
                return im.Username;
            }

            set
            {
                im.Username = value;
            }
        }

        public string Password
        {
            get
            {
                return im.Password;
            }

            set
            {
                im.Password = value;
            }
        }

        public bool Authenticated
        {
            get
            {
                return im.Authenticated;
            }
        }

        public bool Tls
        {
            get
            {
                return im.Tls;
            }

            set
            {
                im.Tls = value;
            }
        }

        public bool Connected
        {
            get
            {
                if (im != null)
                {
                    return im.Connected;
                }
                else
                {
                    return false;
                }
            }
        }

        public Jid Jid
        {
            get
            {
                return im.Jid;
            }
        }

        public bool IsEncrypted
        {
            get
            {
                return im.IsEncrypted;
            }
        }

        public RemoteCertificateValidationCallback Validate
        {
            get
            {
                return im.Validate;
            }

            set
            {
                im.Validate = value;
            }
        }

        private Boolean IsXmppSuccess
        {
            get;
            set;
        }

        public string Hostname
        {
            get
            {
                return im.Hostname;
            }

            set
            {
                im.Hostname = value;
            }
        }

        public event EventHandler<MessageEventArgs> Message
        {
            add
            {
                im.Message += value;
            }
            remove
            {
                im.Message -= value;
            }
        }

        public XMPPConnection()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            //this.Load += new EventHandler(XMPPConnection_Load);
        }

        private void XMPPConnection_Load(object sender, EventArgs e)
        {
            sendMsgtbx.Width = tableLayoutPanel1.Width;
            Login lgn = new Login(this);
            lgn.Show();
            this.Hide();
        }

        public void XMPPConnect(string username,  string password)
        {
            //this.Show();
            //string hostname = "jacurutu.net";
            //string username = "sindhu";
            //string password = "1234";

            string hostname = username.Split('@')[1];
            username = username.Split('@')[0];
            //string username = "admin";
            //string password = "admin";
            int port = 5222; bool tls = true;
            RemoteCertificateValidationCallback validate = null;

            im = new XmppIm(hostname, username, password, port, tls, validate);
            im.Message += OnNewMessage;

            try
            {
                im.Connect();
            }
            catch (XmppException e)
            {
                Console.Write(e);
                return;
            }
            catch (ObjectDisposedException e)
            {
                Console.Write(e);
                return;
            }
            catch (System.IO.IOException e)
            {
                Console.Write(e);
                return;
            }
            catch (System.Security.Authentication.AuthenticationException e)
            {
                Login lgn = new ChatApplication.Login(this);
                lgn.setError();
                lgn.Show();
                Console.Write(e);
                return;
            }

            this.Show();
            lblUsername.Text = GetBareJid(im.Jid);

            BindRoster();
        }

        private void BindRoster()
        {
            //Invoke XMPPRoster class to build Roster for the chat application.
            XMPPRoster xmpproster = new XMPPRoster(this);
            int location_ptr = 1;
            panel4.Controls.Clear();

            foreach (var item in xmpproster.rosteritems)
            {
                if (GetBareJid(item.Jid).Equals(im.Username)) {
                    continue;
                }
                Button button = new Button();
                panel4.Controls.Add(button);
                button.Top = location_ptr * 50;
                button.Image = Properties.Resources.usr_normal;
                button.ImageAlign = ContentAlignment.MiddleLeft;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Size = new Size(220, 50);
                button.Margin = new Padding(0,0,0,0);
                button.Text = GetBareJid(item.Jid);
                button.Name = GetBareJid(item.Jid);
                button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
                button.Click += new System.EventHandler(Button_Click);
                button.ContextMenuStrip = contextMenuStrip;
                location_ptr = location_ptr + 1;
            }
            //sendMsgtbx.Enabled = false;
        }

        private void refreshContactsbtn_Click(object sender, EventArgs e)
        {
            BindRoster();
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            sendMsgtbx.Enabled = true;
            sendMsgtbx.Focus();
            sendTo = button.Name.ToString();
            receipientName = button.Text;

            startConversation(receipientName);
        }

        private void startConversation(string receipientName)
        {
            //Using delegate to set the Tab controls from a different thread on a new oncoming message.
            if (this.InvokeRequired)
            {
                SetTabCntrls delgateTabCntrl = new SetTabCntrls(startConversation);
                object[] parameters = { receipientName };
                this.Invoke(delgateTabCntrl, parameters);
            }
            else
            {
                //Invoke XMPPConverstion class to perform screen conversation updates
                if (!tabControl.TabPages.ContainsKey(receipientName))
                {
                    userTabPage = new TabPage(receipientName);
                    tabControl.TabPages.Add(userTabPage);
                    convHistorytxb = new RichTextBox();
                    convHistorytxb.Size = new Size(752, 654);
                    convHistorytxb.AcceptsTab = true;
                    convHistorytxb.ReadOnly = true;
                    convHistorytxb.Dock = DockStyle.Fill;
                    convHistorytxb.Location = new Point(3, 3);
                    convHistorytxb.Name = receipientName;

                    userTabPage.Controls.Add(convHistorytxb);
                    userTabPage.Text = receipientName;
                    userTabPage.Name = receipientName;

                    tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey(receipientName);
                }
                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey(receipientName);
            }
        }

        private void sendMsgtbx_KeyUp(object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  startConversation(receipientName);
                // string sentto = sendTo;
                if (sendTo == null)
                {
                    sendTo = sentbyJID;
                }
                Jid tojid = new Jid(sendTo);
                sendMessage(sendTo);
            }
        }

        private void sendMessage(string sentto)
        {
           string message = sendMsgtbx.Text;
           if(!sentto.StartsWith(tabControl.SelectedTab.Text, StringComparison.OrdinalIgnoreCase))
            {
                string hostsuffix = sentto.Split('@')[1];
                sentto = tabControl.SelectedTab.Text +'@' + hostsuffix;
            }

            AssertValid();
            im.SendMessage(sentto, message);
            string sentby = GetBareJid(im.Jid);

            UpdateConversationHistory(message, sentby, sentto);
            sendMsgtbx.Text = string.Empty;
        }

        private void sndMsgbtn_Click(object sender, EventArgs e)
        {
            //Jid sendTo = "test@desktop-gm279k1";
            //Jid sendTo = "kingw";'

            if(sendTo == null)
            {
                sendTo = sentbyJID;
            }
            Jid tojid = new Jid(sendTo);
            //string sentto = tojid.Node.ToString();

            sendMessage(sendTo);
        }

        //On receiving a new message from another client
        private void OnNewMessage(object sender, MessageEventArgs e)
        {
            string msg = e.Message.Body;
            string sentby = GetBareJid(e.Message.From);
            string sentto = GetBareJid(e.Message.To);
            sentbyJID = GetBareJid(e.Message.From);

            //sendMsgtbx.Enabled = true;
            startConversation(sentby);
            UpdateConversationHistory(msg, sentby, sentto);
        }

        public void UpdateConversationHistory(string msg, string sentby, string sentto)
        {
            if (this.InvokeRequired)
            {
                //Using delegate to update ConversationLog textbox from a different thread.
                SetConverstionLog delgate = new SetConverstionLog(UpdateConversationHistory); //delegate
                object[] parameters = { msg, sentby, sentto };
                this.Invoke(delgate, parameters);
            }

            else
            {
                string title = sentto;
                int tabIndex = tabControl.TabPages.IndexOf(tabControl.SelectedTab);
                RichTextBox tmpbox = (RichTextBox)tabControl.TabPages[tabIndex].Controls[0];
                tmpbox.Text += DateTime.Now + " " + sentby + ":\n";
                tmpbox.Text += msg + "\n";
            }
        }

        private void Searchtxb_leave(object sender, EventArgs e)
        {
            if (Searchtxb.Text.Trim().Length == 0)
            {
                Searchtxb.Text = "Search";
                Searchtxb.ForeColor = Color.LightGray;
            }
        }

        private void Searchtxb_Enter(object sender, EventArgs e)
        {
            Searchtxb.Text = string.Empty;
        }

        private void AddContactbtn_Click(object sender, EventArgs e)
        {
            // Roster.AddContact(jid, name, groups);
            //Roster.RemoveContact();
            ClearControls();
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            SearchContact(Searchtxb.Text);
            //Searchtxb.ThrowIfNull("Search keyword is empty");
            //foreach (var user in im.GetRoster().ToString())
            //{
            //    if (user.Contains(Searchtxb.Text))
            //        Nicknametxb = user.Name;
            //}
        }

        private void ClearControls()
        {
            Searchtxb.Text = string.Empty;
            //Groupscombobox.Items.Clear();
            //Groupscombobox.SelectedText = "";
        }

        private void SearchContact(string s_contact)
        {
            ClearControls();
            if (s_contact == string.Empty)
            {
                MessageBox.Show("Searchbtn Keyword is null");
            }
            else
            {
                AssertValid();
                foreach (var contact in im.GetRoster())
                {
                    if (contact.Name.StartsWith(s_contact, StringComparison.OrdinalIgnoreCase))
                    {
                        Searchtxb.Text = contact.Name;
                        if (!Groupscombobox.Items.Contains(contact.Groups))
                        {
                            Groupscombobox.Items.Add(contact.Groups.FirstOrDefault());
                        }
                        Groupscombobox.SelectedText = contact.Groups.FirstOrDefault();
                    }
                }
            }
        }

        private void MyPopupEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(tabControl.TabCount > 0)
            {
                newGrpConv.Enabled = true;
            }

        }

        private void NewGroupConv_Click(object sender, EventArgs e)
        {
            string grpconvTitle = tabControl.SelectedTab.Text;
            string addUsertogrpconv = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Text.ToString();
            if(!grpconvTitle.Contains(addUsertogrpconv))
            {
                grpconvTitle += "," + addUsertogrpconv;
                string Title = string.Empty;
                    if(grpconvTitle.StartsWith("Group Chat", StringComparison.OrdinalIgnoreCase))
                {
                    Title += grpconvTitle;
                }
                    else
                {
                    Title = "Group Chat - " + grpconvTitle;
                }
                tabControl.SelectedTab.Text = Title;

            }
        }

        public void AssertValid()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);
            if (!Connected)
                throw new InvalidOperationException("Not connected to XMPP server.");
            if (!Authenticated)
                throw new InvalidOperationException("Not authenticated with XMPP server.");
        }

        private string GetBareJid(Jid jid) {
            return jid.ToString().Split('@')[0];
        }
    }
}

