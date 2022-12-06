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

        public ActionResult Plantilla()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase plantillaExcel, int tipo)
        {
            string mensaje = "";
            //mensaje = _BLCertificado.RegistroMasivo(plantillaExcel, tipo);
            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
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
        
    }
}
