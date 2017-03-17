using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum PackageOrderByEnum
    {
        [Name("FR", "Priorité")]
        Priority=10,
        [Name("FR", "Date objectif")]
        DateObjectif=20,
        [Name("FR", "N° package")]
        PackageId=30,
    };
}
