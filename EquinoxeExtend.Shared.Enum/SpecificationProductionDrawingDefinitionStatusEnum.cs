using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum SpecificationProductionDrawingDefinitionStatusEnum
    {
        [Name("FR", "")]
        None = 1,
        [Name("FR", "Attente génération modèles")]
        WaitingModelBuilding = 20,
        [Name("FR","Modèles générés")]
        ModelBuilt = 30,
        [Name("FR", "Etude en cours")]
        Studying = 40,
        [Name("FR", "Attente génération documents finaux")]
        WaitingFinalBuilding = 50,
        [Name("FR", "Terminé")]
        Completed = 60,
    };
}
