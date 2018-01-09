using Library.Entity;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBLock.Data
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
                    _RepositoryDBContext = new LockDBContext(_ConnectionString);
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

        public long AddLock(T_E_Lock iLock)
        {
            LogValidation(iLock);

            if (iLock.LockId != -1) throw new ArgumentException("L'id du lock est différent de -1");
            return Add(iLock).LockId;
        }

        public void DeleteLock(long iLock)
        {
            if (iLock < 1) throw new ArgumentException("L'id du lock est invalide");
            var newEntity = new T_E_Lock();
            newEntity.LockId = iLock;
            Delete(newEntity);
        }

        public T_E_Lock GetLockById(long iLock)
        {
            if (iLock < 1) throw new ArgumentException("L'id du lock est invalide");
            return GetSingleOrDefault<T_E_Lock>(x => x.LockId == iLock);
        }

        #endregion

        #region Private METHODS

        private void LogValidation(T_E_Lock iLock)
        {
            if (iLock == null)
                throw new Exception("Le lock est null");

            if (iLock.LockDate == null)
                throw new Exception("Le lock date est requis");

            if (iLock.SessionGUID.IsNullOrEmpty())
                throw new Exception("La session est requise");

            if (iLock.UserId == null)
                throw new Exception("Le user id est invalide");
        }

        #endregion
    }
}