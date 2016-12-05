using System;
using System.IO;
using S22.Xmpp;
using S22.Xmpp.Im;

namespace ChatApplication
{
    public partial class XMPPRoster: XMPPConnection
    {
        public Roster rosteritems;
        public XMPPConnection xmppconnection = new XMPPConnection();

        public XMPPRoster(XMPPConnection xmppconnection)
        {
            this.xmppconnection = xmppconnection;
            try
            {
                xmppconnection.AssertValid();
                rosteritems = xmppconnection.im.GetRoster();
            }
            catch (XmppException e)
            {
                Console.Write(e);
                return;
            }
            catch (InvalidOperationException e)
            {
                Console.Write(e);
                return;
            }
            catch (IOException e)
            {
                Console.Write(e);
                return;
            }
            catch (System.Security.Authentication.AuthenticationException e)
            {
                Console.Write(e);
                return;
            }
            catch (XmppErrorException e)
            {
                Console.Write(e);
                return;
            }
        }
    }
}
