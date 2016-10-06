using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum MainTaskStatusEnum
    {
        [Name("FR", "Annulé")]
        Canceled = 1,
        [Name ("FR","Demande")]
        Requested=10,
        [Name("FR", "En attente")]
        Waiting=20,
        [Name("FR", "En dev")]
        Dev=30,
        [Name("FR", "En test")]
        Staging = 35,
        [Name("FR", "Terminée")]
        Completed=40,
    };
}
