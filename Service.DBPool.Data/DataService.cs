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

        public string GetPoolCursor(string iPoolName, int? iLenght = null, int? iPrefix = null)
        {
            if (iPoolName.IsNullOrEmpty())
                throw new Exception("Le pool name n'est pas valide");

            if (iLenght == null && iPrefix != null)
                throw new Exception("La longueur du compteur est requis quand un préfixe est demandé");

            if (iLenght != null && iPrefix == null)
                throw new Exception("Le prefixe est requis si une longueur est demandée");

            if (iLenght <= iPrefix.ToString().Length)
                throw new Exception("La longeur du préfixe ne pas être supérieur ou égal à la longeur demandé");

            if (iLenght != null)
                if (iLenght < 1)
                    throw new Exception("La longueur ne peut pas être inférieur à 1");

            //Vérification du préfixe, si pas correcte alors réinitilisation
            if (iPrefix != null)
            {
                var entity = DBContext.T_E_Pool.Single(x => x.PoolId == iPoolName);
                if (!entity.Cursor.ToString().StartsWith(iPrefix.ToString()))
                {
                    var cursorString = iPrefix.ToString();

                    for (int a = 1; a <= iLenght - iPrefix.ToString().Length - 1; a++)
                        cursorString += 0.ToString();

                    cursorString += "1";
                    entity.Cursor = Convert.ToInt64(cursorString);
                    DBContext.SaveChanges();
                }
            }

            //Incrémentation atomic du compteur
            return DBContext.P_D_StepPool(iPoolName).Single().Value.ToString();
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