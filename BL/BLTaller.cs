using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DA;

namespace BL
{
    public class BLTaller
    {
        private readonly DATaller _DATaller = new DATaller();

        public List<BETaller> GetListaTalleres()
        {
            List<BETaller> lista = new List<BETaller>();
            lista = _DATaller.ListarTalleres();
            return lista;
        }

        public string CreateTaller(string nombre, string descripcion)
        {
            string mensaje = "";
            mensaje = _DATaller.RegistrarTaller(nombre, descripcion);
            return mensaje;
        }

        public string EliminarTaller(int id)
        {
            string mensaje = "";
            mensaje = _DATaller.EliminarTaller(id);
            return mensaje;
        }
    }
}
