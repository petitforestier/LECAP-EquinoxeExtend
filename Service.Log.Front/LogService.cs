using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Log;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Service.DBLog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Log.Front
{
    public partial class LogService
    {
        #region Public CONSTRUCTORS

        public LogService(string iConnectionString)
        {
            if (iConnectionString.IsNullOrEmpty())
                throw new Exception("La chaine de connection est invalide");
            _ConnectionString = iConnectionString;
            InitDataService();
        }

        #endregion
    }

    public partial class LogService : IDisposable
    {
        #region Public METHODS

        public void Dispose()
        {
            if (disposed)
                return;

            if (_DBLogDataService != null)
                _DBLogDataService.Dispose();

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
        private Service.DBRecord.Data.DataService _DBRecordDataService;
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

        private Service.DBRecord.Data.DataService DBRecordDataService
        {
            get
            {
                if (_DBRecordDataService == null)
                    _DBRecordDataService = new Service.DBRecord.Data.DataService(_ConnectionString);
                return _DBRecordDataService;
            }
        }

        private void InitDataService()
        {
            var init1 = DBReleaseDataService;
            var init2 = DBProductDataService;
            var init3 = DBPoolDataService;
            var init4 = DBLogDataService;
            var init5 = DBRecordDataService;
        }

        #endregion
    }
}