using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.DBLock.Data
{
    public partial class LockDBContext : DbContext
    {
        public LockDBContext(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
