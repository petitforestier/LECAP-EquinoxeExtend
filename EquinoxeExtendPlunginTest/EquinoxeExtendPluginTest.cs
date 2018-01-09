using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquinoxeExtendPlunginTest
{
    [TestClass]
    public class EquinoxeExtendPluginTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //SEULEMENT TESTER LES UDF QUI N'ACCEDENT PAS A LA BASE DE DONNEES
            try
            {
                var plugin = new EquinoxeExtendPlugin.UDF();
                var itemValue = "toto";
                var itemString = itemValue + EquinoxeExtendPlugin.Consts.Consts.IDSeparator + "51020";
                var result = plugin.UDFGetStringWithoutID(itemString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
