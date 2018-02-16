using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum SpecificationPrefabricationDefinitionStatusEnum
    {
        [Name("FR", "")]
        None = 1,
        [Name("FR", "En cours")]
        Working = 20,
        [Name("FR","Terminé")]
        Completed = 30,
    };
}
