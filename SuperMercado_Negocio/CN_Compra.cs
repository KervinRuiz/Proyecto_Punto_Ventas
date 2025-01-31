using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Compra
    {
        public CD_Compra CD_Compra = new CD_Compra();

        public int ObtenerCorrelativo()
        {
            return CD_Compra.ObtenerCorrelativo();
        }
        public bool Registrar(Compras obj,DataTable DetalleCompra,out string Mensaje)
        {
            return CD_Compra.Registrar(obj,DetalleCompra,out Mensaje);
        }
        public Compras ObtenerCompra(string numero)
        {
            Compras oCompra = CD_Compra.ObtenerCompra(numero);
            if(oCompra.ID_Compra != 0 )
            {
                List<Detalle_Compra> oDetalleCompra = CD_Compra.ObtenerDetalleCompra(oCompra.ID_Compra);

                oCompra.oDetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }

        public List<Compras> Listar()
        {
            return CD_Compra.Listar();
        }
    }
}
