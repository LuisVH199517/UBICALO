using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UBICALO.ViewModel;
using UBICALO.Models;
using System.IO;

using System.Drawing;
using System.Drawing.Imaging;

using UBICALO.Helpers;

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
                    if (cliente.IDApi != null)
                        return RedirectToAction("login");
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


        [HttpPost]
        public ActionResult registrarUsuario(VmLogin vm)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Usuario = vm.usuario;
                cliente.Clave = vm.clave;
                cliente.Correo = vm.correo;
                cliente.IDApi = null;
                cliente.Foto = "user.png";
                //string imageFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/user.png");
                //var srcImage = Image.FromFile(imageFile);
                //var stream = new MemoryStream();
                //srcImage.Save(stream, ImageFormat.Png);
                //cliente.Foto= stream.ToArray();
                //cliente.Rol = "User";

                UbicaloEntities context = new UbicaloEntities();

                context.Cliente.Add(cliente);
                context.SaveChanges();
                Session["objUsuario"] = cliente;
                Session["rol"] = "C";
                return RedirectToAction("verMapa");
            }
            catch (Exception)
            {
                return RedirectToAction("login");
            }
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
                    if (vm.correo != null && vm.correo != "undefined")
                        cliente.Correo = vm.correo;
                    else
                        cliente.Correo = "";
                    cliente.IDApi = vm.FbID;
                    cliente.Foto = vm.FbID; //vm.imagen;

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
            catch (Exception)
            {
                return RedirectToAction("login");
            }
        }

        public ActionResult configurarCuenta()
        {
            return View();
        }


        public ActionResult cerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("login");
        }



        public ActionResult verMapa()
        {
            VmEstaMapa vm = new VmEstaMapa();
            vm.fill();

            return View(vm);
        }


        public ActionResult establecimientoBusqueda()
        {
            VmEstablecimientoBusqueda vm = new VmEstablecimientoBusqueda();
            vm.fill();

            return View(vm);
        }

        [HttpPost]
        public ActionResult establecimientoBusqueda(VmEstablecimientoBusqueda vm)
        {
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



        public ActionResult compraProducto(int productoID)
        {
            VmCompraProducto vm = new VmCompraProducto();

            vm.productoID = productoID;
            vm.fill();
            return View(vm);
        }

        [HttpPost]
        public ActionResult compraProducto(VmCompraProducto vm)
        {
            try
            {
                Compra compra = new Compra();
                compra.ClienteID = ((Cliente)Session["objUsuario"]).ClienteID;
                compra.ProductoID = vm.productoID;
                
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

        
        public ActionResult getQr(int id)
        {
            Bitmap bmp = QRHelper.GenerateQrCode(id.ToString());
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            byte[] bt = ms.ToArray();
            return File(bt, "image/png");
        }


        public ActionResult listarProductos()
        {
            VmListarProductos vm = new VmListarProductos();
            vm.fill(((Asociado)Session["objUsuario"]).EstablecimientoID);
            return View(vm);
        }

        [HttpPost]
        public ActionResult listarProductos(VmListarProductos vm)
        {
            vm.fill(((Asociado)Session["objUsuario"]).EstablecimientoID);
            return View(vm);
        }



        public ActionResult registrarProducto(int? productoID)
        {
            UbicaloEntities context = new UbicaloEntities();
            VmRegistrarProducto vm = new VmRegistrarProducto();
            vm.fill(context, productoID);            

            return View(vm);
        }

        [HttpPost]
        public ActionResult registrarProducto(VmRegistrarProducto vm, HttpPostedFileBase file)
        {
            UbicaloEntities context = new UbicaloEntities();

            try
            {                
                Producto producto = null;

                String categoriaNombre = context.Categoria.FirstOrDefault(x => x.CategoriaID == vm.categoria).Nombre;


                if (vm.productoID.HasValue)
                {                    
                    producto = context.Producto.FirstOrDefault(x => x.ProductoID == vm.productoID);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(categoriaNombre+"-"+vm.nombre + ".jpg");
                        var path = Path.Combine(Server.MapPath("~/Content/images/productos"), fileName);
                        file.SaveAs(path);
                        producto.imagen = fileName;
                    }
                }
                else
                {
                    producto = new Producto();
                    context.Producto.Add(producto);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(categoriaNombre + "-" + vm.nombre+".jpg");
                        var path = Path.Combine(Server.MapPath("~/Content/images/productos"), fileName);
                        file.SaveAs(path);
                        producto.imagen = fileName;
                    }
                    else
                    {
                        producto.imagen = "default_product.gif";
                    }

                }

                producto.Nombre = vm.nombre;
                producto.Descripcion = vm.descripcion;
                producto.Costo = vm.costo;
                
                producto.EstablecimientoID = ((Asociado)Session["objUsuario"]).EstablecimientoID;
                producto.CategoriaID = vm.categoria;

                context.SaveChanges();

                return RedirectToAction("listarProductos");
            }
            catch (Exception)
            {
                vm.fill(context, null);
                TryUpdateModel(vm);
                return View(vm);
            }
        }

        

        public ActionResult estadoCuenta()
        {
            VmEstadoCuenta vmEstadoCuenta = new VmEstadoCuenta();
            vmEstadoCuenta.fill(((Asociado)Session["objUsuario"]).EstablecimientoID);
            return View(vmEstadoCuenta);
        }

    }
}