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
    public class CD_Cliente
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idCliente,Cedula,Nombre,Apellidos,Telefono,Estado from Cliente");
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
                            lista.Add(new Cliente()
                            {
                                ID_Cliente = Convert.ToInt32(dr["idCliente"].ToString()),
                                Cedula = dr["Cedula"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Cliente>();
                }
                return lista;
            }
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Identificacion", obj.Cedula);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado",SqlDbType.Int).Direction = ParameterDirection.Output;
                    //Agregamos un nuevo sql comannd para ejecutar procedimiento
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    //Ejecutamos el comando
                    cmd.ExecuteNonQuery();

                    idgenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idgenerado = 0;
                Mensaje = ex.Message;
            }
            return idgenerado;
        }


        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_EditarCliente", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("idcliente", obj.ID_Cliente);
                    cmd.Parameters.AddWithValue("Cedula", obj.Cedula);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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


        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("delete from Cliente where Cedula = @id",oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.Cedula);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    //Ejecutamos el comando
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true: false;
                }
            }
            catch (Exception ex)
            {
                respuesta = true;
                Mensaje = ex.Message;
            }
            return respuesta;
        }


        public List<Credito_Cliente> ListarCreditos()
        {
            List<Credito_Cliente> lista = new List<Credito_Cliente>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select cc.ID_Cliente_Credito,c.Cedula,c.Nombre,c.Apellidos,cc.Monto_Credito,CONVERT(char(10),cc.Fecha_Registro, 103) AS Fecha_Registro");
                    query.AppendLine("from ClienteCredito cc inner join Cliente c on Cedula = Info_Cliente");
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
                            lista.Add(new Credito_Cliente()
                            {
                                id_Cliente = Convert.ToInt32(dr["ID_Cliente_Credito"]),
                                Info_cliente = new Cliente() { Cedula = dr["Cedula"].ToString(), Nombre = dr["Nombre"].ToString(), Apellidos = dr["Apellidos"].ToString() },
                                Monto_Credito = Convert.ToDecimal(dr["Monto_Credito"].ToString()),
                                Fecha_Registro = dr["Fecha_Registro"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Credito_Cliente>();
                    MessageBox.Show(ex.Message);
                }
                return lista;
            }
        }
    }
}
