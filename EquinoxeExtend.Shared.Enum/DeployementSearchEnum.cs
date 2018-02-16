using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum DeployementSearchEnum
    {
        [Name("FR", "Tout")]
        All=10,
        [Name("FR", "Préprod")]
        Staging=40,
        [Name("FR", "Product")]
        Production=50,
    };
}
