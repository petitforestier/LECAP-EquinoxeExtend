using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Product;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Object.Release
{
    public static class MainTaskAssembler
    {
        #region Public METHODS

        public static MainTask Convert(this T_E_MainTask iEntity)
        {
            if (iEntity == null) return null;

            return new MainTask
            {
                Comments = iEntity.Comments,
                CompletedDate = iEntity.CompletedDate,
                CreationDate = iEntity.CreationDate,
                CreationUserGUID = iEntity.CreationUserGUID,
                Description = iEntity.Description,
                Status = (MainTaskStatusEnum)iEntity.StatusRef,
                MainTaskId = iEntity.MainTaskId,
                Name = iEntity.Name,
                ObjectifCloseDate = iEntity.ObjectifCloseDate,
                OpenedDate = iEntity.OpenedDate,
                PackageId = iEntity.PackageId,
                Priority = iEntity.Priority,
                ExternalProjectId = iEntity.ExternalProjectId,
                RequestUserGUID = iEntity.RequestUserGUID,
                StartDate = iEntity.StartDate,
                TaskType = (MainTaskTypeEnum)iEntity.TaskTypeRef,        
            };
        }

        public static void Merge(this T_E_MainTask iEntity, MainTask iObj)
        {
            iEntity.Comments = iObj.Comments;
            iEntity.CompletedDate = iObj.CompletedDate;
            iEntity.CreationDate = iObj.CreationDate;
            iEntity.CreationUserGUID = iObj.CreationUserGUID;
            iEntity.Description = iObj.Description;
            iEntity.StatusRef = (short)iObj.Status;
            iEntity.MainTaskId = iObj.MainTaskId;
            iEntity.Name = iObj.Name;
            iEntity.ObjectifCloseDate = iObj.ObjectifCloseDate;
            iEntity.OpenedDate = iObj.OpenedDate;
            iEntity.PackageId = iObj.PackageId;
            iEntity.Priority = iObj.Priority;
            iEntity.ExternalProjectId = iObj.ExternalProjectId;
            iEntity.RequestUserGUID = iObj.RequestUserGUID;
            iEntity.TaskTypeRef = (short)iObj.TaskType;            
        }

        #endregion
    }

    public class MainTask
    {
        #region Public PROPERTIES

        public long MainTaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public MainTaskStatusEnum Status { get; set; }
        public MainTaskTypeEnum TaskType { get; set; }
        public long? ExternalProjectId { get; set; }
        public Guid CreationUserGUID { get; set; }
        public Guid RequestUserGUID { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<long> PackageId { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? ObjectifCloseDate { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime? OpenedDate { get; set; }
        public System.DateTime? CompletedDate { get; set; }

        public List<SubTask> SubTasks { get; set; }
        public Package Package { get; set; }
        public List<ProductLine> ProductLines { get; set; }
        public ExternalProject ExternalProject { get; set; }

        public string MainTaskIdString
        {
            get
            {
                return TypeDocumentEnum.MainTask.GetName("FR") + MainTaskId.ToString("0000000");
            }
        }

        #endregion
    }
}