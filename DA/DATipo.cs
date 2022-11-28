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
    public class DATipo
    {
        ConexionBD ConexionBD = new ConexionBD();
        private readonly BE.BETipo _BETipo;

        public List<BETipo> ListarTipos()
        {
            List<BETipo> lista = new List<BETipo>();

            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_Listar_Tipos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BETipo obj = new BETipo()
                    {
                        id_tipo = dr.GetInt32(0),
                        nombreTipo = dr.GetString(1),
                        plantilla=dr.GetString(2)
                    };

                    lista.Add(obj);
                }

                dr.Close();
                con.Close();
            }
            return lista;
        }
    }
}
