using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Entidades
{
    public class Producto
    {
        public int ID_Producto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioConIVA { get; set; }
        public Categoria_Producto Info_Categoria { get; set; }
        public string Codigo { get; set; }
        public string Iva {  get; set; }
        public string PorcentajeDeGanancia { get; set; }
        public int Cantidad { get; set; }
        public int CantidadAlerta { get; set; }
        public bool Estado { get; set; }
        public string Fecha_Agregado { get; set; }
    }
}
