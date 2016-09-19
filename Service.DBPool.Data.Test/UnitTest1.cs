using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.DBPool.Data;
using System.Data.EntityClient;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Service.DBPool.Data.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var entityConnectionStringBuilder = new EntityConnectionStringBuilder();
                entityConnectionStringBuilder.Provider = "System.Data.SqlClient";
                entityConnectionStringBuilder.ProviderConnectionString = connectionString;
                entityConnectionStringBuilder.Metadata = "res://*";

                var service = new DataService(entityConnectionStringBuilder.ToString());
                service.GetPoolCursor("test");  
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private string connectionString = "Server=S2SQL01;Database=Equinoxe_Sandbox_Extend;User Id=sa;password=SQLequinoxe";

    }
}
