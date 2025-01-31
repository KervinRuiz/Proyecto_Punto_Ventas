using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Detalle_Venta
    {
        public int ID_Detalle_Venta { get; set; }
        public Producto oProducto { get; set; }
        public decimal Precio_Venta { get; set; }
        public int cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public string Fecha_Regitro { get; set; }
    }
}
