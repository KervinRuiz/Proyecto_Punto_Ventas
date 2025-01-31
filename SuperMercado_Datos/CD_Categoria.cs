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
    public class CD_Categoria
    {
        public List<Categoria_Producto> Listar()
        {
            List<Categoria_Producto> lista = new List<Categoria_Producto>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select ID_Categoria,Tipo_Categoria,Estado from Categoria_Productos");
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
                            lista.Add(new Categoria_Producto()
                            {
                                ID_Categoria = Convert.ToInt32(dr["ID_Categoria"]),
                                Tipo_Categoria = dr["Tipo_Categoria"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Categoria_Producto>();
                }
                return lista;
            }
        }

        public bool Registrar(Categoria_Producto obj, out string Mensaje)
        {
            bool idcategoriagenerado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertar categorias
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Tipo_Categoria", obj.Tipo_Categoria);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    //Agregamos un nuevo sql comannd para ejecutar procedimiento
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    //Ejecutamos el comando
                    cmd.ExecuteNonQuery();

                    idcategoriagenerado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idcategoriagenerado = false;
                Mensaje = ex.Message;
            }
            return idcategoriagenerado;
        }


        public bool Editar(Categoria_Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Id_Categoria", obj.ID_Categoria);
                    cmd.Parameters.AddWithValue("Tipo_Categoria", obj.Tipo_Categoria);
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


        public bool Eliminar(Categoria_Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdCategoria", obj.ID_Categoria);
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
