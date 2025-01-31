using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Detalle_Compra
    {
        public int ID_detalle_Compra { get; set; }
        public Producto Info_Producto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int cantidad { get; set; }
        public decimal Monto_Total { get; set; }
        public string Fecha_Registro { get; set; }
    }
}
