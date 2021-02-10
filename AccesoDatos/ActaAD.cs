using Actas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Actas.AccesoDatos
{

    public class ActaAD
    {
        /// <summary>
        /// ESTE METODO DEVUELVE EL LISTADO DE PERSONAS TEMPORALES ASIGNADOS AL ACTA EN CURSO
        /// </summary>
        /// <returns></returns>
        public static List<Persona> obtenerListadoPersonasActaTemp(int numeroActa)
        {

            List<Persona> lista = new List<Persona>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInf = "SELECT DISTINCt PA.*,it.eliminarDisponible FROM Personas PA JOIN PersonasxActaTemp it ON pa.id_personas = it.id_personasTemp WHERE it.numeroActaTemp =" + numeroActa;
                // comando.Parameters.Clear();

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccInf;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Persona x = new Persona();

                        x.id_personas = (int)lector["id_personas"];
                        x.documentos = lector["documentos"].ToString();
                        x.nroDoc = lector["nroDoc"].ToString();
                        x.nombre = lector["nombre"].ToString();
                        x.apellido = lector["apellido"].ToString();
                        x.calle = lector["calle"].ToString();

                        x.nrocalle = lector["nrocalle"].ToString();
                        x.localidad = lector["localidad"].ToString();
                        x.codPostal = (int)lector["codPostal"];
                        x.eliminarDisponible = (bool)lector["eliminarDisponible"];



                        lista.Add(x);
                    }
                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return lista;

        }
        /// <summary>
        /// ESTE METODO DEVUELVE EL LISTADO DE PERSONAS ASIGNADA AL ACTA
        /// </summary>
        /// <returns></returns>
        public static List<Persona> obtenerListadoPersonasTempActaTemp(int numeroActa)
        {

            List<Persona> lista = new List<Persona>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInf = "SELECT DISTINCt PA.*,it.eliminarDisponible FROM PersonasTemp PA JOIN PersonasxActaTemp it ON pa.id_personasT = it.id_personasTemp WHERE it.numeroActaTemp =" + numeroActa;
                // comando.Parameters.Clear();

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccInf;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Persona x = new Persona();


                        x.id_personas = (int)lector["id_personasT"];
                        x.nroDoc = lector["nroDocT"].ToString();
                        x.documentos = lector["documentosT"].ToString();
                        x.nombre = lector["nombreT"].ToString();
                        x.apellido = lector["apellidoT"].ToString();
                        x.calle = lector["calleT"].ToString();
                        x.nrocalle = lector["nrocalleT"].ToString();
                        x.localidad = lector["localidadT"].ToString();
                        x.codPostal = (int)lector["codPostalT"];
                        x.eliminarDisponible = (bool)lector["eliminarDisponible"];



                        lista.Add(x);
                    }
                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return lista;

        }
        /// <summary>
        /// ESTE METODO DEVUELVE EL LISTADO DE PERSONAS QUE SE ASIGNARON AL ACTA
        /// </summary>
        /// <param name="numeroActa"></param>
        /// <returns></returns>
        public static List<Persona> obtenerListadoPersonasActa(int numeroActa) {
            List<Persona> lista = new List<Persona>();
            try
            {
                var listaPersonasTemp = obtenerListadoPersonasTempActaTemp(numeroActa);
                var listaPersonas = obtenerListadoPersonasActaTemp(numeroActa);

                lista.AddRange(listaPersonasTemp);
                lista.AddRange(listaPersonas);
            }
            catch (Exception e)
            {

                throw;
            }
            return lista;
        }



        /// <summary>
        /// ESTE METODO REGISTRA LA PERSONA AL ACTA Y DEVUELVE EL LISTADO AACTUALIZADO DE LAS PERSONAS VINCULADAS AL ACTA
        /// </summary>
        /// <param name="numeroActa"></param>
        /// <param name="idpersona"></param>
        /// <returns></returns>
        public static List<Persona> verListaSeleccionP(int numeroActa, int idpersona)
        {

            List<Persona> lista = new List<Persona>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                RegistrarPerdeBD(numeroActa, idpersona,false);
                lista = obtenerListadoPersonasActa(numeroActa);
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return lista;
        }


        /// <summary>
        /// ESTE METODO DEVUELVE TODAS LAS PERSONAS REGISTRADAS
        /// </summary>
        /// <returns></returns>
        public static List<Persona> verListaPersonas()
        {

            List<Persona> listaPersonas = new List<Persona>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInsp = "SELECT * FROM Personas p WHERE p.id_personas NOT IN(SELECT pia.id_personasTemp FROM PersonasxActaTemp pia)"; 
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccInsp;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Persona per = new Persona();
                        per.id_personas = (int)lector["id_personas"];
                        per.documentos = lector["documentos"].ToString();
                        per.nroDoc = lector["nroDoc"].ToString();
                        per.nombre = lector["nombre"].ToString();
                        per.apellido = lector["apellido"].ToString();
                        per.calle = lector["calle"].ToString();
                        per.nrocalle = lector["nrocalle"].ToString();
                        per.localidad = lector["localidad"].ToString();
                        per.codPostal = (int)lector["codPostal"];

                        listaPersonas.Add(per);
                    }
                }
            }

            catch (Exception)
            { throw; }
            finally
            {
                con.Close();

            }

            return listaPersonas;

        }


        public static bool EliminarTablasxNuevo()
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "DELETE FROM PersonasxActaTemp DELETE FROM PersonasTemp DELETE FROM InfraccionxActaTemp";
                cmd.Parameters.Clear();
               
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception e)
            {
                resultado = false;
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }





        /// <summary>
        /// ESTE METODO REGISTRAR UNA PERSONA TEMPORAL Y LA RELACIONA AL ACTA
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool CrearPersonaTemp(PersonasTemp x)
        {
            bool resultado = false;
            try
            {
               var idPersona = GuardarDatosPersonaTemp(x);
                if (idPersona != 0) {
                   resultado = GuardarPersonaActaTemp(idPersona, x.NroActaT);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return resultado;
        }
        /// <summary>
        /// ESTE METODO GUARDA LOS DATOS DE LA PERSONA TEMPORAL
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int GuardarDatosPersonaTemp(PersonasTemp x)
        {
            int resultado = 0;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO PersonasTemp VALUES(@nombreT, @apellidoT, @calleT, @nrocalleT, @localidadT, @documentosT, @nroDocT, @codPostalT); SET @ID = SCOPE_IDENTITY();";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@nombreT", x.nombreT);
                cmd.Parameters.AddWithValue("@apellidoT", x.apellidoT);
                cmd.Parameters.AddWithValue("@calleT", x.calleT);
                cmd.Parameters.AddWithValue("@nrocalleT", x.nrocalleT);
                cmd.Parameters.AddWithValue("@localidadT", x.localidadT);
                cmd.Parameters.AddWithValue("@codPostalT", x.codPostalT);
                cmd.Parameters.AddWithValue("@documentosT", x.documentosT);
                cmd.Parameters.AddWithValue("@nroDocT", x.nroDocT);
                cmd.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                var idPersona = (int)cmd.Parameters["@ID"].Value;
                resultado = idPersona;
            }
            catch (Exception e)
            {
                resultado = 0;
                throw;
            }
            finally
            {
                cn.Close();
            }
            return resultado;
        }
        /// <summary>
        /// ESTE METODO REGISTRAR UNA PERSONA AL ACTA
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool GuardarPersonaActaTemp(int idPersona, int NroActaT)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO PersonasxActaTemp VALUES(@id_personasTemp, @numeroActaTemp, @respLegalTemp,@eliminarDisponible)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_personasTemp", idPersona);
                cmd.Parameters.AddWithValue("@numeroActaTemp", NroActaT);
                cmd.Parameters.AddWithValue("@respLegalTemp", 1);
                cmd.Parameters.AddWithValue("@eliminarDisponible", false);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception e)
            {
                resultado = false;
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }



        public static bool RegistrarPerdeBD(int numeroActa, int idpersona,bool eliminarDisponible)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta2 = "INSERT INTO PersonasxActaTemp VALUES(@id_personasTemp,@numeroActaTemp,@respLegalTemp,@eliminarDisponible);";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@id_personasTemp", idpersona);
                cmd.Parameters.AddWithValue("@numeroActaTemp", numeroActa);
                cmd.Parameters.AddWithValue("@respLegalTemp", 1);
                cmd.Parameters.AddWithValue("@eliminarDisponible", eliminarDisponible);
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta2;


                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;

            }
            catch (Exception e)
            {
                resultado = false;
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }









        //lista de código de infracciones
        public static List<CodInf> verListaCodInfracciones()
        {

            List<CodInf> listaInfr = new List<CodInf>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccCodInfrac = "SELECT * FROM CodInf ci WHERE ci.id_codInf NOT IN " +
                    " (SELECT id_codInfTemp FROM InfraccionxActaTemp);";
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccCodInfrac;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        CodInf infra = new CodInf();
                        infra.id_codInf = (int)lector["id_codInf"];
                        infra.id_grupo = (int)lector["id_grupo"];
                        infra.id_normativa = lector["id_normativa"].ToString();
                        infra.codigo = lector["codigo"].ToString();
                        infra.concepto = lector["concepto"].ToString();
                        infra.concepto_largo = lector["concepto_largo"].ToString();
                        infra.calificacion = lector["calificacion"].ToString();
                        infra.articulo = lector["articulo"].ToString();
                        infra.inciso = lector["inciso"].ToString();
                        infra.estado = lector["estado"].ToString();
                        listaInfr.Add(infra);
                    }
                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return listaInfr;

        }







        //lista de código de inspectores 


        public static List<Inspectore> verListaInspectores()
        {

            List<Inspectore> listaInsp = new List<Inspectore>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInsp = "SELECT * FROM Inspectores";
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccInsp;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Inspectore insp = new Inspectore();
                        insp.id_inspector = int.Parse(lector["id_inspector"].ToString());
                        insp.nombreInsp = lector["nombreInsp"].ToString();
                        insp.apellidoInsp = lector["apellidoInsp"].ToString();
                        insp.direccionIPinsp = lector["direccionIPinsp"].ToString();
                        insp.rangoMinActas = int.Parse(lector["rangoMinActas"].ToString());
                        insp.rangoMaxActas = int.Parse(lector["rangoMaxActas"].ToString());

                        listaInsp.Add(insp);
                    }
                }
            }

            catch (Exception)
            { throw; }
            finally
            {
                con.Close();

            }

            return listaInsp;

        }
        //Ver lista de Automotores

        public static List<Automotore> verListadoAutomotores()
        {

            List<Automotore> listadoAutomotores = new List<Automotore>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string selectAutomotor = "SELECT * FROM Automotores;";
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = selectAutomotor;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        Automotore a = new Automotore();
                        a.id_automotor = int.Parse(lector["id_automotor"].ToString());
                        a.tipoAuto = lector["tipoAuto"].ToString();
                        a.modeloAuto = lector["modeloAuto"].ToString();
                        a.colorAuto = lector["colorAuto"].ToString();
                        a.patenteAuto = lector["patenteAuto"].ToString();
                        a.marcaAuto = lector["marcaAuto"].ToString();

                        listadoAutomotores.Add(a);
                    }
                }
            }

            catch (Exception)
            { throw; }
            finally
            {
                con.Close();

            }

            return listadoAutomotores;

        }

        //Ver lista de personas nuevas que se agregan, además de las existentes, o si es un auto nuevo
        public static List<PersonasTemp> verListaPersonasT(int numeroActa)
        {

            List<PersonasTemp> listaPersonasT = new List<PersonasTemp>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInsp = "SELECT [id_personasT],[documentosT], [nroDocT], [nombreT], [apellidoT],[calleT],[nrocalleT],[localidadT],[codPostalT] FROM PersonasTemp";
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccInsp;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        PersonasTemp perT = new PersonasTemp();
                        perT.id_personasT = (int)lector["id_personasT"];
                        perT.documentosT = lector["documentosT"].ToString();
                        perT.nroDocT = lector["nroDocT"].ToString();

                        perT.nombreT = lector["nombreT"].ToString();
                        perT.apellidoT = lector["apellidoT"].ToString();
                        perT.calleT = lector["calleT"].ToString();
                        perT.nrocalleT = lector["nrocalleT"].ToString();
                        perT.localidadT = lector["localidadT"].ToString();
                        perT.codPostalT = (int)lector["codPostalT"];
                        RegistrarPerdeBD(numeroActa, perT.id_personasT,false);


                        listaPersonasT.Add(perT);
                    }
                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return listaPersonasT;

        }

        /// <summary>
        /// Metodo que trae el listado de personas relacionadas al vehiculo y lo asigna al acta
        /// </summary>
        /// <param name="id_automotor"></param>
        /// <param name="numeroActa"></param>
        /// <returns></returns>
        public static List<Persona> verListaPersonasAutomotor(string id_automotor, int numeroActa)
        {

            List<Persona> listaPersonas = new List<Persona>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccPerxAuto = "SELECT p.* FROM Personas p JOIN PersonaxAuto pa ON pa.id_persona = p.id_personas WHERE pa.id_auto =" + id_automotor;
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccPerxAuto;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {

                        Persona per = new Persona();
                        per.id_personas = (int)lector["id_personas"];
                        per.nombre = lector["nombre"].ToString();
                        per.apellido = lector["apellido"].ToString();
                        per.calle = lector["calle"].ToString();
                        per.nrocalle = lector["nrocalle"].ToString();
                        per.localidad = lector["localidad"].ToString();
                        per.codPostal = (int)lector["codPostal"];
                        per.documentos = lector["documentos"].ToString();
                        per.nroDoc = lector["nroDoc"].ToString();
                        per.eliminarDisponible = true;
                        RegistrarPerdeBD(numeroActa, per.id_personas,true);
                        listaPersonas.Add(per);
                    }
                }
            }

            catch (Exception)
            { throw; }
            finally
            {
                con.Close();

            }

            return listaPersonas;

        }
        /// <summary>
        /// ESTE METODO ELIMINA LA PERSONA SELECCIONADA
        /// </summary>
        /// <param name="idPersona"></param>
        /// <param name="nroActa"></param>
        /// <returns></returns>
        public static bool EliminarPers(int idPersona, int nroActa)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "DELETE FROM PersonasxActaTemp WHERE id_personasTemp = " + idPersona + " AND numeroActaTemp = " + nroActa;
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }
        //Eliminar personas temporales 

        public static bool EliminarPerT(int idPersonaTemp)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "DELETE FROM PersonasTemp WHERE id_personasT = @id_personasT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_personasT", idPersonaTemp);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }
        //prueba verificacion 



        public static List<PersonasActaTemp> hayTitular(int nroActa)
        {

            List<PersonasActaTemp> listaVerifTit = new List<PersonasActaTemp>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string buscarTit = "SELECT [respLegalTemp] FROM PersonasxActaTemp WHERE respLegalTemp= " + 1;
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = buscarTit;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {

                        PersonasActaTemp x = new PersonasActaTemp();
                      x.respLegalTemp = (int)lector["respLegalTemp"];
                    
                        listaVerifTit.Add(x);
                       }

                }
           }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }


            return listaVerifTit;
        }

        public static List<PersonasActaTemp> hayInfractor(int nroActa)
        {

            List<PersonasActaTemp> listaVerifTit = new List<PersonasActaTemp>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string buscarTit = "SELECT [respLegalTemp] FROM PersonasxActaTemp WHERE respLegalTemp= " + 2;
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = buscarTit;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {

                        PersonasActaTemp x = new PersonasActaTemp();
                     
                        x.respLegalTemp = (int)lector["respLegalTemp"];
                 
                        listaVerifTit.Add(x);
                    }

                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }


            return listaVerifTit;
        }





        public static List<InfrxActaTemp> hayUnaInfraccion(int nroActa)
        {
         
            List<InfrxActaTemp> listaVerifInfracc = new List<InfrxActaTemp>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();

           
                string buscarInfracc= "SELECT * FROM InfraccionxActaTemp WHERE nroActaTemp= " + nroActa;
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = buscarInfracc;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {

                        InfrxActaTemp x = new InfrxActaTemp();

                    
                        x.id_codInfTemp = (int)lector["id_codInfTemp"];
                        x.nroActaTemp = (int)lector["nroActaTemp"];
                         listaVerifInfracc.Add(x);
                     
              
                      
                    }
                
                    
                }

            } 

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }


            return listaVerifInfracc;
        }


















        //fin prueba verificacion 



        public static bool EliminarInf(int idCodigoElim, int nroActa)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "DELETE FROM InfraccionxActaTemp WHERE id_codInfTemp = " + idCodigoElim + " AND nroActaTemp = " + nroActa;
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }
        public static bool selecInfrac(int idcodigo)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM CodInf WHERE codigo =" + idcodigo;
                //string consulta2= "INSERT INTO [codigoT],[conceptoT],[calificacionT],[articuloT],[incisoT],[estadoT],[id_grupoT],[concepto_largoT],[id_normativaT] SELECT VALUES FROM CodInf [codigo],[concepto],[calificacion],[articulo],[inciso],[estado],[id_grupo],[concepto_largo],[id_normativa];" ; //revisar
                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }


        public static List<CodInfTemp> verListaSeleccionTemp(int id_codInfTemp)
        {
            List<CodInfTemp> listaInfrT = new List<CodInfTemp>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccCodInfrac = "SELECT * FROM CodInfTemp";
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccCodInfrac;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        CodInfTemp infra = new CodInfTemp();
                        infra.id_codInfT = (int)lector["id_codInfT"];
                        infra.id_grupoT = lector["id_grupoT"].ToString();
                        infra.id_normativaT = lector["id_normativaT"].ToString();
                        infra.codigoT = lector["codigoT"].ToString();
                        infra.conceptoT = lector["conceptoT"].ToString();
                        infra.concepto_largoT = lector["concepto_largoT"].ToString();
                        infra.calificacionT = lector["calificacionT"].ToString();
                        infra.articuloT = lector["articuloT"].ToString();
                        infra.incisoT = lector["incisoT"].ToString();
                        infra.estadoT = lector["estadoT"].ToString();
                        listaInfrT.Add(infra);
                    }
                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return listaInfrT;

        }



        public static List<CodInf> verListaInfraccFinal(int nroActa)
        {

            List<CodInf> lista = new List<CodInf>(nroActa);
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInf = "SELECT * FROM CodInf CI JOIN InfraccionxActaTemp it ON ci.id_codInf = it.id_codInfTemp WHERE it.nroActaTemp = " + nroActa;
                // comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccInf;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        CodInf x = new CodInf();
                        x.id_codInf = (int)lector["id_codInf"];
                        x.codigo = lector["codigo"].ToString();
                        x.concepto = lector["concepto"].ToString();
                        x.calificacion = lector["calificacion"].ToString();

                        x.articulo = lector["articulo"].ToString();
                        x.inciso = lector["inciso"].ToString();
                        x.estado = lector["estado"].ToString();
                        x.id_grupo = (int)lector["id_grupo"];
                        x.concepto_largo = lector["concepto_largo"].ToString();
                        x.id_normativa = lector["id_normativa"].ToString();



                        lista.Add(x);
                    }
                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return lista;

        }








        public static List<CodInf> verListaInfraccTemp(int numeroActa, int idCodigo)
        {

            List<CodInf> lista = new List<CodInf>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            RegistrarInfraccionActaTemp(numeroActa, idCodigo);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInf = "SELECT * FROM CodInf CI JOIN InfraccionxActaTemp it ON ci.id_codInf = it.id_codInfTemp WHERE it.nroActaTemp = " + numeroActa;
                // comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = seleccInf;
                con.Open();
                comando.Connection = con;
                SqlDataReader lector = comando.ExecuteReader();
                if (lector != null)
                {
                    while (lector.Read())
                    {
                        CodInf x = new CodInf();
                        x.id_codInf = (int)lector["id_codInf"];
                        x.codigo = lector["codigo"].ToString();
                        x.concepto = lector["concepto"].ToString();
                        x.calificacion = lector["calificacion"].ToString();

                        x.articulo = lector["articulo"].ToString();
                        x.inciso = lector["inciso"].ToString();
                        x.estado = lector["estado"].ToString();
                        x.id_grupo = (int)lector["id_grupo"];
                        x.concepto_largo = lector["concepto_largo"].ToString();
                        x.id_normativa = lector["id_normativa"].ToString();



                        lista.Add(x);
                    }
                }
            }

            catch (Exception e)
            { throw; }
            finally
            {
                con.Close();

            }

            return lista;

        }

        public static bool RegistrarInfraccionActaTemp(int numeroActa, int idCodigo)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta1 = "INSERT INTO InfraccionxActaTemp VALUES(@id_codInfTemp,@nroActaTemp);";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@id_codInfTemp", idCodigo);
                cmd.Parameters.AddWithValue("@nroActaTemp", numeroActa);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta1;


                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;

            }
            catch (Exception e)
            {
                resultado = false;
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }

        public static bool obtenerPersonaT(string nroDocT)
        {
            bool resultado = false; //inicia en falso, inicialmente se asume que el acta no coincide
            PersonasTemp per = new PersonasTemp();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string verifDoc = "SELECT nroDocT" +
                " FROM PersonasTemp " +
                " WHERE nroDocT = @nroDocT;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nroDocT", nroDocT);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = verifDoc;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null) //si devuelve algo se convierte en true, por ende el nro de acta existe
                {
                    while (dataReader.Read())
                    {

                        per.nroDocT = (dataReader["nroDocT"].ToString());
                        resultado = true;

                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }
        public static bool obtenerPersonaTdeBD(string nroDoc)
        {
            bool resultado = false; //inicia en falso, inicialmente se asume que el acta no coincide
            Persona perBD = new Persona();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string verifDoc = "SELECT nroDoc" +
                " FROM Personas " +
                " WHERE nroDoc = @nroDoc;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nroDoc", nroDoc);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = verifDoc;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null) //si devuelve algo se convierte en true, por ende el nro de acta existe
                {
                    while (dataReader.Read())
                    {

                        perBD.nroDoc = (dataReader["nroDoc"].ToString());
                        resultado = true;

                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        public static bool obtenerNroActa(int nroActa)
        {
            bool resultado = false; //inicia en falso, inicialmente se asume que el acta no coincide
            ActasModel acta = new ActasModel();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string seleccionarActa = "SELECT nroActa" +
                " FROM ActasTabla " +
                " WHERE nroActa = @nroActa;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nroActa", nroActa);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = seleccionarActa;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null) //si devuelve algo se convierte en true, por ende el nro de acta existe
                {
                    while (dataReader.Read())
                    {

                        acta.nroActa = int.Parse(dataReader["nroActa"].ToString());
                        resultado = true;

                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        public static int rangoMaxInsp(int id_inspector)
        {
            int rMax = 0;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string rango = "SELECT * FROM Inspectores WHERE id_inspector = @id_inspector;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_inspector", id_inspector);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = rango;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {

                        rMax = int.Parse(dataReader["rangoMaxActas"].ToString());

                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return rMax;
        }

        public static int rangoMinInsp(int id_inspector)
        {
            int rMin = 0;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string rango = "SELECT * FROM Inspectores WHERE id_inspector = @id_inspector;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_inspector", id_inspector);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = rango;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {

                        rMin = int.Parse(dataReader["rangoMinActas"].ToString());

                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return rMin;
        }


        public static bool obtenerNroPatente(string patenteAuto)
        {
            bool resultado = false; //inicia en falso, inicialmente se asume que la patente no coincide
            Automotore patente = new Automotore();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string seleccionarPatente = "SELECT patenteAuto" +
                " FROM Automotores " +
                " WHERE patenteAuto = @patenteAuto;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@patenteAuto", patenteAuto);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = seleccionarPatente;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null) //si devuelve algo se convierte en true, por ende el nro de acta existe
                {
                    while (dataReader.Read())
                    {

                        patente.patenteAuto = dataReader["patenteAuto"].ToString();
                        resultado = true;

                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }


        /*  pasar lo que tengo en el objeto datos formulario a los objetos de cada modelo
              tabla modelo actas de la tabla, en el dato formulario mapear uno con el otro 
              actastabla.nro acta = datosformm. nro acta 


        public static bool CrearObjetos(ActasModel x)
        {
            bool resultado = false;
            ActasModel 


        } return resultado;
        */

    }
}