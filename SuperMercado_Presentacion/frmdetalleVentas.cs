using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using SuperMercado_Entidades;
using SuperMercado_Negocio;
using SuperMercado_Presentacion.Modales;
using SuperMercado_Presentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmdetalleVentas : Form
    {
        public frmdetalleVentas()
        {
            InitializeComponent();
        }

        private void frmdetalleVentas_Load(object sender, EventArgs e)
        {
            
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (txtbusqueda.Text == "")
            {
                MessageBox.Show("Debes ingresar el numero de documento");
                return;
            }
            try
            {
                Venta oVenta = new CN_Venta().ObtenerVenta(txtbusqueda.Text);
                if (oVenta.ID_Venta != 0)
                {
                    txtnumeroDocumento.Text = oVenta.NumeroDocumento;
                    txtfecha.Text = oVenta.Fecha_Registro.ToString("dd/MM/yyyy HH:mm:ss");
                    txttipoDocumento.Text = oVenta.TipoDocumento;
                    txtusuario.Text = oVenta.ID_Usuario_Venta.Nombre_Usuario;


                    dgtdetalle.Rows.Clear();
                    foreach (Detalle_Venta dv in oVenta.oDetalleventa)
                    {
                        dgtdetalle.Rows.Add(new object[]
                        {
                        dv.oProducto.NombreProducto,
                        dv.Precio_Venta,
                        dv.cantidad,
                        dv.SubTotal,
                        dv.oProducto.Iva
                        });
                    }
                    //Asignamos los valora a las cajas de textos de los montos
                    txtmontototal.Text = oVenta.MontoTotal.ToString();
                    txtmontopago.Text = oVenta.MontoPago.ToString("0,00");
                    txtmontocambio.Text = oVenta.MontoCambio.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtfecha.Text = "";
            txttipoDocumento.Text = "";
            txtnumeroDocumento.Text = "";

            txtusuario.Text = "";
            txtbusqueda.Text = "";

            dgtdetalle.Rows.Clear();
            txtmontototal.Text = "0.00";
            txtmontocambio.Text = "";
            txtmontopago.Text = "";
        }

        private void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            //Validamos si hay datos para poder crear pdf
            if (txttipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontro informacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Lista  de los precios para restarles el iva
            List<decimal> preciosFinales = new List<decimal>();
            decimal totalPrecioSinIVA = 0;
            //Tabla para imprimir la informacion en la factura
            DataTable ImprimirFactura = new DataTable();
            //Le aagregamos las columnas a el dataTable
            ImprimirFactura.Columns.Add("Producto", typeof(string));
            ImprimirFactura.Columns.Add("Precio_Compra", typeof(decimal));
            ImprimirFactura.Columns.Add("Cantidad", typeof(int));
            ImprimirFactura.Columns.Add("SubTotal", typeof(decimal));
            ImprimirFactura.Columns.Add("IVA", typeof(string));
            //Vamos a leer cada fila del datagridview
            foreach (DataGridViewRow row in dgtdetalle.Rows)
            {
                ImprimirFactura.Rows.Add(new object[]
                {
                        row.Cells["Producto"].Value.ToString(),
                        row.Cells["Precio_Compra"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["SubTotal"].Value.ToString(),
                        row.Cells["IVA"].Value.ToString()
                });

                // Obtener el valor de la columna "Precio_Venta" de cada fila
                decimal precioVenta = Convert.ToDecimal(row.Cells["SubTotal"].Value);
                // Obtener el valor de la columna "IVA" de cada fila como cadena (varchar)
                string valorIVAComoCadena = row.Cells["IVA"].Value.ToString().TrimEnd('%');
                // Convertir el valor de IVA a decimal
                if (decimal.TryParse(valorIVAComoCadena, out decimal porcentajeIVA))
                {
                    // Calcular el precio sin IVA y sumarlo al total
                    totalPrecioSinIVA = precioVenta - (precioVenta / (1 + (porcentajeIVA / 100)));
                }
                // Agregar el precio sin IVA a la lista
                preciosFinales.Add(totalPrecioSinIVA);
            }
            // Calcular la suma de todas las diferencias
            int sumaDiferencias = Convert.ToInt32(preciosFinales.Sum());
                // Crea una instancia de PrintDocument
                PrintDocument printDocument = new PrintDocument();

                // Manejador de evento para imprimir
                printDocument.PrintPage += (sender1, e1) =>
                {
                    // Configuración de fuente
                    Font fontTitulo = new Font("Arial", 14, FontStyle.Bold);
                    Font fontSubtitulo = new Font("Arial", 8);
                    Font fontContenido = new Font("Arial", 8);

                    // Contenido de impresión
                    e1.Graphics.DrawString("Pulperia el sueño", fontTitulo, Brushes.Black, 10, 10);
                    e1.Graphics.DrawString("Natalia Ramirez Angulo", fontContenido, Brushes.Black, 10, 30);
                    e1.Graphics.DrawString("Cedula: 304790955", fontContenido, Brushes.Black, 10, 40);
                    e1.Graphics.DrawString("Direccion: Ciudad Quesada", fontContenido, Brushes.Black, 10, 50);
                    e1.Graphics.DrawString("Correo: Abastecedorjsn2024@gmail.com", fontContenido, Brushes.Black, 10, 60);

                    // Línea divisoria
                    Pen penLineaDivisoria = new Pen(Brushes.Black, 2);
                    penLineaDivisoria.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e1.Graphics.DrawLine(penLineaDivisoria, 10, 80, 290, 80); // Línea divisoria horizontal

                    // Fecha
                    DateTime fechaActual = DateTime.Now;
                    e1.Graphics.DrawString($"Fecha: {txtfecha.Text.ToString()}", fontContenido, Brushes.Black, 10, 90);
                    // Detalles de la factura
                    e1.Graphics.DrawString($"Número de factura: {txtnumeroDocumento.Text}", fontContenido, Brushes.Black, 10, 100);

                    // Títulos fijos
                    e1.Graphics.DrawString("Producto", fontSubtitulo, Brushes.Black, 10, 130);
                    e1.Graphics.DrawString("Cantidad", fontSubtitulo, Brushes.Black, 100, 130);
                    e1.Graphics.DrawString("Monto", fontSubtitulo, Brushes.Black, 200, 130);

                    // Línea divisoria
                    e1.Graphics.DrawLine(penLineaDivisoria, 10, 150, 290, 150); // Línea divisoria horizontal

                    // Lista de productos
                    int posY = 160;
                    foreach (DataRow row in ImprimirFactura.Rows)
                    {
                        string nombreProducto = row["Producto"].ToString();
                        decimal precio = Convert.ToDecimal(row["Precio_Compra"]);
                        int cantidad = Convert.ToInt32(row["Cantidad"]);

                        // Contenido por producto
                        e1.Graphics.DrawString(nombreProducto, fontContenido, Brushes.Black, 10, posY);
                        e1.Graphics.DrawString(cantidad.ToString(), fontContenido, Brushes.Black, 100, posY);
                        e1.Graphics.DrawString($"₡{precio * cantidad:N2}", fontContenido, Brushes.Black, 200, posY);

                        posY += 15;
                    }

                    // Línea divisoria
                    e1.Graphics.DrawLine(penLineaDivisoria, 10, posY, 290, posY); // Línea divisoria horizontal

                    // Total
                    posY += 15;
                    e1.Graphics.DrawString($"Pago: ₡{txtmontopago.Text:N2}", fontContenido, Brushes.Black, 10, posY);
                    posY += 15;
                    e1.Graphics.DrawString($"Total + IVA: ₡{txtmontototal.Text:N2}", fontContenido, Brushes.Black, 10, posY);
                   // posY += 15;
                    //e1.Graphics.DrawString($"IVA: ₡{sumaDiferencias:N2}", fontContenido, Brushes.Black, 10, posY);
                    posY += 15;
                    e1.Graphics.DrawString($"Cambio: ₡{txtmontocambio.Text:N2}", fontContenido, Brushes.Black, 10, posY);
                };

                // Imprime el documento en la impresora predeterminada
                printDocument.Print();

                dgtdetalle.Rows.Clear();
                txtmontocambio.Text = "";
                txtmontopago.Text = "";
                txtmontototal.Text = "0";
        }

        private void btnlistado_Click(object sender, EventArgs e)
        {
            // Al dar click al boton abriremos el modal donde se muestran
            // todos los provedores en listados
            using (var ModalVentas = new modalVentas())
            {
                var result = ModalVentas.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtbusqueda.Text = ModalVentas._venta.NumeroDocumento;
                }
                else
                {
                    txtnumeroDocumento.Select();
                }
            }
        }

        private void txtmontototal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtmontototal.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void txtmontocambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtmontocambio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void txtmontopago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtmontopago.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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
    }
}
