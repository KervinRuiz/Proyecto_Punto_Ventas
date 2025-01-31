using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Cliente
    {
        public int ID_Cliente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set;}
        public string Telefono { get; set;}

        public bool Estado { get; set; }

        public string Fecha_Agregado { get; set; }
    }
}
