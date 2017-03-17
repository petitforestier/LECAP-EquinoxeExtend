using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquinoxeExtendPlugin.Controls.Planning
{
    public partial class ucPlanning : UserControl
    {
        public ucPlanning()
        {
            InitializeComponent();


            _mManager = new ProjectManager();
            var work = new MyTask(_mManager) { Name = "Prepare for Work" };
            var wake = new MyTask(_mManager) { Name = "Wake Up" };
            var teeth = new MyTask(_mManager) { Name = "Brush Teeth" };
            var shower = new MyTask(_mManager) { Name = "Shower" };
            var clothes = new MyTask(_mManager) { Name = "Change into New Clothes" };
            var hair = new MyTask(_mManager) { Name = "Blow My Hair" };
            var pack = new MyTask(_mManager) { Name = "Pack the Suitcase" };

            _mManager.Add(work);
            _mManager.Add(wake);
            _mManager.Add(teeth);
            _mManager.Add(shower);
            _mManager.Add(clothes);
            _mManager.Add(hair);
            _mManager.Add(pack);

            // Create another 1000 tasks for stress testing
            Random rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var task = new MyTask(_mManager) { Name = string.Format("New Task {0}", i.ToString()) };
                _mManager.Add(task);
                _mManager.SetStart(task, rand.Next(300));
                _mManager.SetDuration(task, rand.Next(50));
            }

            // Set task durations, e.g. using ProjectManager methods 
            _mManager.SetDuration(wake, 3);
            _mManager.SetDuration(teeth, 5);
            _mManager.SetDuration(shower, 7);
            _mManager.SetDuration(clothes, 4);
            _mManager.SetDuration(hair, 3);
            _mManager.SetDuration(pack, 5);

            // demostrate splitting a task
            _mManager.Split(pack, new MyTask(_mManager), new MyTask(_mManager), 2);

            // Set task complete status, e.g. using newly created properties
            wake.Complete = 0.9f;
            teeth.Complete = 0.5f;
            shower.Complete = 0.4f;

            // Give the Tasks some organisation, setting group and precedents
            _mManager.Group(work, wake);
            _mManager.Group(work, teeth);
            _mManager.Group(work, shower);
            _mManager.Group(work, clothes);
            _mManager.Group(work, hair);
            _mManager.Group(work, pack);
            _mManager.Relate(wake, teeth);
            _mManager.Relate(wake, shower);
            _mManager.Relate(shower, clothes);
            _mManager.Relate(shower, hair);
            _mManager.Relate(hair, pack);
            _mManager.Relate(clothes, pack);
        }
    }
}
