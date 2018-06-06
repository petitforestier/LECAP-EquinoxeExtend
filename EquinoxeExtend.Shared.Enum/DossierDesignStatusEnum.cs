using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum DossierDesignStatusEnum
    {
        [Name("FR", "Non commencé")]
        None = 10,
        [Name("FR", "Commencé")]
        Started = 20,
    };
}
