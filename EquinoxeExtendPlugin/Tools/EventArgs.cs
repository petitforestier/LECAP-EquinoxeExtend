using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtendPlugin
{
   
    public class EditionEventArgs : System.EventArgs
    {
        public enum StatusEnum
        {
            New,
            Update,
            Cancel,
        }

        public EditionEventArgs(long iId, StatusEnum iStatus)
        {
            this.Id = iId;
            this.Status = iStatus;
        }

        public long Id { get; private set; }
        public StatusEnum Status { get; private set; }
    }
}
