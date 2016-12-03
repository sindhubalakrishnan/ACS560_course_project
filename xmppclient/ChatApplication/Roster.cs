using System;
using S22.Xmpp;
using S22.Xmpp.Client;
using S22.Xmpp.Im;

namespace ChatApplication
{
    public partial class Roster : XMPPConnection
    {
        public Roster()
        {
            InitializeComponent();
        }

        private void Roster_Load(object sender, EventArgs e)
        {

        }

        public void AddContact(Jid jid, string name = null, params string[] groups)
        {
            jid.ThrowIfNull("jid");
            //im.AddToRoster(new RosterItem(jid, name, groups));
            im.RequestSubscription(jid);
        }

        //public void RemoveContact(Jid jid)
        public void RemoveContact()
        {
            //jid.ThrowIfNull("jid");
            // This removes the contact from the user's roster AND also cancels any
            // subscriptions.
           //im.RemoveFromRoster(jid);
        }
    }
}
