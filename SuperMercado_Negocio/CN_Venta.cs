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
    public class CN_Venta
    {
        public CD_Ventas CDVentas = new CD_Ventas();
        public int ObtenerCorrelativo()
        {
            return CDVentas.ObtenerCorrelativo();
        }
        public Dictionary<string, decimal> ObtenerTotalesPorTipoPago()
        {
            return CDVentas.ObtenerTotalesPorTipoPago();
        }
        public bool RegistrarCredito(Credito_Cliente obj,out string Mensaje)
        {
            return CDVentas.RegistrarCredito(obj,out Mensaje);
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return CDVentas.Registrar(obj, DetalleVenta, out Mensaje);
        }
        public Venta ObtenerVenta(string numero)
        {
            Venta oVenta = CDVentas.ObtenerVenta(numero);
            if (oVenta.ID_Venta != 0)
            {
                List<Detalle_Venta> oDetalleVenta = CDVentas.ObtenerDetalleVenta(oVenta.ID_Venta);

                oVenta.oDetalleventa = oDetalleVenta;
            }
            return oVenta;
        }

        public List<Venta> Listar()
        {
            return CDVentas.Listar();
        }
    }
}
