using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum GenerationTypeEnum
    {
        [Name("FR", "Maquette numérique")]
        Model = 10,
        [Name("FR", "Liasses de production")]
        ProductionBundle = 20,
    };
}
