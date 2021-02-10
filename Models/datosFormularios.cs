using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Actas.Models
{
    public class datosFormularios
    {
        public string calleInf_form { get; set; }
        public string fechaInf_form { get; set; }
        public string horaInf_form { get; set; }
        public int numeroActa_form { get; set; }
        public int inspectorId_form { get; set; }
        public string observacionesInf_form { get; set; }
        public string tipoAuto_form { get; set; }
        public string modeloAuto_form { get; set; }
        public string marcaAuto_form { get; set; }
        public string colorAuto_form { get; set; }
        public int id_automotor_form { get; set; } = 0;
        public string patenteAuto_form { get; set; }
        public bool retieneVehiculo_form { get; set; }
        public bool retieneLicencia_form { get; set; }


    }
}