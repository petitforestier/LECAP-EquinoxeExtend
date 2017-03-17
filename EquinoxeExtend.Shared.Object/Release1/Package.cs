using EquinoxeExtend.Shared.Enum;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;
using Library.Tools.Extensions;

namespace EquinoxeExtend.Shared.Object.Release
{
    public static class PackageAssembler
    {
        #region Public METHODS

        public static Package Convert(this T_E_Package iEntity)
        {
            if (iEntity == null) return null;

            return new Package
            {
                PackageId = iEntity.PackageId,
                ReleaseNumber = iEntity.ReleaseNumber,
                Status = (PackageStatusEnum)iEntity.StatusRef,
                IsLocked = iEntity.IsLocked,
                Priority = iEntity.Priority,
                DeployementDateObjectif = iEntity.DeployementObjectifDate,

            };
        }

        public static void Merge(this T_E_Package iEntity, Package iObj)
        {
            iEntity.PackageId = iObj.PackageId;
            iEntity.ReleaseNumber = iObj.ReleaseNumber;
            iEntity.IsLocked = iObj.IsLocked;
            iEntity.StatusRef = (short)iObj.Status;
            iEntity.Priority = iObj.Priority;
            iEntity.DeployementObjectifDate = iObj.DeployementDateObjectif;
        }

        #endregion
    }

    public class Package
    {
        #region Public PROPERTIES

        public long PackageId { get; set; }
        public int? ReleaseNumber { get; set; }
        public bool IsLocked { get; set; }
        public PackageStatusEnum Status { get; set; }
        public int? Priority { get; set; }
        public DateTime? DeployementDateObjectif { get; set; }

        public List<Deployement> Deployements { get; set; }
        public List<MainTask> MainTasks { get; set; }
        public List<SubTask> SubTasks { get; set; }

        public string PackageIdString
        {
            get
            {
                return TypeDocumentEnum.Package.GetName("FR") + PackageId.ToString("0000000") ;
            }
        }

        public string PackageIdStatusString
        {
            get
            {
                return PackageIdString + " (" + Status.GetName("FR") + ")";
            }
        }

        public int DurationSum
        {
            get
            {
                return MainTasks.Enum().Sum(x => x.DurationSum);
            }
        }

        public int DoneDuration
        {
            get
            {
                return MainTasks.Enum().Sum(x => x.DoneDuration);
            }
        }

        #endregion
    }
}