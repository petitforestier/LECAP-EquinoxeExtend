using EquinoxeExtend.Shared.Enum;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtend.Shared.Object.Release
{
    public static class ExternalProjectAssembler
    {
        #region Public METHODS

        public static ExternalProject Convert(this T_E_ExternalProject iEntity)
        {
            if (iEntity == null) return null;

            return new ExternalProject
            {
                DateObjectiveEnd = iEntity.DateObjectiveEnd,
                Description = iEntity.Description,
                ExternalProjectId = iEntity.ExternalProjectId,
                IsProcessed = iEntity.IsProcessed,
                Pilote = iEntity.Pilote,
                Priority = iEntity.Priority,
                ProjectName = iEntity.ProjectName,
                ProjectNumber = iEntity.ProjectNumber,
                Status = (ExternalProjectStatusEnum)iEntity.StatusRef,
                Type = (ExternalProjectTypeEnum)iEntity.TypeRef,
                BEImpacted = iEntity.BEImpacted
            };
        }

        public static void Merge(this T_E_ExternalProject iEntity, ExternalProject iObj)
        {
            iEntity.DateObjectiveEnd = iObj.DateObjectiveEnd;
            iEntity.Description = iObj.Description;
            iEntity.ExternalProjectId = iObj.ExternalProjectId;
            iEntity.IsProcessed = iObj.IsProcessed;
            iEntity.Pilote = iObj.Pilote;
            iEntity.Priority = iObj.Priority;
            iEntity.ProjectName = iObj.ProjectName;
            iEntity.ProjectNumber = iObj.ProjectNumber;
            iEntity.StatusRef = (short)iObj.Status;
            iEntity.TypeRef = (short)iObj.Type;
            iEntity.BEImpacted = iObj.BEImpacted;
        }

        #endregion
    }

    public class ExternalProject
    {
        #region Public PROPERTIES

        public long ExternalProjectId { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Pilote { get; set; }
        public Nullable<System.DateTime> DateObjectiveEnd { get; set; }
        public Nullable<int> Priority { get; set; }
        public ExternalProjectStatusEnum Status { get; set; }
        public ExternalProjectTypeEnum Type { get; set; }
        public bool IsProcessed { get; set; }
        public bool BEImpacted { get; set; }

        #endregion
    }
}