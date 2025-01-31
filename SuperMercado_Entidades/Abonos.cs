using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Abonos
    {
        public int ID_Abonos { get; set; }
        public Cliente Info_Cliente { get; set; }
        public decimal Monto_Abono { get; set; }
        public string Fecha_Registro { get; set; }
    }
}
