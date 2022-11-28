using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEUsuario
    {
        public int IdUsuario { get; set; }
        public string NombresUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public int Estado { get; set; }

        public string NombreCompleto { get { return NombresUsuario + ' ' + ApellidosUsuario; } }
    }
}
