using System.Windows.Forms;
using System.Drawing;


namespace ChatApplication
{
    public partial class XMPPConversation : XMPPConnection
    {
        public XMPPConnection xmppconnection;

        public XMPPConversation(XMPPConnection xmppconnection)
        {
            this.xmppconnection = xmppconnection;
        }
    }
}
