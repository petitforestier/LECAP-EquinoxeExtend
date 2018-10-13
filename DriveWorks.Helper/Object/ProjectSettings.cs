using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveWorks.Helper.Object
{
    public class ProjectSettings
    {
        #region Public PROPERTIES

        public Decimal ProjectVersion { get; set; }
        public string TagIgnore { get; set; }
        public string ErrorColorName { get; set; }
        public string NoErrorColorName { get; set; }
        public string UserDebugControlName { get; set; }
        public string ErrorConstantName { get; set; }        
        public string ProjectName { get; set; }
        public string EPDMVaultName { get; set; }
        public string EPDMMasterVersionPrefixe { get; set; }

        #endregion
    }
}