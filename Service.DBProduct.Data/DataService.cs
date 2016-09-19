using Library.Entity;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBProduct.Data
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
                    _RepositoryDBContext = new ProductDBContext(_ConnectionString);
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
        #region Private METHODS

        public long AddProductLine(T_E_ProductLine iProductLine)
        {
            ProductLineValidation(iProductLine);

            if (iProductLine.ProductLineId != -1) throw new ArgumentException("L'id de la gamme est différent de -1");
            return Add(iProductLine).ProductLineId;
        }

        public void UpdateProductLine(T_E_ProductLine iProductLine)
        {
            ProductLineValidation(iProductLine);

            if (iProductLine.ProductLineId < 1) throw new ArgumentException("L'id de la gamme est inferieure à 1");
            Update(iProductLine);
        }

        public void DeleteProductLine(long iProductLineId)
        {
            if (iProductLineId < 1) throw new ArgumentException("L'id de la gamme est invalide");
            var newEntity = new T_E_ProductLine();
            newEntity.ProductLineId = iProductLineId;
            Delete(newEntity);
        }

        public T_E_ProductLine GetProductLineById(long iProductLineId)
        {
            if (iProductLineId < 1) throw new ArgumentException("L'id de la gamme est invalide");
            return GetSingleOrDefault<T_E_ProductLine>(x => x.ProductLineId == iProductLineId);
        }

        private void ProductLineValidation(T_E_ProductLine iProductLine)
        {
            if (iProductLine == null)
                throw new Exception("La gamme est nulle");

            if (iProductLine.Name.IsNullOrEmpty())
                throw new Exception("Le nom de la gamme est requis");
        }

        #endregion
    }
}