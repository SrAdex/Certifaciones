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
    public class DAEstado
    {
        ConexionBD ConexionBD = new ConexionBD();
        private readonly BE.BEEstado _BEEstado;

        public List<BEEstado> ListarEstados()
        {
            List<BEEstado> lista = new List<BEEstado>();

            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_Listar_Estados", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BEEstado obj = new BEEstado()
                    {
                        id_estado = dr.GetInt32(0),
                        nombre_estado = dr.GetString(1)
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
