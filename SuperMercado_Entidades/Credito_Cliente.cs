using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Credito_Cliente
    {
        public int id_Cliente { get; set; }
        public Cliente Info_cliente { get; set; }
        public decimal Monto_Credito { get; set; }
        public string Fecha_Registro { get; set; }
    }
}
