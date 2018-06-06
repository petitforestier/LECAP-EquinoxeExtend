using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum DossierStatusEnum
    {
        [Name("FR", "Brouillon")]
        Drafting = 10,
        [Name("FR", "Terminé")]
        Completed = 20,
        [Name("FR", "Annulé")]
        Canceled = 30,
    };
}
