using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Actas.Models
{
    public class ActasModel
    {
        public int id_acta { get; set; }
        public int nroActa { get; set; }
        public bool estadoActa { get; set; }
        public string fechaAlta { get; set; }
        public int id_inspector { get; set; }
        public int id_infraccionxActa { get; set; }

        public int id_automotor { get; set; }
        public Automotore automotor { get; set; }

        public int personaxacta { get; set; }
        public string calleInf { get; set; }
        public string fechaInf { get; set; }
        public string horaInf { get; set; }
        public string observacionesInf { get; set; }
        public int id_personaxauto { get; set; }
        public string direccionIP { get; set; }
        public bool retieneVehiculo { get; set; }
        public bool retieneLicencia { get; set; }






        public List<CodInf> codInfraccion { get; set; }
        public Inspectore inspector { get; set; }

        public PersonasTemp PersonasTemp { get; set; }
        public int infraccionxActa { get; set; }
        public List<PersonasTemp> listaPersonasTemp { get; set; }

    }
}
