using Library.Entity;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBLog.Data
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
                    _RepositoryDBContext = new LogDBContext(_ConnectionString);
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

        public long AddLog(T_E_Log iLog)
        {
            LogValidation(iLog);

            if (iLog.LogId != -1) throw new ArgumentException("L'id du log est différent de -1");
            return Add(iLog).LogId;
        }

        public void UpdateLog(T_E_Log iLog)
        {
            LogValidation(iLog);

            if (iLog.LogId < 1) throw new ArgumentException("L'id du log est inferieur à 1");
            Update(iLog);
        }

        public void DeleteLog(long iLogId)
        {
            if (iLogId < 1) throw new ArgumentException("L'id du log est invalide");
            var newEntity = new T_E_Log();
            newEntity.LogId = iLogId;
            Delete(newEntity);
        }

        public T_E_Log GetLogById(long iLog)
        {
            if (iLog < 1) throw new ArgumentException("L'id de la tâche est invalide");
            return GetSingleOrDefault<T_E_Log>(x => x.LogId == iLog);
        }

        private void LogValidation(T_E_Log iLog)
        {
            if (iLog == null)
                throw new Exception("Le log est null");

            if (iLog.Message.IsNullOrEmpty())
                throw new Exception("Le message du log est requis");

            if (iLog.Title.IsNullOrEmpty())
                throw new Exception("Le titre du log est requis");

            if (iLog.Type < 1)
                throw new Exception("Le type du log est invalide");
        }

        #endregion
    }
}