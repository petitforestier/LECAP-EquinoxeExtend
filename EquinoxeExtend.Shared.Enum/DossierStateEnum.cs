using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum DossierStateEnum
    {
        [Name("FR", "Brouillon")]
        Drafting = 10,
        [Name("FR", "Incomplet")]
        NotCompleted = 20,
        [Name("FR", "Complet")]
        Completed = 30,
    };
}
