using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEEvento
    {
        [Key]
        [DisplayName("ID")]
        [Required]
        public int IdEvento { get; set; }
        [DisplayName("Nombre")]
        public string NomEvento { get; set; }
        [DisplayName("Descripción")]
        public string DesEvento { get; set; }
        [DisplayName("F. Creación")]
        public DateTime FCreate { get; set; }
        [DisplayName("Usuario")]
        public string UsrCreate { get; set; }
        public DateTime FUpdate { get; set; }
        public string UsrUpdate { get; set; }
        public string EstadoEven{ get; set; }
        public string ruta { get; set; }
        public int posicionY { get; set; }
        public int tamanioLetra { get; set; }
    }
}
