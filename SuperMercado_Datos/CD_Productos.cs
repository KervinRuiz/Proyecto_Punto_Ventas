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
    public class CD_Productos
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select ID_Producto,Codigo,NombreProducto,Iva,PrecioCompra,PrecioConIva,p.Cantidad,p.PorcentajeDeGanancia,p.CantidadAlerta,c.ID_Categoria,c.Tipo_Categoria,p.Estado from Productos p");
                    query.AppendLine("inner join Categoria_Productos c on c.ID_Categoria = p.Info_Categoria");
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
                            lista.Add(new Producto()
                            {
                                ID_Producto = Convert.ToInt32(dr["ID_Producto"]),
                                Codigo = dr["Codigo"].ToString(),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                CantidadAlerta = Convert.ToInt32(dr["CantidadAlerta"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Iva = dr["Iva"].ToString(),
                                PorcentajeDeGanancia = dr["PorcentajeDeGanancia"].ToString(),
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                PrecioConIVA = Convert.ToDecimal(dr["PrecioConIva"].ToString()),
                                Info_Categoria = new Categoria_Producto() {ID_Categoria = Convert.ToInt32(dr["ID_Categoria"]),Tipo_Categoria = dr["Tipo_Categoria"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                     MessageBox.Show("ERROR");
                }
                return lista;
            }
        }
        public decimal TotalDeProductos()
        {
           decimal total = 0;

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT @Total = SUM(PrecioCompra * Cantidad) FROM Productos");
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    SqlParameter paramTotal = new SqlParameter("@Total", SqlDbType.Decimal);
                    paramTotal.Direction = ParameterDirection.Output;
                    paramTotal.Precision = 10;
                    paramTotal.Scale = 2;
                    cmd.Parameters.Add(paramTotal);
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    //Utilizamos el usign para poder leer el resultado de nuestro comando
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Obtenemos el valor del parámetro de salida
                        if (paramTotal.Value != DBNull.Value)
                        {
                            total = Convert.ToDecimal(paramTotal.Value);
                        }
                    }
                }
                catch (Exception ex)
                {;
                    MessageBox.Show("ERROR");
                }
                return total;
            }
        }
        public decimal TotalDeProductosConIva()
        {
            decimal total = 0;

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT @Total = SUM(PrecioConIva * Cantidad) FROM Productos");
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    SqlParameter paramTotal = new SqlParameter("@Total", SqlDbType.Decimal);
                    paramTotal.Direction = ParameterDirection.Output;
                    paramTotal.Precision = 10;
                    paramTotal.Scale = 2;
                    cmd.Parameters.Add(paramTotal);
                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    //Utilizamos el usign para poder leer el resultado de nuestro comando
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Obtenemos el valor del parámetro de salida
                        if (paramTotal.Value != DBNull.Value)
                        {
                            total = Convert.ToDecimal(paramTotal.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ;
                    MessageBox.Show("ERROR");
                }
                return total;
            }
        }
        public int Registrar(Producto obj, out string Mensaje)
        {
            int idgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_IngresarProductos", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombre", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("Precio", obj.PrecioCompra);
                    cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("CantidadAlerta", obj.CantidadAlerta);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("IVA", obj.Iva);
                    cmd.Parameters.AddWithValue("Ganancia", obj.PorcentajeDeGanancia);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Categoria", obj.Info_Categoria.ID_Categoria);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    //Agregamos un nuevo sql comannd para ejecutar procedimiento
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    //Ejecutamos el comando
                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    idgenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                idgenerado = 0;
                Mensaje = ex.Message;
            }
            return idgenerado;
        }


        public bool Editar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Idproducto", obj.ID_Producto);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("Precio", obj.PrecioCompra);
                    cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("Alerta", obj.CantidadAlerta);
                    cmd.Parameters.AddWithValue("IVA", obj.Iva);
                    cmd.Parameters.AddWithValue("Ganancia", obj.PorcentajeDeGanancia);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    //Agregamos un nuevo sql comannd para ejecutar procedimiento
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    //Ejecutamos el comando
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }


        public int Eliminar(Producto obj, out string Mensaje)
        {
            int respuesta = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_ElimarProducto", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Idproducto", obj.ID_Producto);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    //Agregamos un nuevo sql comannd para ejecutar procedimiento
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    //Ejecutamos el comando
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                Mensaje = ex.Message;
            }
            return respuesta;
        }
        public int DescontarCantidad(Producto obj, int descuento, out string Mensaje)
        {
            int respuesta = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    // Llamamos al procedimiento almacenado para descontar la cantidad
                    SqlCommand cmd = new SqlCommand("sp_DescontarCantidad", conexion);
                   
                    // Agregamos los parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("ID", obj.ID_Producto);
                    cmd.Parameters.AddWithValue("Descuento", descuento);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("MensajeOut", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    // Abrimos la conexión y ejecutamos el comando
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    // Obtener el mensaje de salida
                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["MensajeOut"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                Mensaje = ex.Message;
            }
            return respuesta;
        }
        public int AumentarCantidad(Producto obj, int descuento)
        {
            int respuesta = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    // Llamamos al procedimiento almacenado para descontar la cantidad
                    SqlCommand cmd = new SqlCommand("sp_AumentarCantidad", conexion);

                    // Agregamos los parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("id", obj.ID_Producto);
                    cmd.Parameters.AddWithValue("CantidadAumento", descuento);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // Abrimos la conexión y ejecutamos el comando
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
            }
            return respuesta;
        }
        public List<Roles> ListarRoles()
        {
            List<Roles> lista = new List<Roles>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select ID_Rol,Nombre_Rol from Roles");
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
                            lista.Add(new Roles()
                            {
                                Id_Rol = Convert.ToInt32(dr["ID_Rol"]),
                                NombreRol = dr["Nombre_Rol"].ToString(),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Roles>();
                }
                return lista;
            }
        }
    }
}
