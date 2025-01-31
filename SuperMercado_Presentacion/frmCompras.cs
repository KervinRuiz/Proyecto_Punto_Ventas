using SuperMercado_Entidades;
using SuperMercado_Negocio;
using SuperMercado_Presentacion.Modales;
using SuperMercado_Presentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmCompras : Form
    {
        private Usuariocs _Usuario;
        public frmCompras(Usuariocs oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            //Llenamos el comobo box de tipo documento, para mostrarlo en el datagriewd
            cmbtipodocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cmbtipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            //Definimos lo que tiene que verse en el comobo box
            cmbtipodocumento.DisplayMember = "Texto";
            cmbtipodocumento.ValueMember = "Valor";
            cmbtipodocumento.SelectedIndex = 0;

            //Definimos la fecha actual en la caja de texto que se muestra en el formulario
            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtfecha.Enabled = false;

            txtidproducto.Text = "0";
            txtidprovedor.Text = "0";
        }

        private void btnbuscarprovedor_Click(object sender, EventArgs e)
        {
            // Al dar click al boton abriremos el modal donde se muestran
            // todos los provedores en listados
            using(var ModalProvedor = new mdProvedor())
            {
                var result = ModalProvedor.ShowDialog();
                
                if (result == DialogResult.OK)
                {
                    txtidprovedor.Text = ModalProvedor._Provedor.ID_Provedores.ToString();
                    txtnombreprovedor.Text = ModalProvedor._Provedor.NOmbre_Provedor;
                    txttelefonoprovedor.Text = ModalProvedor._Provedor.Telefono_Provedor;
                }
                else
                {
                    txtidprovedor.Select();
                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using(var ModalProducto = new mdProductos())
            {
                var result = ModalProducto.ShowDialog();

                if(result == DialogResult.OK)
                {
                    txtidproducto.Text = ModalProducto._Producto.ID_Producto.ToString();
                    txtnombreproducto.Text = ModalProducto._Producto.NombreProducto.ToString();
                    txtprecioVenta.Text = ModalProducto._Producto.PrecioCompra.ToString();
                    txtprecioCompra.Select();
                }
                else
                {
                    txtidproducto.Select();
                }
                
            }
        }

        private void txtnombreproducto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                Producto oProducto = new CN_Productos().Listar()
                .Where(p => p.NombreProducto == txtnombreproducto.Text && p.Estado == true)
                .FirstOrDefault();

                if(oProducto != null)
                {
                    txtnombreproducto.BackColor = Color.Honeydew;
                    txtidproducto.Text = oProducto.ID_Producto.ToString();
                    txtprecioVenta.Text = oProducto.PrecioCompra.ToString();
                    txtprecioCompra.Select();
                }
                else
                {
                    txtidproducto.Text = "0";
                    MessageBox.Show("Producto no encontrado");
                }
            }
        }

        private void txtnombreprovedor_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                Provedores oProvedor = new CN_Provedores().Listar()
                .Where(p => p.NOmbre_Provedor == txtnombreprovedor.Text && p.Estado == true)
                .FirstOrDefault();

                if(oProvedor != null)
                {
                    txtnombreprovedor.BackColor = Color.Honeydew;
                    txtidprovedor.Text = oProvedor.ID_Provedores.ToString();
                    txtnombreprovedor.Text = oProvedor.NOmbre_Provedor;
                    txttelefonoprovedor.Text = oProvedor.Telefono_Provedor;
                }
                else
                {
                    MessageBox.Show("Provedor no encontrado");
                    txtidprovedor.Text = "0";
                }
            }
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            decimal precioCompra = 0;
            decimal precioVenta = 0;
            bool producto_Existe = false;
            //Validacion de que nesecita seleccionar un producto 
            if(int.Parse(txtidproducto.Text) == 0)
            {
                MessageBox.Show("Debes seleccionar un producto","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            // Validacion si en los campos de texto de los precios son del formato correcto
            if(!decimal.TryParse(txtprecioCompra.Text, out precioCompra))
            {
                MessageBox.Show("Formato de moneda incorrecto","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txtprecioCompra.Select();
                return;
            }
            if(!decimal.TryParse(txtprecioVenta.Text, out precioVenta))
            {
                MessageBox.Show("Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioVenta.Select();
                return;
            }
            //Validacion de si ya existe este producto dentro del dataGriedView
            foreach (DataGridViewRow file in dtgDetalle.Rows)
            {
                if (file.Cells["idProducto"].Value.ToString() == txtidproducto.Text)
                {
                    producto_Existe = true;
                    MessageBox.Show("Producto ya agregado");
                    break;
                }
            } 
            //Insertamos el producto al dataGriedView
            if(!producto_Existe)
            {
                dtgDetalle.Rows.Add(new object[]
                {
                    txtidproducto.Text,
                    txtnombreproducto.Text,
                    precioCompra.ToString("0.00"),
                    precioVenta.ToString("0.00"),
                    txtcantidad.Value.ToString(),
                    (txtcantidad.Value * precioCompra).ToString("0.00")
                });
                CalcularTotal();
                LimpiarProducto();
                txtnombreproducto.Select();
            }
        }
        /// <summary>
        /// Metodo en el cual va a limpiar todos los camos del producto
        /// </summary>
        private void LimpiarProducto()
        {
            txtnombreproducto.Text = "";
            txtidproducto.Text = "0";
            txtcantidad.Value = 1;
            txtprecioCompra.Text = "";
            txtprecioVenta.Text = "";
        }
        /// <summary>
        /// Metodo por el cual voy a calcular el total de las celdas del datagriedview
        /// </summary>
        private void CalcularTotal()
        {
            decimal total = 0;
            //Validamos si hay registros en el datagridview
            if(dtgDetalle.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dtgDetalle.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["Sub_Total"].Value);
                }
                txttotal.Text = total.ToString("0.00");
            }
        }

        private void dtgDetalle_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            if(e.ColumnIndex == 7)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.borrar.Width;
                var h = Properties.Resources.borrar.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.borrar,new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dtgDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgDetalle.Columns[e.ColumnIndex].Name == "Column1")
            {
                int indice = e.RowIndex;

                if(indice >= 0)
                {
                    dtgDetalle.Rows.RemoveAt(indice);
                    CalcularTotal();
                }
            }
        }

        private void txtprecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtprecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled=true;
                }
                else
                {
                    //Validacion que permite ingresar el punto
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtprecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtprecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    //Validacion que permite ingresar el punto
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void bntRegistrar_Click(object sender, EventArgs e)
        {
            
                if(Convert.ToInt32(txtidprovedor.Text) == 0)
                {
                    MessageBox.Show("Debes seleccionar un provedor","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
                if (dtgDetalle.Rows.Count < 1)
                {
                    MessageBox.Show("Debes ingresar productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                // Creamos un datatable
                DataTable detalle_Compra = new DataTable();
                //Le aagregamos las columnas a el dataTable
                detalle_Compra.Columns.Add("Info_Producto",typeof(int));
                detalle_Compra.Columns.Add("PrecioCompra",typeof(decimal));
                detalle_Compra.Columns.Add("PrecioVenta",typeof(decimal));
                detalle_Compra.Columns.Add("Cantidad", typeof(int));
                detalle_Compra.Columns.Add("MontoTotal", typeof(decimal));

                //Vamos a leer cada fila del datagridview
                foreach(DataGridViewRow row in dtgDetalle.Rows)
                {
                    detalle_Compra.Rows.Add(new object[]
                    {
                        Convert.ToInt32(row.Cells["idProducto"].Value.ToString()),
                        row.Cells["Precio_Compra"].Value.ToString(),
                        row.Cells["Precio_Venta"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["Sub_Total"].Value.ToString()
                    });
                }
            //Creamos el numero de documento
            int idCorrectivo = new CN_Compra().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}",idCorrectivo);

            //Creamos el objeto compra para poder registrar la compra
            Compras oCompra = new Compras()
            {
                ID_Usuario_Compra = new Usuariocs() { ID_Usuarios = _Usuario.ID_Usuarios },
                Info_Provedor = new Provedores() { ID_Provedores = Convert.ToInt32(txtidprovedor.Text) },
                TipoDocumento = ((OpcionCombo)cmbtipodocumento.SelectedItem).Texto,
                Numero_Documento = numeroDocumento.ToString(),
                MontoTotal = Convert.ToDecimal(txttotal.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compra().Registrar(oCompra,detalle_Compra, out mensaje);

            if(respuesta)
            {
                var result = MessageBox.Show("Numero de compra generada:\n" + numeroDocumento ,"Mensaje", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if(result == DialogResult.Yes)
                {
                    Clipboard.SetText(numeroDocumento);

                    txtidprovedor.Text = "0";
                    txtnombreprovedor.Text = "";
                    txttelefonoprovedor.Text = "";
                    dtgDetalle.Rows.Clear();
                    CalcularTotal();
                }
            }
            else
            {
                MessageBox.Show(mensaje,"Alerta",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
