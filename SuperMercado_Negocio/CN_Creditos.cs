using SuperMercado_Datos;
using SuperMercado_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMercado_Negocio
{
    public class CN_Creditos
    {
        public CD_Creditos CD_Creditos = new CD_Creditos();

        public List<Credito_Cliente> Listar()
        {
            return CD_Creditos.Listar();
        }

        public bool RegistrarAbonos(Abonos obj, out string mensaje)
        {
            return CD_Creditos.RegistrarAbono(obj, out mensaje);
        }
    }
}
