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
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class ActasTabla
    {
        public int id_acta { get; set; }
        [Remote(action: "VerifyActa", controller: "Home")]
        [Display(Name = "nroActa")]
        public string nroActa { get; set; }
        public bool estadoActa { get; set; }
        public string fechaAlta { get; set; }
        public Nullable<int> id_inspector { get; set; }
        public Nullable<int> id_infraccionxActa { get; set; }
        public Nullable<int> id_automotor { get; set; }
        public Nullable<int> id_personaxacta { get; set; }
        public string calleInf { get; set; }
        public string fechaInf { get; set; }
        public string horaInf { get; set; }
        public byte[] observacionesInf { get; set; }
        public Nullable<int> id_personaxauto { get; set; }
    
        public virtual Automotore Automotore { get; set; }
        public virtual InfraccionxActa InfraccionxActa { get; set; }
        public virtual Inspectore Inspectore { get; set; }
        public virtual PersonaxActa PersonaxActa { get; set; }
        public virtual PersonaxActa PersonaxActa1 { get; set; }
        public virtual PersonaxAuto PersonaxAuto { get; set; }
    }
}
