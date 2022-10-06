using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Conection
    {
        public static string Conexion()
        {
            string con = ConfigurationManager.ConnectionStrings["FLuna20220907"].ConnectionString;
            return con;
        }
    }
}
