using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquinoxeExtendPlugin;
using Library.Tools.Debug;

namespace EquinoxeExtendPluginTest
{
    [TestClass]
    public class UDFTEST
    {
        [TestMethod]
        public void UDFSplitTest()
        {
            try
            {
                var udf = new EquinoxeExtendPlugin.UDF();
                var text = "toto+edzedz+zedzed+zedze";
                var test = udf.UDFSplit(text, "+");
                MyDebug.PrintInformation(test);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void UDFGetOccurenceTest()
        {
            try
            {
                var udf = new EquinoxeExtendPlugin.UDF();
                var text = "toto+edzedz+zedzed+zedze";
                if("edzedz" != udf.UDFGetOccurence(text, " + ",2))
                    throw new Exception("2ième occurence mauvaise");

                if ("zedzed" != udf.UDFGetOccurence(text, " + ", -2))
                    throw new Exception("-2ième occurence mauvaise");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
