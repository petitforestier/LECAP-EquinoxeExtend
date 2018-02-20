using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtendPlugin.Object
{
    public class Dossier : EquinoxeExtend.Shared.Object.Record.Dossier
    {
        public List<KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, DriveWorks.Specification.SpecificationDetails>> SpecificationPairs { get; set; } 

    }

    public static class DossierAssembler
    {
        public static EquinoxeExtendPlugin.Object.Dossier ConvertFull(this EquinoxeExtend.Shared.Object.Record.Dossier iDossier)
        {
            if (iDossier == null) return null;

            return new EquinoxeExtendPlugin.Object.Dossier
            {
                IsTemplate = iDossier.IsTemplate,
                ProjectName = iDossier.ProjectName,
                Specifications = iDossier.Specifications,
                TemplateDescription = iDossier.TemplateDescription,
                TemplateName = iDossier.TemplateName,
                Name = iDossier.Name,
                DossierId = iDossier.DossierId,
                Lock = iDossier.Lock,
                IsCreateVersionOnGeneration = iDossier.IsCreateVersionOnGeneration,
                State = iDossier.State,
            };
        }
    }
}
