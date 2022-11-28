using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BE;
using DA;
using SpreadsheetLight;

namespace BL
{
    public class BLCertificado
    {
        private readonly DACertificado _DACertificado = new DACertificado();
        private readonly DAEstado _DAEstado = new DAEstado();
        private readonly DATipo _DATipo = new DATipo();
        private readonly DATaller _DATaller = new DATaller();

        public List<BECertificado> GetListaCertificados()
        {
            List<BECertificado> lista = new List<BECertificado>();
            lista = _DACertificado.ListarCertificados();
            return lista;
        }

        public List<BECertificado> GetListaCertificadosFiltrada(string condicionEstado, string condicionTipo)
        {
            List<BECertificado> lista = new List<BECertificado>();
            lista = _DACertificado.ListarCertificadosFiltro(condicionEstado,condicionTipo);
            return lista;
        }

        public List<BEEstado> GetListaEstados()
        {
            List<BEEstado> lista = new List<BEEstado>();
            lista = _DAEstado.ListarEstados();
            return lista;
        }

        public List<BETipo> GetListaTipos()
        {
            List<BETipo> lista = new List<BETipo>();
            lista = _DATipo.ListarTipos();
            return lista;
        }

        public List<BETaller> GetListaTalleres()
        {
            List<BETaller> lista = new List<BETaller>();
            lista = _DATaller.ListarTalleres();
            return lista;
        }

        public string RegistroMasivo(HttpPostedFileBase plantillaExcel, int tipo)
        {
            string respuesta = "";
            var server = HttpContext.Current.Server;
            //Ruta
            string ruta = "~/Cargas/Registro_Masivo_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".xlsx";

            //Guarda una copia de la plantilla de Excel subido
            plantillaExcel.SaveAs(server.MapPath(ruta));

            //Asigna la direccion de la copia
            string path = server.MapPath(ruta);

            //Abre la direccion de la copia
            SLDocument sl = new SLDocument(path);

            List<BECertificado> Certificados = new List<BECertificado>();

            //Inicia en la fila 2 a escanear los valores del archivo Excel
            int iRow = 2;

            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                BECertificado certificado = new BECertificado();
                certificado.apellidos = sl.GetCellValueAsString(iRow, 1);
                certificado.nombres = sl.GetCellValueAsString(iRow, 2);
                certificado.correo = sl.GetCellValueAsString(iRow, 3);
                certificado.idTipo = tipo;

                Certificados.Add(certificado);

                iRow++;
            }

            foreach (var certificado in Certificados)
            {
                respuesta = _DACertificado.RegistrarCertificadosMasivos(certificado);
                if (respuesta.StartsWith("Error"))
                    break;
            }
            return respuesta;

        }

        public string EliminarCertificado(int id)
        {
            string mensaje = "";
            mensaje = _DACertificado.EliminarCertificado(id);
            return mensaje;
        }
    }
}
