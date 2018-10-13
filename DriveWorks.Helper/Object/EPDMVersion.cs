using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace DriveWorks.Helper.Object
{
    public class EPDMVersion
    {
        [Name("FR", "CodeDocument")]
        public string CodeDocument { get; set; }

        [Name("FR", "Version3D")]
        public int Version3D { get; set; }

        [Name("FR", "Version2D")]
        public int Version2D { get; set; }
    }
}
