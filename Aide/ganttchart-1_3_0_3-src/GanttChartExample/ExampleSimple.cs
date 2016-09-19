using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Braincase.GanttChart
{
    public partial class ExampleSimple : Form
    {
        ProjectManager _mProject;

        public ExampleSimple()
        {
            InitializeComponent();
            _mProject = new ProjectManager();         
            _mProject.TimeScale = TimeScale.Day;

            var theTask = new Task() { Name = "New Task" };
            _mProject.Add(theTask);
            _mProject.SetComplete(theTask, 0.5f);
            _mProject.SetDuration(theTask, 10);
            _mChart.Init(_mProject);
        }
    }
}
