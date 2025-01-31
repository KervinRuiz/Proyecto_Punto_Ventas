using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Provedores
    {
        //Instanciamos de nuestra clase de CD_Provedores
        private CD_Prrovedores provedor = new CD_Prrovedores();
        public List<Provedores> Listar()
        {
            return provedor.Listar();
        }
        public int Registrar(Provedores obj, out string Mensaje)
        {
            return provedor.Registrar(obj, out Mensaje);
        }
        public bool Editar(Provedores obj, out string Mensaje)
        {
            return provedor.Editar(obj, out Mensaje);
        }
        public bool Eliminar(Provedores obj, out string Mensaje)
        {
            return provedor.Eliminar(obj, out Mensaje);
        }   
    }
}
