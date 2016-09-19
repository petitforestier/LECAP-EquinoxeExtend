using Service.DBProduct.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtend.Shared.Object.Product
{
    public static class ProductLineAssembler
    {
        #region Public METHODS

        public static ProductLine Convert(this T_E_ProductLine iEntity)
        {
            if (iEntity == null) return null;

            return new ProductLine
            {
                Name = iEntity.Name,
                ProductLineId = iEntity.ProductLineId,
            };
        }

        public static void Merge(this T_E_ProductLine iEntity, ProductLine iObj)
        {
            iEntity.Name = iObj.Name;
            iEntity.ProductLineId = iObj.ProductLineId;
        }

        #endregion
    }

    public class ProductLine
    {
        #region Public PROPERTIES

        public long ProductLineId { get; set; }
        public string Name { get; set; }

        #endregion
    }
}