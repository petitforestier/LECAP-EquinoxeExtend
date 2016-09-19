using Library.Entity;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBSpecification.Data
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
                    _RepositoryDBContext = new SpecificationDBContext(_ConnectionString);
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

    public partial class DataService
    {
        #region Public METHODS

        public string AddSpecification(T_E_Specification iSpecification)
        {
            SpecificationValidation(iSpecification);
            return Add(iSpecification).SpecificationName;
        }

        public void UpdateSpecification(T_E_Specification iSpecification)
        {
            SpecificationValidation(iSpecification);
            Update(iSpecification);
        }

        public void DeleteSpecificatio(string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty()) throw new ArgumentException("Le nom de la spécification est invalide");
            var newEntity = new T_E_Specification();
            newEntity.SpecificationName = iSpecificationName;
            Delete(newEntity);
        }

        public T_E_Specification GetSpecification(string iSpecificationName)
        {
            if (iSpecificationName.IsNullOrEmpty()) throw new ArgumentException("Le nom de la spécification est invalide");
            return GetSingleOrDefault<T_E_Specification>(x => x.SpecificationName == iSpecificationName);
        }

        public void SpecificationValidation(T_E_Specification iSpecification)
        {
            if (iSpecification == null)
                throw new Exception("La spécification est nulle");

            if (iSpecification.Constants.IsNullOrEmpty())
                throw new Exception("Les constantes de la spécification sont vides");

            if (iSpecification.Controls.IsNullOrEmpty())
                throw new Exception("Les controls de la spécification sont vides");

            if (iSpecification.DisplayName.IsNullOrEmpty() && iSpecification.IsTemplate)
                throw new Exception("Le nom d'affichage de spécification est requis pour les modèles");

            if (iSpecification.ProjectVersion <= 0)
                throw new Exception("La version de projet est invalide");

            if (iSpecification.SpecificationName.IsNullOrEmpty())
                throw new Exception("Le nom de la spécification est requis");
        }

        #endregion
    }
}