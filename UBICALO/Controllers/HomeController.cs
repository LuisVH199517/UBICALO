using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UBICALO.ViewModel;
using UBICALO.Models;

namespace UBICALO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult login() {


            return View();

        }

        [HttpPost]
        public ActionResult login(VmLogin vmLogin)
        {

            try
            {
                UbicaloEntities context = new UbicaloEntities();

                Cliente cliente = context.Cliente.FirstOrDefault(x => x.Usuario == vmLogin.usuario && x.Clave == vmLogin.clave);
                if (cliente == null)
                {
                    Asociado asociado = context.Asociado.FirstOrDefault(x => x.Usuario == vmLogin.usuario && x.Clave == vmLogin.clave);
                    if (asociado == null)
                    {
                        Administrador administrador = context.Administrador.FirstOrDefault(x => x.Usuario == vmLogin.usuario && x.Clave == vmLogin.clave);
                        if (administrador == null)
                            return View(vmLogin);
                        Session["objUsuario"] = administrador;
                        Session["rol"] = "D";
                        return RedirectToAction("listarAsociados");

                    }

                    Session["objUsuario"] = asociado;
                    Session["rol"] = "A";
                    return RedirectToAction("estadoCuenta");
                }


                Session["objUsuario"] = cliente;
                Session["rol"] = "C";
                return RedirectToAction("gimnacioMapa");



            }
            catch (Exception)
            {

                return View(vmLogin);
            }

        }

        public ActionResult verMapa() {
            VmEstaMapa vm = new VmEstaMapa();
            vm.fill();

            return View(vm);

        }

    }
}