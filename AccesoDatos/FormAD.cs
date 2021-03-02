using Actas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Actas.AccesoDatos
{
    public class FormAD
    {
        //En este metodo mapea los datos del formulario con los modelos de la BD y los guarda
        public static bool GestionarActa(datosFormularios data)
        {
            bool resultado = true;
            try
            {

                if (data.id_automotor_form == 0)
                {
                    ActasModel actaM = new ActasModel();
                    Automotore automotor = new Automotore();
                    automotor.modeloAuto = data.modeloAuto_form;
                    automotor.patenteAuto = data.patenteAuto_form;
                    automotor.marcaAuto = data.marcaAuto_form;
                    automotor.colorAuto = data.colorAuto_form;
                    automotor.tipoAuto = data.tipoAuto_form;
                    actaM.automotor = automotor;
                    data.id_automotor_form = RegistrarAutomotor(actaM);
                }

                ActasModel acta = new ActasModel();
                acta.nroActa = data.numeroActa_form;
                acta.estadoActa = true;
                acta.fechaAlta = (DateTime.Now).ToString();
                acta.id_inspector = data.inspectorId_form;
                acta.id_automotor = data.id_automotor_form;
                acta.calleInf = data.calleInf_form;
                acta.fechaInf = data.fechaInf_form;
                acta.horaInf = data.horaInf_form;
                if (data.observacionesInf_form == null) { acta.observacionesInf = " "; } else { acta.observacionesInf = data.observacionesInf_form; }
               acta.direccionIP = "186.122.181.216";
                acta.retieneVehiculo = data.retieneVehiculo_form;
                acta.retieneLicencia = data.retieneLicencia_form;

                RegistrarActa(acta);
                
                string SqlPersonasTemp = "INSERT INTO Personas(id_personas, nombre, apellido, calle, nrocalle, localidad, documentos, nroDoc, codPostal) SELECT id_personasT, nombreT, apellidoT, calleT, nrocalleT, localidadT, documentosT, nroDocT, codPostalT FROM PersonasTemp";
                string SqlPersonasActaTemp = "INSERT INTO PersonaxActa (id_persona, numeroacta, resplegal) SELECT id_personasTemp, numeroActaTemp, respLegalTemp FROM PersonasxActaTemp";
                string SqlInfraccionesActaTemp = "INSERT INTO InfraccionxActa (id_codInf, nroActa) SELECT id_codInfTemp, nroActaTemp FROM InfraccionxActaTemp";
                string SqlPersonasDelTemp = "DELETE PersonasTemp";
                string SqlPersonasActaDelTemp = "DELETE PersonasxActaTemp";
                string SqlInfraccionesActaDelTemp = "DELETE InfraccionxActaTemp";
  
                ejecutarConsulta(SqlPersonasTemp);
                ejecutarConsulta(SqlPersonasActaTemp);
                ejecutarConsulta(SqlInfraccionesActaTemp);
                ejecutarConsulta(SqlPersonasDelTemp);
                ejecutarConsulta(SqlPersonasActaDelTemp);
                ejecutarConsulta(SqlInfraccionesActaDelTemp);

        
            }
            catch (Exception e)
            {
                resultado = false;
            }
            return resultado;
        }




        public static bool ejecutarConsulta(string sql)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            { //consulta para cargar la infraccion  

                SqlCommand cmd = new SqlCommand();

                string consulta1 = sql;
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta1;

                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception e)
            {
                resultado = false;
            }
            finally
            {
                cn.Close();

            }

            return resultado;
        }



        //Registro del acta 
        private static bool RegistrarActa(ActasModel x)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            { //consulta para cargar la infraccion 
                SqlCommand cmd = new SqlCommand();

                string consulta1 = "INSERT INTO ActasTabla VALUES(@nroActa, @estadoActa, @fechaAlta, @id_inspector, @id_automotor, @calleInf, @fechaInf, @horaInf, @observacionesInf, @retieneLicencia, " +
                    "@direccionIP, @retieneVehiculo)";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@nroActa", x.nroActa);
                cmd.Parameters.AddWithValue("@estadoActa", x.estadoActa);
                cmd.Parameters.AddWithValue("@fechaAlta", x.fechaAlta);
                cmd.Parameters.AddWithValue("@id_inspector", x.id_inspector);
                cmd.Parameters.AddWithValue("@id_automotor", x.id_automotor);
                cmd.Parameters.AddWithValue("@calleInf", x.calleInf);
                cmd.Parameters.AddWithValue("@fechaInf", x.fechaInf);
                cmd.Parameters.AddWithValue("@horaInf", x.horaInf);
                cmd.Parameters.AddWithValue("@observacionesInf", x.observacionesInf);
                cmd.Parameters.AddWithValue("@retieneLicencia", x.retieneLicencia);
                cmd.Parameters.AddWithValue("@direccionIP", x.direccionIP);
                cmd.Parameters.AddWithValue("@retieneVehiculo", x.retieneVehiculo);


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta1;

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

        public static int RegistrarAutomotor(ActasModel x)
        {
            int resultado = 0;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            { //consulta para cargar el auto 
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO Automotores VALUES( @tipoAuto, @modeloAuto, @colorAuto, @patenteAuto, @marcaAuto) SET @ID = SCOPE_IDENTITY();";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@tipoAuto", x.automotor.tipoAuto);
                cmd.Parameters.AddWithValue("@modeloAuto", x.automotor.modeloAuto);
                cmd.Parameters.AddWithValue("@colorAuto", x.automotor.colorAuto);
                cmd.Parameters.AddWithValue("@patenteAuto", x.automotor.patenteAuto);
                cmd.Parameters.AddWithValue("@marcaAuto", x.automotor.marcaAuto);
                cmd.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = Convert.ToInt32(cmd.Parameters["@ID"].Value);
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
     
        public static bool listaCodSeleccionados(CodInfTemp x, CodInf y)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO CodInfTemp VALUES(@codigoT, @conceptoT, @calificacionT, @articuloT, @incisoT, @estadoT, @id_grupoT, @concepto_largoT, @id_normativaT, @montoT) SELECT FROM CodInf VALUES(@codigo, @concepto, @calificacion, @articulo, @inciso, @estado, @id_grupo, @concepto_largo, @id_normativa, @monto)";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@codigoT", y.codigo);
                cmd.Parameters.AddWithValue("@conceptoT", y.concepto);
                cmd.Parameters.AddWithValue("@calificacionT", y.calificacion);
                cmd.Parameters.AddWithValue("@articuloT", y.articulo);
                cmd.Parameters.AddWithValue("@incisoT", y.inciso);
                cmd.Parameters.AddWithValue("@estadoT", y.estado);
                cmd.Parameters.AddWithValue("@id_grupoT", y.id_grupo);
                cmd.Parameters.AddWithValue("@concepto_largoT", y.concepto_largo);
                cmd.Parameters.AddWithValue("@id_normativaT", y.id_normativa);
                //esto es nuevo
                cmd.Parameters.AddWithValue("@montoT", y.monto);


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





        public static bool RegistrarPersonasActaTemp(List<PersonasActaTemp> listaTemp)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            var idPersona = listaTemp.FirstOrDefault().id_personasTemp;
            var resultadoBorrado = EliminarPersonasActaTemp(idPersona);
            SqlConnection cn = new SqlConnection(cadenaConexion);
            cn.Open();
            try
            { //consulta para cargar la infraccion 
                if (resultadoBorrado)
                {
                    foreach (var x in listaTemp)
                    {
                        SqlCommand cmd = new SqlCommand();

                        string consulta1 = "INSERT INTO PersonasxActaTemp VALUES(@id_personasTemp,@numeroActaTemp, @respLegalTemp,@eliminarDisponible);";
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@id_personasTemp", x.id_personasTemp);
                        cmd.Parameters.AddWithValue("@numeroActaTemp", x.numeroActaTemp);
                        cmd.Parameters.AddWithValue("@respLegalTemp", x.respLegalTemp);
                        cmd.Parameters.AddWithValue("@eliminarDisponible", x.eliminarDisponible);

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = consulta1;


                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }

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

        public static List<PersonasActaTemp> GetResponsabilidadesPersonasTemp(int idPersona)
        {

            List<PersonasActaTemp> lista = new List<PersonasActaTemp>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand comando = new SqlCommand();


                string seleccInf = "SELECT * FROM PersonasxActaTemp WHERE id_personasTemp =" + idPersona;
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
                        PersonasActaTemp x = new PersonasActaTemp();

                        x.id_personasTemp = (int)lector["id_personasTemp"];
                        x.numeroActaTemp = (int)lector["numeroActaTemp"];
                        x.respLegalTemp = (int)lector["respLegalTemp"];

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


        public static bool EliminarPersonasActaTemp(int idPersona)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            { //consulta para cargar la infraccion  

                SqlCommand cmd = new SqlCommand();

                string consulta1 = "DELETE PersonasxActaTemp WHERE id_personasTemp = " + idPersona;
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta1;

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


    }
}