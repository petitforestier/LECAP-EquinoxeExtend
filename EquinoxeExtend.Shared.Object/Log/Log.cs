using EquinoxeExtend.Shared.Enum;
using Service.DBLog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtend.Shared.Object.Log
{
    public static class LogAssembler
    {
        #region Public METHODS

        public static Log Convert(this T_E_Log iEntity)
        {
            if (iEntity == null) return null;

            return new Log
            {
                Date = iEntity.Date,
                LogId = iEntity.LogId,
                Message = iEntity.Message,
                Title = iEntity.Title,
                Type = (TypeLogEnum)iEntity.Type,
            };
        }

        public static void Merge(this T_E_Log iEntity, Log iObj)
        {
            iEntity.Date = iObj.Date;
            iEntity.LogId = iObj.LogId;
            iEntity.Message = iObj.Message;
            iEntity.Title = iObj.Title;
            iEntity.Type = (int)iObj.Type;
        }

        #endregion
    }

    public class Log
    {
        #region Public PROPERTIES

        public long LogId { get; set; }
        public TypeLogEnum Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public System.DateTime Date { get; set; }

        #endregion
    }
}