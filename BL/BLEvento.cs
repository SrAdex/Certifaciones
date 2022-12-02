using BE;
using DA;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLEvento
    {
        public readonly DAEvento _DAEvento = new DAEvento();
        public readonly DATipo _DATipo = new DATipo();

        public List<BEEvento> GetListaEventos()
        {
            List<BEEvento> lista = new List<BEEvento>();
            lista = _DAEvento.ListarEventos().ToList();
            return lista;
        }

        public BEEvento GetListaEventosxID(int id)
        {
            return GetListaEventos().FirstOrDefault(e => e.IdEvento == id);
        }

        public string RegistrarEvento(BEEvento _BEEvento)
        {
            string mensaje = "";
            mensaje = _DAEvento.RegistrarEventos(_BEEvento);
            return mensaje;
        }

        public string ActualizarEvento(BEEvento _BEEvento)
        {
            string mensaje = "";
            mensaje = _DAEvento.ActualizarEventos(_BEEvento);
            return mensaje;
        }

        public string EliminarEvento(int id)
        {
            string mensaje = "";
            mensaje = _DAEvento.EliminarEventos(id);
            return mensaje;
        }
    }
}
