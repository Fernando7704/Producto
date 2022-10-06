using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            ML.Result result = BL.Producto.GetAll();
            ML.Producto producto = new ML.Producto();
            producto.Productos = new List<object>();
            producto.Productos = result.Objects;
            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            ML.Result Dep = BL.Departamento.GetAll();
            ML.Result Are = BL.Area.GetAll();
            ML.Result Pro = BL.Proveedor.GetAll();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Departamentos = new List<object>();
            producto.Departamento.Area = new ML.Area();
            producto.Departamento.Area.Areas = new List<object>();
            producto.Departamento.Departamentos = Dep.Objects;
            producto.Departamento.Area.Areas = Are.Objects;
            producto.Provedor = new ML.Proveedor();
            producto.Provedor.Provedores = new List<object>();
            producto.Provedor.Provedores = Pro.Objects;
            if (IdProducto==null)
            {               
                return View(producto);
            }else
            {
                ML.Result result = BL.Producto.GetById(IdProducto.Value);
                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    producto.Departamento.Departamentos = Dep.Objects;
                    //producto.Departamento.Area = new ML.Area();
                    producto.Departamento.Area.IdArea = ((ML.Producto)result.Object).Departamento.Area.IdArea;
                    producto.Departamento.Area.Nombre = ((ML.Producto)result.Object).Departamento.Area.Nombre;
                    producto.Departamento.Area.Areas = Are.Objects;
                    producto.Provedor.Provedores = Pro.Objects;
                    return View(producto);
                }else
                {
                    return View(producto);
                }
               
            }

        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            HttpPostedFileBase file = Request.Files["ImagenData"];
            if (file.ContentLength > 0)
            {
                producto.Imagen = ConvertToBytes(file);
            }
            if (producto.IdProducto == 0)
            {
               result = BL.Producto.Add(producto);
                if (result.Correct)
                {
                    ViewBag.Message = result.ErrorMessenge;
                   
                }

            }else
            {
                result = BL.Producto.Update(producto);
                ViewBag.Message = result.ErrorMessenge;
            }
            return PartialView("Modal");
        }
        [HttpPost]
        public JsonResult GetId(int IdArea)
        {
            var result = BL.Departamento.GetAllByArea(IdArea);
            List<SelectListItem> Opciones = new List<SelectListItem>();
            Opciones.Add(new SelectListItem { Text = "--Seleccionar el Departamento--", Value = "0" });
            if (result.Objects != null)
            {
                foreach(ML.Departamento departamento in result.Objects)
                {
                    Opciones.Add(new SelectListItem { Text = departamento.Nombre, Value = departamento.IdDepartamento.ToString() });
                }
            }
            return Json(new SelectList(Opciones,"Value","Text",JsonRequestBehavior.AllowGet));
        }
        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }
    }
}