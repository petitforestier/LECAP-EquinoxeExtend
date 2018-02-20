using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum SpecificationCAMDefinitionStatusEnum
    {
        [Name("FR", "")]
        None = 1,
        [Name("FR", "Attente génération programme")]
        WaitingForCAMBuilding = 20,
        [Name("FR", "Génération programme en cours")]
        CAMBuilding = 30,
        [Name("FR","Terminé")]
        Completed = 40,
    };
}
