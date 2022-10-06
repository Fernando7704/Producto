using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Producto
    {
        public void GetAll()
        {
            ML.Result result = BL.Producto.GetAll();
            
            if (result.Correct)
            {
                foreach(ML.Producto producto in result.Objects)
                {
                    Console.WriteLine("El Id es " + producto.IdProducto);
                    Console.WriteLine("El nombre es " + producto.Nombre);
                    //p = producto;               
                }
            }else
            {
                Console.WriteLine("Hay un error");
            }
            
        }
        public void GetById(int IdProducto)
        {
            ML.Result result = BL.Producto.GetById(IdProducto);
            ML.Producto producto = new ML.Producto();
            if (result.Correct)
            {
                Console.WriteLine("Correcto");
                producto = (ML.Producto)result.Object;
            }
            else
            {
                Console.WriteLine("Incorrecto");
            }
        }
        public void Add()
        {
            ML.Producto producto = new ML.Producto();
            Console.WriteLine("Ingresa el nombre del producto");
            producto.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese la Descripcion");
            producto.Descripcion = Console.ReadLine();
            Console.WriteLine("Ingrese el precio");
            producto.Precio = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el stock");
            producto.Stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el IdProvedor");
            producto.Provedor.IdProvedor = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el IdDepartamento");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = int.Parse(Console.ReadLine());
            producto.Imagen = null;
            ML.Result result = BL.Producto.Add(producto);
            if (result.Correct)
            {
                Console.WriteLine(result.ErrorMessenge);
            }else
            {
                Console.WriteLine(result.ErrorMessenge);

            }
        }
        public void Update()
        {
            ML.Producto producto = new ML.Producto();
            Console.WriteLine("Ingresa el ID del producto");
            producto.IdProducto = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el nombre del producto");
            producto.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese la Descripcion");
            producto.Descripcion = Console.ReadLine();
            Console.WriteLine("Ingrese el precio");
            producto.Precio = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el stock");
            producto.Stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el IdProvedor");
            producto.Provedor.IdProvedor = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el IdDepartamento");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = int.Parse(Console.ReadLine());
            producto.Imagen = null;
            ML.Result result = BL.Producto.Update(producto);
            if (result.Correct)
            {
                Console.WriteLine(result.ErrorMessenge);
            }else
            {
                Console.WriteLine(result.ErrorMessenge);
            }
        }
    }
}
