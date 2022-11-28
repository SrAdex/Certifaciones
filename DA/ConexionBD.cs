using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DA
{
    public class ConexionBD
    {
        public static SqlConnection ObtenerConexion()
        {
            //Credenciales
            string BD_Server = ConfigurationManager.AppSettings["BD_Server"];
            string BD_Name = ConfigurationManager.AppSettings["BD_Name"];
            string BD_User = ConfigurationManager.AppSettings["BD_User"];
            string BD_Password = ConfigurationManager.AppSettings["BD_Password"];

            //Cadena de Conexion
            string conexion = "server=" + BD_Server + "; Initial Catalog=" + BD_Name + "; uid=" + BD_User + "; pwd=" + BD_Password;

            //Conexion con BD
            SqlConnection Conn = new SqlConnection(conexion);
            return Conn;
        }
    }
}
