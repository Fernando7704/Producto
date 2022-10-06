using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
     public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT IdProvedor,Nombre FROM PROVEDOR";
                        cmd.Connection.Open();
                        SqlDataAdapter sa = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sa.Fill(dt);
                        if (dt.Rows.Count>0)
                        {
                            result.Objects = new List<object>();
                            foreach(DataRow row in dt.Rows)
                            {
                                ML.Proveedor provedor = new ML.Proveedor();
                                provedor.IdProvedor = int.Parse(row[0].ToString());
                                provedor.Nombre = row[1].ToString();
                                result.Objects.Add(provedor);
                            }
                            result.Correct = true;
                            result.ErrorMessenge = "Consulta éxitosa";
                            cmd.Connection.Close();
                            
                        }
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessenge = "Consulta no éxitosa";
            }
            return result;
        }
    }
}
