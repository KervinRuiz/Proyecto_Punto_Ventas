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
    public class CD_Creditos
    {
        public List<Credito_Cliente> Listar()
        {
            List<Credito_Cliente> lista = new List<Credito_Cliente>();

            //Me conecto a la base de datos
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un query para seleccionar a tabla de usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select ID_Cliente_Credito,c.Cedula,Monto_Credito,CONVERT(char(10),Fecha_Registro, 103) AS Fecha_Registro");
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
                                Info_cliente = new Cliente() { Cedula = dr["Cedula"].ToString()},
                                Monto_Credito = Convert.ToDecimal(dr["Monto_Credito"].ToString()),
                                Fecha_Registro = dr["Fecha_Registro"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Credito_Cliente>();
                }
                return lista;
            }
        }
        public bool RegistrarAbono(Abonos obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //Agregamos un nuevo sql comannd para ejecutar el query
                    SqlCommand cmd = new SqlCommand("sp_RegistroAbonos".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("Info_Cliente", obj.Info_Cliente.Cedula);
                    cmd.Parameters.AddWithValue("Monto", obj.Monto_Abono);
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
    }
}
