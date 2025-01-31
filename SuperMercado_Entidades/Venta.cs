using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Venta
    {
        public int ID_Venta { get; set; }
        public Usuariocs ID_Usuario_Venta { get; set; }
        public Cliente Info_Cliente { get; set; }
        public string Info_Pago { get; set; } 
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoTotal { get; set; }
        public List<Detalle_Venta> oDetalleventa { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
