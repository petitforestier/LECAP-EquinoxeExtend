using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Extensions;
using Service.DBRelease.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquinoxeExtend.Shared.Enum;

namespace Service.Release.Front
{
    public partial class ReleaseService
    {
        #region Public METHODS

        public long AddSubTask(SubTask iSubTask)
        {
            if (iSubTask == null) throw new Exception("La sous tâche est nulle");

            //Validation des conditions
            var mainTask = GetMainTaskById(iSubTask.MainTaskId, Library.Tools.Enums.GranularityEnum.Nude);
            if (mainTask.Status != EquinoxeExtend.Shared.Enum.MainTaskStatusEnum.InProgress && iSubTask.Progression != 0)
                throw new Exception("L'avancement d'une sous tâche requiert que la tâche soit en cours");

            if (mainTask.Status != EquinoxeExtend.Shared.Enum.MainTaskStatusEnum.InProgress &&
               mainTask.Status != EquinoxeExtend.Shared.Enum.MainTaskStatusEnum.Waiting)
                throw new Exception("L'ajout d'une sous tâche n'est pas possible pour ce status de tâche");

            //Validation des superpositions de package
            if(iSubTask.ProjectGUID != null)
            {
                if(!IsProjectCanJoinThisMainTask(mainTask.MainTaskId, (Guid)iSubTask.ProjectGUID))
                    throw new Exception("Ce projet est déjà utilisé dans une tâche en cours");
            }

            var entity = new T_E_SubTask();
            entity.Merge(iSubTask);
            return DBReleaseDataService.AddSubTask(entity);
        }

        public void UpdateSubTask(SubTask iSubTask)
        {
            if (iSubTask == null) throw new Exception("La sous tâche est null");
            if (iSubTask.SubTaskId < 1) throw new Exception("L'id de la sous tâche est invalide");

            //Vérification que le projet est toujours le même
            var originalSubTask = DBReleaseDataService.GetSubTask(iSubTask.SubTaskId);
            if (originalSubTask.ProjectGUID != iSubTask.ProjectGUID)
                throw new Exception("Le projet d'une tâche de projet ne peut pas être modifiée");

            //Validation des conditions
            var mainTask = GetMainTaskById(iSubTask.MainTaskId, Library.Tools.Enums.GranularityEnum.Nude);
            if (mainTask.Status != EquinoxeExtend.Shared.Enum.MainTaskStatusEnum.InProgress && iSubTask.Progression != 0)
                throw new Exception("L'avancement d'un sous tâche requiert que la tâche soit en cours");

            var entity = new T_E_SubTask();
            entity.Merge(iSubTask);
            DBReleaseDataService.UpdateSubTask(entity);
        }

        public void DeleteProjectTask(SubTask iSubTask)
        {
            if (iSubTask == null) throw new Exception("La sous tâche est null");
            if (iSubTask.SubTaskId < 1) throw new Exception("L'id de la sous tâche est invalide");

            //Validation des conditions
            var mainTask = GetMainTaskById(iSubTask.MainTaskId, Library.Tools.Enums.GranularityEnum.Nude);

            if (mainTask.Status != EquinoxeExtend.Shared.Enum.MainTaskStatusEnum.InProgress &&
               mainTask.Status != EquinoxeExtend.Shared.Enum.MainTaskStatusEnum.Requested &&
                mainTask.Status != EquinoxeExtend.Shared.Enum.MainTaskStatusEnum.Waiting)
                throw new Exception("L'ajout d'une sous tâche n'est pas possible pour ce status de tâche");

            DBReleaseDataService.DeleteSubTask(iSubTask.SubTaskId);
        }

        public List<SubTask> GetSubTaskByMainTaskId(long iMainTask)
        {
            return DBReleaseDataService.GetList<T_E_SubTask>(x => x.MainTaskId == iMainTask).Enum().Select(x => x.Convert()).Enum().ToList();
        }

        public List<SubTask> GetOpenedTask()
        {
            return DBReleaseDataService.GetList<T_E_SubTask>(x => x.T_E_MainTask.StatusRef == (short)MainTaskStatusEnum.InProgress).Enum().Select(x=> x.Convert()).Enum().ToList();
        }


        #endregion
    }
}