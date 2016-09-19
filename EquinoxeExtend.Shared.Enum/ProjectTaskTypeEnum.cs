using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum ProjectTaskTypeEnum
    {
        [Name("FR", "Autre")]
        Other = 10,
        [Name("FR", "Projet driveworks")]
        DriveWorksProject = 20,
    };
}
