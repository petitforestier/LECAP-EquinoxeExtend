using DriveWorks.Helper;
using DriveWorks.Helper.Manager;
using DriveWorks.Helper.Object;
using DriveWorks.Specification;
using EquinoxeExtend.Shared.Enum;
using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Service.Log.Front;
using Service.Specification.Front;
using Service.Specification.Front.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquinoxeExtendPlugin
{
    [Task("SPECMGT:DeleteTemplate", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class DeleteTemplate : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public DeleteTemplate()
        {
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom specification modèle", "Nom de la spécification modèle à supprimer");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    if (SpecificationNameProperty.Value.IsNullOrEmpty())
                        throw new Exception("Le nom de la spécification est invalide");

                    //Suppression Base de données
                    specificationService.DeleteSpecification(SpecificationNameProperty.Value);

                    //Suppression DW
                    ctx.Group.DeleteSpecification(SpecificationNameProperty.Value);

                    ctx.Project.AddMessage("Modèle '{0}' a été supprimé avec succès".FormatString(SpecificationNameProperty.Value));
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la suppression du modèle", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> SpecificationNameProperty;

        #endregion
    }

    [Task("SPECMGT:RenameTemplate", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class RenameTemplate : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public RenameTemplate()
        {
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom spécification", "Nom de la spécification à renommer");
            NewTemplateNameProperty = Properties.RegisterStringProperty("Nouveau nom modèle", "Nouveau nom du modèle");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    var newName = NewTemplateNameProperty.Value;
                    if (newName.IsNullOrEmpty())
                        throw new Exception("Le nouveau nom n'est pas valide");

                    specificationService.UpdateDisplayName(SpecificationNameProperty.Value, newName);

                    ctx.Project.AddMessage("La spécification '{0}' a été renommé en '{1}' avec succès".FormatString(SpecificationNameProperty.Value, newName));
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la renommage du modèle", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> SpecificationNameProperty;
        private FlowProperty<string> NewTemplateNameProperty;

        #endregion
    }

    [Task("SPECMGT:UpdateTemplateDescription", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class UpdateTemplateDescription : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public UpdateTemplateDescription()
        {
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom spécification", "Nom de la spécification à renommer");
            NewDescriptionProperty = Properties.RegisterStringProperty("Description du modèle", "Description du modèle");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    var newDescription = NewDescriptionProperty.Value;
                    specificationService.UpdateDescription(SpecificationNameProperty.Value, newDescription);
                    ctx.Project.AddMessage("La description de la spécification '{0}'a été modifiée avec succès".FormatString(SpecificationNameProperty.Value, newDescription));
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la modification de la description du modèle", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> SpecificationNameProperty;
        private FlowProperty<string> NewDescriptionProperty;

        #endregion
    }

    [Task("SPECMGT:NewSpecification", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class NewSpecification : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public NewSpecification()
        {
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom spécification", "Nom de la spécification à créer");
            CheckErrorProperty = Properties.RegisterBooleanProperty("Vérifier les erreurs", "True si une vérification doit être effectué, False s'il ne faut pas, par exemple pour sauvegarder un brouillon");
            TemplateDisplayNameProperty = Properties.RegisterStringProperty("Nom du modèle", "Nom textuel d'affichage pour différentier les modèles autrement que le numéro de spécification");
            TemplateDescriptionProperty = Properties.RegisterStringProperty("Description du modèle", "Description du modèle");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    //Vérification des erreurs
                    if (CheckErrorProperty.Value)
                        if (ctx.Project.GetErrorMessageList().IsNotNullAndNotEmpty())
                            throw new Exception("La création n'est pas possible car contient des erreurs");

                    //Vérification que tous les controls et constantes sont renseignés dans les tables
                    ctx.Project.CheckControlConstantManagement();

                    var newSpecification = new EquinoxeExtend.Shared.Object.Specification.Specification();

                    newSpecification.SpecificationName = SpecificationNameProperty.Value;
                    newSpecification.IsTemplate = (TemplateDisplayNameProperty.Value.IsNullOrEmpty()) ? false : true;
                    newSpecification.DisplayName = (TemplateDisplayNameProperty.Value.IsNullOrEmpty()) ? null : TemplateDisplayNameProperty.Value;
                    newSpecification.Description = TemplateDescriptionProperty.Value;
                    newSpecification.ProjectVersion = ctx.Project.GetProjectSettings().ProjectVersion;
                    newSpecification.Constants = ctx.Project.GetCurrentConstantList().SerializeList();
                    newSpecification.Controls = ctx.Project.GetCurrentControlStateList().SerializeList();

                    newSpecification.SpecificationVersion = 1;

                    specificationService.NewSpecification(newSpecification);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la création de la spécification", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> SpecificationNameProperty;
        private FlowProperty<bool> CheckErrorProperty;
        private FlowProperty<string> TemplateDisplayNameProperty;
        private FlowProperty<string> TemplateDescriptionProperty;

        #endregion
    }

    [Task("SPECMGT:UpdateSpecification", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class UpdateSpecification : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public UpdateSpecification()
        {
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom spécification", "Nom de la spécification à modifier");
            CheckErrorProperty = Properties.RegisterBooleanProperty("Vérifier les erreurs", "True si une vérification doit être effectué, False s'il ne faut pas, par exemple pour sauvegarder un brouillon");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    if (SpecificationNameProperty.Value.IsNullOrEmpty())
                        throw new Exception("Aucune spécification n'a été chargée");

                    var theSpecification = specificationService.GetSpecificationByName(SpecificationNameProperty.Value);

                    decimal projectVersion = ctx.Project.GetProjectSettings().ProjectVersion;

                    if (theSpecification.ProjectVersion > projectVersion)
                        throw new Exception("La spécification est à la version '{0}', elle ne peut être supérieure à la version du projet '{1}'".FormatString(theSpecification.ProjectVersion, projectVersion));

                    //Ajout des valeurs
                    theSpecification.ProjectVersion = ctx.Project.GetProjectSettings().ProjectVersion;
                    theSpecification.Constants = ctx.Project.GetCurrentConstantList().SerializeList();
                    theSpecification.Controls = ctx.Project.GetCurrentControlStateList().SerializeList();

                    theSpecification.SpecificationVersion ++;

                    specificationService.UpdateSpecification(theSpecification);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la création de la spécification", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> SpecificationNameProperty;
        private FlowProperty<bool> CheckErrorProperty;

        #endregion
    }

    [Task("SPECMGT:LoadSpecification", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class LoadSpecification : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public LoadSpecification()
        {
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom spécification", "Nom de la spécification");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    var theSpecification = specificationService.GetSpecificationByName(SpecificationNameProperty.Value);
                    if (theSpecification == null)
                        throw new Exception("La spécification '{0}' n'existe pas".FormatString(SpecificationNameProperty.Value));

                    var errorList = new List<string>();
                    var projectVersion = ctx.Project.GetProjectSettings().ProjectVersion;

                    //Controle de Version
                    if (theSpecification.ProjectVersion > projectVersion)
                        throw new Exception("La version du projet de la sauvegarde '{0}', ne peut pas être supérieur à la version du projet actuellement ouvert '{1}'".FormatString(theSpecification.ProjectVersion, projectVersion));

                    //Constant
                    var specificationConstantStateList = theSpecification.Constants.DeserializeList<ConstantState>();
                    var constantBuildResult = ctx.Project.BuildConstantListFromVersion(specificationConstantStateList, theSpecification.ProjectVersion);

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
                    var specificationControlStateList = theSpecification.Controls.DeserializeList<ControlState>();
                    var controlBuildResult = ctx.Project.BuildControlListFromVersion(specificationControlStateList, theSpecification.ProjectVersion);

                    specificationControlStateList = controlBuildResult.Item1;
                    var deletedMessageList = controlBuildResult.Item2;

                    var errorCount = -1;

                    var specificationRestControlStateList = specificationControlStateList;

                    while (errorCount != 0)
                    {
                        specificationRestControlStateList = Tools.Tools.SetControlValueListToSpecification(ctx, specificationRestControlStateList);

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

                            throw new Exception(errorList.Concat(Environment.NewLine));
                        }
                        else
                        {
                            errorCount = specificationRestControlStateList.Count();
                        }
                    }

                    //Vérification des valeurs de tous les controls
                    foreach (var controlStateItem in specificationControlStateList)
                    {
                        var theControl = ctx.Project.Navigation.GetControl(controlStateItem.Name);
                        var controlState = ctx.Project.GetControlStateFormControlBase(theControl);

                        if (controlState.Value != controlStateItem.Value)
                            errorList.Add("Controle '{0}' la valeur est de '{1}' au lieu de '{2}', contacter l'administrateur.".FormatString(theControl.Name, controlState.Value, controlStateItem.Value));

                        if (controlState.Value2 != controlStateItem.Value2)
                            errorList.Add("Controle '{0}' la valeur 2 est de '{1}' au lieu de '{2}', contacter l'administrateur.".FormatString(theControl.Name, controlState.Value2, controlStateItem.Value2));
                    }

                    if(errorList.IsNotNullAndNotEmpty())
                        throw new Exception(errorList.Concat(Environment.NewLine));

                    ctx.Project.AddMessage("Chargement effectué".FormatString(SpecificationNameProperty.Value));
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de la création de la spécification", ex);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> SpecificationNameProperty;

        #endregion
    }

    //[Task("SPECMGT:SetControlValue", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    //public class SetControlValue : DriveWorks.Specification.Task
    //{
    //    private FlowProperty<string> ControlNameProperty;
    //    private FlowProperty<string> ValueControlProperty;

    //    public SetControlValue()
    //    {
    //        ControlNameProperty = Properties.RegisterStringProperty("Nom du Contrôle", "Nom du Contrôle");
    //        ValueControlProperty = Properties.RegisterStringProperty("Valeur du Contrôle", "Texte à envoyer au contrôle");
    //    }

    //    protected override void Execute(SpecificationContext ctx)
    //    {

    //        var theControl = ctx.Project.Navigation.GetControl(ControlNameProperty.Value);
    //        theControl.SetInputValue(ValueControlProperty.Value);
    //    }

    //}

    //[Task("SPECMGT:TransferControlValue", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    //public class TransferControlValue : DriveWorks.Specification.Task
    //{
    //    private FlowProperty<string> ControlSourceProperty;
    //    private FlowProperty<string> ControlCibleProperty;

    //    public TransferControlValue()
    //    {
    //        ControlSourceProperty = Properties.RegisterStringProperty("Nom du Controle Source", "Nom du Controle Source");
    //        ControlCibleProperty = Properties.RegisterStringProperty("Nom du Controle Cible", "Nom du Controle Cible");
    //    }

    //    protected override void Execute(SpecificationContext ctx)
    //    {
    //        var theControlSource = ctx.Project.Navigation.GetControl(ControlSourceProperty.Value);
    //        var inputvalue = theControlSource.GetInputValue();
            
    //        var theControlCible = ctx.Project.Navigation.GetControl(ControlCibleProperty.Value);
    //        theControlCible.SetInputValue(inputvalue);

    //    }

    //}
       
    //[Task("SPECMGT:SetControlColor", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    //public class SetControlColor : DriveWorks.Specification.Task
    //{
    //    private FlowProperty<string> ControlNameProperty;
    //    private FlowProperty<string> ColorControlProperty;

    //    public SetControlColor()
    //    {
    //        ControlNameProperty = Properties.RegisterStringProperty("Nom du Controle", "Nom du Controle");
    //        ColorControlProperty = Properties.RegisterStringProperty("Couleur du Contrôle", "Couleur à affecter au contrôle");
    //    }

    //    protected override void Execute(SpecificationContext ctx)
    //    {

    //        var theControlColor = ctx.Project.Navigation.GetControl(ControlNameProperty.Value);
    //        theControlColor.BackgroundColor = DriveWorks.Forms.SimpleColor.FromName(ColorControlProperty.Value);
            
    //    }

    //}


    //[Task("SPECMGT:ChooseControlColor", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    //public class ChooseControlColor : DriveWorks.Specification.Task
    //{
    //    private FlowProperty<string> ControlNameProperty;
    //    private FlowProperty<string> ControlResultProperty;
    //    private FlowProperty<string> ControlCibleProperty;


    //    public ChooseControlColor()
    //    {
    //        ControlNameProperty = Properties.RegisterStringProperty("Nom du Controle Couleur", "Nom du Controle Couleur");
    //        ControlResultProperty = Properties.RegisterStringProperty("Nom du Controle Résultat", "Nom du Controle Résultat");
    //        ControlCibleProperty = Properties.RegisterStringProperty("Nom du Controle Cible", "Nom du Controle Cible");
    //    }

    //    protected override void Execute(SpecificationContext ctx)
    //    {

    //        var theControlColor = ctx.Project.Navigation.GetControl(ControlNameProperty.Value);
    //        var inputcolor = theControlColor.GetInputValue();

    //        var theControlResult = ctx.Project.Navigation.GetControl(ControlResultProperty.Value);
    //        theControlResult.SetInputValue(inputcolor);

    //        var theControlCible = ctx.Project.Navigation.GetControl(ControlCibleProperty.Value);

    //        //if (inputcolor.ToString() == "OptionColorRed")
    //        //{
    //        //    theControlCible.BackgroundColor = DriveWorks.Forms.SimpleColor.FromName("Red");
    //        //}
    //        //else
    //        //{
    //        //    theControlCible.BackgroundColor = DriveWorks.Forms.SimpleColor.FromName("Blue");
    //        //}

    //        theControlCible.BackgroundColor = DriveWorks.Forms.SimpleColor.FromName(inputcolor.ToString());

    //    }

    //}


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

                using (var logService = new LogService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
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

    [Task("SPECMGT:FillSpecificationTable", "embedded://MyExtensionLibrary.Puzzle-16x16.png", "SpecificationManagement")]
    public class FillSpecificationTable : DriveWorks.Specification.Task
    {
        #region Public CONSTRUCTORS

        public FillSpecificationTable()
        {
            SpecificationNameProperty = Properties.RegisterStringProperty("Nom Spécification", "Facultatif, Nom complet ou partiel à rechercher");
            UserNameProperty = Properties.RegisterStringProperty("Nom utilisateur", "Facultatif, Nom de l'utilisateur créateur ou modificateur à rechercher");
            StateNameProperty = Properties.RegisterStringProperty("Etat", "Nom de état à afficher");
            SpecificationTableControlNameProperty = Properties.RegisterStringProperty("Nom controle Datatable sortie", "Définir le nom du controle où les données seront retournées");
            TemplateProperty = Properties.RegisterBooleanProperty("Montrer modèle", "True pour voir exclusivement les modèles, False pour ne voir aucun modèle");
            ThrowErrorProperty = Properties.RegisterBooleanProperty("Lever erreur", "Lever une erreur si une erreur à lieu");
        }

        #endregion

        #region Protected METHODS

        protected override void Execute(SpecificationContext ctx)
        {
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    var userList = ctx.Group.GetUserList();
                    var projectDic = ctx.Group.GetProjectAliasDic();

                    var resultList = new List<KeyValuePair<EquinoxeExtend.Shared.Object.Specification.Specification, SpecificationDetails>>();

                    //Recherche sur le nom
                    if (SpecificationNameProperty.Value.IsNotNullAndNotEmpty())
                    {
                        var specification = specificationService.GetSpecificationByName(SpecificationNameProperty.Value);
                        var specificationDetail = ctx.Group.Specifications.GetSpecification(SpecificationNameProperty.Value);

                        if (specification != null && specification != null)
                            resultList.Add(new KeyValuePair<EquinoxeExtend.Shared.Object.Specification.Specification, SpecificationDetails>(specification, specificationDetail));
                    }
                    //Autres
                    else
                    {
                        var specifications = specificationService.GetSpecifications(TemplateProperty.Value);
                        var specificationDetails = ctx.Group.Specifications.GetSpecificationsByModifiedDate(true);

                        var listComparator = new Library.Tools.Comparator.ListComparator<EquinoxeExtend.Shared.Object.Specification.Specification, SpecificationDetails>(specifications, x => x.SpecificationName, specificationDetails, x => x.Name);

                        var commonList = listComparator.CommonPairList;

                        //State
                        if (StateNameProperty.Value.IsNotNullAndNotEmpty())
                            commonList = commonList.Enum().Where(x => x.Value.StateName == StateNameProperty.Value).Enum().ToList();

                        //User
                        if (UserNameProperty.Value.IsNotNullAndNotEmpty())
                        {
                            var userId = ctx.Group.GetUserList().Single(x => x.DisplayName == UserNameProperty.Value);
                            commonList = commonList.Enum().Where(x => x.Value.CreatorId == userId.Id || x.Value.EditorId == userId.Id).Enum().ToList();
                        }
                        resultList = commonList;
                    }

                    //Ne garde pas les spécifications locké
                    resultList = resultList.Enum().Where(x => x.Value.StateType != StateType.Running).Enum().OrderByDescending(x => x.Value.Name).ToList();

                    //Mise en forme
                    var arraySize = (TemplateProperty.Value) ? 9 : 8;
                    var objectArray = new object[resultList.Count + 1, arraySize];

                    if (TemplateProperty.Value)
                    {
                        objectArray[0, 0] = "Nom spécification";
                        objectArray[0, 1] = "Nom modèle";
                        objectArray[0, 2] = "Gamme";
                        objectArray[0, 3] = "Statut";
                        objectArray[0, 4] = "Créateur";
                        objectArray[0, 5] = "Date création";
                        objectArray[0, 6] = "Modificateur";
                        objectArray[0, 7] = "Date modification";
                        objectArray[0, 8] = "ProjectName";
                    }
                    else
                    {
                        objectArray[0, 0] = "Nom spécification";
                        objectArray[0, 1] = "Gamme";
                        objectArray[0, 2] = "Statut";
                        objectArray[0, 3] = "Créateur";
                        objectArray[0, 4] = "Date création";
                        objectArray[0, 5] = "Modificateur";
                        objectArray[0, 6] = "Date modification";
                        objectArray[0, 7] = "ProjectName";
                    }

                    //Mise en forme
                    int rowCounter = 1;
                    foreach (var specItem in resultList.Enum())
                    {
                        if (TemplateProperty.Value)
                        {
                            objectArray[rowCounter, 0] = specItem.Value.Name;
                            objectArray[rowCounter, 1] = specItem.Key.DisplayName;
                            objectArray[rowCounter, 2] = projectDic[specItem.Value.ProjectId.ToString()];
                            objectArray[rowCounter, 3] = specItem.Value.StateName;
                            objectArray[rowCounter, 4] = userList.Single(x => x.Id == specItem.Value.CreatorId).DisplayName;
                            objectArray[rowCounter, 5] = specItem.Value.DateCreated.ToStringDMY();
                            objectArray[rowCounter, 6] = userList.Single(x => x.Id == specItem.Value.EditorId).DisplayName;
                            objectArray[rowCounter, 7] = specItem.Value.DateEdited.ToStringDMY();
                            objectArray[rowCounter, 8] = specItem.Value.OriginalProjectName;
                        }
                        else
                        {
                            objectArray[rowCounter, 0] = specItem.Value.Name;
                            objectArray[rowCounter, 1] = projectDic[specItem.Value.ProjectId.ToString()];
                            objectArray[rowCounter, 2] = specItem.Value.StateName;
                            objectArray[rowCounter, 3] = userList.Single(x => x.Id == specItem.Value.CreatorId).DisplayName;
                            objectArray[rowCounter, 4] = specItem.Value.DateCreated.ToStringDMY();
                            objectArray[rowCounter, 5] = userList.Single(x => x.Id == specItem.Value.EditorId).DisplayName;
                            objectArray[rowCounter, 6] = specItem.Value.DateEdited.ToStringDMY();
                            objectArray[rowCounter, 7] = specItem.Value.OriginalProjectName;
                        }

                        rowCounter++;
                    }

                    var tableValue = new TableValue();
                    tableValue.Data = objectArray;

                    ctx.Project.SetTableControlItems(SpecificationTableControlNameProperty.Value, tableValue);
                }
                catch (Exception ex)
                {
                    ctx.Project.AddErrorMessage("Erreur lors de l'ajout d'un log", ex);
                    if (ThrowErrorProperty.Value)
                        throw ex;
                }
            }
        }

        #endregion

        #region Private FIELDS

        private FlowProperty<string> SpecificationNameProperty;
        private FlowProperty<string> UserNameProperty;
        private FlowProperty<string> StateNameProperty;
        private FlowProperty<string> SpecificationTableControlNameProperty;
        private FlowProperty<bool> TemplateProperty;
        private FlowProperty<bool> ThrowErrorProperty;

        #endregion
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
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
            {
                try
                {
                    var projectDic = ctx.Group.GetProjectAliasDic();

                    List<EquinoxeExtend.Shared.Object.Specification.Specification> dbSpecifications = specificationService.GetSpecifications(true);

                    var dwSpecifications = ctx.Group.Specifications.GetSpecificationsByModifiedDate(true);

                    var listComparator = new Library.Tools.Comparator.ListComparator<EquinoxeExtend.Shared.Object.Specification.Specification, SpecificationDetails>(dbSpecifications, x => x.SpecificationName, dwSpecifications, x => x.Name);

                    var commonList = listComparator.CommonPairList.Enum().ToList();

                    var objectArray = new object[commonList.Count + 1, 4];

                    objectArray[0, 0] = "Nom Modèle";
                    objectArray[0, 1] = "Nom spécification";
                    objectArray[0, 2] = "Gamme";
                    objectArray[0, 3] = "Description";

                    //Mise en forme
                    int rowCounter = 1;
                    foreach (var specItem in commonList.Enum())
                    {
                        objectArray[rowCounter, 0] = specItem.Key.DisplayName;
                        objectArray[rowCounter, 1] = specItem.Key.SpecificationName;
                        objectArray[rowCounter, 2] = projectDic[specItem.Value.ProjectId.ToString()];
                        objectArray[rowCounter, 3] = specItem.Key.Description;

                        rowCounter++;
                    }
                    var tableValue = new TableValue();
                    tableValue.Data = objectArray;

                    ctx.Project.SetTableControlItems(TemplateTableControlNameProperty.Value, tableValue);
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

        private FlowProperty<string> TemplateTableControlNameProperty;
        private FlowProperty<bool> ThrowErrorProperty;

        #endregion
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
            using (var logService = new LogService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
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
            using (var specificationService = new SpecificationService(ctx.Group.GetEnvironment().GetExtendConnectionString()))
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