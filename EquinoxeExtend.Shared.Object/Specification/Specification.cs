using Service.DBSpecification.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtend.Shared.Object.Specification
{
    public static class SpecificationAssembler
    {
        #region Public METHODS

        public static Specification Convert(this T_E_Specification iEntity)
        {
            if (iEntity == null) return null;

            return new Specification
            {
                Constants = iEntity.Constants,
                Controls = iEntity.Controls,
                Description = iEntity.Description,
                DisplayName = iEntity.DisplayName,
                IsTemplate = iEntity.IsTemplate,
                ProjectVersion = iEntity.ProjectVersion,
                SpecificationName = iEntity.SpecificationName,
                SpecificationVersion = iEntity.SpecificationVersion,
            };
        }

        public static void Merge(this T_E_Specification iEntity, Specification iObj)
        {
            iEntity.Constants = iObj.Constants;
            iEntity.Controls = iObj.Controls;
            iEntity.Description = iObj.Description;
            iEntity.DisplayName = iObj.DisplayName;
            iEntity.IsTemplate = iObj.IsTemplate;
            iEntity.ProjectVersion = iObj.ProjectVersion;
            iEntity.SpecificationName = iObj.SpecificationName;
            iEntity.SpecificationVersion = iObj.SpecificationVersion;
        }

        #endregion
    }

    public class Specification
    {
        #region Public PROPERTIES

        public string SpecificationName { get; set; }
        public string Description { get; set; }
        public int SpecificationVersion { get; set; }
        public decimal ProjectVersion { get; set; }
        public string Controls { get; set; }
        public string Constants { get; set; }
        public bool IsTemplate { get; set; }
        public string DisplayName { get; set; }

        #endregion
    }
}