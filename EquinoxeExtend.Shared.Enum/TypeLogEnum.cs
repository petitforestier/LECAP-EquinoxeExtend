using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum TypeLogEnum
    {
        [Name("FR", "Erreur")]
        Error = 10,
        [Name("FR", "Avertissement")]
        Warning = 20,
        [Name("FR", "Information")]
        Information = 30
    };
}
