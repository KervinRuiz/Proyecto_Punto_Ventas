using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Compras
    {
        public int ID_Compra { get; set; }
        public Usuariocs ID_Usuario_Compra { get; set; }
        public Provedores Info_Provedor { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero_Documento { get; set; }
        public decimal MontoTotal { get; set; }
        public List<Detalle_Compra> oDetalleCompra { get; set; }
        public string Fecha_Registro { get; set; }

    }
}
