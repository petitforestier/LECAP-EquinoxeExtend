using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.DBSpecification.Data
{
    public partial class SpecificationDBContext : DbContext
    {
        public SpecificationDBContext(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
