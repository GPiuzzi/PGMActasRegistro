using Actas.AccesoDatos;
using Actas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace Actas.Controllers
{
    public class ValidacionesController : Controller
    {



        public HtmlString VerifyActa(int nroActa)
        {

            var success = ActaAD.obtenerNroActa(nroActa);
            var mensaje = success ? " El número de acta "+nroActa+" ya está en uso, por favor seleccione otro. " : " Número de acta disponible. ";
            //var mensaje2 = "EL número de acta debe encontrarse dentro del rango correspondiente a cada inspector. Revise los datos";

            var response = new { success = success, Message = mensaje };
            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            return new HtmlString(jSerializer.Serialize(response));

        }

        public HtmlString VerifyPersona(string nroDocT)
        {
            var success = false;
            var mensaje = " ¡Número de documento válido!";
         var existeTemp = ActaAD.obtenerPersonaT(nroDocT);
            var existeBD = ActaAD.obtenerPersonaTdeBD(nroDocT);
            if (existeTemp)
            {   success = true;
                mensaje = success ? " El número de documento " + nroDocT + " ya ha ingresado. Ingrese uno diferente. " : " Número de documento disponible. ";
             

            }
            else if (existeBD) 
            { success = true;  mensaje = success ? " Una persona con el documento "+ nroDocT +" ya existe en el sistema. Búsquela desde la base de datos o ingrese uno diferente. " : " Número de documento disponible. "; 
            }
            var response = new { success = success, Message = mensaje };
            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }
    
        public HtmlString VerifyPatente(string patenteAuto)
        {

            var success = ActaAD.obtenerNroPatente(patenteAuto);
            var mensaje = success ? " El número de patente " + patenteAuto.ToUpper() + " ya existe. Seleccionela desde el botón 'buscar' para cargar los datos de manera automática. " : " Número de patente disponible. ";
            var response = new { success = success, Message = mensaje };
            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }

        public HtmlString verificarRangoInsp(int id_inspector, int nroActa)
        {
            var success = false;
            var rangoMin = ActaAD.rangoMinInsp(id_inspector);
            var rangoMax = ActaAD.rangoMaxInsp(id_inspector);
            if ((nroActa >= rangoMin) && (nroActa <= rangoMax))
            {
                success = true;
            }

            var mensaje = success ? " Acta dentro del rango del inspector. " : " El acta nro: "+ nroActa +" se encuentra fuera del rango del inspector.";
            var response = new { success = success, Message = mensaje };
            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }

        //validacion roles 
        public HtmlString verificarTitulareInfractor(int nroActa)
        {

            var success = (ActaAD.hayTitular(nroActa).Count() > 0 && ActaAD.hayInfractor(nroActa).Count() == 1); ;
                var mensaje = success ? "Ha cargado al menos una infracción. Ya puede registrar el acta " : " Debe ingresar al menos una infracción para continuar";
                var response = new { success = success, Message = mensaje };
                System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                return new HtmlString(jSerializer.Serialize(response));
            }




          

        //prueba
        public HtmlString verificarUnCodInf(int nroActa)
        {

            var success = (ActaAD.hayUnaInfraccion(nroActa).Count()>0);
            var mensaje = success ? "Ha cargado al menos una infracción. Ya puede registrar el acta " : " Debe ingresar al menos una infracción para continuar";
            var response = new { success = success, Message = mensaje };
            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }
        //prueba

        /*public PartialViewResult seleccionarInf(string id_automotor)
                {
                    List<Persona> listaper = ActaAD.verListaPersonasAutomotor(id_automotor);

                    return PartialView("_PVpersonasBusqueda", listaper);

                }  */


        [HttpPost]
        public ActionResult seleccionarInf(int numeroActa, int idCodigo)
        {
            List<CodInf> listaTempI = ActaAD.verListaInfraccTemp(numeroActa, idCodigo);
            return PartialView("_PVlistaInfraccionesUsadas", listaTempI);
        }

     

        [HttpPost]
        public ActionResult DeleteInf(int idCodigoElim, int nroActa)
        {
            bool resultado = ActaAD.EliminarInf(idCodigoElim, nroActa);
            if (resultado)
            {
                  List<CodInf> listaTempD = ActaAD.verListaInfraccFinal(nroActa);
            return PartialView("_PVlistaInfraccionesUsadas", listaTempD);

            }
            else
            {
                return null;

            }
        }

        [HttpPost]
        public ActionResult DeletePerT(int idPersonaTemp, int numeroActa)
        {
            bool resultado = ActaAD.EliminarPerT(idPersonaTemp);
            if (resultado)
            {
                List<PersonasTemp> listaTemp = ActaAD.verListaPersonasT(numeroActa);
               
                return PartialView("_PVperTBusqueda", listaTemp);


            }
            else
            {
                return null;

            }
        }
        public bool eliminartablas()
        {
    
            bool resultado = ActaAD.EliminarTablasxNuevo();
            if (resultado) {
            return true;
             
            } else 
            { return false; }
        }


        public HtmlString ObtenerResponsabilidadesPersonasTemp(int idPersona) {
            //(List<PersonasActaTemp> personaResp - responsabilidades
            var listado = FormAD.GetResponsabilidadesPersonasTemp(idPersona);
            var response = new { responsabilidades = listado };
            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }


        //VISTA PERSONAS

        /// <summary>
        /// ESTE METODO ES EL ENCARGADO DE ARMAR LA VISTA CUANDO VIENE LA INFORMACION DEL AUTOMOTOR
        /// </summary>
        /// <param name="id_automotor"></param>
        /// <param name="numeroActa"></param>
        /// <returns></returns>
        public PartialViewResult BusqPersonasAutomotor(string id_automotor, int numeroActa)
        {
            List<Persona> listaper = ActaAD.verListaPersonasAutomotor(id_automotor, numeroActa);
            return PartialView("_PVpersonasBDselecc", listaper);

        }

        /// <summary>
        /// ESTE METODO DEVUELVE EL LISTADO DE PERSONAS DE LA BD
        /// </summary>
        /// <param name="busquedaPer">SIRVE PARA FILTRAR LA LISTA</param>
        /// <returns></returns>
       [HttpPost]
        public PartialViewResult BusqPersonas(string busquedaPer)
        {
            busquedaPer = busquedaPer.ToLower().Trim();
            List<Persona> listaper = ActaAD.verListaPersonas();
            if (!string.IsNullOrEmpty(busquedaPer))
            {
                var per = busquedaPer;
                listaper = listaper.Where(s => s.nombre.ToLower().Contains(busquedaPer) || s.apellido.ToLower().Contains(busquedaPer) || (s.nombre.ToLower() + " " + s.apellido.ToLower()).Contains(busquedaPer) || s.calle.ToLower().Contains(busquedaPer) || s.localidad.ToLower().Contains(busquedaPer) || s.nrocalle.ToLower().Contains(busquedaPer) || s.nroDoc.ToLower().Contains(busquedaPer) || s.documentos.ToLower().Contains(busquedaPer) || s.id_personas.ToString() == per).ToList();
            }
            return PartialView("_PVpersonasSeleccion", listaper);
        }
        /// <summary>
        /// ESTE METODO REGISTRAR UNA PERSONA EN LAS TABLAS ACTAPERSONATEMP Y DEVUELVE EL LISTADO ACTUALIZADO
        /// </summary>
        /// <param name="numeroActa"></param>
        /// <param name="idpersona"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult seleccionarPer(int numeroActa, int idpersona)
        {
            List<Persona> listaP = ActaAD.verListaSeleccionP(numeroActa, idpersona);
            return PartialView("_PVpersonasBDselecc", listaP);
        }

        /// <summary>
        /// ESTE METODO DEVUELVE LA VISTA PARA CARGAR UNA NUEVA PERSONA
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearPersona()
        {
            return PartialView("_PVcreatePersonaTemporal");
        }
        /// <summary>
        /// ESTE METODO REGISTRA UNA PERSONA NUEVA
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CrearPersona(PersonasTemp x)
        {
            bool resultado = ActaAD.CrearPersonaTemp(x);
            return resultado;
        }

        /// <summary>
        /// ESTE METODO DEVUELVE EL LISTADO DE LAS PERSONAS REGISTARDAS AL ACTA
        /// </summary>
        /// <param name="numeroActa"></param>
        /// <returns></returns>
        public PartialViewResult tablaPerNueva(int numeroActa)
        {
            List<Persona> listaTemp = ActaAD.obtenerListadoPersonasActa(numeroActa);
            return PartialView("_PVpersonasBDselecc", listaTemp);
        }

        /// <summary>
        /// ESTE METODO ELIMINA LA PERSONA SELeCCIONaDA
        /// </summary>
        /// <param name="idPersona"></param>
        /// <param name="nroActa"></param>
        /// <returns></returns>
            [HttpPost]
        public ActionResult DeletePersona(int idPersona, int nroActa)
        {
            bool resultado = ActaAD.EliminarPers(idPersona, nroActa);
            if (resultado)
            {
                List<Persona> listaTempD = ActaAD.obtenerListadoPersonasActa(nroActa);
                return PartialView("_PVpersonasBDselecc", listaTempD);

            }
            else
            {
                return null;

            }
        }

        /// <summary>
        /// ESTE METODO ES EL ENCARGADO DE ACTUALIZAR LAS RESPONSABILIDADES
        /// </summary>
        /// <param name="personaResp"></param>
        /// <returns></returns>
        public HtmlString GuardarResponsabilidadesPersonasTemp(List<PersonasActaTemp> personaResp)
        {

            var success = FormAD.RegistrarPersonasActaTemp(personaResp); //realizar guardado personax
            var mensaje = success ? " Las responsabilidades de la persona fueron guardadas correctamente" : " Error al intentar guardar las responsabilidades";
            var response = new { success = success, Message = mensaje };
            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }

    }
}
