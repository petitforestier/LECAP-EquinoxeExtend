using Library.Entity;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBRecord.Data
{
    public partial class DataService : Repository
    {
        #region Public PROPERTIES

        public override DbContext RepositoryDBContext
        {
            get
            {
                if (_RepositoryDBContext == null)
                {
                    if (_ConnectionString.IsNullOrEmpty())
                        throw new Exception("La chaine de connection ne peut pas être nulle");
                    _RepositoryDBContext = new RecordDBContext(_ConnectionString);
                }

                return _RepositoryDBContext;
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public DataService(string iExtendDataBaseConnectionString)
             : base(true, iExtendDataBaseConnectionString)
        {
        }

        #endregion

        #region Private FIELDS

        private DbContext _RepositoryDBContext;

        #endregion
    }


    /// <summary>
    /// Dossier
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddDossier(T_E_Dossier iDossier)
        {
            if (iDossier.DossierId != -1)
                throw new Exception("L'id du dossier n'est pas valide");

            DossierValidation(iDossier);
            return Add(iDossier).DossierId;
        }

        public void UpdateDossier(T_E_Dossier iDossier)
        {
            if (iDossier.DossierId < 1)
                throw new Exception("L'id du dossier n'est pas valide");

            DossierValidation(iDossier);
            Update(iDossier);
        }

        public void DeleteDossier(long iDossierId)
        {
            if (iDossierId < 1)
                throw new Exception("L'id du dossier n'est pas valide");

            var newEntity = new T_E_Dossier();
            newEntity.DossierId = iDossierId;
            Delete(newEntity);
        }

        public T_E_Dossier GetDossier(string iDossierName)
        {
            if (iDossierName.IsNullOrEmpty()) throw new ArgumentException("Le nom du dossier est invalide");
            return GetSingleOrDefault<T_E_Dossier>(x => x.Name == iDossierName);
        }

        public T_E_Dossier GetDossier(long iDossierId)
        {
            return GetSingleOrDefault<T_E_Dossier>(x => x.DossierId == iDossierId);
        }

        public void DossierValidation(T_E_Dossier iDossier)
        {
            if (iDossier == null)
                throw new Exception("Le dossier est nul");

            if (iDossier.TemplateName.IsNullOrEmpty() && iDossier.IsTemplate)
                throw new Exception("Le nom du modèle est requis pour un modèle");
        }

        #endregion
    }

    /// <summary>
    /// Specification
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddSpecification(T_E_Specification iSpecification)
        {
            if (iSpecification.SpecificationId != -1)
                throw new Exception("L'id de la spécification n'est pas valide");

            SpecificationValidation(iSpecification);
            return Add(iSpecification).SpecificationId;
        }

        public void UpdateSpecification(T_E_Specification iSpecification)
        {
            if (iSpecification.SpecificationId < 1)
                throw new Exception("L'id de la spécification n'est pas valide");

            SpecificationValidation(iSpecification);
            Update(iSpecification);
        }

        public void DeleteSpecification(long iSpecificationId)
        {
            if (iSpecificationId < 1)
                throw new Exception("L'id de la spécification n'est pas valide");

            var newEntity = new T_E_Specification();
            newEntity.SpecificationId = iSpecificationId;
            Delete(newEntity);
        }

        public T_E_Specification GetSpecification(string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty()) throw new ArgumentException("Le nom de la spécification est invalide");
            return GetSingleOrDefault<T_E_Specification>(x => x.Name == iSpecificationName);
        }

        public T_E_Specification GetSpecification(long iSpecificationId)
        {
            return GetSingleOrDefault<T_E_Specification>(x => x.SpecificationId == iSpecificationId);
        }

        public void SpecificationValidation(T_E_Specification iSpecification)
        {
            if (iSpecification == null)
                throw new Exception("La spécification est nulle");

            if (iSpecification.DossierId < 1)
                throw new Exception("La spécification doit être lié à un dossier");

            if (iSpecification.Constants.IsNullOrEmpty())
                throw new Exception("Les constantes de la spécification sont vides");

            if (iSpecification.Controls.IsNullOrEmpty())
                throw new Exception("Les controls de la spécification sont vides");

            if (iSpecification.ProjectVersion <= 0)
                throw new Exception("La version de projet est invalide");

            if (iSpecification.Name.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est requis");
        }

        #endregion
    }
}