using Actas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Actas.AccesoDatos
    {
        public class PersonasAD
        {

            public static bool boolCreatePersona(Persona persona)
            {
                bool resultado = false;
                string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
                SqlConnection cn = new SqlConnection(cadenaConexion);
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    string consulta = "INSERT INTO Personas VALUES(@nombre, @apellido, @calle, @nrocalle, @localidad, @documentos, @nroDoc)";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@nombre", persona.nombre);
                    cmd.Parameters.AddWithValue("@apellido", persona.apellido);
                    cmd.Parameters.AddWithValue("@calle", persona.calle);
                    cmd.Parameters.AddWithValue("@nrocalle", persona.nrocalle);
                    cmd.Parameters.AddWithValue("@localidad", persona.localidad);
                    cmd.Parameters.AddWithValue("@documentos", persona.documentos);
                    cmd.Parameters.AddWithValue("@nroDoc", persona.nroDoc);

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = consulta;


                    cn.Open();
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                    resultado = true;


                }
                catch (Exception)
                { throw; }
                finally
                {
                    cn.Close();

                }

                return resultado;
            }
        }
}