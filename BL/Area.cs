using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    string query = "SELECT IdArea,Nombre FROM Area";
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = query;
                        SqlDataAdapter sa = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sa.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach(DataRow row in dt.Rows)
                            {
                                ML.Area area = new ML.Area();
                                area.IdArea = int.Parse(row[0].ToString());
                                area.Nombre = row[1].ToString();
                                result.Objects.Add(area);
                            }
                            result.Correct = true;
                            result.ErrorMessenge = "Consulta exitosa!";
                        }else
                        {
                            result.Correct = false;
                            result.ErrorMessenge = "Consulta no exitosa!";
                        }
                    }
                }

            }catch(Exception ex)
            {
                result.ErrorMessenge = ex.Message;
                result.Correct = false;

            }
            return result;
        }
    }
}
