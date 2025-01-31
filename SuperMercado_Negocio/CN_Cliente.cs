using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Cliente
    {
        //Instanciamos de nuestra clase de CD_Usuarios
        private CD_Cliente clientedatos = new CD_Cliente();
        public List<Cliente> Listar()
        {
            return clientedatos.Listar();
        }
        public int Registrar(Cliente obj, out string Mensaje)
        {
            return clientedatos.Registrar(obj, out Mensaje);
        }
        public bool Editar(Cliente obj, out string Mensaje)
        {
            return clientedatos.Editar(obj, out Mensaje);
        }
        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return clientedatos.Eliminar(obj, out Mensaje);
        }

        public List<Credito_Cliente> ListarCreditos()
        {
            return clientedatos.ListarCreditos();
        }
    }
}
