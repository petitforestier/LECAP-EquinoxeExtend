//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.DBRelease.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_E_SubTask
    {
        public long SubTaskId { get; set; }
        public long MainTaskId { get; set; }
        public string Designation { get; set; }
        public Nullable<System.Guid> ProjectGUID { get; set; }
        public int Progression { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> Start { get; set; }
        public Nullable<System.Guid> DevelopperGUID { get; set; }
        public string Comments { get; set; }
    
        public virtual T_E_MainTask T_E_MainTask { get; set; }
    }
}
