using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Extensions;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Release.Front
{
    public partial class ReleaseService
    {
        #region Public METHODS

        public long AddDeployement(Deployement iDeployement)
        {
            var newEntity = new T_E_Deployement();
            newEntity.Merge(iDeployement);
            return DBReleaseDataService.AddDeployement(newEntity);
        }

        public Deployement GetDeployementById(long iDeployementId)
        {
            var query = DBReleaseDataService.GetQuery<T_E_Deployement>(null).SingleOrDefault(x=>x.DeployementId == iDeployementId);

            if (query != null)
                return query.Convert();
            else
                return null;
        }

        public List<Deployement> GetDeployementList()
        {
            return DBReleaseDataService.GetList<T_E_Deployement>().Enum().Select(x => x.Convert()).Enum().ToList();
        }

        public List<Deployement> GetDeployementByPackageId(long iPackageId)
        {
            if (iPackageId < 1)
                throw new Exception("L'id du package n'est pas valide");

            return DBReleaseDataService.GetQuery<T_E_Deployement>(null).Where(x => x.PackageId == iPackageId).Enum().Select(x => x.Convert()).Enum().OrderBy(x=>x.DeployementDate).Enum().ToList();
        }

        #endregion
    }
}