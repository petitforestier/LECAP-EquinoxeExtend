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
    }
 
}
