using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.DBPool.Data
{
    public partial class PoolDBContext : DbContext
    {
        public PoolDBContext(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
