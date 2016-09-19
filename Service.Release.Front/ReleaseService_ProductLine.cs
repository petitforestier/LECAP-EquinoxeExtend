using EquinoxeExtend.Shared.Object.Product;
using Library.Tools.Extensions;
using Service.DBProduct.Data;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Release.Front
{
    public partial class ReleaseService
    {
        #region Public METHODS

        public List<ProductLine> GetProductLineByMainTaskId(long iMainTaskId)
        {
            var result = new List<ProductLine>();
            var productLineTaskEntityList = DBReleaseDataService.GetList<T_E_ProductLineTask>(x => x.MainTaskId == iMainTaskId).Enum().ToList();

            foreach (var item in productLineTaskEntityList.Enum())
                result.Add(DBProductDataService.GetProductLineById(item.ProductLineId).Convert());

            return result;
        }

        public List<ProductLine> GetProductLineList()
        {
            return DBProductDataService.GetList<T_E_ProductLine>().Enum().Select(x => x.Convert()).Enum().ToList();
        }

        #endregion
    }
}