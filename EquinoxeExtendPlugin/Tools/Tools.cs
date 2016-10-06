using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveWorks;
using DriveWorks.Applications;
using DriveWorks.Helper;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Extensions;

namespace EquinoxeExtendPlugin.Tools
{
    public static class Tools
    {
        public static void ReleaseProjectsRights(Group iGroup)
        {
            using (var releaseService = new Service.Release.Front.ReleaseService(iGroup.GetEnvironment().GetExtendConnectionString()))
            {
                var devSubTasks = releaseService.GetDevSubTasks();

                var devProjects = devSubTasks.Enum().Where(x => x.ProjectGUID != null).Enum().Select(x => (Guid)x.ProjectGUID).Enum().ToList();

                //Applique les droits seulement sur ces projets ouverts
                iGroup.SetExclusitivelyPermissionToTeam(iGroup.Security.GetTeams().Single(x => x.DisplayName == EnvironmentEnum.Developpement.GetDevelopperTeam()), devProjects);
            }
        }

    }
}
