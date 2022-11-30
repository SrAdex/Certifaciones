using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sistema_de_Certificados.Controllers
{
    public class AccountController : Controller
    {
        private readonly DA.DAUsuario _DAUsuario;

        public AccountController()
        {
            _DAUsuario = new DA.DAUsuario();
        }
        public ActionResult Login(string rutaOrigen = "")
        {
            ViewBag.rutaOrigen = rutaOrigen;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string correo, string clave, string rutaOrigen = "", string token = "")
        {
            //if (_DAUsuario.EstaEnActiveDirectory(correo, clave))
            //{
                BE.BEUsuario usuario = _DAUsuario.EstaEnBaseDeDatos(correo);
                if (usuario != null)
                {
                    _DAUsuario.IniciarSesion(usuario);

                    if (!string.IsNullOrEmpty(rutaOrigen))
                    {
                        return Redirect(rutaOrigen);
                    }
                    else
                    {
                        return RedirectToAction("Certificados");
                    }
                }
                else
                {
                    TempData["mensaje"] = "Sin acceso al Sistema";
                    return View();
                }
            /*}
            else
            {
                TempData["alert"] = "Credenciales Incorrectas";
                return RedirectToAction("Login", "Account");
            }*/
        }

        public ActionResult Logout()
        {
            _DAUsuario.CerrarSesion();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Certificados()
        {
            return RedirectToAction("Index", "Certificados");
        }

        [HttpPost]
        public void ExtenderTiempoSesion()
        {
            Session.Timeout += 20;
        }
    }
}