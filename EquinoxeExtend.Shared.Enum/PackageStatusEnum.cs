using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum PackageStatusEnum
    {
        [Name("FR", "En attente")]
        Waiting = 1,
        [Name("FR", "Dev")]
        Developpement = 20,
        [Name("FR","PréProd")]
        Test = 30,
        [Name("FR", "Prod")]
        Production = 40,
        [Name("FR", "Annulé")]
        Canceled = 50
    };
}
