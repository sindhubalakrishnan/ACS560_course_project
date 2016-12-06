using System;
using System.Windows.Forms;

namespace ChatApplication
{
    public partial class Login : Form
    {
        private XMPPConnection xmppconnection;

        public Login(XMPPConnection xmppconnection)
        {
            InitializeComponent();
            this.xmppconnection = xmppconnection;
            this.BringToFront();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string usrname, psswd;
            usrname = usrnametxt1.Text;
            psswd = passwordtxt.Text;
            lblerror.Visible = false;

            xmppconnection.XMPPConnect(usrname, psswd);
            this.Hide();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void setError()
        {
            lblerror.Visible = true;
        }

    }
}
