//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.DBRecord.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_E_Generation
    {
        public long GenerationId { get; set; }
        public long SpecificationId { get; set; }
        public int StateRef { get; set; }
        public int TypeRef { get; set; }
        public string ProjectName { get; set; }
        public System.Guid CreatorGUID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string History { get; set; }
        public string Comments { get; set; }
    
        public virtual T_E_Specification T_E_Specification { get; set; }
    }
}