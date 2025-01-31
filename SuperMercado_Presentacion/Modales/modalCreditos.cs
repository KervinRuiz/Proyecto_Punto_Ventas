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
    public partial class modalCreditos : Form
    {
        public Credito_Cliente _creditocliente { get; set; }
        public modalCreditos()
        {
            InitializeComponent();
        }

        private void modalCreditos_Load(object sender, EventArgs e)
        {
            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dgtcreditos.Columns)
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
            List<Credito_Cliente> listaCreditos = new CN_Creditos().Listar();

            foreach (Credito_Cliente item in listaCreditos)
            {
                dgtcreditos.Rows.Add(new object[] {item.id_Cliente,item.Info_cliente.Cedula,item.Monto_Credito,
                    item.Fecha_Registro
                });
            }
        }

        private void dgtcreditos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Capturamos los indices de la fila y columna
            int Row = e.RowIndex;
            int Column = e.ColumnIndex;

            //Validamos que sean mayores o igual a cero
            if (Row >= 0 && Column > 0)
            {
                _creditocliente = new Credito_Cliente()
                {
                    id_Cliente = Convert.ToInt32(dgtcreditos.Rows[Row].Cells["id"].Value.ToString()),
                    Monto_Credito = Convert.ToDecimal(dgtcreditos.Rows[Row].Cells["Monto"].Value.ToString()),
                    Fecha_Registro = dgtcreditos.Rows[Row].Cells["Fecha_Registro"].Value.ToString()
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
            if (dgtcreditos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgtcreditos.Rows)
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
            foreach (DataGridViewRow row in dgtcreditos.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
