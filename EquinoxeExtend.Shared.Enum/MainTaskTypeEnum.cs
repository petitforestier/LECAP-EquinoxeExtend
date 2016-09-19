using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum MainTaskTypeEnum
    {
        [Name("FR", "Project développer catalogue")]
        ProjectDeveloppement = 10,
        [Name("FR", "Correction")]
        Fix = 20,
        [Name("FR", "Amélioration continue")]
        ContinuousAmelioration = 30
    };
}
