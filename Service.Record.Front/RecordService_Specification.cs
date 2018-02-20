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

            if (iNewSpecification.CreatorGUID == null)
                throw new Exception("Le nom du createur est invalide");

            if (iNewSpecification.CreationDate == null)
                throw new Exception("La date de création est invalide");

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

        public EquinoxeExtend.Shared.Object.Record.Specification GetSpecificationByName(string iSpecificationName, bool iIsFull)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            var specificationEntity = DBRecordDataService.GetSingleOrDefault<T_E_Specification>(x => x.Name == iSpecificationName);

            if (specificationEntity != null)
            {
                var specification = specificationEntity.Convert();
                if(iIsFull)
                    specification.Generations = GetGenerationBySpecificationId(specification.SpecificationId);
                return specification;
            }               
            else
                return null;
        }

        public List<EquinoxeExtend.Shared.Object.Record.Specification> GetSpecificationsByDossierId(long iDossierId, bool iIsFull)
        {
            var result = new List<EquinoxeExtend.Shared.Object.Record.Specification>();
            var entities = DBRecordDataService.GetList<T_E_Specification>(x => x.DossierId == iDossierId);
            foreach(var entity in entities.Enum())
                result.Add(GetSpecificationByName(entity.Name, iIsFull));

            return result.Enum().OrderBy(x=>x.CreationDate).ToList();
        }

        #endregion
    }
}