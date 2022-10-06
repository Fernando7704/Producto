using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    string query = "SELECT [IdProducto],[Nombre],[Descripcion],[Precio]" +
                                  ",[Stock],[IdProvedor],[IdDepartamento],[Imagen]" +
                                  "FROM [Producto]";
                    using (SqlCommand cmd = new SqlCommand(query,con))
                    {
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count>0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in dt.Rows)
                            {
                                ML.Producto producto = new ML.Producto();
                                producto.IdProducto = Convert.ToInt32(row[0]);
                                producto.Nombre = row[1].ToString();
                                producto.Descripcion = row[2].ToString();
                                producto.Precio = Convert.ToDecimal(row[3]);
                                producto.Stock = Convert.ToInt16(row[4]);
                                producto.Provedor = new ML.Proveedor();
                                producto.Provedor.IdProvedor = Convert.ToInt16(row[5]);
                                producto.Departamento = new ML.Departamento();
                                producto.Departamento.IdDepartamento = Convert.ToInt16(row[6]);
                                if (row[7].ToString() != "")
                                {
                                    //producto.Imagen = System.Convert.FromBase64String(producto.BASE64 = row[7].ToString());
                                    producto.Imagen = System.Convert.FromBase64String(row[7].ToString());
                                }
                                else
                                {
                                    producto.Imagen = null;
                                }
                                result.Objects.Add(producto);
                            }
                            result.Correct = true;
                            result.ErrorMessenge = "Consulta Exitosa";

                        }else
                        {
                            result.Correct = false;
                            result.ErrorMessenge = "No hay datos existentes";
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
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    //string query = "SELECT [IdProducto],[Nombre],[Descripcion],[Precio]" +
                    //              ",[Stock],[IdProvedor],[IdDepartamento] FROM PRODUCTO WHERE IdProducto ="+IdProducto;
                    string query = "SELECT P.[IdProducto], P.[Nombre],P.[Descripcion],P.[Precio],P.[Stock],P.[IdProvedor],P.[IdDepartamento],P.[Imagen]"
                    + ",PRO.[Nombre] AS NombreProvedor ,PRO.ApellidoPaterno, PRO.ApellidoMaterno, PRO.Telefono, D.Nombre AS NombreDepartamento  , D.IdArea,A.Nombre AS NombreArea FROM[dbo].[Producto]  AS P INNER JOIN Provedor AS PRO ON P.IdProvedor= PRO.IdProvedor INNER JOIN Departamento AS D ON P.IdDepartamento=D.IdDepartamento INNER JOIN Area AS A ON D.IdArea =A.IdArea WHERE P.IdProducto=" + IdProducto;
                    using(SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = Convert.ToInt32(row[0]);
                            producto.Nombre = row[1].ToString();
                            producto.Descripcion = row[2].ToString();
                            producto.Precio = Convert.ToDecimal(row[3]);
                            producto.Stock = Convert.ToInt16(row[4]);
                            producto.Provedor = new ML.Proveedor();
                            producto.Provedor.IdProvedor = Convert.ToInt16(row[5]);
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = Convert.ToInt16(row[6]);
                            if (row[7].ToString() != "")
                            {
                                //producto.Imagen = System.Convert.FromBase64String(producto.BASE64 = row[7].ToString());
                                producto.Imagen = System.Convert.FromBase64String(row[7].ToString());
                            }
                            else
                            {
                                producto.Imagen = null;
                            }
                            producto.Provedor.Nombre = row[8].ToString();
                            producto.Provedor.ApellidoPaterno = row[9].ToString();
                            producto.Provedor.ApellidoMaterno = row[10].ToString();
                            producto.Provedor.Telefono = row[11].ToString(); 
                            producto.Departamento.Nombre= row[12].ToString();
                            producto.Departamento.Area = new ML.Area();
                            producto.Departamento.Area.IdArea = int.Parse(row[13].ToString());
                            producto.Departamento.Area.Nombre = row[14].ToString();
                            result.Object = producto;
                            result.Correct = true;
                            result.ErrorMessenge = "Consulta exitosa";

                        }else
                        {
                            result.Correct = false;
                            result.ErrorMessenge = "No hay registros";
                        }
                    }

                }

            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessenge = ex.Message;
            }

            return result;
        }
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                string base64String = Convert.ToBase64String(producto.Imagen, 0, producto.Imagen.Length);
                using (SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    string query = "INSERT INTO [dbo].[Producto]" +
                    "([Nombre],[Descripcion],[Precio],[Stock],[IdProvedor],[IdDepartamento],[Imagen])VALUES('" +
                    producto.Nombre + "','" +
                    producto.Descripcion + "'," +
                    producto.Precio + "," +
                    producto.Stock + "," +
                    producto.Provedor.IdProvedor + "," +
                    producto.Departamento.IdDepartamento + ",'" +
                    base64String + "')";
                    using(SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        int RowsAffect = cmd.ExecuteNonQuery();
                        if (RowsAffect > 0)
                        {
                            result.Correct = true;
                            result.ErrorMessenge = "Se ha insertado correctamente los registros";
                        }else
                        {
                            result.Correct = false;
                            result.ErrorMessenge = "No se ha insertado correctamente los registros";
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
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection con = new SqlConnection(DL.Conection.Conexion()))
                {
                    string base64String = Convert.ToBase64String(producto.Imagen, 0, producto.Imagen.Length);
                    string query = "UPDATE Producto SET Nombre ='"+producto.Nombre+"',Descripcion ='"+producto.Descripcion+"',Precio="+producto.Precio+",Stock="+producto.Stock+",IdProvedor="+producto.Provedor.IdProvedor+",IdDepartamento = "+producto.Departamento.IdDepartamento+",Imagen='"+ base64String + "' WHERE IdProducto ="+producto.IdProducto;
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = query;
                        cmd.Connection.Open();
                        
                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                            result.ErrorMessenge = "Se ha actualizado correctamente el producto " + producto.IdProducto;
                        }else
                        {
                            result.Correct = false;
                            result.ErrorMessenge = "No se ha actualizado correctamente el producto " + producto.IdProducto;
                        }
                        cmd.Connection.Close();
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
