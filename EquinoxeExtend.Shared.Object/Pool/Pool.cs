using Service.DBPool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtend.Shared.Object.Pool
{
    public static class PoolAssembler
    {
        #region Public METHODS

        public static Pool Convert(this T_E_Pool iEntity)
        {
            if (iEntity == null) return null;

            return new Pool
            {
                Cursor = iEntity.Cursor,
                PoolId = iEntity.PoolId,
            };
        }

        public static void Merge(this T_E_Pool iEntity, Pool iObj)
        {
            iEntity.Cursor = iObj.Cursor;
            iEntity.PoolId = iObj.PoolId;
        }

        #endregion
    }

    public class Pool
    {
        #region Public PROPERTIES

        public string PoolId { get; set; }
        public long Cursor { get; set; }

        #endregion
    }
}