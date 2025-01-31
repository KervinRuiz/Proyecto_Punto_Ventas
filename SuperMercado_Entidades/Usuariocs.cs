using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Usuariocs
    {
        public int ID_Usuarios { get; set; }
        public string No_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Contra { get; set; }
        public bool Estado { get; set; }
        public string Correo { get; set; }
        public Roles IdRol {  get; set; }
        public string Fecha_Agregado { get; set;}
    }
}
