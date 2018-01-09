using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.DBRecord.Data
{
    public partial class RecordDBContext : DbContext
    {
        public RecordDBContext(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
