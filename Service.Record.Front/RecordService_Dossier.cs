using EquinoxeExtend.Shared.Object.Record;
using Library.Tools.Extensions;
using Service.DBRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EquinoxeExtend.Shared.Enum;

namespace Service.Record.Front
{
    public partial class RecordService
    {
        #region Public METHODS

        public void NewDossier(EquinoxeExtend.Shared.Object.Record.Dossier iNewDossier)
        {
            if (iNewDossier.DossierId != -1)
                throw new Exception("L'Id de la spécification est invalide");

            if (iNewDossier.Name.IsNullOrEmpty())
                throw new Exception("Le nom du dossier est invalide");

            if (iNewDossier.ProjectName.IsNullOrEmpty())
                throw new Exception("Le nom du projet est invalide");

            if (DBRecordDataService.Any<T_E_Dossier>(x => x.Name == iNewDossier.Name))
                throw new Exception("Le dossier existe déjà");

            if (iNewDossier.Specifications.Enum().Count() != 1)
                throw new Exception("Le nombre de spécification doit être égal à 1");

            if(iNewDossier.IsTemplate)
            {
                if (iNewDossier.TemplateName.IsNullOrEmpty())
                    throw new Exception("Le nom du template n'est pas valide");
                if (DBRecordDataService.Any<T_E_Dossier>(x => x.TemplateName == iNewDossier.TemplateName))
                    throw new Exception("Le nom du template est déjà existant");
            }

            using (var ts = new TransactionScope())
            {               
                var newSpecification = iNewDossier.Specifications.Single();

                //Création du Dossier
                var newEntity = new T_E_Dossier();
                newEntity.Merge(iNewDossier);

                newSpecification.DossierId = DBRecordDataService.AddDossier(newEntity);

                //Création de la Spécification
                NewSpecification(newSpecification);

                ts.Complete();
            }
        }

        public void UpdateDossier(EquinoxeExtend.Shared.Object.Record.Dossier iDossier)
        {
            if (iDossier.Name.IsNullOrEmpty())
                throw new Exception("Le nom du dossier est invalide");

            if (DBRecordDataService.Any<T_E_Dossier>(x => x.Name == iDossier.Name) == false)
                throw new Exception("Le dossier est inexistant");

            var theEntity = new T_E_Dossier();
            theEntity.Merge(iDossier);
            DBRecordDataService.Update(theEntity);
        }

        public void UpdateTemplateName(string iDossierName, string iNewTemplateName)
        {
            if (iDossierName.IsNullOrEmpty())
                throw new Exception("Le nom du dossier est invalide");

            if (iNewTemplateName.IsNullOrEmpty())
                throw new Exception("Le nouveau nom d'affichage est invalide");

            var theEntity = DBRecordDataService.GetSingle<T_E_Dossier>(x => x.Name == iDossierName);

            if (theEntity.IsTemplate == false)
                throw new Exception("Seul un modèle peut être renommé");

            //Vérification doublons
            if (DBRecordDataService.Any<T_E_Dossier>(x => x.TemplateName == iNewTemplateName))
                throw new Exception("Le dossier modèle'{0}' est déjà existant".FormatString(iNewTemplateName));

            theEntity.TemplateName = iNewTemplateName;
            DBRecordDataService.Update(theEntity);
        }

        public void UpdateDescription(string iDossierName, string iNewDescription)
        {
            if (iDossierName.IsNullOrEmpty())
                throw new Exception("Le nom du dossier est invalide");

            var theEntity = DBRecordDataService.GetSingle<T_E_Dossier>(x => x.Name == iDossierName);

            if (theEntity.IsTemplate == false)
                throw new Exception("Seul un modèle peut modifier sa description ");

            theEntity.TemplateDescription = iNewDescription;
            DBRecordDataService.Update(theEntity);
        }

        public void DeleteDossier(string iDossierName)
        {
            if (iDossierName.IsNullOrEmpty())
                throw new Exception("Le nom du dossier est invalide");

            var theDossier = GetDossierByName(iDossierName);

            using (var ts = new TransactionScope())
            {
                //Suppression spécifications
                foreach(var specificationItem in theDossier.Specifications.Enum())
                    DBRecordDataService.DeleteSpecification(specificationItem.SpecificationId);

                //Suppression dossier
                DBRecordDataService.DeleteDossier(theDossier.DossierId);

                ts.Complete();
            }
        }

        public EquinoxeExtend.Shared.Object.Record.Dossier GetDossierById(long iDossierId)
        {
            if (iDossierId < 1)
                throw new Exception("L'id du dossier est invalide");

            var theDossierEntity = DBRecordDataService.GetSingleOrDefault<T_E_Dossier>(x => x.DossierId == iDossierId);

            if (theDossierEntity != null)
            {
                var theDossier = theDossierEntity.Convert();
                theDossier.Specifications = GetSpecificationsByDossierId(theDossier.DossierId,true);
                theDossier.Lock = GetLockByDossierId(theDossier.DossierId);
                return theDossier;
            }
            else
                return null;
        }

        public EquinoxeExtend.Shared.Object.Record.Dossier GetDossierByName(string iDossierName)
        {
            if (iDossierName.IsNullOrEmpty())
                throw new Exception("Le nom du dossier est invalide");

            var theDossierEntity = DBRecordDataService.GetSingleOrDefault<T_E_Dossier>(x => x.Name == iDossierName);

            if (theDossierEntity != null)
            {
                var theDossier = theDossierEntity.Convert();
                theDossier.Specifications = GetSpecificationsByDossierId(theDossier.DossierId,true);
                theDossier.Lock = GetLockByDossierId(theDossier.DossierId);
                return theDossier;
            }
            else
                return null;
        }

        public EquinoxeExtend.Shared.Object.Record.Dossier GetDossierByTemplateName(string iTemplateName, bool iIsFull)
        {
            if (iTemplateName.IsNullOrEmpty())
                throw new Exception("Le nom du modèle est invalide");

            var theDossierEntity = DBRecordDataService.GetSingleOrDefault<T_E_Dossier>(x => x.TemplateName == iTemplateName);
            if (theDossierEntity != null)
            {
                var theDossier = theDossierEntity.Convert();
                if(iIsFull)
                {
                    theDossier.Specifications = GetSpecificationsByDossierId(theDossier.DossierId, iIsFull);
                    theDossier.Lock = GetLockByDossierId(theDossier.DossierId);
                }
                    
                return theDossier;
            }
            else
                return null;
        }

        public List<EquinoxeExtend.Shared.Object.Record.Dossier> GetDossiers(bool iIsNotTemplate)
        {
            var theDossiers = DBRecordDataService.GetList<T_E_Dossier>(x => x.IsTemplate != iIsNotTemplate).Enum().Select(x => x.Convert()).Enum().ToList();

            var result = new List<Dossier>();
            foreach (var DossierItem in theDossiers.Enum())
            {
                DossierItem.Specifications = GetSpecificationsByDossierId(DossierItem.DossierId,true);
                DossierItem.Lock = GetLockByDossierId(DossierItem.DossierId);
                result.Add(DossierItem);
            }
            return theDossiers;
        }

        public List<EquinoxeExtend.Shared.Object.Record.Dossier> GetDossiers(bool iIsNotTemplate, string iDossierName, Guid? iCreatorModificator, DossierStatusEnum? iDossierStatusEnum)
        {
            var theQuery = DBRecordDataService.GetQuery<T_E_Dossier>(null);

            //Template
            if (iIsNotTemplate)
                theQuery = theQuery.Where(x => x.IsTemplate != iIsNotTemplate);

            //DossierName
            if(iDossierName.IsNotNullAndNotEmpty())
                theQuery = theQuery.Where(x => x.Name == iDossierName);

            //Createur/Modificateur
            if (iCreatorModificator != null)
                theQuery = theQuery.Where(x => x.T_E_Specification.Any(y => y.CreatorGUID == iCreatorModificator));

            //Status
            if(iDossierStatusEnum !=null )
                theQuery = theQuery.Where(x => x.StateRef == (short)iDossierStatusEnum);

            var result = theQuery.ToList().Enum().Select(x=>x.Convert()).ToList();
          
            foreach (var DossierItem in result.Enum())
            {
                DossierItem.Specifications = GetSpecificationsByDossierId(DossierItem.DossierId, true);
                DossierItem.Lock = GetLockByDossierId(DossierItem.DossierId);
            }
            return result;
        }

        #endregion
    }
}