using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Usuario
    {
        //Instanciamos de nuestra clase de CD_Usuarios
        private CD_Usuarios usuario = new CD_Usuarios();
        public List<Usuariocs> Listar()
        {
            return usuario.Listar();
        }
        public int Registrar(Usuariocs obj, out string Mensaje)
        {
            return usuario.Registrar(obj,out Mensaje);
        }
        public bool Editar(Usuariocs obj, out string Mensaje)
        {
            return usuario.Editar(obj, out Mensaje);
        }
        public int Eliminar(Usuariocs obj, out string Mensaje)
        {
            return usuario.Eliminar(obj, out Mensaje);
        }
    }
}
