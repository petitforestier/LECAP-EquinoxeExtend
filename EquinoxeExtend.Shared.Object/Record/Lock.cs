using EquinoxeExtend.Shared.Enum;
using Service.DBRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtend.Shared.Object.Record
{
    public static class LockAssembler
    {
        #region Public METHODS

        public static Lock Convert(this T_E_Lock iEntity)
        {
            if (iEntity == null) return null;

            return new Lock
            {
                DossierId = iEntity.DossierId,
                LockDate = iEntity.LockDate,
                LockId = iEntity.LockId,
                SessionGUID = iEntity.SessionGUID,
                UserId = iEntity.UserId,
            };
        }

        public static void Merge(this T_E_Lock iEntity, Lock iObj)
        {
            iEntity.DossierId = iObj.DossierId;
            iEntity.LockDate = iObj.LockDate;
            iEntity.LockId = iObj.LockId;
            iEntity.SessionGUID = iObj.SessionGUID;
            iEntity.UserId = iObj.UserId;
        }

        #endregion
    }

    public class Lock
    {
        #region Public PROPERTIES

        public long LockId { get; set; }
        public long DossierId { get; set; }
        public string SessionGUID { get; set; }
        public System.DateTime LockDate { get; set; }
        public System.Guid UserId { get; set; }

        #endregion
    }
}