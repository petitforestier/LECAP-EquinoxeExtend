using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtendPlugin.Object
{
    public class Specification : EquinoxeExtend.Shared.Object.Record.Specification
    {
        public DriveWorks.Specification.SpecificationDetails SpecificationDetails { get; set; } 
    }

    public static class SpecificationAssembler
    {
        public static EquinoxeExtendPlugin.Object.Specification ConvertFull(this EquinoxeExtend.Shared.Object.Record.Specification iSpecification)
        {
            if (iSpecification == null) return null;

            return new EquinoxeExtendPlugin.Object.Specification
            {
                 Constants = iSpecification.Constants,
                 Controls = iSpecification.Controls,
                 CreationDate = iSpecification.CreationDate,
                 DossierId = iSpecification.DossierId,
                 Name = iSpecification.Name,
                 ProjectVersion = iSpecification.ProjectVersion,
                 SpecificationId = iSpecification.SpecificationId,
                 Comments = iSpecification.Comments,
            };
        }
    }
}
