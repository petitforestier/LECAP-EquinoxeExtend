using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.DBProduct.Data
{
    public partial class ProductDBContext : DbContext
    {
        public ProductDBContext(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
