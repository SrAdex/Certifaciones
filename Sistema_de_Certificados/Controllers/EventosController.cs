using BE;
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

            if (EventoList.Count == 0)
            {
                TempData["message"] = "No hay productos en la base de datos";
            }

            return View(EventoList);
        }
        public ActionResult Index2()
        {
            var EventoList = _BLEvento.GetListaEventos().ToList();
            ViewBag.EventoList = EventoList;

            return View();
        }

        public ActionResult CRUD(int id)
        {
            BEEvento _Evento = new BEEvento();
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                _Evento = _BLEvento.GetListaEventosxID(id);
            }

            return View(_Evento);
        }
        [HttpPost]
        public ActionResult CRUD(BEEvento _Evento)
        {
            TempData["mensaje"] = (_BLEvento.GetListaEventosxID(_Evento.IdEvento) == null ? _BLEvento.RegistrarEvento(_Evento) : _BLEvento.ActualizarEvento(_Evento));
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
        public ActionResult Create(BEEvento _BEEvento)
        {
            string mensaje = "";
            try
            {
                if (ModelState.IsValid)
                {
                    mensaje = _BLEvento.RegistrarEvento(_BEEvento);
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

        // GET: Eventos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Eventos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
