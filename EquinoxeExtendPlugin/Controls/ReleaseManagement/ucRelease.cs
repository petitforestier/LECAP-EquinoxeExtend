using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Control.UserControls;
using DriveWorks;
using DriveWorks.Applications;

namespace EquinoxeExtendPlugin.Controls.ReleaseManagement
{
    public partial class ucRelease : UserControl, IUcUserControl
    {
        public ucRelease()
        {
            InitializeComponent();
        }

        private Group _Group;

        public event EventHandler Close;

        public void Initialize(IApplication iApplication, Group iGroup)
        {
            _Group = iGroup;
            using (var timer1 = new Library.Tools.Debug.MyTimer(true, "tache"))
            {
                this.ucTaskManager.Initialize(iGroup);
            }
            using (var timer1 = new Library.Tools.Debug.MyTimer(true, "package"))
            {
                this.ucPackageManagement.Initialize(iApplication, iGroup);
            }
        }

        private void CloseFake()
        {
            Close(null, null);
        }
    }
}
