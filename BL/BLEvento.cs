using BE;
using DA;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
<<<<<<< HEAD
using PdfSharp.Pdf;
using PdfSharp.Drawing;

=======
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
>>>>>>> origin/main

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
<<<<<<< HEAD
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
=======
            //Obtener la dirección actual dónde está ubicado el proyecto
            var server = HttpContext.Current.Server;

            //Ruta dónde se va a guardar los certificados
            string fechaActual = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");

            string ruta = "/Plantilla_Certificados_JPG/Plantilla_" + NomEvent + ".jpg";
            string rutaPDF= "/Plantilla_Certificados_PDF/Plantilla_" + NomEvent + ".pdf";

            //Guarda certificado
            plantillaCertificado.SaveAs(server.MapPath(ruta));

            //Guardamos el pdf de la plantilla del certificado
            GeneratePDF("C:\\ADEX\\SistemaCertificado\\Sistema_de_Certificados" + rutaPDF, "C:\\ADEX\\SistemaCertificado\\Sistema_de_Certificados" + ruta);

            string mensaje = "";
            mensaje = _DAEvento.RegistrarEventos(NomEvent, DesEvent, UsrCreate, UsrUpdate, rutaPDF);
>>>>>>> origin/main
            return mensaje;
        }

       
        public string EliminarEvento(int id)
        {
            string mensaje = "";
            mensaje = _DAEvento.EliminarEventos(id);
            return mensaje;
        }

<<<<<<< HEAD
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
=======
        //El jpegSamplePath es la locación de la imagen
        private void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }


        private void GeneratePDF(string filename, string imageLoc)
        {
            #region Generar PDF
            PdfDocument document = new PdfDocument();
            //PdfDocument document = PdfGenerator.GeneratePdf("<h1>a</h1>", PageSize.A1, 0, null, null, null);

            // Create an empty page or load existing
            PdfPage page = document.AddPage();
            
            // Obtiene las coordenadas x e y del .jpg para ajustarlo a la coordenadas del nuevo objeto pdf creado
            XImage xImage = XImage.FromFile(imageLoc);

            //Se ajusta las coordenadas de la imagen con la del pdf
            page.Width = xImage.PixelWidth;
            page.Height = xImage.PixelHeight;
            
            //XGraphics gfx = XGraphics.FromPdfPage(page) --> Indicamos que vamos a graficar dentro del objeto page;
>>>>>>> origin/main
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, imageLoc, 0, 0, xImage.PixelWidth, xImage.PixelHeight);
            #endregion

            #region Letras
<<<<<<< HEAD
            XBrush redBrush = new XSolidBrush(XColor.FromArgb(96, 185, 153));

            XFont font = new XFont("Arial", 55, XFontStyle.Bold | XFontStyle.Underline);

            gfx.DrawString("Apellidos Nombres", font, redBrush,
                           new XRect(0, 490, page.Width, page.Height),
                           XStringFormats.TopCenter);
=======
            //Letras

            XBrush redBrush = new XSolidBrush(XColor.FromArgb(96, 185, 153));
            XBrush negro = new XSolidBrush(XColor.FromArgb(71, 71, 71));
            //gfx.DrawString("black", XFontStyle.BoldItalic, redBrush, new XRect(x, y, width, height), XStringFormats.Center);



            // Create a font --> Averiguar que fuentes acepta pdfsharp
            //XFont font = new XFont("Arial", 40, XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline);
            //const XFontStyle BoldItalicUnderline = XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline; --> se comenta porque nunca usa
            XFont font = new XFont("Arial", 55, XFontStyle.Bold | XFontStyle.Underline);
            XFont font2 = new XFont("Arial Black", 30);
            // Draw the text


           
            gfx.DrawString("Apellidos y Nombres", font, redBrush,
                           new XRect(0, 490, page.Width, page.Height),
                           XStringFormats.TopCenter);

>>>>>>> origin/main
            #endregion

            // Save and start View
            document.Save(filename);
        }

<<<<<<< HEAD

        private void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
=======
        public string ActualizarEvento(int idEvento, int posicionY, int fontSize,string rgb)
        {
            string mensaje = "";

            BEEvento Evento = new BEEvento();

            Evento = GetListaEventosxID(idEvento);

            //Ruta dónde se va a guardar los certificados
            string fechaActual = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string ruta = "/Plantilla_Certificados_JPG/Plantilla_" + Evento.NomEvento + ".jpg";
            string rutaPDF = "/Plantilla_Certificados_PDF/Plantilla_" + Evento.NomEvento + ".pdf";

            //Guardamos el pdf de la plantilla del certificado
            ActualizarPDF("C:\\ADEX\\SistemaCertificado\\Sistema_de_Certificados" + rutaPDF, "C:\\ADEX\\SistemaCertificado\\Sistema_de_Certificados" + ruta, fontSize, posicionY, rgb);

            mensaje = _DAEvento.ActualizarEventos(idEvento, posicionY, fontSize);
            
            return mensaje;
        }

        private void ActualizarPDF(string filename, string imageLoc, double fontSize, double ejeY, string rgb)
        {
            
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XImage xImage = XImage.FromFile(imageLoc);

            page.Width = xImage.PixelWidth;
            page.Height = xImage.PixelHeight;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, imageLoc, 0, 0, xImage.PixelWidth, xImage.PixelHeight);


            #region Letras

            string[] paleta = rgb.Split(',');

            int r = int.Parse(paleta[0]);
            int g = int.Parse(paleta[1]);
            int b = int.Parse(paleta[2]);

            XBrush xbrush = new XSolidBrush(XColor.FromArgb(r,g,b));

            //XBrush redBrush = new XSolidBrush(XColor.FromArgb(96, 185, 153));
            //XBrush negro = new XSolidBrush(XColor.FromArgb(71, 71, 71));

            XFont font = new XFont("Arial", fontSize, XFontStyle.Bold | XFontStyle.Underline);
            XFont font2 = new XFont("Arial Black", 30);

            gfx.DrawString("Apellidos y Nombres", font, xbrush,
                           new XRect(0, ejeY, page.Width, page.Height),
                           XStringFormats.TopCenter);

            #endregion

            document.Save(filename);
>>>>>>> origin/main
        }
    }
}
