using Library.Tools.Extensions;
using Library.Tools.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriveWorks.Helper.Manager;
using EquinoxeExtend.Shared.Enum;

namespace EquinoxeExtendPlugin.Controls.Package
{
    public partial class ucPackageEdit : UserControl
    {
        #region Public EVENTS

        public event EventHandler Close = delegate { };

        #endregion

        #region Public PROPERTIES

        public DialogResult DialogResult { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        public ucPackageEdit()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void Initialize(DriveWorks.Group iGroup)
        {
            _Group = iGroup;
        }

        public void EditPackage(EquinoxeExtend.Shared.Object.Release.Package iPackage)
        {
            if (_IsLoading.Value) return;
            using (var locker = new BoolLocker(ref _IsLoading))
            {
                _Package = iPackage;
                FillControl();
                cmdOk.Focus();
            }
        }

        #endregion

        #region Protected METHODS

        protected void FillControl()
        {
            dtpDeployementDateObjectif.Checked = false;

            if (_Package != null)
            {
                if (_Package.DeployementDateObjectif != null)
                {
                    dtpDeployementDateObjectif.Checked = true;
                    dtpDeployementDateObjectif.Value = (DateTime)_Package.DeployementDateObjectif;
                }
            }
        }

        protected EquinoxeExtend.Shared.Object.Release.Package FillFromControl()
        {
            if (dtpDeployementDateObjectif.Checked)
                _Package.DeployementDateObjectif = dtpDeployementDateObjectif.Value;
            else
                _Package.DeployementDateObjectif = null;

            return _Package;
        }

        #endregion

        #region Private FIELDS

        private BoolLock _IsLoading = new BoolLock();

        private EquinoxeExtend.Shared.Object.Release.Package _Package;

        private DriveWorks.Group _Group;

        #endregion

        #region Private METHODS

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    _Package = FillFromControl();
                    using (var releaseService = new Service.Release.Front.ReleaseService(_Group.GetEnvironment().GetSQLExtendConnectionString()))
                    {
                        releaseService.UpdatePackage(_Package);
                    }
                }
                Close(null, null);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_IsLoading.Value) return;
                using (var locker = new BoolLocker(ref _IsLoading))
                {
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                Close(null, e);
            }
            catch (Exception ex)
            {
                ex.ShowInMessageBox();
            }
        }

        #endregion
    }
}