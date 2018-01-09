using DriveWorks.Helper;
using DriveWorks.Helper.Manager;
using DriveWorks.Helper.Object;
using DriveWorks.Specification;
using EquinoxeExtend.Shared.Enum;
using EquinoxeExtend.Shared.Object.Record;
using EquinoxeExtendPlugin.Object;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Service.Log.Front;
using Service.Record.Front;
using Service.Record.Front.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Control.Datagridview;
using Library.Control.UserControls;

namespace EquinoxeExtendPlugin
{
    [Task("SPECMGT:DeleteTemplate", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class DeleteTemplate : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public DeleteTemplate()
        {
            DossierNameProperty = Properties.RegisterStringProperty("Nom dossier modèle", "Nom du dossier modèle à supprimer");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var recordService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    if (DossierNameProperty.Value.IsNullOrEmpty())
                        throw new Exception("Le nom de la spécification est invalide");

                    //Suppression Base de données
                    recordService.DeleteDossier(DossierNameProperty.Value);

                    ctx.Project.AddMessage("Modèle '{0}' a été supprimé avec succès".FormatString(DossierNameProperty.Value));
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la suppression du modèle", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> DossierNameProperty;

        #endregion
    }

    [Task("SPECMGT:RenameTemplate", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class RenameTemplate : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public RenameTemplate()
        {
            DossierNameProperty = Properties.RegisterStringProperty("Nom dossier", "Nom du dossier à renommer");
            NewTemplateNameProperty = Properties.RegisterStringProperty("Nouveau nom modèle", "Nouveau nom du modèle");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var recordService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    var newName = NewTemplateNameProperty.Value;
                    if (newName.IsNullOrEmpty())
                        throw new Exception("Le nouveau nom n'est pas valide");

                    recordService.UpdateTemplateName(DossierNameProperty.Value, newName);

                    ctx.Project.AddMessage("Le dossier modèle '{0}' a été renommé en '{1}' avec succès".FormatString(DossierNameProperty.Value, newName));
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la renommage du modèle", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> DossierNameProperty;
        private FlowProperty<string> NewTemplateNameProperty;

        #endregion
    }

    [Task("SPECMGT:UpdateTemplateDescription", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class UpdateTemplateDescription : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public UpdateTemplateDescription()
        {
            DossierNameProperty = Properties.RegisterStringProperty("Nom dossier", "Nom de la spécification à renommer");
            NewDescriptionProperty = Properties.RegisterStringProperty("Description du modèle", "Description du modèle");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var recordService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    var newDescription = NewDescriptionProperty.Value;
                    recordService.UpdateDescription(DossierNameProperty.Value, newDescription);
                    ctx.Project.AddMessage("La description de la spécification '{0}'a été modifiée avec succès".FormatString(DossierNameProperty.Value, newDescription));
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la modification de la description du modèle", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> DossierNameProperty;
        private FlowProperty<string> NewDescriptionProperty;

        #endregion
    }

    [Task("SPECMGT:NewDossier", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class NewDossier : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public NewDossier()
        {
            DossierNameProperty = Properties.RegisterStringProperty("Nom Dossier", "Nom du Dossier à créer");
            CheckErrorProperty = Properties.RegisterBooleanProperty("Vérifier les erreurs", "True si une vérification doit être effectué, False s'il ne faut pas, par exemple pour sauvegarder un brouillon");
            TemplateDescriptionProperty = Properties.RegisterStringProperty("Description du modèle", "Description du modèle");
            IsTemplateProperty = Properties.RegisterBooleanProperty("Est un Modèle", "Est un Modèle");
            TemplateNameProperty = Properties.RegisterStringProperty("Nom du modèle", "Nom textuel d'affichage pour différentier les modèles autrement que le numéro du Dossier");
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom Spécification", "Nom de la Spécification à Créer");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var dossierService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    //Vérification des erreurs
                    if (CheckErrorProperty.Value)
                        if (ctx.Project.GetErrorMessageList().IsNotNullAndNotEmpty())
                            throw new Exception("La création n'est pas possible car contient des erreurs");

                    //Vérification que tous les controls et constantes sont renseignés dans les tables
                    ctx.Project.CheckControlConstantManagement();

                    //Creation du Dossier
                    var newDossier = new EquinoxeExtend.Shared.Object.Record.Dossier();

                    newDossier.DossierId = -1;
                    newDossier.Name = DossierNameProperty.Value;
                    newDossier.IsTemplate = IsTemplateProperty.Value;
                    newDossier.TemplateDescription = TemplateDescriptionProperty.Value;
                    newDossier.TemplateName = (TemplateNameProperty.Value.IsNullOrEmpty()) ? null : TemplateNameProperty.Value;
                    newDossier.ProjectName = ctx.Project.Name;

                    //Creation de la Specification
                    newDossier.Specifications = new List<EquinoxeExtend.Shared.Object.Record.Specification>();

                    var newSpecification = new EquinoxeExtend.Shared.Object.Record.Specification();

                    newSpecification.SpecificationId = -1;
                    newSpecification.Constants = ctx.Project.GetCurrentConstantList().SerializeList();
                    newSpecification.Controls = ctx.Project.GetCurrentControlStateList().SerializeList();
                    newSpecification.CreationDate = DateTime.Now;
                    newSpecification.Name = newDossier.Name;
                    newSpecification.ProjectVersion = ctx.Project.GetProjectSettings().ProjectVersion;
                    newSpecification.Name = SpecificationNameProperty.Value;

                    newDossier.Specifications.Add(newSpecification);

                    dossierService.NewDossier(newDossier);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la création du Dossier", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> DossierNameProperty;
        private FlowProperty<bool> CheckErrorProperty;
        private FlowProperty<string> TemplateDescriptionProperty;
        private FlowProperty<bool> IsTemplateProperty;
        private FlowProperty<string> TemplateNameProperty;

        //private FlowProperty<string> LockDateProperty;
        private FlowProperty<string> SpecificationNameProperty;

        #endregion
    }

    [Task("SPECMGT:LoadDossier", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class LoadDossier : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public LoadDossier()
        {
            DossierNameProperty = Properties.RegisterStringProperty("Nom Dossier", "Nom du Dossier");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var dossierService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    var theDossier = dossierService.GetDossierByName(DossierNameProperty.Value);
                    if (theDossier == null)
                        throw new Exception("Le Dossier '{0}' n'existe pas".FormatString(DossierNameProperty.Value));

                    var errorList = new List<string>();
                    var projectVersion = ctx.Project.GetProjectSettings().ProjectVersion;

                    //récupère la dernière specification
                    var lastSpecification = theDossier.Specifications.OrderBy(x => x.CreationDate).Last();

                    //Controle de Version
                    if (lastSpecification.ProjectVersion > projectVersion)
                        throw new Exception("La version du projet de la sauvegarde '{0}', ne peut pas être supérieure à la version du projet actuellement ouvert '{1}'".FormatString(lastSpecification.ProjectVersion, projectVersion));

                    //Constant
                    var specificationConstantStateList = lastSpecification.Constants.DeserializeList<ConstantState>();
                    var constantBuildResult = ctx.Project.BuildConstantListFromVersion(specificationConstantStateList, lastSpecification.ProjectVersion);

                    //Constantes
                    foreach (var item in constantBuildResult.Enum())
                    {
                        var theConstant = ctx.Project.Constants.GetConstant(item.Name);
                        if (theConstant != null)
                            theConstant.Value = item.Value;
                        else
                            errorList.Add("La constant '{0}' est inexistante".FormatString(item.Name));
                    }

                    //Control
                    var specificationControlStateList = lastSpecification.Controls.DeserializeList<ControlState>();
                    var controlBuildResult = ctx.Project.BuildControlListFromVersion(specificationControlStateList, lastSpecification.ProjectVersion);

                    specificationControlStateList = controlBuildResult.Item1;
                    var deletedMessageList = controlBuildResult.Item2;

                    var addedMessageList = specificationControlStateList.Enum().Where(x => x.Message.IsNotNullAndNotEmpty()).Enum().ToList();

                    var errorCount = -1;

                    var specificationRestControlStateList = specificationControlStateList;

                    //Bouclage pour définir la valeur des controles. L'écriture en une seule fois n'est pas possible car certaines valeures sont dépendantes d'autres valeurs. Il est normale que le nombre d'erreur diminue à chaque itération.
                    while (errorCount != 0)
                    {
                        specificationRestControlStateList = Tools.Tools.SetControlValueListToSpecification(ctx, specificationControlStateList);

                        if (specificationRestControlStateList.Count() == errorCount)
                        {
                            foreach (var controlStateItem in specificationRestControlStateList)
                            {
                                var theControl = ctx.Project.Navigation.GetControl(controlStateItem.Name);
                                var controlState = ctx.Project.GetControlStateFormControlBase(theControl);

                                if (controlState.Value != controlStateItem.Value)
                                    errorList.Add("Controle '{0}' la valeur est de '{1}' au lieu de '{2}', contacter l'administrateur.".FormatString(theControl.Name, controlState.Value, controlStateItem.Value));

                                if (controlState.Value2 != controlStateItem.Value2)
                                    errorList.Add("Controle '{0}' la valeur 2 est de '{1}' au lieu de '{2}', contacter l'administrateur.".FormatString(theControl.Name, controlState.Value2, controlStateItem.Value2));
                            }

                            if (errorList.IsNullOrEmpty())
                                throw new Exception("Conctacter l'administrateur, Erreur dans l'écriture des valeurs de contrôles");

                            break;
                        }
                        else
                        {
                            errorCount = specificationRestControlStateList.Count();
                        }
                    }

                    //Création du message Utilisateur
                    var endMessage = "Chargement effectué";

                    if (errorList.IsNotNullAndNotEmpty())
                        endMessage += Environment.NewLine + Environment.NewLine + "ERREUR(S) A CORRIGER :" + Environment.NewLine + errorList.Concat(Environment.NewLine);

                    if (addedMessageList.IsNotNullAndNotEmpty())
                        endMessage += Environment.NewLine + Environment.NewLine + "CONTROLE(S) AJOUTE(S) :" + Environment.NewLine + addedMessageList.Select(x=>x.Message).ToList().Concat(Environment.NewLine);

                    if (deletedMessageList.IsNotNullAndNotEmpty())
                        endMessage += Environment.NewLine + Environment.NewLine + "CONTROLE(S) SUPPRIME(S) :" + Environment.NewLine + deletedMessageList.Concat(Environment.NewLine);

                    ctx.Project.AddMessage(endMessage);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors du chargement du dossier", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> DossierNameProperty;

        #endregion
    }

    [Task("SPECMGT:UpdateDossier", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class UpdateDossier : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public UpdateDossier()
        {
            DossierNameProperty = Properties.RegisterStringProperty("Nom Dossier", "Nom du Dossier à modifier");
            CheckErrorProperty = Properties.RegisterBooleanProperty("Vérifier les erreurs", "True si une vérification doit être effectué, False s'il ne faut pas, par exemple pour sauvegarder un brouillon");
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom Spécification", "Nom de la Spécification à Créer");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var dossierService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    // Vérification des erreurs
                    if (CheckErrorProperty.Value)
                        if (ctx.Project.GetErrorMessageList().IsNotNullAndNotEmpty())
                            throw new Exception("La création n'est pas possible car contient des erreurs");
                    if (DossierNameProperty.Value.IsNullOrEmpty())
                        throw new Exception("Aucun nom de dossier n'a été renseigné");

                    if (SpecificationNameProperty.Value.IsNullOrEmpty())
                        throw new Exception("Aucun nom de spécification n'a été renseigné");

                    //récupération du dossier
                    var theDossier = dossierService.GetDossierByName(DossierNameProperty.Value);

                    decimal projectVersion = ctx.Project.GetProjectSettings().ProjectVersion;

                    //Creation de la Specification
                    theDossier.Specifications = new List<EquinoxeExtend.Shared.Object.Record.Specification>();

                    var newSpecification = new EquinoxeExtend.Shared.Object.Record.Specification();

                    newSpecification.SpecificationId = -1;
                    newSpecification.Constants = ctx.Project.GetCurrentConstantList().SerializeList();
                    newSpecification.Controls = ctx.Project.GetCurrentControlStateList().SerializeList();
                    newSpecification.CreationDate = DateTime.Now;
                    newSpecification.DossierId = theDossier.DossierId;
                    newSpecification.Name = SpecificationNameProperty.Value;
                    newSpecification.ProjectVersion = ctx.Project.GetProjectSettings().ProjectVersion;

                    dossierService.NewSpecification(newSpecification);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la mise à jour du dossier", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> DossierNameProperty;
        private FlowProperty<string> SpecificationNameProperty;
        private FlowProperty<bool> CheckErrorProperty;

        #endregion
    }

    [Task("SPECMGT:CheckErrorList", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class CheckErrorList : DriveWorks.Specification.Task
    {
        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            try
            {
                var errorMessages = ctx.Project.GetErrorMessageList();
                if (errorMessages.IsNotNullAndNotEmpty())
                {
                    var messageList = new List<string>();
                    messageList.Add("La spécification contient {0} erreur(s)".FormatString(errorMessages.Count()));
                    messageList.AddRange(errorMessages);
                    ctx.Project.AddMessage(messageList.Concat(Environment.NewLine));

                    ctx.Project.SetErrorConstant(true);
                }
            }
            catch (Exception ex)
            {
                ctx.Project.AddErrorMessage("Erreur lors de la vérification des erreurs", ex);
            }
        }

        #endregion
    }

    [Task("SPECMGT:AddLog", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class AddLog : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public AddLog()
        {
            TitleProperty = Properties.RegisterStringProperty("Titre", "Titre du log");
            MessageProperty = Properties.RegisterStringProperty("Message", "Message du log");
            TypeProperty = Properties.RegisterDoubleProperty("Type", "Type de message 1 : Erreur, 2 : Avertissement, 3 : Information");
            ThrowErrorProperty = Properties.RegisterBooleanProperty("Lever erreur", "Lever une erreur si une erreur à lieu");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            try
            {
                if (TitleProperty.Value.IsNullOrEmpty())
                    throw new Exception("Le titre du log est invalide");

                if (MessageProperty.Value.IsNullOrEmpty())
                    throw new Exception("Le message du log est invalide");

                var typePropertyEnum = (TypeLogEnum)TypeProperty.Value;
                if ((TypeProperty.Value % 1) != 0 || !(TypeProperty.Value >= 1 && TypeProperty.Value <= 3))
                    throw new Exception("Le type est invalide");

                using (var logService = new LogService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
                {
                    var newLog = new EquinoxeExtend.Shared.Object.Log.Log();
                    newLog.Message = MessageProperty.Value;
                    newLog.Title = TitleProperty.Value;
                    newLog.Type = typePropertyEnum;
                    logService.AddLog(newLog);
                }
            }
            catch (Exception ex)
            {
                ctx.Project.AddErrorMessage("Erreur lors de l'ajout d'un log", ex);
                if (ThrowErrorProperty.Value)
                    throw ex;
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> TitleProperty;
        private FlowProperty<string> MessageProperty;
        private FlowProperty<double> TypeProperty;
        private FlowProperty<bool> ThrowErrorProperty;

        #endregion
    }

    [Task("SPECMGT:FillDossierTable", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class FillDossierTable : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public FillDossierTable()
        {
            DossierNameProperty = Properties.RegisterStringProperty("Nom dossier", "Facultatif, Nom complet ou partiel à rechercher");
            UserNameProperty = Properties.RegisterStringProperty("Nom utilisateur", "Facultatif, Nom de l'utilisateur créateur ou modificateur à rechercher");
            StateNameProperty = Properties.RegisterStringProperty("Etat", "Nom de état à afficher");
            DossierTableControlNameProperty = Properties.RegisterStringProperty("Nom controle Datatable sortie", "Définir le nom du controle où les données seront retournées");
            ThrowErrorProperty = Properties.RegisterBooleanProperty("Lever erreur", "Lever une erreur si une erreur à lieu");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    var userList = ctx.Group.GetUserList();
                    var resultList = new List<EquinoxeExtendPlugin.Object.Dossier>();

                    //Recherche sur le nom
                    if (DossierNameProperty.Value.IsNotNullAndNotEmpty())
                    {
                        var simpleDossier = specificationService.GetDossierByName(DossierNameProperty.Value);
                        if (simpleDossier != null)
                        {
                            var fullDossier = simpleDossier.ConvertFull();
                            fullDossier.SpecificationPairs = new List<KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, SpecificationDetails>>();

                            foreach (var item in fullDossier.Specifications.Enum())
                            {
                                var theSpecificationDetails = ctx.Group.Specifications.GetSpecification(item.Name);
                                if (theSpecificationDetails == null)
                                    throw new Exception("la spécification '{0}' n'existe pas dans driveworks, contacter l'administrateur".FormatString(item.Name));

                                fullDossier.SpecificationPairs.Add(new KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, SpecificationDetails>(item, theSpecificationDetails));
                            }
                            resultList.Add(fullDossier);
                        }
                    }

                    //Autres
                    else
                    {
                        List<DriveWorks.Helper.Object.Specification> dataBaseSpecifications = null;
                        var dataBaseQuery = new DriveWorks.Helper.DataBaseQuery(ctx.Group.GetEnvironment().GetSQLConnectionString());

                        //State
                        if (StateNameProperty.Value.IsNotNullAndNotEmpty())
                        {
                            dataBaseSpecifications = dataBaseQuery.GetSpecificationsByStateName(StateNameProperty.Value);
                        }   

                        //User
                        if (UserNameProperty.Value.IsNotNullAndNotEmpty())
                        {
                            var UserDetails = userList.Enum().SingleOrDefault(x => x.LoginName == UserNameProperty.Value);
                            if (UserDetails == null)
                                throw new Exception("Le nom de l'utilisateur n'existe pas");

                            if (StateNameProperty.Value.IsNotNullAndNotEmpty())
                                dataBaseSpecifications = dataBaseSpecifications.Where(x => x.CreatorID == UserDetails.Id.ToString()).Enum().ToList();
                            else
                                dataBaseSpecifications = dataBaseQuery.GetSpecificationsByCreatorId(UserDetails.Id.ToString());
                        }

                        var tempDossiers = new List<EquinoxeExtend.Shared.Object.Record.Dossier>();

                        //Bouclage sur chaque spécification pour enrichir et filtrer
                        foreach(var specificationItem in dataBaseSpecifications.Enum())
                        {
                            //Récupére la spécification en base extend
                            var specification = specificationService.GetSpecificationByName(specificationItem.Name);

                            if (specification == null)
                                continue;
                            //Récupère le dossier en base extend
                            var parentDossier = specificationService.GetDossierById(specification.DossierId);

                            if (parentDossier == null)
                                continue;

                            //Pour l'état ne garder 
                            if (StateNameProperty.Value.IsNotNullAndNotEmpty())
                            {
                                var lastestSpecification = parentDossier.Specifications.OrderByDescending(x => x.CreationDate).First();
                                if(lastestSpecification.SpecificationId == specification.SpecificationId)
                                    tempDossiers.Add(parentDossier);
                            }
                            else
                            {
                                tempDossiers.Add(parentDossier);
                            }
                        }

                        //Enrichissement des objets
                        foreach (var dossierItem in tempDossiers.Enum())
                        {
                            var fullDossier = dossierItem.ConvertFull();
                            fullDossier.SpecificationPairs = new List<KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, SpecificationDetails>>();

                            foreach (var item in fullDossier.Specifications.Enum())
                            {
                                var theSpecificationDetails = ctx.Group.Specifications.GetSpecification(item.Name);
                                if (theSpecificationDetails == null)
                                    throw new Exception("la spécification '{0}' n'existe pas dans driveworks, contacter l'administrateur".FormatString(item.Name));

                                fullDossier.SpecificationPairs.Add(new KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, SpecificationDetails>(item, theSpecificationDetails));
                            }
                            resultList.Add(fullDossier);
                        }
                    }

                    //enlève les templates
                    resultList = resultList.Where(x => x.IsTemplate == false).Enum().ToList();

                    //var viewList = resultList.Select(x=>)

                    //Mise en forme
                    var arraySize = 8;
                    var objectArray = new object[resultList.Count + 1, arraySize];

                    objectArray[0, 0] = "Nom dossier";
                    objectArray[0, 1] = "Configurateur";
                    objectArray[0, 2] = "Statut";
                    objectArray[0, 3] = "Créateur";
                    objectArray[0, 4] = "Date création";
                    objectArray[0, 5] = "Modificateur";
                    objectArray[0, 6] = "Date modification";
                    objectArray[0, 7] = "Verrouillé";

                    //Mise en forme
                    int rowCounter = 1;
                    foreach (var dossierItem in resultList.Enum())
                    {
                        var creationSpecPair = dossierItem.SpecificationPairs.OrderBy(y => y.Value.DateCreated).First();
                        Nullable<KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, SpecificationDetails>> modificationSpecPair = null;

                        if (dossierItem.SpecificationPairs.Count != 1)
                            modificationSpecPair = dossierItem.SpecificationPairs.OrderBy(y => y.Value.DateCreated).Last();

                        //Nom dossier
                        objectArray[rowCounter, 0] = dossierItem.Name;

                        //Configurateur
                        objectArray[rowCounter, 1] = dossierItem.ProjectName;

                        //Statut
                        objectArray[rowCounter, 2] = dossierItem.SpecificationPairs.OrderBy(x => x.Value.DateCreated).Last().Value.StateName;

                        //Créateur
                        objectArray[rowCounter, 3] = userList.Single(x => x.Id == creationSpecPair.Value.CreatorId).DisplayName;

                        //Date création
                        objectArray[rowCounter, 4] = creationSpecPair.Value.DateCreated.ToStringDMY();

                        //Modificateur
                        objectArray[rowCounter, 5] = modificationSpecPair != null ? userList.Single(x => x.Id == modificationSpecPair.Value.Value.CreatorId).DisplayName : null;

                        //Date modification
                        objectArray[rowCounter, 6] = modificationSpecPair != null ? modificationSpecPair.Value.Value.DateCreated.ToStringDMY() : null;

                        //Verrouillé
                        objectArray[rowCounter, 7] = dossierItem.SpecificationPairs.OrderBy(y => y.Value.DateCreated).Last().Value.StateType == StateType.Running ? "Oui" : "Non";

                        rowCounter++;
                    }

                    var tableValue = new TableValue();
                    tableValue.Data = objectArray;

                    ctx.Project.SetTableControlItems(DossierTableControlNameProperty.Value, tableValue);
                }
                catch (Exception ex)
                {
                    ctx.Project.SetTableControlItems(DossierTableControlNameProperty.Value, null);

                    ctx.Project.AddErrorMessage("Erreur lors de la recherche de dossier", ex);
                    if (ThrowErrorProperty.Value)
                        throw ex;
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> DossierNameProperty;
        private FlowProperty<string> UserNameProperty;
        private FlowProperty<string> StateNameProperty;
        private FlowProperty<string> DossierTableControlNameProperty;
        private FlowProperty<bool> ThrowErrorProperty;

        #endregion

        protected class TemplateView
        {
            #region Public PROPERTIES

            [Name("FR", "Nom modèle")]
            [WidthColumn(30)]
            public string TemplateName { get; set; }

            [Name("FR", "Nom dossier")]
            [WidthColumn(30)]
            public string DossierName { get; set; }

            [Name("FR", "Statut")]
            [WidthColumn(30)]
            public string State { get; set; }

            [Name("FR", "Configurateur")]
            [WidthColumn(30)]
            public string ProjectName { get; set; }

            [Name("FR", "Description")]
            [WidthColumn(30)]
            public string Description { get; set; }

            [Name("FR", "Verrou")]
            [WidthColumn(30)]
            public string Lock { get; set; }

            public EquinoxeExtend.Shared.Object.Record.Dossier Object { get; set; }

            #endregion

            #region Public METHODS

            public static TemplateView ConvertTo(EquinoxeExtend.Shared.Object.Record.Dossier iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new TemplateView();
                newView.Object = iObj;

                newView.Description = iObj.TemplateDescription;
                newView.DossierName = iObj.Name;
                newView.Lock = iObj.Lock != null ? "Oui" : "Non";
                newView.ProjectName = iObj.ProjectName;
                newView.State = iObj.State.GetName("FR");
                newView.TemplateName = iObj.TemplateName;

                return newView;
            }

            #endregion
        }
    }

    

    [Task("SPECMGT:FillTemplateTable", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class FillTemplateTable : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public FillTemplateTable()
        {
            TemplateTableControlNameProperty = Properties.RegisterStringProperty("Nom controle Datatable sortie", "Définir le nom du controle où les données seront retournées");
            ThrowErrorProperty = Properties.RegisterBooleanProperty("Lever erreur", "Lever une erreur si une erreur à lieu");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            try
            {
                using (var dossierService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
                {
                    //Récupération des templates
                    var dossierList = dossierService.GetDossiers(false);
                    var resultList = new List<EquinoxeExtendPlugin.Object.Dossier>();

                    //Bouclage sur les dossiers modèles
                    foreach (var dossierItem in dossierList.Enum())
                    {
                        var newDossier = new EquinoxeExtendPlugin.Object.Dossier();
                        newDossier = dossierItem.ConvertFull();

                        newDossier.SpecificationPairs = new List<KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, SpecificationDetails>>();

                        //Bouclage sur les spécifications du dossier
                        foreach (var specificationItem in dossierItem.Specifications.Enum())
                        {
                            var theSpecificationDetails = ctx.Group.Specifications.GetSpecification(specificationItem.Name);
                            if (theSpecificationDetails == null)
                                throw new Exception("la spécification '{0}' n'existe pas dans driveworks, contacter l'administrateur".FormatString(specificationItem.Name));

                            newDossier.SpecificationPairs.Add(new KeyValuePair<EquinoxeExtend.Shared.Object.Record.Specification, SpecificationDetails>(specificationItem, theSpecificationDetails));
                        }
                        resultList.Add(newDossier);
                    }

                    var viewList = resultList.Enum().Select(x => TemplateView.ConvertTo(x)).Enum().ToList();

                    //Alimentation de la table
                    var objectArray = new object[viewList.Count + 1, 5];

                    objectArray[0, 0] = "Nom modèle";
                    objectArray[0, 1] = "Nom dossier";
                    objectArray[0, 2] = "Statut";
                    objectArray[0, 3] = "Configurateur";
                    objectArray[0, 4] = "Description";

                    //Mise en forme
                    int rowCounter = 1;
                    foreach (var dossierItem in resultList.Enum())
                    {
                        objectArray[rowCounter, 0] = dossierItem.TemplateName;
                        objectArray[rowCounter, 1] = dossierItem.Name;
                        objectArray[rowCounter, 2] = dossierItem.SpecificationPairs.OrderBy(x => x.Value.DateCreated).Last().Value.StateName;
                        objectArray[rowCounter, 3] = dossierItem.ProjectName;
                        objectArray[rowCounter, 4] = dossierItem.TemplateDescription;

                        rowCounter++;
                    }
                    var tableValue = new TableValue();
                    tableValue.Data = objectArray;

                    ctx.Project.SetTableControlItems(TemplateTableControlNameProperty.Value, tableValue);
                }
            }
            catch (Exception ex)
            {
                ctx.Project.AddErrorMessage("Erreur chargement des templates", ex);
                if (ThrowErrorProperty.Value)
                    throw ex;
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> TemplateTableControlNameProperty;
        private FlowProperty<bool> ThrowErrorProperty;

        #endregion

        protected class TemplateView
        {
            #region Public PROPERTIES

            [Name("FR", "Nom modèle")]
            [WidthColumn(30)]
            public string TemplateName { get; set; }

            [Name("FR", "Nom dossier")]
            [WidthColumn(30)]
            public string DossierName { get; set; }

            [Name("FR", "Statut")]
            [WidthColumn(30)]
            public string State { get; set; }

            [Name("FR", "Configurateur")]
            [WidthColumn(30)]
            public string ProjectName { get; set; }

            [Name("FR", "Description")]
            [WidthColumn(30)]
            public string Description { get; set; }

            [Name("FR", "Verrou")]
            [WidthColumn(30)]
            public string Lock { get; set; }

            public EquinoxeExtend.Shared.Object.Record.Dossier Object { get; set; }

            #endregion

            #region Public METHODS

            public static TemplateView ConvertTo(EquinoxeExtend.Shared.Object.Record.Dossier iObj)
            {
                if (iObj == null)
                    return null;

                var newView = new TemplateView();
                newView.Object = iObj;

                newView.Description = iObj.TemplateDescription;
                newView.DossierName = iObj.Name;
                newView.Lock = iObj.Lock != null ? "Oui" : "Non";
                newView.ProjectName = iObj.ProjectName;
                newView.State = iObj.State.GetName("FR");
                newView.TemplateName = iObj.TemplateName;

                return newView;
            }

            #endregion
        }
    }

    [Task("SPECMGT:FillLogTable", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class FillLogTable : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public FillLogTable()
        {
            TableControlNameProperty = Properties.RegisterStringProperty("Nom controle Datatable sortie", "Définir le nom du controle où les données seront retournées");
            TakeProperty = Properties.RegisterDoubleProperty("Nombre afficher", "Nombre de logs à afficher");
            TypeProperty = Properties.RegisterDoubleProperty("Type", "Type de message 0 : tout, 1 : Erreur, 2 : Avertissement, 3 : Information");
            ThrowErrorProperty = Properties.RegisterBooleanProperty("Lever erreur", "Lever une erreur si une erreur à lieu");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var logService = new LogService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    if (TableControlNameProperty.Value.IsNullOrEmpty())
                        throw new Exception("Le nom du control table est invalide");

                    if ((TakeProperty.Value % 1) != 0 || TakeProperty.Value < 1)
                        throw new Exception("Le nombre à afficher est invalide");

                    if ((TypeProperty.Value % 1) != 0 || !(TypeProperty.Value >= 0 && TypeProperty.Value <= 3))
                        throw new Exception("Le type est invalide");

                    var typeEnum = (TypeLogEnum)TypeProperty.Value;
                    var logList = logService.GetLogs(typeEnum, (int)TakeProperty.Value, 0);

                    var objectArray = new object[logList.Count + 1, 4];

                    objectArray[0, 0] = "Date";
                    objectArray[0, 1] = "Type";
                    objectArray[0, 2] = "Titre";
                    objectArray[0, 3] = "Message";

                    //Mise en forme
                    int rowCounter = 1;
                    foreach (var logItem in logList.Enum())
                    {
                        objectArray[rowCounter, 0] = logItem.Date.ToStringDMY();
                        objectArray[rowCounter, 1] = typeEnum.GetName("FR");
                        objectArray[rowCounter, 2] = logItem.Title;
                        objectArray[rowCounter, 3] = logItem.Message;

                        rowCounter++;
                    }

                    var tableValue = new TableValue();
                    tableValue.Data = objectArray;

                    ctx.Project.SetTableControlItems(TableControlNameProperty.Value, tableValue);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur chargement des templates", ex);
                    if (ThrowErrorProperty.Value)
                        throw ex;
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> TableControlNameProperty;
        private FlowProperty<double> TakeProperty;
        private FlowProperty<double> TypeProperty;
        private FlowProperty<bool> ThrowErrorProperty;

        #endregion
    }

    [Task("SPECMGT:FillGenerationTable", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class FillGenerationTable : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public FillGenerationTable()
        {
            TableControlNameProperty = Properties.RegisterStringProperty("Nom controle Datatable sortie", "Définir le nom du controle où les données seront retournées");
            CriteriaProperty = Properties.RegisterDoubleProperty("Critères", "1 retourne les assemblages de têtes en attente de génération, 2 retourne les dernières erreurs");
            TakeProperty = Properties.RegisterDoubleProperty("Nombre afficher", "Nombre de logs à afficher");
            ThrowErrorProperty = Properties.RegisterBooleanProperty("Lever erreur", "Lever une erreur si une erreur à lieu");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new RecordService(ctx.Group.GetEnvironment().GetSQLExtendConnectionString()))
            {
                try
                {
                    if ((TakeProperty.Value % 1) != 0 || TakeProperty.Value < 1)
                        throw new Exception("Le nombre à afficher est invalide");

                    var iCriteria = (CriteriaEnum)CriteriaProperty.Value;

                    DriveWorks.IGroupResultEnumerator<DriveWorks.Components.ReleasedComponentDetails> result;

                    if (iCriteria == CriteriaEnum.HeadAssembly)
                        result = ctx.Group.ReleasedComponents.GetComponents(true, false, true, false);
                    else if (iCriteria == CriteriaEnum.LastError)
                        result = ctx.Group.ReleasedComponents.GetComponents(false, false, true, true);
                    else
                        throw new Exception("Critère non géré");

                    var resultList = new List<DriveWorks.Components.ReleasedComponentDetails>();

                    var resultCounter = 0;
                    while (true)
                    {
                        if (resultCounter > TakeProperty.Value)
                            break;

                        try
                        {
                            result.MoveNext();
                            var component = result.Current;

                            if (iCriteria == CriteriaEnum.LastError && component.Failed == 0)
                                continue;

                            resultList.Add(result.Current);
                            resultCounter++;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }

                    var objectArray = new object[resultList.Count + 1, 3];

                    objectArray[0, 0] = "Name";
                    objectArray[0, 1] = "Master";
                    objectArray[0, 2] = "Chemin";

                    //Mise en forme
                    int rowCounter = 1;
                    foreach (var componentItem in resultList.Enum())
                    {
                        objectArray[rowCounter, 0] = Path.GetFileNameWithoutExtension(componentItem.TargetName);
                        objectArray[rowCounter, 1] = Path.GetFileNameWithoutExtension(componentItem.MasterPath);
                        objectArray[rowCounter, 2] = componentItem.TargetPath;

                        rowCounter++;
                    }
                    var tableValue = new TableValue();
                    tableValue.Data = objectArray;

                    ctx.Project.SetTableControlItems(TableControlNameProperty.Value, tableValue);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur chargement des templates", ex);
                    if (ThrowErrorProperty.Value)
                        throw ex;
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> TableControlNameProperty;
        private FlowProperty<double> CriteriaProperty;
        private FlowProperty<double> TakeProperty;
        private FlowProperty<bool> ThrowErrorProperty;

        #endregion
    }

   
}