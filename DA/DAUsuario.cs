using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DA
{
    public class DAUsuario
    {
        public bool EstaEnActiveDirectory(string correo, string clave)
        {
            bool resultado = false;
            string dominio = ConfigurationManager.AppSettings["DomainActiveDirectory"];
            //using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, dominio))
            //{
                //resultado = pc.ValidateCredentials(correo, clave);
            //}

            return resultado;
        }

        public BE.BEUsuario EstaEnBaseDeDatos(string correo)
        {
            BE.BEUsuario usuario = null;

            using (SqlConnection con = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("USP_BUSCAR_USUARIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@correo", correo);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    usuario = new BE.BEUsuario()
                    {
                        IdUsuario = dr.GetInt32(0),
                        NombresUsuario = dr.GetString(1),
                        ApellidosUsuario = dr.GetString(2),
                        CorreoUsuario = dr.GetString(3),
                        Estado = dr.GetInt32(9)
                    };
                }
                dr.Close();
                con.Close();
            }

            return usuario;
        }

        public void IniciarSesion(BE.BEUsuario usuario)
        {
            HttpContext.Current.Session["usuario"] = usuario;
            HttpContext.Current.Session["minutosSesionRestantes"] = 560;
            HttpContext.Current.Session.Timeout = 20;
        }

        public void CerrarSesion()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
