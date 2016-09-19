using Library.Entity;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBRelease.Data
{
    public partial class DataService : Repository
    {
        #region Public PROPERTIES

        public override DbContext RepositoryDBContext
        {
            get
            {
                if (_RepositoryDBContext == null)
                {
                    if (_ConnectionString.IsNullOrEmpty())
                        throw new Exception("La chaine de connection ne peut pas être nulle");
                    _RepositoryDBContext = new ReleaseDBContext(_ConnectionString);
                }

                return _RepositoryDBContext;
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public DataService(string iExtendDataBaseConnectionString)
             : base(true, iExtendDataBaseConnectionString)
        {
        }

        #endregion

        #region Private FIELDS

        private DbContext _RepositoryDBContext;

        #endregion
    }

    /// <summary>
    /// Deployement
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddDeployement(T_E_Deployement iDeployement)
        {
            T_E_DeployementValidation(iDeployement);

            if (iDeployement.DeployementId != -1) throw new ArgumentException("L'id du déploiement est différent de -1");
            return Add(iDeployement).DeployementId;
        }

        public void UpdateDeployement(T_E_Deployement iDeployement)
        {
            T_E_DeployementValidation(iDeployement);

            if (iDeployement.DeployementId < 1) throw new ArgumentException("L'id du déploiement est inferieur à 1");
            Update(iDeployement);
        }

        public void DeleteDeployement(long iDeployementId)
        {
            if (iDeployementId < 1) throw new ArgumentException("L'id du déploiement est invalide");
            var newEntity = new T_E_Deployement();
            newEntity.DeployementId = iDeployementId;
            Delete(newEntity);
        }

        public T_E_Deployement GetDeployementById(long iDeployementId)
        {
            if (iDeployementId < 1) throw new ArgumentException("L'id du déploiement est invalide");
            return GetSingleOrDefault<T_E_Deployement>(x => x.DeployementId == iDeployementId);
        }

        public void T_E_DeployementValidation(T_E_Deployement iDeployement)
        {
            if (iDeployement == null)
                throw new Exception("Le deploiement est null");

            if (iDeployement.EnvironmentDestinationRef <= 0)
                throw new Exception("L'environment est invalide");

            if (iDeployement.PackageId < 1)
                throw new Exception("Le package id est invalide");
        }

        #endregion
    }

    /// <summary>
    /// MainTask
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddMainTask(T_E_MainTask iMainTask)
        {
            MainTaskValidation(iMainTask);
            if (iMainTask.MainTaskId != -1) throw new ArgumentException("L'id de la tâche est différent de -1");
            return Add(iMainTask).MainTaskId;
        }

        public void UpdateMainTask(T_E_MainTask iMainTask)
        {
            MainTaskValidation(iMainTask);

            if (iMainTask.MainTaskId < 1) throw new ArgumentException("L'id de la tâche est inferieur à 1");
            Update(iMainTask);
        }

        public void DeleteMainTask(long iMainTaskId)
        {
            if (iMainTaskId < 1) throw new ArgumentException("L'id de la tâche est invalide");
            var newEntity = new T_E_MainTask();
            newEntity.MainTaskId = iMainTaskId;
            Delete(newEntity);
        }

        public T_E_MainTask GetMainTaskById(long iMainTaskId)
        {
            if (iMainTaskId < 1) throw new ArgumentException("L'id de la tâche est invalide");
            return GetSingleOrDefault<T_E_MainTask>(x => x.MainTaskId == iMainTaskId);
        }

        public void MainTaskValidation(T_E_MainTask iMainTask)
        {
            if (iMainTask == null)
                throw new Exception("La tâche est nulle");

            if (iMainTask.CreationUserGUID == null)
                throw new Exception("Le créateur de la tâche est requis");

            if (iMainTask.Name.IsNullOrEmpty())
                throw new Exception("Le nom de la tâche est requise");

            if (iMainTask.Name.Length > 300)
                throw new Exception("Le nom de la tâche est limité à 300 caractères");

            if (iMainTask.RequestUserGUID == null)
                throw new Exception("Le demandeur de la tâche est requis");
        }

        #endregion
    }

    /// <summary>
    /// Package
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddPackage(T_E_Package iPackage)
        {
            PackageValidation(iPackage);

            if (iPackage.PackageId != -1) throw new ArgumentException("L'id du package est différent de -1");
            return Add(iPackage).PackageId;
        }

        public void UpdatePackage(T_E_Package iPackage)
        {
            PackageValidation(iPackage);
            if (iPackage.PackageId < 1) throw new ArgumentException("L'id du package est inferieur à 1");
            Update(iPackage);
        }

        public void DeletePackage(long iPackageId)
        {
            if (iPackageId < 1) throw new ArgumentException("L'id du package est invalide");
            var newEntity = new T_E_Package();
            newEntity.PackageId = iPackageId;
            Delete(newEntity);
        }

        public T_E_Package GetPackageById(long iPackageId)
        {
            if (iPackageId < 1) throw new ArgumentException("L'id de la tâche est invalide");
            return GetSingleOrDefault<T_E_Package>(x => x.PackageId == iPackageId);
        }

        public void PackageValidation(T_E_Package iPackage)
        {
            if (iPackage == null)
                throw new Exception("La tâche est nulle");

            if (iPackage.StatusRef < 1)
                throw new Exception("Le status du package est requis");
        }

        #endregion
    }

    /// <summary>
    /// ProjectTask
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddSubTask(T_E_SubTask iSubTask)
        {
            SubTaskValidation(iSubTask);

            if (iSubTask.SubTaskId != -1) throw new ArgumentException("L'id de la sous est différent de -1");
            return Add(iSubTask).SubTaskId;
        }

        public void UpdateSubTask(T_E_SubTask iSubTask)
        {
            SubTaskValidation(iSubTask);

            if (iSubTask.SubTaskId < 1) throw new ArgumentException("L'id de la sous tâche est inferieur à 1");
            Update(iSubTask);
        }

        public void DeleteSubTask(long iSubTaskId)
        {
            if (iSubTaskId < 1) throw new ArgumentException("L'id de la tâche est invalide");
            var newEntity = new T_E_SubTask();
            newEntity.SubTaskId = iSubTaskId;
            Delete(newEntity);
        }

        public T_E_SubTask GetSubTask(long iSubTaskId)
        {
            if (iSubTaskId < 1) throw new ArgumentException("L'id de la tâche est invalide");
            return GetSingleOrDefault<T_E_SubTask>(x => x.SubTaskId == iSubTaskId);
        }

        public void SubTaskValidation(T_E_SubTask iSubTask)
        {
            if (iSubTask == null)
                throw new Exception("La tâche est nulle");

            if (iSubTask.MainTaskId < 1)
                throw new Exception("La tâche principale est requise");

            if (iSubTask.Progression < 0 || iSubTask.Progression > 100)
                throw new Exception("La progression doit être comprise entre 0 et 100");
        }

        #endregion
    }

    /// <summary>
    /// ProductLineTask
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddProductLineTask(T_E_ProductLineTask iProductLineTask)
        {
            ProductLineTaskValidation(iProductLineTask);

            if (iProductLineTask.ProductLineTaskId != -1) throw new ArgumentException("L'id de tâche de gamme est différent de -1");
            return Add(iProductLineTask).ProductLineTaskId;
        }

        public void UpdateProductLineTask(T_E_ProductLineTask iProductLineTask)
        {
            ProductLineTaskValidation(iProductLineTask);

            if (iProductLineTask.ProductLineTaskId < 1) throw new ArgumentException("L'id de la tâche de gamme est inferieur à 1");
            Update(iProductLineTask);
        }

        public void DeleteProductLineTask(long iProductLineTaskId)
        {
            if (iProductLineTaskId < 1) throw new ArgumentException("L'id de la tâche de gamme est invalide");
            var newEntity = new T_E_ProductLineTask();
            newEntity.ProductLineTaskId = iProductLineTaskId;
            Delete(newEntity);
        }

        public T_E_ProductLineTask GetProductLineTask(long iProductLineTaskId)
        {
            if (iProductLineTaskId < 1) throw new ArgumentException("L'id de la tâche de gamme est invalide");
            return GetSingleOrDefault<T_E_ProductLineTask>(x => x.ProductLineTaskId == iProductLineTaskId);
        }

        public void ProductLineTaskValidation(T_E_ProductLineTask iProductLineTask)
        {
            if (iProductLineTask == null)
                throw new Exception("La tâche de gamme est nulle");

            if (iProductLineTask.MainTaskId < 1)
                throw new Exception("La tâche principale est requise");

            if (iProductLineTask.ProductLineId < 1)
                throw new Exception("La gamme est requise");
        }

        #endregion
    }

    /// <summary>
    /// ExternalProject
    /// </summary>
    public partial class DataService
    {
        #region Public METHODS

        public long AddExternalProject(T_E_ExternalProject iExternalProject)
        {
            ExternalProjectValidation(iExternalProject);

            if (iExternalProject.ExternalProjectId != -1) throw new ArgumentException("L'id du projet est différent de -1");
            return Add(iExternalProject).ExternalProjectId;
        }

        public void UpdateExternalProject(T_E_ExternalProject iExternalProject)
        {
            ExternalProjectValidation(iExternalProject);

            if (iExternalProject.ExternalProjectId < 1) throw new ArgumentException("L'id du projet externe est inferieur à 1");
            Update(iExternalProject);
        }

        public void DeleteExternalProject(long iExternalProjectId)
        {
            if (iExternalProjectId < 1) throw new ArgumentException("L'id du projet externe est invalide");
            var newEntity = new T_E_ExternalProject();
            newEntity.ExternalProjectId = iExternalProjectId;
            Delete(newEntity);
        }

        public T_E_ExternalProject GetExternalProject(long iExternalProjectId)
        {
            if (iExternalProjectId < 1) throw new ArgumentException("L'id du projet externe est invalide");
            return GetSingleOrDefault<T_E_ExternalProject>(x => x.ExternalProjectId == iExternalProjectId);
        }

        public void ExternalProjectValidation(T_E_ExternalProject iExternalProject)
        {
            if (iExternalProject == null)
                throw new Exception("Le projet externe est nulle");

            if (iExternalProject.ProjectName.IsNullOrEmpty())
                throw new Exception("Le nom du projet est requis");

            if (iExternalProject.ProjectNumber.IsNullOrEmpty())
                throw new Exception("Le numéro de projet est requis");

            if (iExternalProject.StatusRef < 1)
                throw new Exception("Le statut est invalide");

            if (iExternalProject.TypeRef < 1)
                throw new Exception("Le type est invalide");
        }

        #endregion
    }
}