using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_de_Certificados.Controllers
{
    public class TallerController : Controller
    {
        public readonly BL.BLTaller _BLTaller;

        public TallerController()
        {
            _BLTaller = new BL.BLTaller();
        }

        public ActionResult Index()
        {
            ViewBag.ListTalleres = _BLTaller.GetListaTalleres();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string nombre, string descripcion)
        {
            if (nombre == "" || descripcion == "") 
            {
                TempData["mensaje"] = "campos";
                return RedirectToAction("Index");
            }
            string mensaje = "";
            ViewBag.ListTalleres = _BLTaller.GetListaTalleres();
            mensaje=_BLTaller.CreateTaller(nombre, descripcion);
            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
        }

        public JsonResult EliminarTaller(int id)
        {
            bool rpta = true;
            string mensaje = "";
            mensaje = _BLTaller.EliminarTaller(id);
            TempData["alert"] = mensaje;
            if (mensaje != "Taller eliminado correctamente.")
                rpta = false;

            return Json(new { resultado = rpta }, JsonRequestBehavior.AllowGet);
        }
    }
}