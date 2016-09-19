using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum MainTaskStatusSearchEnum
    {
        [Name("FR", "Tout")]
        All=10,
        [Name("FR", "Non clôturé")]
        NotCompleted=20,
        [Name("FR", "Demande")]
        Request=30,
        [Name("FR", "En attente")]
        Waiting=40,
        [Name("FR", "En cours")]
        InProgress=50,
        [Name("FR", "Clôturé")]
        Completed=60,
        [Name("FR", "Annulé")]
        Canceled=70,
    };
}
