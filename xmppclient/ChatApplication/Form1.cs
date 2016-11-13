using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using S22.Xmpp;
using S22.Xmpp.Client;
using S22.Xmpp.Im;
using System.Net.Security;


namespace ChatApplication
{
    public partial class Form1 : Form
    {
        int contact_count = 0;
        public XmppClient client;
        public XmppIm im;

        delegate void SetConverstionLog(string text);

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

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XMPPConnect();
        }

        public void XMPPConnect()
        {
            string hostname = "desktop-gm279k1";
            string username = "admin";
            string password = "admin";
            Jid test = "test@desktop-gm279k1";

            client = new XmppClient(hostname, username, password);
            client.Message += OnNewMessage;
            client.Connect();
            //Console.WriteLine("Connected as " + client.Jid + Environment.NewLine);
            //Console.WriteLine(" Type 'send <JID> <Message>' to send a chat message, or 'quit' to exit.");
            //Console.WriteLine(" Example: send user@domain.com Hello!");
            //Console.WriteLine();

            client.GetRoster();
            foreach (var item in client.GetRoster())
            {
                contact_count++;
                //MessageBox.Show(item.Jid.ToString());
            }
    }

        private void refreshContactsbtn_Click(object sender, EventArgs e)
        {
            int location_ptr = 1;
            panel4.Controls.Clear();

            for (int i = 0; i < contact_count; i++)
            {
                Button button = new Button();
                panel4.Controls.Add(button);
                var count = panel4.Controls.OfType<Button>().ToList().Count;
                button.Top = location_ptr * 70;
                button.Left = 13;
                button.Size = new Size(150, 50);
                button.Margin = new Padding(10, 10, 10, 10);
                button.Name = "button_" + (count + 1);
                button.Text = "Button " + (count + 1);
                button.Click += new System.EventHandler(this.Button_Click);
                location_ptr = location_ptr + 1;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            sendMsgtbx.Focus();
        }

        private void sndMsgbtn_Click(object sender, EventArgs e)
        {
            Jid sendTo = "test@desktop-gm279k1";
            string message = sendMsgtbx.Text;
            client.SendMessage(sendTo, message);
            UpdateConversationHistory(message);
            sendMsgtbx.Text = "";
        }

        //On receiving a new message from another client
        private void OnNewMessage(object sender, MessageEventArgs e)
        {
            string msg = e.Message.Body;
            UpdateConversationHistory(msg);
        }

        public void UpdateConversationHistory(string msg)
        {
            if (this.InvokeRequired)
            {
                //Using delegate to update ConversationLog textbox from a different thread.
                SetConverstionLog del = new SetConverstionLog(UpdateConversationHistory); //delegate
                object[] parameters = { msg }; 
                this.Invoke(del, parameters); 
            }
            else
            {
                convHistorytxb.Text += DateTime.Now + ":\n";
                convHistorytxb.Text += msg + "\n";
            }
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
        
    }
}

