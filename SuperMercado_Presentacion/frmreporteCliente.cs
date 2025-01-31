using SuperMercado_Entidades;
using SuperMercado_Negocio;
using SuperMercado_Presentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmreporteCliente : Form
    {
        public frmreporteCliente()
        {
            InitializeComponent();
        }

        private void frmreporteCliente_Load(object sender, EventArgs e)
        {
            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dtcliente.Columns)
            {
                if (columna.Visible == true && columna.Name != "Column7")
                {
                    cmbuscar.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            //Definimos lo que tiene que verse en el comobo box
            cmbuscar.DisplayMember = "Texto";
            cmbuscar.ValueMember = "Valor";
            cmbuscar.SelectedIndex = 0;
            cargar();
        }

        private void cargar()
        {
            // Mostramos todos los usuarios
            List<Credito_Cliente> lista = new CN_Cliente().ListarCreditos();

            foreach (Credito_Cliente item in lista)
            {
                dtcliente.Rows.Add(new object[] {"",item.id_Cliente,item.Info_cliente.Cedula,
                item.Info_cliente.Nombre,
                item.Info_cliente.Apellidos,
                item.Monto_Credito,
                item.Fecha_Registro
                });
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            //Obtengo la columna para hacer la filtracion de busqueda
            string columnFiltro = ((OpcionCombo)cmbuscar.SelectedItem).Valor.ToString();

            //Validacion si existen las filas dentro del datagridw
            if (dtcliente.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dtcliente.Rows)
                {
                    //Aqui empezamos con la filtracion de los datos
                    if (row.Cells[columnFiltro].Value.ToString().Trim().ToLower().Contains(txtbuscar.Text.Trim().ToLower()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbuscar.Text = "";
            foreach (DataGridViewRow row in dtcliente.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
