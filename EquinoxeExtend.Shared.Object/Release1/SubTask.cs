using EquinoxeExtend.Shared.Enum;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Object.Release
{
    public static class SubTaskAssembler
    {
        #region Public METHODS

        public static SubTask Convert(this T_E_SubTask iEntity)
        {
            if (iEntity == null) return null;

            return new SubTask
            {              
                Comments = iEntity.Comments,
                Designation = iEntity.Designation,
                DevelopperGUID = iEntity.DevelopperGUID,
                Duration = iEntity.Duration,
                MainTaskId = iEntity.MainTaskId,
                Progression = iEntity.Progression,
                ProjectGUID = iEntity.ProjectGUID,
                SubTaskId = iEntity.SubTaskId,
                Start = iEntity.Start,
            };
        }

        public static void Merge(this T_E_SubTask iEntity, SubTask iObj)
        {
            iEntity.Comments = iObj.Comments;
            iEntity.Designation = iObj.Designation;
            iEntity.DevelopperGUID = iObj.DevelopperGUID;
            iEntity.Duration = iObj.Duration;
            iEntity.MainTaskId = iObj.MainTaskId;
            iEntity.Progression = iObj.Progression;
            iEntity.ProjectGUID = iObj.ProjectGUID;
            iEntity.SubTaskId = iObj.SubTaskId;
            iEntity.Start = iObj.Start;
        }

        #endregion
    }

    public class SubTask
    {
        #region Public PROPERTIES

        public long SubTaskId { get; set; }
        public long MainTaskId { get; set; }
        public System.Guid? ProjectGUID { get; set; }
        public string Designation { get; set; }
        public int Progression { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> Start { get; set; }
        public Nullable<System.Guid> DevelopperGUID { get; set; }
        public string Comments { get; set; }

        public string ProjectTaskIdString
        {
            get
            {
                return TypeDocumentEnum.ProjectTask.GetName("FR") + SubTaskId.ToString("0000000");
            }
        }

        #endregion
    }
}