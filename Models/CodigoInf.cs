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
    
    public partial class CodInf
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CodInf()
        {
            this.InfraccionxActas = new HashSet<InfraccionxActa>();
        }
    
        public int id_codInf { get; set; }
        public string codigo { get; set; }
        public string concepto { get; set; }
        public string concepto_largo { get; set; }
        public string id_normativa { get; set; }
        public int id_grupo { get; set; }
        public string descripcion { get; set; }
        public string articulo { get; set; }
        public string inciso { get; set; }
        public string calificacion { get; set; }
        public string grupo { get; set; }
        public string estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InfraccionxActa> InfraccionxActas { get; set; }
    }
}
