using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Log;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Service.DBLog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Log.Front
{
    public partial class LogService
    {
        #region Public METHODS

        public long AddLog(EquinoxeExtend.Shared.Object.Log.Log iLog)
        {
            if (iLog == null) throw new NullReferenceException();

            var logEntity = new T_E_Log();
            logEntity.Merge(iLog);

            return DBLogDataService.AddLog(logEntity);
        }

        public List<EquinoxeExtend.Shared.Object.Log.Log> GetLogs(TypeLogEnum? iType, int iTake, int iSkip)
        {
            if (iTake < 1)
                throw new Exception("Le nombre à sélectionner doit supérieur ou égal à 1");

            if (iSkip < 0)
                throw new Exception("Le nombre à retirer doit supérieur à 0");

            var entities = DBLogDataService.GetQuery<T_E_Log>(null).OrderByDescending(x => x.Date).AsQueryable();

            if (iType != null)
                entities = entities.Where(x => x.Type == (int)iType);

            return entities.Take(iTake).Skip(iSkip).ToList().Enum().Select(x => x.Convert()).Enum().ToList();
        }

        #endregion
    }
}