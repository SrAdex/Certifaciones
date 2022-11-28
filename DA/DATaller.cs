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
    public class DATaller
    {
        ConexionBD ConexionBD = new ConexionBD();

        public List<BETaller> ListarTalleres()
        {
            List<BETaller> lista = new List<BETaller>();

            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_Listar_Taller", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BETaller obj = new BETaller()
                    {
                        id_taller = dr.GetInt32(0),
                        nombreTaller = dr.GetString(1),
                        descripcionTaller = dr.GetString(2)
                    };

                    lista.Add(obj);
                }

                dr.Close();
                con.Close();
            }
            return lista;
        }

        public string RegistrarTaller(string nombre, string descripcion)
        {
            string mensaje = "";
            SqlConnection con = ConexionBD.ObtenerConexion();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_Registrar_Taller", con, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreTaller", nombre);
                cmd.Parameters.AddWithValue("@descripcionTaller", descripcion);

                if (cmd.ExecuteNonQuery() >= 1)
                {
                    mensaje = "Taller registrado correctamente.";
                }
                else
                {
                    mensaje = "Error: No se registró el taller.";
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                mensaje = "Error: No se registró el taller.";
                tr.Rollback();
            }
            finally
            {
                con.Close();
            }

            return mensaje;
        }

        public string EliminarTaller(int id)
        {
            string mensaje = "";
            SqlConnection con = ConexionBD.ObtenerConexion();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_Eliminar_Taller", con, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_taller", id);

                if (cmd.ExecuteNonQuery() >= 1)
                {
                    mensaje = "Taller eliminado correctamente.";
                }
                else
                {
                    mensaje = "Error: No se eliminado el taller.";
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                mensaje = "Error: No se eliminado el taller.";
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
