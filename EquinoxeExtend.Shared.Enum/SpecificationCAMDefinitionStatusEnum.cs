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
        Working = 20,
        [Name("FR","Terminé")]
        Completed = 30,
    };
}
