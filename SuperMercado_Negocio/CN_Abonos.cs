using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Abonos
    {
        private CD_Abonos CNAbonos = new CD_Abonos();
        public bool RegistrarAbono(Abonos obj, out string Mensaje)
        {
            return CNAbonos.RegistrarAbono(obj, out Mensaje);
        }
    }
}
