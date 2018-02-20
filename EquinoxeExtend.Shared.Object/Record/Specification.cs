using Service.DBRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtend.Shared.Object.Record
{
    public static class SpecificationAssembler
    {
        #region Public METHODS

        public static Specification Convert(this T_E_Specification iEntity)
        {
            if (iEntity == null) return null;

            return new Specification
            {
                SpecificationId = iEntity.SpecificationId,
                Name = iEntity.Name,
                DossierId = iEntity.DossierId,
                ProjectVersion = iEntity.ProjectVersion,
                Controls = iEntity.Controls,
                Constants = iEntity.Constants,
                CreationDate = iEntity.CreationDate,
                Comments = iEntity.Comments,
                CreatorGUID = iEntity.CreatorGUID,
            };
        }

        public static void Merge(this T_E_Specification iEntity, Specification iObj)
        {
            iEntity.SpecificationId = iObj.SpecificationId;
            iEntity.Name = iObj.Name;
            iEntity.DossierId = iObj.DossierId;
            iEntity.ProjectVersion = iObj.ProjectVersion;
            iEntity.Controls = iObj.Controls;
            iEntity.Constants = iObj.Constants;
            iEntity.CreationDate = iObj.CreationDate;
            iEntity.Comments = iObj.Comments;
            iEntity.CreatorGUID = iObj.CreatorGUID;
        }

        #endregion
    }

    public class Specification
    {
        #region Public PROPERTIES

        public long SpecificationId { get; set; }
        public string Name { get; set; }
        public long DossierId { get; set; }
        public decimal ProjectVersion { get; set; }
        public string Controls { get; set; }
        public string Constants { get; set; }
        public System.DateTime CreationDate { get; set; } 
        public Guid CreatorGUID { get; set; }
        public string Comments { get; set; }

        public List<Generation> Generations { get; set; }

        #endregion
    }

}
