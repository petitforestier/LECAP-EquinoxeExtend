using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Extensions;
using Service.DBProduct.Data;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquinoxeExtend.Shared.Enum;
using Service.PDCExcelFile.Data;
using System.Configuration;
using Library.Tools.Comparator;

namespace Service.Release.Front
{
    public partial class ReleaseService
    {
        #region Public METHODS

        private const string PDCFILEPATH = @"\\lecapitaine.fr\Siege\Donnees\Projets\Developper catalogue\1-Suivi projets\Suivi PDC.xlsm";

        public void AddAndUpdateExternalProjectFromFile()
        {
            var PDCExcelFileDataService = new Service.PDCExcelFile.Data.DataService();
            var externalProjectFromFileList = PDCExcelFileDataService.GetExternalProjectListFromFile(PDCFILEPATH).Enum().Where(x=>x.Status == ExternalProjectStatusEnum.InProgress).Enum().ToList();
            var orginalExternalProjectList = DBReleaseDataService.GetList<T_E_ExternalProject>().Enum().Select(x=>x.Convert()).Enum().ToList();

            var comparator = new ListComparator<ExternalProject, ExternalProject>(orginalExternalProjectList, x => x.ProjectNumber.ToLower(), externalProjectFromFileList, x => x.ProjectNumber.ToLower());

            //Nouveau
            foreach(var item in comparator.NewList.Enum())
            {
                item.Type = ExternalProjectTypeEnum.PDC;
                item.ExternalProjectId = -1;
                AddExternalProject(item);
            }

            //Update
            foreach(var item in comparator.CommonPairList.Enum())
            {
                var originalIem = item.Key;
                var newItem = item.Value;

                newItem.ExternalProjectId = originalIem.ExternalProjectId;
                newItem.IsProcessed = originalIem.IsProcessed;
                newItem.Type = originalIem.Type;
                UpdatexternalProject(newItem);
            }
        }

        private long AddExternalProject(ExternalProject iExternalProject)
        {
            if (iExternalProject == null) throw new Exception("Le projet externe est null");
            if (iExternalProject.ExternalProjectId != -1) throw new Exception("L'id du projet externe est invalide");

            var newEntity = new T_E_ExternalProject();
            newEntity.Merge(iExternalProject);
            return DBReleaseDataService.AddExternalProject(newEntity);           
        }

        private void UpdatexternalProject(ExternalProject iExternalProject)
        {
            if (iExternalProject == null) throw new Exception("Le projet externe est null");
            if (iExternalProject.ExternalProjectId < 1) throw new Exception("L'id du projet externe est invalide");

            var newEntity = new T_E_ExternalProject();
            newEntity.Merge(iExternalProject);
            DBReleaseDataService.UpdateExternalProject(newEntity);
        }

        public void ProcessedExternalProject(ExternalProject iExternalProject)
        {
            if (iExternalProject == null) throw new Exception("Le projet externe est null");
            if (iExternalProject.ExternalProjectId < 1) throw new Exception("L'id du projet externe est invalide");

            var entity = DBReleaseDataService.GetExternalProject(iExternalProject.ExternalProjectId);
            entity.IsProcessed = true;
            DBReleaseDataService.UpdateExternalProject(entity);
        }

        public ExternalProject GetExternalProject(long iExternalProjectId)
        {
            if (iExternalProjectId < 1)
                throw new Exception("L'id est invalide");

            return DBReleaseDataService.GetExternalProject(iExternalProjectId).Convert();
        }

        public List<ExternalProject> GetExternalProjectList(ExternalProjectStatusSearchEnum iExternalProjectStatusSearchEnum)
        {        
            IQueryable<T_E_ExternalProject> theQuery = DBReleaseDataService.GetQuery<T_E_ExternalProject>(null);

            if (iExternalProjectStatusSearchEnum != ExternalProjectStatusSearchEnum.All)
            {
                if (iExternalProjectStatusSearchEnum == ExternalProjectStatusSearchEnum.Completed)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)ExternalProjectStatusEnum.Completed);
                else if (iExternalProjectStatusSearchEnum == ExternalProjectStatusSearchEnum.InProgress)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)ExternalProjectStatusEnum.InProgress);
                else if (iExternalProjectStatusSearchEnum == ExternalProjectStatusSearchEnum.Waiting)
                    theQuery = theQuery.Where(x => x.StatusRef == (short)ExternalProjectStatusEnum.Waiting);
                else if (iExternalProjectStatusSearchEnum == ExternalProjectStatusSearchEnum.UnProcessed)
                    theQuery = theQuery.Where(x => x.IsProcessed == false);
                else
                    throw new NotSupportedException(iExternalProjectStatusSearchEnum.ToStringWithEnumName());
            }

            return theQuery.ToList().Enum().OrderBy(x => x.ProjectNumber).Enum().Select(x => x.Convert()).Enum().ToList(); 
        }



        #endregion
    }
}