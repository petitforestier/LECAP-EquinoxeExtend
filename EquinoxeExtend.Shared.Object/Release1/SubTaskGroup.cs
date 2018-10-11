using EquinoxeExtend.Shared.Enum;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;
using Library.Tools.Extensions;

namespace EquinoxeExtend.Shared.Object.Release
{
  
    public class SubTaskGroup : SubTask
    {
        #region Public PROPERTIES

        public List<SubTask> SubTasks { get; set; }

        public int DurationSum
        {
            get
            {
                return SubTasks.IsNotNullAndNotEmpty() ? (int)SubTasks.Enum().Sum(x => x.Duration) : 0;
            }
        }

        public int DoneDuration
        {
            get
            {
                return SubTasks.IsNotNullAndNotEmpty() ? (int)(Math.Truncate(SubTasks.Enum().Sum(x => decimal.Multiply(Convert.ToDecimal(x.Duration), decimal.Divide(x.Progression, 100))))) : 0;
            }
        }
        #endregion
    }
}