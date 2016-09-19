using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Pool.Front
{
    public partial class PoolService
    {
        #region Public CONSTRUCTORS

        public PoolService(string iConnectionString)
        {
            if (iConnectionString.IsNullOrEmpty())
                throw new Exception("La chaine de connection est invalide");
            _ConnectionString = iConnectionString;
            InitDataService();
        }

        #endregion
    }

    public partial class PoolService : IDisposable
    {
        #region Public METHODS

        public void Dispose()
        {
            if (disposed)
                return;

            if (_DBPoolDataService != null)
                _DBPoolDataService.Dispose();

            disposed = true;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private FIELDS

        private string _ConnectionString;
        private bool disposed = false;

        private Service.DBLog.Data.DataService _DBLogDataService;
        private Service.DBPool.Data.DataService _DBPoolDataService;
        private Service.DBProduct.Data.DataService _DBProductDataService;
        private Service.DBRelease.Data.DataService _DBReleaseDataService;
        private Service.DBSpecification.Data.DataService _DBSpecificationDataService;
        #endregion

        #region Private PROPERTIES

        private Service.DBLog.Data.DataService DBLogDataService
        {
            get
            {
                if (_DBLogDataService == null)
                    _DBLogDataService = new Service.DBLog.Data.DataService(_ConnectionString);
                return _DBLogDataService;
            }
        }

        private Service.DBPool.Data.DataService DBPoolDataService
        {
            get
            {
                if (_DBPoolDataService == null)
                    _DBPoolDataService = new Service.DBPool.Data.DataService(_ConnectionString);
                return _DBPoolDataService;
            }
        }

        private Service.DBProduct.Data.DataService DBProductDataService
        {
            get
            {
                if (_DBProductDataService == null)
                    _DBProductDataService = new Service.DBProduct.Data.DataService(_ConnectionString);
                return _DBProductDataService;
            }
        }

        private Service.DBRelease.Data.DataService DBReleaseDataService
        {
            get
            {
                if (_DBReleaseDataService == null)
                    _DBReleaseDataService = new Service.DBRelease.Data.DataService(_ConnectionString);
                return _DBReleaseDataService;
            }
        }

        private Service.DBSpecification.Data.DataService DBSpecificationDataService
        {
            get
            {
                if (_DBSpecificationDataService == null)
                    _DBSpecificationDataService = new Service.DBSpecification.Data.DataService(_ConnectionString);
                return _DBSpecificationDataService;
            }
        }

        private void InitDataService()
        {
            var init1 = DBReleaseDataService;
            var init2 = DBProductDataService;
            var init3 = DBPoolDataService;
            var init4 = DBLogDataService;
            var init5 = DBSpecificationDataService;
        }



        #endregion
    }

    public partial class PoolService
    {
        #region Public METHODS

        public string GetPoolCursorWithPrefix(string iPoolName, double iPrefix, double iLenght)
        {
            return DBPoolDataService.GetPoolCursor(iPoolName, Convert.ToInt32(iLenght), Convert.ToInt32(iPrefix));
        }

        public string GetPoolCursor(string iPoolName)
        {
            return DBPoolDataService.GetPoolCursor(iPoolName);
        }

        #endregion
    }
}