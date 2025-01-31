using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Provedores
    {
        public int ID_Provedores { get; set; }
        public string NOmbre_Provedor { get; set; }
        public string Telefono_Provedor { get; set; }
        public bool Estado { get; set; }
        public string Fecha_Convenio { get; set; }
    }
}
