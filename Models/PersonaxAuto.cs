//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Actas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonaxAuto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonaxAuto()
        {
            this.ActasTablas = new HashSet<ActasTabla>();
        }
    
        public int id_personaxauto { get; set; }
        public Nullable<int> id_persona { get; set; }
        public Nullable<int> id_auto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasTabla> ActasTablas { get; set; }
        public virtual Automotore Automotore { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
