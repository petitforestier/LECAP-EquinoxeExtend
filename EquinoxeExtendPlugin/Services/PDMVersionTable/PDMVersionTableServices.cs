using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPDM.Helper;

namespace EquinoxeExtendPlugin.Services.PDMVersionTable
{
    public class PDMVersionTableServices
    {

        private EPDMAPIService EPDMAPIService;

        public PDMVersionTableServices (string iVaultName)
        {
            EPDMAPIService = new EPDMAPIService("iVaultName", 0, Library.Tools.Enums.DebugModeEnum.Minimal);
        }

        //public List<string> GetReference (string fileName)
        //{
           
        //    EPDMAPIService.

        //}


    }
}
