using DriveWorks.Applications;
using DriveWorks.Security;
using EquinoxeExtend.Shared.Enum;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using DriveWorks.Hosting;

namespace DriveWorks.Helper.Manager
{
    public static class EnvironmentManager
    {
        #region Public METHODS

        public static Group OpenGroup(this GroupManager iGroupManager, EnvironmentEnum iEnvironmentToOpen)
        {
            var loginTuple = iEnvironmentToOpen.GetLoginPassword();
            iGroupManager.OpenGroup(iEnvironmentToOpen.GetDWConnectionString(), DriveWorksCredentials.Create(loginTuple.Item1, loginTuple.Item2));
            return iGroupManager.Group;
        }

        public static EnvironmentEnum GetEnvironment(this Group iGroup)
        {
            return new EnvironmentEnum().ParseEnumFromName(iGroup.Name, "FR");
        }

        #endregion
    }
}