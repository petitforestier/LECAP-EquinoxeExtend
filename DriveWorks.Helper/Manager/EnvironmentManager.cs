using DriveWorks.Applications;
using DriveWorks.Security;
using EquinoxeExtend.Shared.Enum;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DriveWorks.Helper.Manager
{
    public static class EnvironmentManager
    {
        #region Public METHODS

        public static Group OpenGroup(this IGroupService iGroupService, EnvironmentEnum iEnvironmentToOpen)
        {
            var loginTuple = iEnvironmentToOpen.GetLoginPassword();
            iGroupService.OpenGroup(iEnvironmentToOpen.GetConnectionString(), DriveWorksCredentials.Create(loginTuple.Item1, loginTuple.Item2));
            return iGroupService.ActiveGroup;
        }

        public static EnvironmentEnum GetEnvironment(this Group iGroup)
        {
            return new EnvironmentEnum().ParseEnumFromName(iGroup.Name, "FR");
        }

        #endregion
    }
}