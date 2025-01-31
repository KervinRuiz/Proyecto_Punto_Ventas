using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SuperMercado_Datos
{
    public class CD_Usuarios
    {
        public List<Usuariocs> Listar()
        {
           List<Usuariocs> lista = new List<Usuariocs>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select ID_Usuarios,No_Usuario,Nombre_Usuario,Contra,Correo,Estado,id_rol from Usuarios");
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd  = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    //Abrimos la cadena de conexion para que pueda ejecutarse el comando
                    oconexion.Open();
                    //Utilizamos el usign para poder leer el resultado de nuestro comando
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    { 
                        // Mientras lee los registros, los vayas almacenando en nuestra lista
                        while (dr.Read())
                        {
                            lista.Add(new Usuariocs()
                            {
                                ID_Usuarios = Convert.ToInt32(dr["ID_Usuarios"]),
                                No_Usuario = dr["No_Usuario"].ToString(),
                                Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                                Contra = dr["Contra"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                IdRol = new Roles() { Id_Rol = Convert.ToInt32(dr["id_rol"])}
                            });
                        }
                    }

                }catch(Exception ex)
                {
                    lista = new List<Usuariocs>();
                }
                return lista;
            }
        }

        public int Registrar(Usuariocs obj, out string Mensaje)
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_InsertarUsuarios", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Identificacion", obj.No_Usuario);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("Contra", obj.Contra);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("id_Rol", obj.IdRol.Id_Rol);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    //Agregamos un nuevo sql comannd para ejecutar procedimiento
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    //Ejecutamos el comando
                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }
            return idusuariogenerado;
        }


        public bool Editar(Usuariocs obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuarios", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Identificacion", obj.No_Usuario);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("Contra", obj.Contra);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("id_Rol", obj.IdRol.Id_Rol);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    //Agregamos un nuevo sql comannd para ejecutar procedimiento
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    //Ejecutamos el comando
                    cmd.ExecuteNonQuery();

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


        public int Eliminar(Usuariocs obj, out string Mensaje)
        {
            int respuesta = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    //Llamamos al procedimiento almacenado para insertasr usuarios
                    SqlCommand cmd = new SqlCommand("sp_EliminarUsuarios", oconexion);
                    //Agregamos los parametros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Identificacion", obj.No_Usuario);
                    //Declaramos los parametros de salida
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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
    }
}
