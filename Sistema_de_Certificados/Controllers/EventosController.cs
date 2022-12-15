using BE;
using BL;
using Microsoft.Ajax.Utilities;
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


        public ActionResult Plantilla(int id)
        {
            var ObjEvento = _BLEvento.GetListaEventosxID(id);

            ViewBag.eventoObj = ObjEvento;

            return View();
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
        public ActionResult Create(string NomEvent, string DesEvent, string UsrCreate, string UsrUpdate, HttpPostedFileBase plantillaCertificado)
        {
            string mensaje = "";
            try
            {

                if (ModelState.IsValid)
                {
                    mensaje = _BLEvento.RegistrarEvento(NomEvent, DesEvent, UsrCreate, UsrUpdate, plantillaCertificado);
                }

                TempData["message"] = mensaje;

                return RedirectToAction("Index");
            }
            catch (SqlException ex)
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

    
        [HttpPost]
        public JsonResult Actualizar(int idEvento, int posicionY, int fontSize, string rgb)
        {
            bool rpta = true;
            string mensaje = "";
            mensaje = _BLEvento.ActualizarEvento(idEvento, posicionY, fontSize, rgb);
            if (!mensaje.Contains("actualizado"))
                rpta = false;

            return Json(new {resultado = rpta});
        }

    }
}
