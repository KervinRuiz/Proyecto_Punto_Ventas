using SuperMercado_Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Datos
{
    public class CD_Compra
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
                    query.AppendLine("select count(*) + 1 from Compra");
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

        public bool Registrar(Compras obj,DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                { 
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand("sp_RegistroCompra".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idsuario",obj.ID_Usuario_Compra.ID_Usuarios);
                    cmd.Parameters.AddWithValue("Info_provedor",obj.Info_Provedor.ID_Provedores);
                    cmd.Parameters.AddWithValue("TipoDocumento",obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento",obj.Numero_Documento);
                    cmd.Parameters.AddWithValue("MontoTotal",obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleCompra",DetalleCompra);
                    cmd.Parameters.Add("Resultado",SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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

        public Compras ObtenerCompra(string numero)
        {
            Compras obj = new Compras();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.ID_Compra,u.Nombre_Usuario,");
                    query.AppendLine("pr.Nombre_Provedor,pr.Telefono_Provedor,");
                    query.AppendLine("c.TipoDocuemento,c.NumeroDocumento,c.MontoTotal, convert(char(10), c.Fecha_Registro , 103)[Fecha_Registro]");
                    query.AppendLine("from Compra c");
                    query.AppendLine("inner join Usuarios u on u.ID_Usuarios = c.ID_Usuario_Compra");
                    query.AppendLine("inner join Provedores pr on pr.ID_Provedores = c.Info_Provedor");
                    query.AppendLine("where c.NumeroDocumento = @Numero");
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
                            obj = new Compras()
                            {
                                ID_Compra = Convert.ToInt32(reader["ID_Compra"]),
                                ID_Usuario_Compra = new Usuariocs() { Nombre_Usuario = reader["Nombre_Usuario"].ToString() },
                                Info_Provedor = new Provedores() { NOmbre_Provedor = reader["Nombre_Provedor"].ToString(), Telefono_Provedor = reader["Telefono_Provedor"].ToString() },
                                TipoDocumento = reader["TipoDocuemento"].ToString(),
                                Numero_Documento = reader["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(reader["MontoTotal"].ToString()),
                                Fecha_Registro = reader["Fecha_Registro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Compras();
                   
                }
            }
            return obj;
        }

        public List<Detalle_Compra> ObtenerDetalleCompra(int idCompra)
        {
            List<Detalle_Compra> oLista = new List<Detalle_Compra>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.NombreProducto,");
                    query.AppendLine("dc.PrecioCompra,");
                    query.AppendLine("dc.Cantidad,");
                    query.AppendLine("dc.MontoTotal");
                    query.AppendLine("from Detalle_Compra dc");
                    query.AppendLine("inner join Productos p on p.ID_Producto = dc.Info_Producto");
                    query.AppendLine("where dc.Info_Compra = @IDCompra");
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IDCompra", idCompra);
                    cmd.CommandType = CommandType.Text;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oLista.Add(new Detalle_Compra()
                            {
                                Info_Producto = new Producto() { NombreProducto = reader["NombreProducto"].ToString() },
                                PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"].ToString()),
                                cantidad = Convert.ToInt32(reader["cantidad"].ToString()),
                                Monto_Total = Convert.ToDecimal(reader["MontoTotal"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    oLista = new List<Detalle_Compra>();
                }
            }
                return oLista;
        }

        public List<Compras> Listar()
        {
            List<Compras> lista = new List<Compras>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT dbo.Compra.ID_Compra,dbo.Compra.NumeroDocumento, dbo.Provedores.Nombre_Provedor,CONVERT(char(10),dbo.Compra.Fecha_Registro, 103) AS Fecha_Registro");
                    query.AppendLine("FROM dbo.Compra INNER JOIN dbo.Detalle_Compra ON dbo.Compra.ID_Compra = dbo.Detalle_Compra.Info_Compra INNER JOIN");
                    query.AppendLine("dbo.Provedores ON dbo.Compra.Info_Provedor = dbo.Provedores.ID_Provedores");
                    query.AppendLine("GROUP BY dbo.Compra.NumeroDocumento, dbo.Provedores.Nombre_Provedor, dbo.Compra.Fecha_Registro,dbo.Compra.ID_Compra");
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
                            lista.Add(new Compras()
                            {
                                ID_Compra = Convert.ToInt32(dr["ID_Compra"]),
                                Numero_Documento = dr["NumeroDocumento"].ToString(),
                                Info_Provedor = new Provedores() { NOmbre_Provedor = dr["Nombre_Provedor"].ToString() },
                                Fecha_Registro = dr["Fecha_Registro"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Compras>();
                }
                return lista;
            }
        }
    }
}
