using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Pool.Front;
using EquinoxeExtend.Shared.Enum;

namespace Service.Front.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                using (var service = new Pool.Front.PoolService(EquinoxeExtend.Shared.Enum.EnvironmentEnum.Sandbox.GetExtendConnectionString()))
                {
                    var test = service.GetPoolCursor("testik");
                    var tesst2 = test.PadLeft(7,'0');
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
