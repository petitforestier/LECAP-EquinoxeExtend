using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Misc;

namespace EquinoxeExtendPlugin.Consts
{
    public static class Consts
    {
        public const string IDSeparator = "<<>>";
        public const string ItemSeparator = "|";
        public static BoolLock DontShowCheckTaskOnStartup = new BoolLock();
        public const string MailingListTeamName = "temListeDiffusion_DeploiementProduct";
        public const string SMTPHOST = "smtp-relay.lecapitaine.fr";
        public const int SMTPPORT = 25;
        public const int WORKINGHOURSADAY = 8;
    }
 
}
