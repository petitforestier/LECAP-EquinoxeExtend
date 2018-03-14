using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveWorks.SolidWorks;
using DriveWorks.Components;
using DriveWorks.Reporting;
using DriveWorks.SolidWorks.Generation;
using DriveWorks.SolidWorks.Generation.Proxies;
using DriveWorks.Components.Tasks;
using DriveWorks.Hosting;
using DriveWorks.Specification;
using EquinoxeExtend.Shared.Enum;
using DriveWorks.Helper.Manager;
using Service.Record.Front;
using Library.Tools.Extensions;
using Library.Tools.Attributes;

namespace EquinoxeExtendPlugin
{
    [GenerationTask("Report generation progress", "Report generation progress", "embedded://MyGenerationTask2.Puzzle-16x16.png", "SpecificationManagement", GenerationTaskScope.All)]
    public class ReportGenerationProgress : DriveWorks.SolidWorks.GenerationTask
    {

        private const string GENERATIONID = "GenerationId";
        private const string STATEID = "StateId";

        public override ComponentTaskParameterInfo[] Parameters
        {
            // Déclaration des arguments
            get
            {
                return new ComponentTaskParameterInfo[]{
                    new ComponentTaskParameterInfo(GENERATIONID, "Id Génération", "Id de la génération à modifier"),
                    new ComponentTaskParameterInfo(STATEID, "Etat de la génération", "10 attente - 20 en cours - 30 Terminée")
                };
            }
        }


        public override void Execute(SldWorksModelProxy model, ReleasedComponent component, GenerationSettings generationSettings)
        {

            var activeEnvironment = EnvironmentManager.GetEnvironment(generationSettings.Group);
            var activeGroupName = generationSettings.Group.Name;

            using (var recordService = new RecordService(activeEnvironment.GetSQLExtendConnectionString()))
            {
                try
                {
                    int generationId = 0;
                    int stateId = 0;

                    if (this.Data.TryGetParameterValueAsInteger(GENERATIONID, ref generationId))
                        this.Report.WriteEntry( ReportingLevel.Minimal,ReportEntryType.Information,"Report generation progress", "Report generation progress", "Generation id est invalide", null);

                    if (this.Data.TryGetParameterValueAsInteger(STATEID, ref stateId))
                        this.Report.WriteEntry(ReportingLevel.Minimal, ReportEntryType.Information, "Report generation progress", "Report generation progress", "State id est invalide", null);

                    //récupération de la génération
                    var theGeneration = recordService.GetGenerationById(generationId);
                    if (theGeneration == null)
                        throw new Exception("La génération est introuvable");

                    theGeneration.History = "Modification le '{0}', par '{1}', vers l'état {2}".FormatString(DateTime.Now.ToStringDMYHMS(), generationSettings.Group.CurrentUser.DisplayName, ((GenerationStatusEnum)stateId).GetName("FR"));
                    theGeneration.State = (GenerationStatusEnum)stateId;

                    recordService.UpdageGeneration(theGeneration);
                }
                catch (Exception ex)
                {
                    this.Report.WriteEntry(ReportingLevel.Minimal, ReportEntryType.Information, "Report generation progress", "Report generation progress", "Erreur : "+ ex.Message, null);
                }
            }
        }

    }
}
