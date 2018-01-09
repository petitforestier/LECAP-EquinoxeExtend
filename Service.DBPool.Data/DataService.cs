using Library.Entity;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBPool.Data
{
    public partial class DataService : Repository
    {
        #region Public PROPERTIES

        public override DbContext RepositoryDBContext
        {
            get
            {
                return DBContext;
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public DataService(string iExtendDataBaseConnectionString)
             : base(true, iExtendDataBaseConnectionString)
        {
        }

        #endregion

        #region Public METHODS

        public string GetPoolCursor(string iPoolName)
        {
            if (iPoolName.IsNullOrEmpty())
                throw new Exception("Le pool name n'est pas valide");

            var entity = DBContext.T_E_Pool.SingleOrDefault(x => x.PoolId == iPoolName);

            //Création du compteur si n'existe pas.
            if (entity == null)
            {
                entity = new T_E_Pool();
                entity.PoolId = iPoolName;
                entity.Cursor = 1;
                DBContext.T_E_Pool.Add(entity);
                DBContext.SaveChanges();
                return 1.ToString();
            }
            else
            {
                try
                {
                    //Incrémentation atomic du compteur
                    return DBContext.P_D_StepPool(iPoolName).Single().Value.ToString();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
               
            }
        }

        #endregion

        #region Private FIELDS

        private PoolDBContext _PoolDBContext;

        #endregion

        #region Private PROPERTIES

        private PoolDBContext DBContext
        {
            get
            {
                if (_PoolDBContext == null)
                    _PoolDBContext = new PoolDBContext(_ConnectionString);
                return _PoolDBContext;
            }
        }

        #endregion
    }
}