using BE;
using BL;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_de_Certificados.Controllers
{
    public class EventosController : Controller
    {
        public readonly BL.BLEvento _BLEvento;

        public EventosController()
        {
            _BLEvento = new BL.BLEvento();
        }

        // GET: Eventos
        public ActionResult Index()
        {
            var EventoList = _BLEvento.GetListaEventos().ToList();
            ViewBag.EventoList = EventoList;
            

            if (EventoList.Count == 0)
            {
                TempData["message"] = "No hay productos en la base de datos";
            }

            ViewBag.ruta = "~/Certificados/";

            return View();
        }
        public ActionResult Index2()
        {
            var EventoList = _BLEvento.GetListaEventos().ToList();
            ViewBag.EventoList = EventoList;

            return View();
        }

        public ActionResult CRUD(string id)
        {
            BEEvento _Evento = new BEEvento();

            var EventoList = _BLEvento.GetListaEventos().ToList();
            ViewBag.EventoList = EventoList;

            if (!string.IsNullOrEmpty(id))
            {
                _Evento = _BLEvento.GetListaEventosxID(int.Parse(id));
            }

            return View(_Evento);
        }


        // GET: Eventos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        [HttpPost]
        public ActionResult Create(string NomEvent, string DesEvent, string UsrCreate, string UsrUpdate)
        {
            string mensaje = "";
            try
            {
                
                if (ModelState.IsValid)
                {
                    mensaje = _BLEvento.RegistrarEvento(NomEvent, DesEvent, UsrCreate, UsrUpdate);
                }

                TempData["message"] = mensaje;

                return RedirectToAction("Index");
            }
            catch(SqlException ex)
            {
                TempData["message"] = ex.Message;
                return View();
            }
        }

        // GET: Eventos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Eventos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult Delete(int id)
        {
            bool rpta = true;
            string mensaje = "";
            mensaje = _BLEvento.EliminarEvento(id);
            TempData["mensaje"] = mensaje;
            if (!mensaje.Contains("Satisfactoriamente"))
                rpta = false;

            return Json(new { resultado = rpta }, JsonRequestBehavior.AllowGet);
        }
        public FileResult Plantilla()
        {
            var ruta = Server.MapPath("../Plantilla_Certificados/Plantilla_CargaMasiva.xlsx");
            return File(ruta, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Plantilla_CargaMasiva.xlsx");
        }

        public ActionResult EstablecerFiltrosGeneral(List<int> estados, List<int> tipos)
        {

            Session["filtroEstado"] = estados == null ? new List<int>() : estados;
            Session["filtroTipo"] = tipos == null ? new List<int>() : tipos;

            return RedirectToAction("Index");

        }

        void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }

        private void GeneratePDF(string filename, string imageLoc, int contador)
        {
            PdfDocument document = new PdfDocument();
            //PdfDocument document = PdfGenerator.GeneratePdf("<h1>a</h1>", PageSize.A1, 0, null, null, null);

            // Create an empty page or load existing
            PdfPage page = document.AddPage();
            // Get an XGraphics object for drawing
            XImage xImage = XImage.FromFile(imageLoc);
            page.Width = xImage.PixelWidth;
            page.Height = xImage.PixelHeight;
            //XGraphics gfx = XGraphics.FromPdfPage(page);
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, imageLoc, 0, 0, xImage.PixelWidth, xImage.PixelHeight);
            //
            XBrush redBrush = new XSolidBrush(XColor.FromArgb(96, 185, 153));
            XBrush negro = new XSolidBrush(XColor.FromArgb(71, 71, 71));
            //gfx.DrawString("black", XFontStyle.BoldItalic, redBrush, new XRect(x, y, width, height), XStringFormats.Center);

            //
            const XFontStyle BoldItalicUnderline = XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline;
            // Create a font
            //XFont font = new XFont("Arial", 40, XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline);
            XFont font = new XFont("Arial", 55, XFontStyle.Bold | XFontStyle.Underline);
            XFont font2 = new XFont("Arial Black", 30);
            // Draw the text


            // Draw the text
            gfx.DrawString("aberr", font2, negro,
                           new XRect(0, 410, page.Width, page.Height),
                           XStringFormats.TopCenter);

            gfx.DrawString("HOLAAAAAAAAAA", font, redBrush,
                           new XRect(0, 490, page.Width, page.Height),
                           XStringFormats.TopCenter);

            gfx.DrawString("Realizado", font2, negro,
                           new XRect(0, 610, page.Width, page.Height),
                           XStringFormats.TopCenter);

            gfx.DrawString("Con una", font2, negro,
                           new XRect(0, 680, page.Width, page.Height),
                           XStringFormats.TopCenter);


            // Save and start View
            document.Save(filename);
            //Process.Start(filename);
            document.Save("D:\\git- sls\\Sistema de Certificados\\document" + contador + ".pdf");
        }
    }
}
