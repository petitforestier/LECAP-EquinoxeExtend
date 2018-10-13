using System;
using DriveWorks;
using DriveWorks.Applications;
using Library.Control.UserControls;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin.Controls.ReleaseManagement
{
    public partial class ucRelease : UserControl, IUcUserControl
    {
        #region Public EVENTS

        public event EventHandler Close;

        #endregion

        #region Public CONSTRUCTORS

        public ucRelease()
        {
            InitializeComponent();
        }

        #endregion

        #region Public METHODS

        public void Initialize(IApplication iApplication, Group iGroup)
        {
            _Group = iGroup;

            this.ucTaskManager.Initialize(iGroup);

            this.ucPackageManagement.Initialize(iApplication, iGroup);
        }

        #endregion

        #region Private FIELDS

        private Group _Group;

        #endregion

        #region Private METHODS

        private void CloseFake()
        {
            Close(null, null);
        }

        #endregion
    }
}