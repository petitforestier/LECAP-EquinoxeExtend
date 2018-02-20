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
    /// Generation
    /// </summary>
    public partial class RecordService
    {
        #region Public METHODS

        public long NewGeneration(EquinoxeExtend.Shared.Object.Record.Generation iNewGeneration)
        {
            if (iNewGeneration.GenerationId!= -1 )
                throw new Exception("L'id de la génération est invalide");

            if (iNewGeneration.CreatorGUID == null)
                throw new Exception("L'id du createur n'est pas valide");

            if (iNewGeneration.ProjectName.IsNullOrEmpty())
                throw new Exception("Le nom du projet est invalide");

            if (iNewGeneration.SpecificationId<1)
                throw new Exception("L'id de la specification est invalide");

            //Création de l'enregistrement
            var newEntity = new T_E_Generation();
            newEntity.Merge(iNewGeneration);

            return DBRecordDataService.Add<T_E_Generation>(newEntity).GenerationId;
        }

        public void UpdageGeneration(EquinoxeExtend.Shared.Object.Record.Generation iNewGeneration)
        {
            if (iNewGeneration.GenerationId < 1)
                throw new Exception("L'id de la génération est invalide");

            if (iNewGeneration.CreatorGUID == null)
                throw new Exception("L'id du createur n'est pas valide");

            if (iNewGeneration.ProjectName.IsNullOrEmpty())
                throw new Exception("Le nom du projet est invalide");

            if (iNewGeneration.SpecificationId < 1)
                throw new Exception("L'id de la specification est invalide");

            //Modification de l'enregistrement
            var theEntity = new T_E_Generation();
            theEntity.Merge(iNewGeneration);
            DBRecordDataService.Update(theEntity);
        }

        public void DeleteGeneration(long iGenerationId)
        {
            if (iGenerationId < 1)
                throw new Exception("L'ID de la genenration est invalide");

            var theEntity = DBRecordDataService.GetSingle<T_E_Generation>(x => x.GenerationId == iGenerationId);

            //Suppression base de données
            DBRecordDataService.Delete<T_E_Generation>(theEntity);
        }

        public EquinoxeExtend.Shared.Object.Record.Generation GetGenerationById(long iGenerationId)
        {
            var entity = DBRecordDataService.GetSingleOrDefault<T_E_Generation>(x => x.GenerationId == iGenerationId);
            if (entity != null)
                return entity.Convert();
            else
                return null;
        }

        public List<EquinoxeExtend.Shared.Object.Record.Generation> GetGenerationBySpecificationId(long iSpecificationId)
        {
            var entities = DBRecordDataService.GetList<T_E_Generation>(x => x.SpecificationId == iSpecificationId);
            if (entities != null)
                return entities.Select(x=>x.Convert()).ToList();
            else
                return null;
        }

        #endregion
    }
}