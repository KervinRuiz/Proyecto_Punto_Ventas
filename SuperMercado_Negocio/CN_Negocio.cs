using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Negocio
    {
        //Instanciamos de nuestra clase de CD_Usuarios
        private CD_Negocio Negocio = new CD_Negocio();
        public Negocio ObtenerDatos()
        {
            return Negocio.ObtenerDatos();
        }
        public bool GuardarDatos(Negocio obj, out string Mensaje)
        {
            return Negocio.GuardarDatos(obj, out Mensaje);
        }
        public byte[] ObtenerLogo(out bool Mensaje)
        {
            return Negocio.ObtenerLogo(out Mensaje);
        }
        public bool ActualizarLogo(byte[] imagen, out string Mensaje)
        {
            return Negocio.ActualizarLogo(imagen, out Mensaje);
        }
    }
}
