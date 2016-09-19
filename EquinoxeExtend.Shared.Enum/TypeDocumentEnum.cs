using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum TypeDocumentEnum
    {
        //Si modification, modifier la liste des documents dans PDM, afin d'avoir une liste exhaustive quelque part

        [Name("FR", "PCK")]
        Package=10,
        [Name("FR", "ETA")]
        MainTask=20,
        [Name("FR", "EST")]
        ProjectTask = 30,

       
    };
}
