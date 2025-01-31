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
    public partial class mdProductos : Form
    {
        public Producto _Producto { get; set; }
        public mdProductos()
        {
            InitializeComponent();
        }

        private void mdProductos_Load(object sender, EventArgs e)
        {
            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dgtproductos.Columns)
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

        public void Cargar()
        {
            // Mostramos todos los Provedores
            List<Producto> lista = new CN_Productos().Listar();

            foreach (Producto item in lista)
            {
                dgtproductos.Rows.Add(new object[] {item.ID_Producto,
                    item.NombreProducto,item.PrecioCompra,
                    item.Info_Categoria.ID_Categoria,
                    item.Info_Categoria.Tipo_Categoria
                });
            }
        }

        private void dgtproductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Capturamos los indices de la fila y columna
            int Row = e.RowIndex;
            int Column = e.ColumnIndex;

            //Validamos que sean mayores o igual a cero
            if (Row >= 0 && Column > 0)
            {
                _Producto = new Producto()
                {
                    ID_Producto = Convert.ToInt32(dgtproductos.Rows[Row].Cells["id"].Value.ToString()),
                    NombreProducto = dgtproductos.Rows[Row].Cells["Nombre"].Value.ToString(),
                    PrecioCompra = Convert.ToDecimal(dgtproductos.Rows[Row].Cells["Precio"].Value.ToString())
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
            if (dgtproductos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgtproductos.Rows)
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
            foreach (DataGridViewRow row in dgtproductos.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
