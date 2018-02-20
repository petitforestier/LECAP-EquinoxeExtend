using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum GenerationStatusEnum
    {
        [Name("FR", "En attente")]
        Waiting = 10,
        [Name("FR", "En cours")]
        InProgress = 20,
        [Name("FR", "Terminée")]
        Completed = 30,
    };
}
