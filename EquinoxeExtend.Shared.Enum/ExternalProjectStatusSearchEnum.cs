using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum ExternalProjectStatusSearchEnum
    {
        [Name("FR", "Tout")]
        All = 1,
        [Name("FR", "En attente")]
        Waiting = 10,
        [Name("FR", "En cours")]
        InProgress = 20,
        [Name("FR", "Clôturé")]
        Completed = 30,
        [Name("FR", "Non traité")]
        UnProcessed = 40,
    };
}
