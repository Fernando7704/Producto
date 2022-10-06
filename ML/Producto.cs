using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        //public int IdProvedor { get; set; }
        public ML.Proveedor Provedor { get; set; }
        //public int IdDepartamento { get; set; }
        public ML.Departamento Departamento { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> Productos { get; set; }
        public string BASE64 { get; set; }
    }
}
