using System;
using System.Collections.Generic;
using S22.Xmpp;
using S22.Xmpp.Im;

namespace ChatApplication
{
    public class RosterItem
    {
        ISet<string> groups = new HashSet<string>();

        public Jid Jid
        {
            get;
            private set;
        }

        public string Nickname
        {
            get;
            private set;
        }

        public IEnumerable<string> Groups
        {
            get
            {
                return groups;
            }
        }

        public SubscriptionState SubscriptionState
        {
            get;
            private set;
        }

        public bool Pending
        {
            get;
            private set;
        }

        public RosterItem(Jid jid, string name = null, params string[] groups)
            : this(jid, name, SubscriptionState.None, false, groups)
        {
        }

        internal RosterItem(Jid jid, string name, SubscriptionState state,
            bool pending, IEnumerable<string> groups)
        {
            jid.ThrowIfNull("jid");
            Jid = jid;
            Nickname = name;
            if (groups != null)
            {
                foreach (string s in groups)
                {
                    if (String.IsNullOrEmpty(s))
                        continue;
                    this.groups.Add(s);
                }
            }
            SubscriptionState = state;
            Pending = pending;
        }
    }
}
