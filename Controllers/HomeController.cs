using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Actas.AccesoDatos;
using Actas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Actas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            bool resultado = ActaAD.EliminarTablasxNuevo();
            if (resultado)
            {
                return View("Index");

            }
            return View("Index");
        }
        public ActionResult RegistrarActa()
        {
            return View("Index");
        }



        public PartialViewResult allCodInfracciones()
        {
            return PartialView("_PVinfracciones");
        }

        public PartialViewResult BusqInfracciones()
        {
            List<CodInf> listaInfraccion = ActaAD.verListaCodInfracciones();
            return PartialView("_PVinfraccionesBusqueda", listaInfraccion);



        }

        [HttpPost]
        public PartialViewResult BusqInfracciones(string busquedaInfracc = "")
        {
                     List<CodInf> listaInf = ActaAD.verListaCodInfracciones();
            if (!string.IsNullOrEmpty(busquedaInfracc))
            {

                var codInf = busquedaInfracc;
                listaInf = listaInf.Where(s => s.id_normativa.Contains(busquedaInfracc) || s.estado.ToLower().Contains(busquedaInfracc.ToLower()) || s.calificacion.ToLower().Contains(busquedaInfracc.ToLower()) ||s.codigo.Contains(busquedaInfracc) || s.inciso.Contains(busquedaInfracc)||s.concepto.ToLower().Contains(busquedaInfracc.ToLower()) ||  s.id_codInf.ToString() == codInf).ToList();

            }



            return PartialView("_PVinfraccionesBusqueda", listaInf);
        }

        [HttpPost]
        public PartialViewResult SelectCodigo(string busquedaInfracc = "")
        {
            List<CodInf> listaInf = ActaAD.verListaCodInfracciones();
            if (!string.IsNullOrEmpty(busquedaInfracc))
            {
                var codInf = Int32.Parse(busquedaInfracc);
                listaInf = listaInf.Where(s => s.id_normativa.Contains(busquedaInfracc) || s.codigo.Contains(busquedaInfracc) || s.concepto.Contains(busquedaInfracc) || s.calificacion.Contains(busquedaInfracc) || s.id_codInf == codInf).ToList();

            }



            return PartialView("_PVinfraccionesBusqueda", listaInf);
        }


        //Get y post para encontrar inspectores
        public PartialViewResult allInspectores()
        {
            return PartialView("_PVinspectores");
        }





        [HttpPost]
        public PartialViewResult BusqInspectores(string busquedaInspec) {
            busquedaInspec = busquedaInspec.ToUpper().Trim();
            List<Inspectore> listaInsp = ActaAD.verListaInspectores();
            if (!string.IsNullOrEmpty(busquedaInspec))
            {
                var insp = busquedaInspec;
                listaInsp = listaInsp.Where(s => s.nombreInsp.Contains(busquedaInspec) || s.apellidoInsp.Contains(busquedaInspec) || (s.nombreInsp+" "+s.apellidoInsp).Contains(busquedaInspec) || s.id_inspector.ToString() == insp).ToList();

            }



            return PartialView("_PVinspectoresBusqueda", listaInsp);
        }



        public PartialViewResult allPersonasT()
        {
            return PartialView("_PVpersonasTemporales");
        }


        [HttpPost]
        public PartialViewResult BusqPersonasT(int numeroActa, string busquedaPerT = "")
        {
            List<PersonasTemp> listaperT = ActaAD.verListaPersonasT(numeroActa);
            if (!string.IsNullOrEmpty(busquedaPerT))
            {
                var per = Int32.Parse(busquedaPerT);
                listaperT = listaperT.Where(s => s.nombreT.Contains(busquedaPerT) || s.apellidoT.Contains(busquedaPerT) || s.calleT.Contains(busquedaPerT) || s.localidadT.Contains(busquedaPerT) || s.nrocalleT.Contains(busquedaPerT) || s.nroDocT.Contains(busquedaPerT) || s.documentosT.Contains(busquedaPerT) || s.id_personasT == per).ToList();

            }



            return PartialView("_PVperTBusqueda", listaperT);
        }
        //ver lista de todas las personas en la BD 

        public PartialViewResult allPersonasBD()
        {
            return PartialView("_PVpersonas");
        }

        

     
      //Búsqueda patentes
        public PartialViewResult allAutomotores()
        {
            return PartialView("_PVpatente");
        }


        [HttpPost]
        public PartialViewResult BusqAutomotor(string busquedaAutomotor = "")
        {
            busquedaAutomotor = busquedaAutomotor.ToUpper().Trim();
            List<Automotore> listaAutomotores = ActaAD.verListadoAutomotores();
            if (!string.IsNullOrEmpty(busquedaAutomotor))
            {
                var auto = busquedaAutomotor;
                listaAutomotores = listaAutomotores.Where(s => s.patenteAuto.Contains(busquedaAutomotor) || s.tipoAuto.Contains(busquedaAutomotor) || s.colorAuto.Contains(busquedaAutomotor) || s.marcaAuto.Contains(busquedaAutomotor) || s.modeloAuto.Contains(busquedaAutomotor) || s.id_automotor.ToString()== auto).ToList();

            }



            return PartialView("_PVpatenteBusqueda", listaAutomotores);
        }




    }
}

