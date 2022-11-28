using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;

namespace DA
{
    public class DACertificado
    {
        ConexionBD ConexionBD = new ConexionBD();
        private readonly BE.BECertificado _BECertificado;

        public List<BECertificado> ListarCertificados()
        {
            List<BECertificado> lista = new List<BECertificado>();

            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_Listar_Certificados", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BECertificado obj = new BECertificado()
                    {
                        id = dr.GetInt32(0),
                        apellidos = dr.GetString(1),
                        //nombres = dr.GetString(2),
                        correo = dr.GetString(3),
                        fecha_creacion = dr.GetDateTime(4),
                        estadoCertificado = dr.GetInt32(5),
                        idTipo = dr.GetInt32(6),
                        codigo = dr.GetGuid(7)
                        //usuario_creacion = dr.GetInt32(12),
                        //fecha_modificacion = dr.GetDateTime(13),
                        //usuario_modificacion = dr.GetInt32(14)
                    };
                    //usuario_creacion
                    if (dr.IsDBNull(8))
                    {
                        obj.usuario_creacion = 0;
                    }
                    else
                    {
                        obj.usuario_creacion = dr.GetInt32(8);
                    }
                    //fecha_modificacion
                    if (dr.IsDBNull(9))
                    {
                        obj.fecha_modificacion = DateTime.MinValue;
                    }
                    else
                    {
                        obj.fecha_modificacion = dr.GetDateTime(9);
                    }
                    //usuario_modificacion
                    if (dr.IsDBNull(10))
                    {
                        obj.usuario_modificacion = 0;
                    }
                    else
                    {
                        obj.usuario_modificacion = dr.GetInt32(10);
                    }


                    lista.Add(obj);
                }

                dr.Close();
                con.Close();
            }

            return lista;
        }

        public List<BECertificado> ListarCertificadosFiltro(string condicionEstado, string condicionTipo)
        {
            List<BECertificado> lista = new List<BECertificado>();

            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_Listar_CertificadosFiltro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@condicion_estado", condicionEstado);
                cmd.Parameters.AddWithValue("@condicion_tipo", condicionTipo);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BECertificado obj = new BECertificado()
                    {
                        id = dr.GetInt32(0),
                        apellidos = dr.GetString(1),
                        nombres = dr.GetString(2),
                        correo = dr.GetString(3),
                        fecha_creacion = dr.GetDateTime(4),
                        estadoCertificado = dr.GetInt32(5),
                        idTipo = dr.GetInt32(6),
                        codigo = dr.GetGuid(7)
                        //usuario_creacion = dr.GetInt32(12),
                        //fecha_modificacion = dr.GetDateTime(13),
                        //usuario_modificacion = dr.GetInt32(14)
                    };
                    //usuario_creacion
                    if (dr.IsDBNull(8))
                    {
                        obj.usuario_creacion = 0;
                    }
                    else
                    {
                        obj.usuario_creacion = dr.GetInt32(8);
                    }
                    //fecha_modificacion
                    if (dr.IsDBNull(9))
                    {
                        obj.fecha_modificacion = DateTime.MinValue;
                    }
                    else
                    {
                        obj.fecha_modificacion = dr.GetDateTime(9);
                    }
                    //usuario_modificacion
                    if (dr.IsDBNull(10))
                    {
                        obj.usuario_modificacion = 0;
                    }
                    else
                    {
                        obj.usuario_modificacion = dr.GetInt32(10);
                    }


                    lista.Add(obj);
                }

                dr.Close();
                con.Close();
            }

            return lista;
        }

        public string RegistrarCertificadosMasivos(BECertificado certificado)
        {
            string mensaje = "";
            DateTime fechaNula = DateTime.MinValue;
            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_Registrar_Certificados_Masivos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@apellidos", certificado.apellidos);
                    cmd.Parameters.AddWithValue("@nombres", certificado.nombres);
                    cmd.Parameters.AddWithValue("@correo", certificado.correo);
                
                    cmd.Parameters.AddWithValue("@idTipo", certificado.idTipo);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    mensaje = "Registro masivo de Diplomas realizado correctamente";

                }
                catch (Exception ex)
                {
                    mensaje = "Error";
                }
                finally
                {
                    con.Close();
                }
                return mensaje;
            }
        }

        public string EliminarCertificado(int id)
        {
            string mensaje = "";
            SqlConnection con = ConexionBD.ObtenerConexion();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_Eliminar_Certificados", con, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                if (cmd.ExecuteNonQuery() >= 1)
                {
                    mensaje = "Certificado eliminado correctamente.";
                }
                else
                {
                    mensaje = "Error: No se eliminado el certificado.";
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                mensaje = "Error: No se eliminado el certificado.";
                tr.Rollback();
            }
            finally
            {
                con.Close();
            }

            return mensaje;
        }
    }
}
