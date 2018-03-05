using Service.DBRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquinoxeExtend.Shared.Enum;

namespace EquinoxeExtend.Shared.Object.Record
{
    public static class GenerationAssembler
    {
        #region Public METHODS

        public static Generation Convert(this T_E_Generation iEntity)
        {
            if (iEntity == null) return null;

            return new Generation
            {
                Comments = iEntity.Comments,
                CreationDate = iEntity.CreationDate,
                CreatorGUID = iEntity.CreatorGUID,
                GenerationId = iEntity.GenerationId,
                ProjectName = iEntity.ProjectName,
                SpecificationId = iEntity.SpecificationId,
                State = (GenerationStatusEnum)iEntity.StateRef,
                Type = (GenerationTypeEnum)iEntity.TypeRef,
                History = iEntity.History,
            };
        }

        public static void Merge(this T_E_Generation iEntity, Generation iObj)
        {
            iEntity.Comments = iObj.Comments;
            iEntity.CreationDate = iObj.CreationDate;
            iEntity.CreatorGUID = iObj.CreatorGUID;
            iEntity.GenerationId = iObj.GenerationId;
            iEntity.ProjectName = iObj.ProjectName;
            iEntity.SpecificationId = iObj.SpecificationId;
            iEntity.StateRef = (short)iObj.State;
            iEntity.TypeRef = (short)iObj.Type;
            iEntity.History = iObj.History;
        }

        #endregion
    }

    public class Generation
    {
        #region Public PROPERTIES

        public long GenerationId { get; set; }
        public long SpecificationId { get; set; }
        public GenerationStatusEnum State { get; set; }
        public GenerationTypeEnum Type { get; set; }
        public string ProjectName { get; set; }
        public System.Guid CreatorGUID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Comments { get; set; }
        public string History { get; set; }

        #endregion
    }

}
