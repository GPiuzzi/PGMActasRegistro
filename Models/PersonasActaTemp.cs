using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Actas.Models
{
    public class PersonasActaTemp
    {
        public int id_personasTemp { get; set; }
        public int numeroActaTemp { get; set; }
        public int respLegalTemp { get; set; }

        public bool eliminarDisponible { get; set; }
    }
}