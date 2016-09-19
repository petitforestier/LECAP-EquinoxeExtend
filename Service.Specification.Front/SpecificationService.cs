using EquinoxeExtend.Shared.Object.Specification;
using Library.Tools.Extensions;
using Service.DBSpecification.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification.Front
{
    public partial class SpecificationService
    {
        #region Public CONSTRUCTORS

        public SpecificationService(string iConnectionString)
        {
            if (iConnectionString.IsNullOrEmpty())
                throw new Exception("La chaine de connection est invalide");
            _ConnectionString = iConnectionString;
            InitDataService();
        }

        #endregion
    }

    public partial class SpecificationService : IDisposable
    {
        #region Public METHODS

        public void Dispose()
        {
            if (disposed)
                return;

            if (_DBSpecificationDataService != null)
                _DBSpecificationDataService.Dispose();

            // Free any unmanaged objects here.

            disposed = true;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private FIELDS

        private string _ConnectionString;
        private bool disposed = false;
        private Service.DBLog.Data.DataService _DBLogDataService;
        private Service.DBPool.Data.DataService _DBPoolDataService;
        private Service.DBProduct.Data.DataService _DBProductDataService;
        private Service.DBRelease.Data.DataService _DBReleaseDataService;
        private Service.DBSpecification.Data.DataService _DBSpecificationDataService;

        #endregion

        #region Private PROPERTIES

        private Service.DBLog.Data.DataService DBLogDataService
        {
            get
            {
                if (_DBLogDataService == null)
                    _DBLogDataService = new Service.DBLog.Data.DataService(_ConnectionString);
                return _DBLogDataService;
            }
        }

        private Service.DBPool.Data.DataService DBPoolDataService
        {
            get
            {
                if (_DBPoolDataService == null)
                    _DBPoolDataService = new Service.DBPool.Data.DataService(_ConnectionString);
                return _DBPoolDataService;
            }
        }

        private Service.DBProduct.Data.DataService DBProductDataService
        {
            get
            {
                if (_DBProductDataService == null)
                    _DBProductDataService = new Service.DBProduct.Data.DataService(_ConnectionString);
                return _DBProductDataService;
            }
        }

        private Service.DBRelease.Data.DataService DBReleaseDataService
        {
            get
            {
                if (_DBReleaseDataService == null)
                    _DBReleaseDataService = new Service.DBRelease.Data.DataService(_ConnectionString);
                return _DBReleaseDataService;
            }
        }

        private Service.DBSpecification.Data.DataService DBSpecificationDataService
        {
            get
            {
                if (_DBSpecificationDataService == null)
                    _DBSpecificationDataService = new Service.DBSpecification.Data.DataService(_ConnectionString);
                return _DBSpecificationDataService;
            }
        }

        #endregion

        #region Private METHODS

        private void InitDataService()
        {
            var init1 = DBReleaseDataService;
            var init2 = DBProductDataService;
            var init3 = DBPoolDataService;
            var init4 = DBLogDataService;
            var init5 = DBSpecificationDataService;
        }

        #endregion
    }

    /// <summary>
    /// Spécification
    /// </summary>
    public partial class SpecificationService
    {
        #region Public METHODS

        public void NewSpecification(EquinoxeExtend.Shared.Object.Specification.Specification iNewSpecification)
        {
            if (iNewSpecification.SpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            if (iNewSpecification.ProjectVersion < 1)
                throw new Exception("La version de projet est invalide");

            if (DBSpecificationDataService.Any<T_E_Specification>(x => x.SpecificationName == iNewSpecification.SpecificationName))
                throw new Exception("La spécification existe déjà");

            //Création de l'enregistrement
            var newEntity = new T_E_Specification();
            newEntity.Merge(iNewSpecification);

            DBSpecificationDataService.Add(newEntity);
        }

        public void UpdateSpecification(EquinoxeExtend.Shared.Object.Specification.Specification iSpecification)
        {
            if (iSpecification.SpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spéfication est invalide");

            if (iSpecification.ProjectVersion < 1)
                throw new Exception("La version de projet est invalide");

            if (DBSpecificationDataService.Any<T_E_Specification>(x => x.SpecificationName == iSpecification.SpecificationName) == false)
                throw new Exception("La specification est inexistante");

            var theEntity = new T_E_Specification();
            theEntity.Merge(iSpecification);
            DBSpecificationDataService.Update(theEntity);
        }

        public void DeleteSpecification(string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            var theEntity = DBSpecificationDataService.GetSingle<T_E_Specification>(x => x.SpecificationName == iSpecificationName);

            //Suppression base de données
            DBSpecificationDataService.Delete(theEntity);
        }

        public void UpdateDisplayName(string iSpecificationName, string iNewDisplayName)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spéfication est invalide");

            if (iNewDisplayName.IsNullOrEmpty())
                throw new Exception("Le nouveau nom d'affichage est invalide");

            var theEntity = DBSpecificationDataService.GetSingle<T_E_Specification>(x => x.SpecificationName == iSpecificationName);

            if (theEntity.IsTemplate == false)
                throw new Exception("Seul un modèle peut être renommé");

            //Vérification dublons
            if (DBSpecificationDataService.Any<T_E_Specification>(x => x.DisplayName == iNewDisplayName))
                throw new Exception("La spécification '{0}' est déjà existante".FormatString(iNewDisplayName));

            theEntity.DisplayName = iNewDisplayName;
            DBSpecificationDataService.Update(theEntity);
        }

        public void UpdateDescription(string iSpecificationName, string iNewDescription)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spéfication est invalide");

            var theEntity = DBSpecificationDataService.GetSingle<T_E_Specification>(x => x.SpecificationName == iSpecificationName);

            if (theEntity.IsTemplate == false)
                throw new Exception("Seul un modèle peut modifier sa description ");

            theEntity.Description = iNewDescription;
            DBSpecificationDataService.Update(theEntity);
        }

        public EquinoxeExtend.Shared.Object.Specification.Specification GetSpecificationByName(string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est invalide");

            return DBSpecificationDataService.GetSingle<T_E_Specification>(x => x.SpecificationName == iSpecificationName).Convert();
        }

        public List<EquinoxeExtend.Shared.Object.Specification.Specification> GetSpecifications(bool iIsTemplate)
        {
            return DBSpecificationDataService.GetList<T_E_Specification>(x => x.IsTemplate == iIsTemplate).Enum().Select(x => x.Convert()).Enum().ToList();
        }

        #endregion
    }
}