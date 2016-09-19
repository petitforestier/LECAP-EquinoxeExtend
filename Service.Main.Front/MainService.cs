using DriveWorks;
using EquinoxeExtend.Shared.Object.Settings;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Main.Front
{
    public partial class MainService
    {
        #region Public CONSTRUCTORS

        public MainService(Project iProject)
        {
            if (iProject == null)
                throw new Exception("Le projet ne peut pas être null");
            _Project = iProject;
            _Group = iProject.Group;
        }

        #endregion
    }

    public partial class MainService : IDisposable
    {
        #region Public METHODS

        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private FIELDS

        private Service.DWProject.Data.DataService _DWProjectDataService;
        private Service.DWGroup.Data.DataService _DWGroupDataService;
        private Project _Project;
        private Group _Group;
        private bool disposed = false;

        #endregion

        #region Private PROPERTIES

        private Service.DWProject.Data.DataService DWProjectDataService
        {
            get
            {
                if (_DWProjectDataService == null)
                    _DWProjectDataService = new Service.DWProject.Data.DataService(_Project);
                return _DWProjectDataService;
            }
        }

        private Service.DWGroup.Data.DataService DWGroupDataService
        {
            get
            {
                if (_DWGroupDataService == null)
                    _DWGroupDataService = new Service.DWGroup.Data.DataService(_Group);
                return _DWGroupDataService;
            }
        }

        #endregion
    }

    public partial class MainService
    {
        #region Public METHODS

        public List<ProjectDetails> GetProjectList()
        {
            return DWGroupDataService.GetProjectList();
        }

        public List<string> GetUserNameList()
        {
            return DWGroupDataService.GetUserList().Enum().ToList().Select(x => x.DisplayName).Enum().ToList();
        }

        public List<DriveWorks.Security.UserDetails> GetUserList()
        {
            return DWGroupDataService.GetUserList();
        }

        public string GetUserNameFromUserId(string iUserId)
        {
            return DWGroupDataService.GetUserNameFromUserId(iUserId);
        }

        public Dictionary<string, string> GetProjectAliasDic()
        {
            return DWGroupDataService.GetProjectAliasDic();
        }

        #endregion
    }

    /// <summary>
    /// Settings
    /// </summary>
    public partial class MainService
    {
        #region Public METHODS

        public GroupSettings GetGroupSettings()
        {
            return DWGroupDataService.GetGroupSettings();
        }

        public ProjectSettings GetProjectSettings()
        {
            return DWProjectDataService.GetProjectSettings();
        }

        #endregion
    }