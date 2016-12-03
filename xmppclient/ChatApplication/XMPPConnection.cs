using System;
using System.Drawing;
using System.Windows.Forms;
using S22.Xmpp;
using S22.Xmpp.Client;
using S22.Xmpp.Im;
using System.Net.Security;
using System.Collections.Generic;
using System.Linq;

namespace ChatApplication
{
    public partial class XMPPConnection : Form
    {
        public XmppClient client;
        public XmppIm im;
        string sendTo;

        delegate void SetConverstionLog(string text, string sentby, string sentto);

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
            this.Load += new EventHandler(XMPPConnection_Load);
        }

        private void XMPPConnection_Load(object sender, EventArgs e)
        {
            XMPPConnect();
        }

        string username = "admin";
        string password = "admin";

        public void XMPPConnect()
        {
            //string hostname = "jacurutu.net";
            //string username = "sindhu";
            //string password = "1234";

            string hostname = "desktop-gm279k1";
            //string username = "admin";
            //string password = "admin";
            int port = 5222; bool tls = true;
            RemoteCertificateValidationCallback validate = null;

            im = new XmppIm(hostname, username, password, port, tls, validate);
            im.Message += OnNewMessage;
            im.Connect();
            populateUserlist();
        }

        public void Authenticate(string username, string password)
        {
            im.Autenticate(username, password);
        }

        private void refreshContactsbtn_Click(object sender, EventArgs e)
        {
            populateUserlist();
        }

        private void populateUserlist()
        {
            int location_ptr = 1;
            panel4.Controls.Clear();

            foreach (var item in im.GetRoster())
            {
                Button button = new Button();
                panel4.Controls.Add(button);
                button.Top = location_ptr * 70;
                button.Left = 13;
                button.Size = new Size(150, 50);
                button.Margin = new Padding(10, 10, 10, 10);
                button.Text = item.Name;
                button.Name = item.Jid.ToString();
                button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
                button.Click += new System.EventHandler(this.Button_Click);
                location_ptr = location_ptr + 1;
            }

            sendMsgtbx.Enabled = false;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            sendMsgtbx.Enabled = true;
            sendMsgtbx.Focus();
            sendTo = button.Name.ToString();

            rosterbtnClick(sendTo);
        }

        private void sendMsgtbx_KeyDown(object sender,KeyEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter)
            {
                rosterbtnClick(sendTo);
            }
        }

        private void rosterbtnClick(string sendTo)
        {




            if (!tabControl.TabPages.ContainsKey(sendTo))
            {
                TabPage userTabPage = new TabPage(sendTo);
                tabControl.TabPages.Add(userTabPage);
                RichTextBox convHistorytxb = new RichTextBox();
                convHistorytxb.Size = new System.Drawing.Size(752, 654);
                convHistorytxb.AcceptsTab = true;
                convHistorytxb.Dock = System.Windows.Forms.DockStyle.Fill;
                convHistorytxb.Location = new System.Drawing.Point(3, 3);
                //convHistorytxb.AutoSize = true;
                convHistorytxb.Name = sendTo;

                userTabPage.Controls.Add(convHistorytxb);
                userTabPage.Text = sendTo;
                userTabPage.Name = sendTo;

                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey(sendTo);

            }
        }

        private void sndMsgbtn_Click(object sender, EventArgs e)
        {
            //Jid sendTo = "test@desktop-gm279k1";
            //Jid sendTo = "kingw";'
            Jid tojid = new Jid(sendTo);
            string message = sendMsgtbx.Text;
            message.ThrowIfNull("message");
            im.SendMessage(sendTo, message);

            string sentby = im.Jid.Node.ToString();
            string sentto = tojid.Node.ToString();
            UpdateConversationHistory(message, sentby, sentto);
            sendMsgtbx.Text = "";

        }

        //On receiving a new message from another client
        private void OnNewMessage(object sender, MessageEventArgs e)
        {
            string msg = e.Message.Body;
            string sentby = e.Message.From.Node;
            string sentto = e.Message.To.Node;
            UpdateConversationHistory(msg, sentby, sentto);
        }

        public void UpdateConversationHistory(string msg, string sentby, string sentto)
        {

            if (this.InvokeRequired)
            {
                //Using delegate to update ConversationLog textbox from a different thread.
                SetConverstionLog del = new SetConverstionLog(UpdateConversationHistory); //delegate
                object[] parameters = { msg, sentby };
                this.Invoke(del, parameters);
            }

            else
            {
                string title = sentto;

               

                int tabIndex = tabControl.TabPages.IndexOfKey(tabControl.SelectedTab.Text);
                
               // MessageBox.Show(tabControl.SelectedTab.Text);
                RichTextBox tmpbox = (RichTextBox)tabControl.TabPages[tabIndex].Controls[0];
                tmpbox.Text += DateTime.Now + " " + sentby + ":\n";
                tmpbox.Text += msg + "\n";
            }
        }

        public void UpdateLog(object sender)
        {

        }

        //public void UpdateContactlist(object sender, List<string> rosterlist)
        //{
        //    userList.Items.Clear();
        //    foreach (string user in rosterlist)
        //    {
        //        if (user != client.Jid)
        //            userList.Items.Add(user);
        //    }
        //}



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
            //lvContacts.Items.Clear();
            if (s_contact == "")
            {
                MessageBox.Show("Searchbtn Keyword is null");
            }
            else
            {
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

        private void XMPPConnection_Load_1(object sender, EventArgs e)
        {

        }
    }
}

