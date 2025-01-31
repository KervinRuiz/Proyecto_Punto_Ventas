using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Datos
{
    public class CD_Prrovedores
    {
        public List<Provedores> Listar()
        {
            List<Provedores> lista = new List<Provedores>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select ID_Provedores,Nombre_Provedor,Telefono_Provedor,Estado from Provedores");
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
                            lista.Add(new Provedores()
                            {
                                ID_Provedores = Convert.ToInt32(dr["ID_Provedores"]),
                                NOmbre_Provedor = dr["Nombre_Provedor"].ToString(),
                                Telefono_Provedor = dr["Telefono_Provedor"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Provedores>();
                }
                return lista;
            }
        }

        public int Registrar(Provedores obj, out string Mensaje)
        {
            int idgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProvedores", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("NombreProvedor", obj.NOmbre_Provedor);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono_Provedor);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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


        public bool Editar(Provedores obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_ModificarProvedor", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("IDProvedor", obj.ID_Provedores);
                    cmd.Parameters.AddWithValue("NombreProvedor", obj.NOmbre_Provedor);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono_Provedor);
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


        public bool Eliminar(Provedores obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_EliminarProvedor", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdProvedor", obj.ID_Provedores);
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
    }
}
