using Actas.AccesoDatos;
using Actas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Actas.Controllers
{
    public class RegistroController : Controller
    {
        public HtmlString GuardarFormulario(datosFormularios datosFormulario)
        { 
            var resultado = FormAD.GestionarActa(datosFormulario);
            var mensaje = resultado ? "Guardado Exitoso" : "Error al intentar realizar el guardado. Revise los datos";
            var response = new { success = resultado, Message = mensaje};


            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }
    }
}