using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace BL
{
    public class Departamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    string sql = "SELECT IdDepartamento,Nombre,IdArea FROM DEPARTAMENTO";
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = sql;
                        SqlDataAdapter sa = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sa.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach(DataRow row in dt.Rows)
                            {
                                ML.Departamento departamento = new ML.Departamento();
                                departamento.IdDepartamento = int.Parse(row[0].ToString());
                                departamento.Nombre = row[1].ToString();
                                departamento.Area = new ML.Area();
                                departamento.Area.IdArea = int.Parse(row[2].ToString());
                                result.Objects.Add(departamento);
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
                result.Correct = false;
                result.ErrorMessenge = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAllByArea(int IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT IdDepartamento,Nombre FROM DEPARTAMENTO WHERE IdArea=" +IdArea;
                        cmd.Connection.Open();
                        SqlDataAdapter sa = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sa.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in dt.Rows)
                            {                              
                                ML.Departamento departamento = new ML.Departamento();
                                departamento.IdDepartamento = int.Parse(row[0].ToString());
                                departamento.Nombre = row[1].ToString();
                                result.Objects.Add(departamento);
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
                result.ErrorMessenge = ex.Message;
            }
            return result;
        }
    }
}
