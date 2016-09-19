using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.DBRelease.Data
{
    public partial class ReleaseDBContext : DbContext
    {
        public ReleaseDBContext(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
