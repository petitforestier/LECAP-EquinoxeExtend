using Service.DBRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquinoxeExtend.Shared.Enum;

namespace EquinoxeExtend.Shared.Object.Record
{
    public static class DossierAssembler
    {
        #region Public METHODS

        public static Dossier Convert(this T_E_Dossier iEntity)
        {
            if (iEntity == null) return null;

            return new Dossier
            {
                DossierId = iEntity.DossierId,
                Name = iEntity.Name,
                ProjectName = iEntity.ProjectName,
                IsTemplate = iEntity.IsTemplate,
                TemplateName = iEntity.TemplateName,
                TemplateDescription = iEntity.TemplateDescription,
            };
        }

        public static void Merge(this T_E_Dossier iEntity, Dossier iObj)
        {
            iEntity.DossierId = iObj.DossierId;
            iEntity.Name = iObj.Name;
            iEntity.ProjectName = iObj.ProjectName;
            iEntity.IsTemplate = iObj.IsTemplate;
            iEntity.TemplateName = iObj.TemplateName;
            iEntity.TemplateDescription = iObj.TemplateDescription;
        }

        #endregion
    }

    public class Dossier
    {
        #region Public PROPERTIES

        public long DossierId { get; set; }
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public bool IsTemplate { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }

        public List<Specification> Specifications { get; set; }
        public EquinoxeExtend.Shared.Object.Record.Lock Lock { get; set; }

        #endregion
    }

}
