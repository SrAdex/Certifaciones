using BE;
using DA;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PdfSharp.Pdf;
using PdfSharp.Drawing;


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

        public BEEvento GetEventoxId(int idEvento)
        {
            BEEvento obj = new BEEvento();
            obj = _DAEvento.GerEventoxId(idEvento);
            return obj;
        }

        public BEEvento GetListaEventosxID(int id)
        {
            return GetListaEventos().FirstOrDefault(e => e.IdEvento == id);
        }

        public string RegistrarEvento(string NomEvent, string DesEvent, string UsrCreate, string UsrUpdate, HttpPostedFileBase plantillaCertificado)
        {
            var server = HttpContext.Current.Server;
            //Fecha Actual
            string fechaActual= DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");


            //Ruta Standart
            string ruta = "/Plantilla_Certificados_JPG/Plantilla_" + fechaActual + ".jpg";
            string rutaPDF = "/Plantilla_Certificados_PDF/Plantilla_" + fechaActual + ".pdf";


            //Guardar el certificado por respaldo
            plantillaCertificado.SaveAs(server.MapPath(ruta));

            //Guardar el pdf de la plantilla del certificado
            GeneratePDF("D:\\ADEX\\Proyectos\\sistema_certificados\\Sistema_de_Certificados"+"/Plantilla_Certificados_PDF/Plantilla_" + fechaActual+ ".pdf", "D:\\ADEX\\Proyectos\\sistema_certificados\\Sistema_de_Certificados"+ ruta);

            string mensaje = "";
            mensaje = _DAEvento.RegistrarEventos(NomEvent, DesEvent, UsrCreate, UsrUpdate, rutaPDF);
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

        private void GeneratePDF(string filename, string imageLoc)
        {
            #region Generar y graficar en pdf
            //Iniciar objeto documento
            PdfDocument document = new PdfDocument();

            //Crear pagina del PDF
            PdfPage page = document.AddPage();

            //Obtener las dimensiones de la plantilla del certificado .jpg
            XImage xImage = XImage.FromFile(imageLoc);
            page.Width = xImage.PixelWidth;
            page.Height = xImage.PixelHeight;

            //Colocar la imagen de fondo
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, imageLoc, 0, 0, xImage.PixelWidth, xImage.PixelHeight);
            #endregion

            #region Letras
            XBrush redBrush = new XSolidBrush(XColor.FromArgb(96, 185, 153));

            XFont font = new XFont("Arial", 55, XFontStyle.Bold | XFontStyle.Underline);

            gfx.DrawString("Apellidos Nombres", font, redBrush,
                           new XRect(0, 490, page.Width, page.Height),
                           XStringFormats.TopCenter);
            #endregion

            // Save and start View
            document.Save(filename);
        }


        private void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }
    }
}
