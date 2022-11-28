using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECertificado
    {
        public int id { get; set; }
        public string apellidos { get; set; }
        public string nombres { get; set; }
        public string correo { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int estadoCertificado { get; set; }
        public int idTipo { get; set; }
        public Guid codigo { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public int usuario_modificacion { get; set; }

        //
        public string nameTipo { get; set; }
        public string nameEstado { get; set; }
    }
}
