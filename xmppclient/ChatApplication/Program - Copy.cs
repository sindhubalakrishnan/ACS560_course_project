using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using S22.Xmpp.Client;
using System.Text.RegularExpressions;

namespace ChatApplication
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string hostname = "desktop-gm279k1";
            string username = "admin";
            string password = "admin";

            using (XmppClient client = new XmppClient(hostname, username, password))
            {
                // Setup any event handlers.
                // ...
                client.Connect();
                //MessageBox.Show(client.Jid.ToString());
                //client.SendMessage(message);
                Console.WriteLine("Connected as " + client.Jid + Environment.NewLine);
                Console.WriteLine(" Type 'send <JID> <Message>' to send a chat message, or 'quit' to exit.");
                Console.WriteLine(" Example: send user@domain.com Hello!");
                Console.WriteLine();

                while (true)
                {
                    Console.Write("> ");
                    //string s = Console.ReadLine();
                    string s = "send admin@desktop-gm279k1/10v39cdykm Hello";
                    if (s.StartsWith("send "))
                    {
                        Match m = Regex.Match(s, @"^send\s(?<jid>[^\s]+)\s(?<message>.+)$");
                        if (!m.Success)
                            continue;
                        string recipient = m.Groups["jid"].Value, message = m.Groups["message"].Value;
                        // Send the chat-message.
                        client.SendMessage(recipient, message);
                    }
                    if (s == "quit")
                        return;

                    foreach (var item in client.GetRoster())
                    //Console.WriteLine(" - " + item.Jid);
                    MessageBox.Show(item.Jid.ToString());
                }

                //Console.WriteLine("Contacts on " + client.Jid.Node + "'s contact-list:");
                
            }
        }

    }
}
