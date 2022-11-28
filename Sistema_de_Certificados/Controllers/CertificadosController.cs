using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_de_Certificados.Controllers
{
    public class CertificadosController : Controller
    {
        public readonly BL.BLCertificado _BLCertificado;

        public CertificadosController()
        {
            _BLCertificado = new BL.BLCertificado();
        }

        public ActionResult Index()
        {
            List<BE.BECertificado> lst = new List<BE.BECertificado>();
            List<int> filtroEstado = Session["filtroEstado"] as List<int>;
            List<int> filtroTipos = Session["filtroTipo"] as List<int>;

            string condicionEstado = "";
            string condicionTipo = "";

            if (filtroEstado != null)
            {
                condicionEstado = string.Join(",", filtroEstado);
            }

            if (filtroTipos != null)
            {
                condicionTipo = string.Join(",", filtroTipos);
            }

            if (condicionEstado.Length > 0 || condicionTipo.Length > 0)
            {
                lst = _BLCertificado.GetListaCertificadosFiltrada(condicionEstado, condicionTipo);
            }
            else
            {
                lst = _BLCertificado.GetListaCertificados();
            }

            ViewBag.filtroEstado = (filtroEstado ?? new List<int>());
            ViewBag.filtroTipos = (filtroTipos ?? new List<int>());

            ViewBag.ListCertificados = lst;
            ViewBag.ListEstados = _BLCertificado.GetListaEstados();
            ViewBag.ListTipos = _BLCertificado.GetListaTipos();
            ViewBag.ListTalleres = _BLCertificado.GetListaTalleres();

            ViewBag.ruta= "~/Certificados/";

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase plantillaExcel, int tipo)
        {
            string mensaje = "";
            mensaje = _BLCertificado.RegistroMasivo(plantillaExcel, tipo);
            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
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

        public JsonResult EliminarCertificado(int id)
        {
            bool rpta = true;
            string mensaje = "";
            mensaje = _BLCertificado.EliminarCertificado(id);
            TempData["mensaje"] = mensaje;
            if (mensaje != "Certificado eliminado correctamente.")
                rpta = false;

            return Json(new { resultado = rpta }, JsonRequestBehavior.AllowGet);
        }

        public FileResult GenerarReporteGeneral()
        {
            var ListEstados = _BLCertificado.GetListaEstados();
            var ListTipos = _BLCertificado.GetListaTipos();
            var ListTalleres = _BLCertificado.GetListaTalleres();

            DataTable dt = new DataTable("Data");
            dt.Columns.AddRange(new DataColumn[8] {     new DataColumn("ID"),
                                                        new DataColumn("Apellidos"),
                                                        new DataColumn("Nombres"),
                                                        new DataColumn("Correo"),
                                                        new DataColumn("Certificado"),
                                                        new DataColumn("Estado"),
                                                        new DataColumn("Código Certificado"),
                                                        new DataColumn("Fecha Creacion")
            });

            List<BE.BECertificado> lst = new List<BE.BECertificado>();

            List<int> filtroEstado = Session["filtroEstado"] as List<int>;
            List<int> filtroTipos = Session["filtroTipo"] as List<int>;

            string condicionEstado = "";
            string condicionTipo = "";

            if (filtroEstado != null)
            {
                foreach (var estado in filtroEstado)
                {
                    condicionEstado += (estado + ",");
                }
            }

            if (filtroTipos != null)
            {
                foreach (var tipo in filtroTipos)
                {
                    condicionTipo += (tipo + ",");
                }
            }

            if (condicionEstado.Length > 0 || condicionTipo.Length > 0)
            {
                lst = _BLCertificado.GetListaCertificadosFiltrada(condicionEstado, condicionTipo);
            }
            else
            {
                lst = _BLCertificado.GetListaCertificados();
            }

            foreach (var detalle in lst)
            {
                foreach (var iTipos in ListTipos) { if (iTipos.id_tipo == detalle.idTipo) { detalle.nameTipo = iTipos.nombreTipo; } }
                foreach (var iEstados in ListEstados) { if (iEstados.id_estado == detalle.estadoCertificado) { detalle.nameEstado = iEstados.nombre_estado; } }
                dt.Rows.Add(detalle.id, detalle.apellidos, detalle.nombres, detalle.correo, detalle.nameTipo,
                    detalle.nameEstado, detalle.codigo, detalle.fecha_creacion);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var sheet = wb.Worksheets.Add(dt, "Reporte General de Certificados");
                sheet.Columns("A", "K").AdjustToContents();
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_General.xlsx");
                }
            }
        }
    }
}