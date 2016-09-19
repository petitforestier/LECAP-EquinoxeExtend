using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.DBLog.Data
{
    public partial class LogDBContext : DbContext
    {
        public LogDBContext(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
