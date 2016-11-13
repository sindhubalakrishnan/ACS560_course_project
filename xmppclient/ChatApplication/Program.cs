using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using S22.Xmpp;
using S22.Xmpp.Client;
using S22.Xmpp.Core;
using S22.Xmpp.Extensions;
using S22.Xmpp.Im;
using System.Text.RegularExpressions;

namespace ChatApplication
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new Form1());
        }
    }
}
