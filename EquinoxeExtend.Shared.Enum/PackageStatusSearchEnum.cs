using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum PackageStatusSearchEnum
    {
        [Name("FR", "Tout")]
        All=10,
        [Name ("FR","En cours")]
        InProgress=20,
        [Name("FR", "Dev")]
        Developpement=30,
        [Name("FR", "Préprod")]
        Quality=40,
        [Name("FR", "Product")]
        Production=50,
        [Name("FR", "Non Cloturé")]
        NotCompleted = 60,
    };
}
