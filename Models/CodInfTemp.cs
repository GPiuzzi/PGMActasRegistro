using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Actas.Models
{
    public class CodInfTemp
    {
      public int id_codInfT { get; set; }
        public string codigoT { get; set; }
        public string conceptoT { get; set; }
        public string calificacionT { get; set; }
        public string articuloT { get; set; }
        public string incisoT { get; set; }
        public string estadoT { get; set; }
        public string id_grupoT { get; set; }
        public string concepto_largoT { get; set; }
        public string id_normativaT { get; set; }
        public double montoT { get; set; }
    }
}