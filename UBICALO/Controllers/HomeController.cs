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

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(VmLogin vm)
        {
            try
            {
                UbicaloEntities context = new UbicaloEntities();

                Cliente cliente = context.Cliente.FirstOrDefault(x => x.Usuario == vm.usuario && x.Clave == vm.clave);
                if (cliente != null)
                {
                    Session["objUsuario"] = cliente;
                    Session["rol"] = "C";
                    return RedirectToAction("verMapa");
                }

                Asociado asociado = context.Asociado.FirstOrDefault(x => x.Usuario == vm.usuario && x.Clave == vm.clave);
                if (asociado != null)
                {
                    Session["objUsuario"] = asociado;
                    Session["rol"] = "A";
                    return RedirectToAction("estadoCuenta");
                }

                Administrador administrador = context.Administrador.FirstOrDefault(x => x.Usuario == vm.usuario && x.Clave == vm.clave);
                if (administrador != null)
                {
                    Session["objUsuario"] = administrador;
                    Session["rol"] = "D";
                    return RedirectToAction("listarAsociados");
                }

                return View(vm);
            }
            catch (Exception)
            {
                return View(vm);
            }
        }

        public ActionResult verMapa()
        {
            VmEstaMapa vm = new VmEstaMapa();
            vm.fill();

            return View(vm);
        }

        public ActionResult establecimientoInfo(int establecimientoID)
        {

            VmEstablecimientoInfo vm = new VmEstablecimientoInfo();
            vm.establecimientoID = establecimientoID;
            vm.fill();

            return View(vm);
        }


        [HttpPost]
        public ActionResult loginFB(VmLogin vm)
        {
            try
            {

                UbicaloEntities context = new UbicaloEntities();
                Cliente cliente = context.Cliente.FirstOrDefault(x => x.IDApi == vm.FbID);

                if (cliente == null)
                {

                    cliente = new Cliente();
                    cliente.Usuario = vm.usuario;
                    cliente.Clave = "";
                    if (vm.correo != null)
                        cliente.Correo = vm.correo;
                    else
                        cliente.Correo = "";
                    cliente.IDApi = vm.FbID;
                    cliente.Foto = vm.imagen;

                    //string imageFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/user.png");
                    //var srcImage = Image.FromFile(imageFile);
                    //var stream = new MemoryStream();
                    //srcImage.Save(stream, ImageFormat.Png);
                    //cliente.Foto= stream.ToArray();
                    //cliente.Rol = "User";

                    context.Cliente.Add(cliente);
                    context.SaveChanges();

                    Session["objUsuario"] = cliente;
                    Session["rol"] = "C";

                    return RedirectToAction("verMapa");
                }

                Session["objUsuario"] = cliente;
                Session["rol"] = "C";
                return RedirectToAction("verMapa");
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
                throw;
            }
        }


        public ActionResult cerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("login");
        }








        public ActionResult compraPlan(int productoID)
        {
            VmCompraProducto vm = new VmCompraProducto();

            vm.productoID = productoID;
            vm.fill();
            return View(vm);
        }

        [HttpPost]
        public ActionResult compraPlan(VmCompraProducto vm)
        {
            try
            {
                Compra compra = new Compra();
                compra.ClienteID = ((Cliente)Session["objUsuario"]).ClienteID;
                compra.ProductoID = vm.productoID;
                //insertar codigo de generacion de cosigo QR
                //BarcodeLib. qrbarcode = new BarcodeLib.Barcode();

                //qrbarcode.
                //BarCode qrcode = new BarCode();
                //qrcode.Symbology = KeepAutomation.Barcode.Symbology.QRCode;


                //QRCodeGenerator qrc = new QRCodeGenerator();
                //QRCodeGenerator.QRCode qc = qrc.CreateQrCode(compra.PlanID.ToString(), QRCodeGenerator.ECCLevel.Q);
                //Bitmap bmp = qc.GetGraphic(20);

                //MemoryStream ms = new MemoryStream();

                //bmp.Save(ms, ImageFormat.Png);

                //byte[] bt = ms.ToArray();

                //Image i = new Image();

                //var filename = Path.GetFileName(bt.FileName);
                //var path = Path.Combine(Server.MapPath("~/Content/image/"), filename);
                //file.SaveAs(path);
                //tyre.Url = filename;

                //_db.EventModels.AddObject(eventmodel);
                //_db.SaveChanges();
                //return RedirectToAction("Index");

                UbicaloEntities context = new UbicaloEntities();

                compra.QR = "codigo";

                context.Compra.Add(compra);
                context.SaveChanges();

                return RedirectToAction("productosAdquiridos");
            }
            catch (Exception)
            {
                return View();
            }

        }


        public ActionResult productosAdquiridos()
        {
            VmProductosAdquiridos vm = new VmProductosAdquiridos();
            vm.fill(((Cliente)Session["objUsuario"]).ClienteID);

            return View(vm);
        }


    }
}