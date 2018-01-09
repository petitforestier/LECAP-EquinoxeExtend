using EquinoxeExtend.Shared.Object.Record;
using Library.Tools.Extensions;
using Service.DBRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Record.Front
{
    /// <summary>
    /// Lock
    /// </summary>
    public partial class RecordService
    {
        #region Public METHODS

        public long NewLock(EquinoxeExtend.Shared.Object.Record.Lock iNewLock)
        {
            if (iNewLock.DossierId<1)
                throw new Exception("L'id du dossier est invalide");

            if (iNewLock.LockDate == null)
                throw new Exception("La date du lock n'est pas valide");

            if (iNewLock.LockId !=-1)
                throw new Exception("L'id du lock est invalide");

            if (iNewLock.SessionGUID == null)
                throw new Exception("Le GUID de session est invalide");

            if (iNewLock.UserId == null)
                throw new Exception("L'id du user est invalide");

            if (DBRecordDataService.Any<T_E_Lock>(x => x.DossierId == iNewLock.DossierId))
                throw new Exception("Un lock est déjà posé sur le dossier '{0}'".FormatString(iNewLock.DossierId.ToString()));

            //Création de l'enregistrement
            var newEntity = new T_E_Lock();
            newEntity.Merge(iNewLock);

            return DBRecordDataService.Add<T_E_Lock>(newEntity).LockId;
        }

        public void DeleteLock(long iLockId)
        {
            if (iLockId < 1)
                throw new Exception("L'ID du lock est invalide");

            var theEntity = DBRecordDataService.GetSingle<T_E_Lock>(x => x.LockId == iLockId);

            //Suppression base de données
            DBRecordDataService.Delete<T_E_Lock>(theEntity);
        }

        public EquinoxeExtend.Shared.Object.Record.Lock GetLockById(long iLockId)
        {
            var entity = DBRecordDataService.GetSingleOrDefault<T_E_Lock>(x => x.LockId == iLockId);
            if (entity != null)
                return entity.Convert();
            else
                return null;
        }

        public EquinoxeExtend.Shared.Object.Record.Lock GetLockByDossierId(long iDossierId)
        {
            var entity = DBRecordDataService.GetSingleOrDefault<T_E_Lock>(x => x.DossierId == iDossierId);
            if (entity != null)
                return entity.Convert();
            else
                return null;
        }

        #endregion
    }
}