using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Datos
{
    public class CD_Ventas
    {
        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from Venta");
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();

                    idCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    idCorrelativo = 0;
                }
            }
            return idCorrelativo;
        }
        public Dictionary<string, decimal> ObtenerTotalesPorTipoPago()
        {
            Dictionary<string, decimal> totalesPorTipoPago = new Dictionary<string, decimal>();

            string query = "SELECT TipoPago, SUM(MontoTotal) as TotalPorTipoPago FROM Venta WHERE CAST(Fecha_Registro AS DATE) = CAST(GETDATE() AS DATE) GROUP BY TipoPago";

            using (SqlConnection connection = new SqlConnection(Conexion.cadena))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipoPago = reader["TipoPago"].ToString();
                            decimal totalPorTipoPago = Convert.ToDecimal(reader["TotalPorTipoPago"]);
                            totalesPorTipoPago.Add(tipoPago, totalPorTipoPago);
                        }
                    }
                }
            }
            return totalesPorTipoPago;
        }
        public bool RegistrarCredito(Credito_Cliente obj,out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand("sp_RegistroCredito".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("Info_Cliente", obj.Info_cliente.Cedula);
                    cmd.Parameters.AddWithValue("Monto", obj.Monto_Credito);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    cmd.ExecuteNonQuery();


                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
                return Respuesta;
            }
        }

        public Venta ObtenerVenta(string numero)
        {
            Venta obj = new Venta();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.ID_Venta,u.Nombre_Usuario,v.TipoDocumento,v.NumeroDocumento,");
                    query.AppendLine("v.MontoCambio,v.MontoPago,v.MontoTotal,");
                    query.AppendLine("convert(char(19),v.Fecha_Registro,120)[Fecha_Registro]");
                    query.AppendLine("from Venta v inner join Usuarios u on u.ID_Usuarios = v.ID_Usuario_Venta");
                    query.AppendLine("where v.NumeroDocumento = @Numero");
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@Numero", numero);
                    cmd.CommandType = CommandType.Text;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj = new Venta()
                            {
                                ID_Venta = Convert.ToInt32(reader["ID_Venta"]),
                                ID_Usuario_Venta = new Usuariocs() { Nombre_Usuario = reader["Nombre_Usuario"].ToString() },
                                TipoDocumento = reader["TipoDocumento"].ToString(),
                                NumeroDocumento = reader["NumeroDocumento"].ToString(),
                                MontoCambio = Convert.ToDecimal(reader["MontoCambio"].ToString()),
                                MontoPago = Convert.ToDecimal(reader["MontoPago"].ToString()),
                                MontoTotal = Convert.ToDecimal(reader["MontoTotal"].ToString()),
                                Fecha_Registro = Convert.ToDateTime(reader["Fecha_Registro"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Venta();
                    MessageBox.Show(ex.ToString());
                }
            }
            return obj;
        }

        public List<Detalle_Venta> ObtenerDetalleVenta(int idVenta)
        {
            List<Detalle_Venta> oLista = new List<Detalle_Venta>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.NombreProducto,dv.Precio_Venta,dv.Cantidad,dv.SubTotal,p.PrecioConIva,p.Iva");
                    query.AppendLine("from Detalle_Venta dv");
                    query.AppendLine("inner join Productos p on p.ID_Producto = dv.Info_Producto");
                    query.AppendLine("where dv.Info_Venta = @IDVenta");
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IDVenta", idVenta);
                    cmd.CommandType = CommandType.Text;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oLista.Add(new Detalle_Venta()
                            {
                                oProducto = new Producto() { NombreProducto = reader["NombreProducto"].ToString(),Iva = reader["Iva"].ToString() },
                                Precio_Venta = Convert.ToDecimal(reader["Precio_Venta"].ToString()),
                                cantidad = Convert.ToInt32(reader["cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"].ToString())
                                
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    oLista = new List<Detalle_Venta>();
                }
            }
            return oLista;
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVenta".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("Info_Cliente", obj.Info_Cliente.Cedula);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.ID_Usuario_Venta.ID_Usuarios);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("TipoPago", obj.Info_Pago);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    cmd.ExecuteNonQuery();


                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
                return Respuesta;
            }
        }

        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT dbo.Venta.ID_Venta, dbo.Venta.NumeroDocumento, dbo.Cliente.Nombre, dbo.Cliente.Cedula, CONVERT(char(19),Venta.Fecha_Registro, 120) AS Fecha_Registro");
                    query.AppendLine("FROM dbo.Detalle_Venta INNER JOIN");
                    query.AppendLine("dbo.Venta ON dbo.Detalle_Venta.Info_Venta = dbo.Venta.ID_Venta INNER JOIN");
                    query.AppendLine("dbo.Cliente ON dbo.Venta.Info_Cliente = dbo.Cliente.Cedula INNER JOIN");
                    query.AppendLine("dbo.Productos ON dbo.Detalle_Venta.Info_Producto = dbo.Productos.ID_Producto");
                    query.AppendLine("GROUP BY dbo.Venta.NumeroDocumento, dbo.Cliente.Nombre, dbo.Cliente.Cedula, dbo.Venta.Fecha_Registro, dbo.Venta.ID_Venta");
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    //Utilizamos el usign para poder leer el resultado de nuestro comando
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras lee los registros, los vayas almacenando en nuestra lista
                        while (dr.Read())
                        {
                            lista.Add(new Venta()
                            {
                                ID_Venta= Convert.ToInt32(dr["ID_Venta"]),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Info_Cliente =  new Cliente() { Cedula = dr["Cedula"].ToString(),Nombre = dr["Nombre"].ToString() },
                                Fecha_Registro = Convert.ToDateTime(dr["Fecha_Registro"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Venta>();
                }
                return lista;
            }
        }
    }
}
