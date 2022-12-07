using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using BE;

namespace DA
{
    public class DAEvento
    {
        ConexionBD ConexionBD = new ConexionBD();

        private readonly BE.BEEvento BEEvento;

        public IEnumerable<BEEvento> ListarEventos()
        {
            List<BEEvento> lista = new List<BEEvento>();
             using(SqlConnection con = ConexionBD.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_listar_eventos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BEEvento obj = new BEEvento()
                    {
                        IdEvento=dr.GetInt32(0),
                        NomEvento=dr.GetString(1),
                        DesEvento=dr.GetString(2)
                    };

                    if (dr.IsDBNull(3))
                    {
                        obj.FCreate = DateTime.Now;
                    }
                    else
                    {
                        obj.FCreate = dr.GetDateTime(3);
                    }

                    if (dr.IsDBNull(4))
                    {
                        obj.UsrCreate = "";
                    }
                    else
                    {
                        obj.UsrCreate = dr.GetString(4);
                    }

                    if (dr.IsDBNull(5))
                    {
                        obj.FUpdate = DateTime.Now;
                    }
                    else
                    {
                        obj.FUpdate = dr.GetDateTime(5);
                    }

                    if (dr.IsDBNull(6))
                    {
                        obj.UsrUpdate = "";
                    }
                    else
                    {
                        obj.UsrUpdate = dr.GetString(6);
                    }
                    lista.Add(obj);
                }
                dr.Close();
                con.Close();
            }
            return lista;
        }

        public BEEvento GerEventoxId(int idEvento)
        {
            BEEvento obj = new BEEvento();
            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_listar_eventosxId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@in_idEvento", idEvento);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    obj = new BEEvento()
                    {
                        IdEvento = dr.GetInt32(0),
                        NomEvento = dr.GetString(1),
                        DesEvento = dr.GetString(2)
                    };

                    if (dr.IsDBNull(3))
                    {
                        obj.FCreate = DateTime.Now;
                    }
                    else
                    {
                        obj.FCreate = dr.GetDateTime(3);
                    }

                    if (dr.IsDBNull(4))
                    {
                        obj.UsrCreate = "";
                    }
                    else
                    {
                        obj.UsrCreate = dr.GetString(4);
                    }

                    if (dr.IsDBNull(5))
                    {
                        obj.FUpdate = DateTime.Now;
                    }
                    else
                    {
                        obj.FUpdate = dr.GetDateTime(5);
                    }

                    if (dr.IsDBNull(6))
                    {
                        obj.UsrUpdate = "";
                    }
                    else
                    {
                        obj.UsrUpdate = dr.GetString(6);
                    }

                    if (dr.IsDBNull(8))
                    {
                        obj.ruta = "";
                    }
                    else
                    {
                        obj.ruta = dr.GetString(8);
                    }

                    if (dr.IsDBNull(9))
                    {
                        obj.posicionY = 0;
                    }
                    else
                    {
                        obj.posicionY = dr.GetInt32(9);
                    }

                    if (dr.IsDBNull(10))
                    {
                        obj.tamanioLetra = 0;
                    }
                    else
                    {
                        obj.tamanioLetra = dr.GetInt32(10);
                    }
                }
                dr.Close();
                con.Close();
            }
            return obj;
        }

        public string RegistrarEventos(string NomEvent, string DesEvent, string UsrCreate, string UsrUpdate, string ruta)
        {
            string mensaje = "";

            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrar_eventos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@vc_NomEvent", NomEvent);
                    cmd.Parameters.AddWithValue("@vc_DesEvent", DesEvent);
                    cmd.Parameters.AddWithValue("@vc_UsrCreate", UsrCreate);
                    cmd.Parameters.AddWithValue("@vc_UsrUpdate", UsrUpdate);
                    cmd.Parameters.AddWithValue("@vc_ruta", ruta);
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    con.Open();
                    int rs = cmd.ExecuteNonQuery();

                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error! " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }

            return mensaje;
        }
    
        public string ActualizarEventos(BEEvento _BEEvento)
        {
            string mensaje = "";

            using(SqlConnection con = ConexionBD.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizar_eventos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@in_IdEvento", _BEEvento.IdEvento);
                    cmd.Parameters.AddWithValue("@vc_NomEvent", _BEEvento.NomEvento);
                    cmd.Parameters.AddWithValue("@vc_DesEvent", _BEEvento.DesEvento);
                    cmd.Parameters.AddWithValue("@vc_UsrUpdate", _BEEvento.UsrUpdate);
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    con.Open();
                    int rs = cmd.ExecuteNonQuery();

                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error! " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }

            return mensaje;
        }
    
        public string EliminarEventos(int idEvento)
        {
            string mensaje = "";

           using(SqlConnection con = ConexionBD.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_eventos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@in_IdEvento", idEvento);
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    con.Open();
                    int rs = cmd.ExecuteNonQuery();
                    
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = "Error! " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }

            return mensaje; 
        }
    }
}
