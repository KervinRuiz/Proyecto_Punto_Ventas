using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Productos
    {
        //Instanciamos de nuestra clase de CD_Productos
        private CD_Productos producto = new CD_Productos();
        public List<Producto> Listar()
        {
            return producto.Listar();
        }
        public List<Roles> ListarRoles()
        {
            return producto.ListarRoles();
        }
        public int Registrar(Producto obj, out string Mensaje)
        {
            return producto.Registrar(obj, out Mensaje);
        }
        public bool Editar(Producto obj, out string Mensaje)
        {
            return producto.Editar(obj, out Mensaje);
        }
        public int Eliminar(Producto obj, out string Mensaje)
        {
            return producto.Eliminar(obj, out Mensaje);
        }
        public int DescontarCantidad(Producto obj, int descuento, out string Mensaje)
        {
            return producto.DescontarCantidad(obj,descuento,out Mensaje);
        }
        public int AumentarCantidad(Producto obj, int descuento)
        {
            return producto.AumentarCantidad(obj, descuento);
        }
        public decimal TotalDeProductos()
        {
            return producto.TotalDeProductos();
        } 
        public decimal TotalDeProductosConIva()
        {
            return producto.TotalDeProductosConIva();
        }
    }
}
