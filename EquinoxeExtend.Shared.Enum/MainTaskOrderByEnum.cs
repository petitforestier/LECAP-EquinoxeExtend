using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum MainTaskOrderByEnum
    {
        [Name("FR", "Priorité tâche")]
        TaskPriority=10,
        [Name("FR", "Priorité package")]
        PackagePriority = 15,
        [Name("FR", "Date objectif")]
        DateObjectif=20,
        [Name("FR", "N° tâche")]
        MainTaskId=30,
        [Name("FR", "N° Projet")]
        ProjectNumber = 40,
        [Name("FR", "Date déploiement production")]
        ProductionDeployementDate = 50,
        [Name("FR", "Date création")]
        CreationDate = 60,
    };
}
