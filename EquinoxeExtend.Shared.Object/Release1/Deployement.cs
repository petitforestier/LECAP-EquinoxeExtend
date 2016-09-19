using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquinoxeExtend.Shared.Enum;

namespace EquinoxeExtend.Shared.Object.Release
{
    public static class DeployementAssembler
    {
        #region Public METHODS

        public static Deployement Convert(this T_E_Deployement iEntity)
        {
            if (iEntity == null) return null;

            return new Deployement
            {
                DeployementDate = iEntity.DeployementDate,
                DeployementId = iEntity.DeployementId,
                EnvironmentDestination = (EnvironmentEnum)iEntity.EnvironmentDestinationRef,
                PackageId = iEntity.PackageId,
            };
        }

        public static void Merge(this T_E_Deployement iEntity, Deployement iObj)
        {
            iEntity.DeployementDate = iObj.DeployementDate;
            iEntity.DeployementId = iObj.DeployementId;
            iEntity.EnvironmentDestinationRef = (short)iObj.EnvironmentDestination;
            iEntity.PackageId = iObj.PackageId;
        }

        #endregion
    }

    public class Deployement
    {
        #region Public PROPERTIES

        public long DeployementId { get; set; }
        public long PackageId { get; set; }
        public System.DateTime DeployementDate { get; set; }
        public EnvironmentEnum EnvironmentDestination { get; set; }

        #endregion
    }
}