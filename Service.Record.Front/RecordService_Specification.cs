using EquinoxeExtend.Shared.Object.Record;
using Library.Tools.Extensions;
using Service.DBRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Record.Front
{
    /// <summary>
    /// Spécification
    /// </summary>
    public partial class RecordService
    {
        #region Public METHODS

        public long NewSpecification(EquinoxeExtend.Shared.Object.Record.Specification iNewSpecification)
        {
            if (iNewSpecification.Name.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            if (iNewSpecification.ProjectVersion < 1)
                throw new Exception("La version de projet est invalide");

            if (DBRecordDataService.Any<T_E_Specification>(x => x.Name == iNewSpecification.Name))
                throw new Exception("La spécification de ce nom existe déjà");

            //Création de l'enregistrement
            var newEntity = new T_E_Specification();
            newEntity.Merge(iNewSpecification);

            return DBRecordDataService.AddSpecification(newEntity);
        }

        public void UpdateSpecification(EquinoxeExtend.Shared.Object.Record.Specification iSpecification)
        {
            if (iSpecification.SpecificationId < 1)
                throw new Exception("L'ID de la spéfication est invalide");

            if (iSpecification.DossierId < 1)
                throw new Exception("L'ID du dossier est invalide");

            if (iSpecification.Name.IsNullOrEmpty())
                throw new Exception("Le nom de la spéfication est invalide");

            if (iSpecification.ProjectVersion < 1)
                throw new Exception("La version de projet est invalide");

            if (DBRecordDataService.Any<T_E_Specification>(x => x.SpecificationId == iSpecification.SpecificationId) == false)
                throw new Exception("La specification est inexistante");

            var theEntity = new T_E_Specification();
            theEntity.Merge(iSpecification);
            DBRecordDataService.UpdateSpecification(theEntity);
        }

        public void DeleteSpecification(long iSpecificationId)
        {
            if (iSpecificationId <1)
                throw new Exception("L'ID de la spécification est invalide");

            var theEntity = DBRecordDataService.GetSingle<T_E_Specification>(x => x.SpecificationId == iSpecificationId);

            //Suppression base de données
            DBRecordDataService.DeleteSpecification(iSpecificationId);
        }

        public EquinoxeExtend.Shared.Object.Record.Specification GetSpecificationByName(string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            var specificationEntity = DBRecordDataService.GetSingleOrDefault<T_E_Specification>(x => x.Name == iSpecificationName);

            if (specificationEntity != null)
                return specificationEntity.Convert();
            else
                return null;
        }

        public List<EquinoxeExtend.Shared.Object.Record.Specification> GetSpecificationsByDossierId(long iDossierId)
        {
            return DBRecordDataService.GetList<T_E_Specification>(x => x.DossierId == iDossierId).Enum().Select(x => x.Convert()).Enum().OrderBy(x=>x.CreationDate).ToList();
        }

        #endregion
    }
}