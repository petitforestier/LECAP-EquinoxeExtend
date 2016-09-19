using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DriveWorks.Helper.Object
{
    public class ControlState
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Value { get; set; }

        [XmlAttribute]
        public string Value2 { get; set; }

        public string Message { get; set; }
    }
}
