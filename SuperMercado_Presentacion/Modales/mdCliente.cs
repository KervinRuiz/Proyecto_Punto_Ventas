using SuperMercado_Datos;
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

namespace SuperMercado_Presentacion.Modales
{
    public partial class mdCliente : Form
    {
        public Cliente _cliente {  get; set; }
        public mdCliente()
        {
            InitializeComponent();
        }

        private void mdCliente_Load(object sender, EventArgs e)
        {
            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dgtclientes.Columns)
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

            Cargar();
        }
        /// <summary>
        /// Metodo por el cual vamos a cargar datagriedview
        /// </summary>
        public void Cargar()
        {
            // Mostramos todos los usuarios
            List<Cliente> listaClientes = new CN_Cliente().Listar();

            foreach (Cliente item in listaClientes)
            {
                dgtclientes.Rows.Add(new object[] {item.ID_Cliente,item.Cedula,item.Nombre,
                    item.Telefono
                });
            }
        }

        private void dgtclientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Capturamos los indices de la fila y columna
            int Row = e.RowIndex;
            int Column = e.ColumnIndex;

            //Validamos que sean mayores o igual a cero
            if (Row >= 0 && Column > 0)
            {
                _cliente = new Cliente()
                {
                    ID_Cliente = Convert.ToInt32(dgtclientes.Rows[Row].Cells["id"].Value.ToString()),
                    Cedula = dgtclientes.Rows[Row].Cells["Cedula"].Value.ToString(),
                    Nombre = dgtclientes.Rows[Row].Cells["Nombre"].Value.ToString(),
                    Telefono = dgtclientes.Rows[Row].Cells["Telefono"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            //Obtengo la columna para hacer la filtracion de busqueda
            string columnFiltro = ((OpcionCombo)cmbuscar.SelectedItem).Valor.ToString();

            //Validacion si existen las filas dentro del datagridw
            if (dgtclientes.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgtclientes.Rows)
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
            foreach (DataGridViewRow row in dgtclientes.Rows)
            {
                row.Visible = true;
            }
        }

    }
}
