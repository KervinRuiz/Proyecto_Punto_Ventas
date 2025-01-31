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
    public partial class modalCompras : Form
    {
        public Compras _compra { get; set; }
        public modalCompras()
        {
            InitializeComponent();
        }

        private void modalCompras_Load(object sender, EventArgs e)
        {
            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dgtdetalle.Columns)
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
            List<Compras> lista = new CN_Compra().Listar();

            foreach (Compras item in lista)
            {
                dgtdetalle.Rows.Add(new object[] {item.ID_Compra,item.Numero_Documento,item.Info_Provedor.NOmbre_Provedor,
                    item.Fecha_Registro
                });
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            //Obtengo la columna para hacer la filtracion de busqueda
            string columnFiltro = ((OpcionCombo)cmbuscar.SelectedItem).Valor.ToString();

            //Validacion si existen las filas dentro del datagridw
            if (dgtdetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgtdetalle.Rows)
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
            foreach (DataGridViewRow row in dgtdetalle.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgtdetalle_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Capturamos los indices de la fila y columna
            int Row = e.RowIndex;
            int Column = e.ColumnIndex;

            //Validamos que sean mayores o igual a cero
            if (Row >= 0 && Column > 0)
            {
                _compra = new Compras()
                {
                    ID_Compra = Convert.ToInt32(dgtdetalle.Rows[Row].Cells["id"].Value.ToString()),
                    Numero_Documento = dgtdetalle.Rows[Row].Cells["Numero_Documento"].Value.ToString(),
                    Fecha_Registro = dgtdetalle.Rows[Row].Cells["Fecha"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
