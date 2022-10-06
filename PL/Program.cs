using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            //ML.Result result = BL.Producto.getAll();
            PL.Producto producto = new PL.Producto();
            //producto.GetAll();
            //producto.GetById(1);
              producto.Add();
            //producto.Update();
            Console.ReadKey();
        }
    }
}
