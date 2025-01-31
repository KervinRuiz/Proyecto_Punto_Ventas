using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Categoria
    {
        //Instanciamos de nuestra clase de CD_Categoria
        private CD_Categoria Categoria = new CD_Categoria();
        public List<Categoria_Producto>Listar()
        {
            return Categoria.Listar();
        }
        public bool Registrar(Categoria_Producto obj, out string Mensaje)
        {
            return Categoria.Registrar(obj, out Mensaje);
        }
        public bool Editar(Categoria_Producto obj, out string Mensaje)
        {
            return Categoria.Editar(obj, out Mensaje);
        }
        public bool Eliminar(Categoria_Producto obj, out string Mensaje)
        {
            return Categoria.Eliminar(obj, out Mensaje);
        }
    }
}
