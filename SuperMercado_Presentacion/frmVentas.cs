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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmVentas : Form
    {
        private Cliente _cliente;
        private Usuariocs _Usuario;
        public int valorAnteriorCantidad; // Variable para almacenar el valor anterior de la cantidad
        public frmVentas(Usuariocs oUsuario = null)
        {
            _Usuario = oUsuario;
            // Habilitar la edición de la columna de cantidad
            if (dtgDetalle != null)
            {
                dtgDetalle.CellEndEdit += dtgDetalle_CellEndEdit;
                dtgDetalle.Columns["Cantidad"].ReadOnly = false;
            }
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            txtcedula.Text = "admin";
            txtnombreCliente.Text = "admin";
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
            txtcambio.Text = "";
            txtpago.Text = "";
            txttotal.Text = "0";
        }

        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            // Al dar click al boton abriremos el modal donde se muestran
            // todos los provedores en listados
            using (var ModalCliente = new mdCliente())
            {
                var result = ModalCliente.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidcliente.Text = ModalCliente._cliente.ID_Cliente.ToString();
                    txtnombreCliente.Text = ModalCliente._cliente.Nombre;
                    txtcedula.Text = ModalCliente._cliente.Cedula;
                }
                else
                {
                    txtidcliente.Select();
                }
            }
        }
        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            Producto oProducto = new CN_Productos().Listar()
                .Where(p => p.Codigo == txtnombreproducto.Text && p.Estado == true)
                .FirstOrDefault();

            if (oProducto != null)
            {
                txtnombreproducto.BackColor = Color.Honeydew;
                txtidproducto.Text = oProducto.ID_Producto.ToString();
                txtprecioVenta.Text = oProducto.PrecioConIVA.ToString();
            }
            else
            {
                txtidproducto.Text = "0";
                MessageBox.Show("Producto no encontrado");
            }
        }

        private void txtnombreproducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                decimal precioCompra = 0;
                decimal precioVenta = 0;
                string Mensaje = string.Empty;
                int Cantidad = 0;

                // Buscamos el producto con el código y validando que esté activo
                Producto oProducto = new CN_Productos().Listar()
                    .FirstOrDefault(p => p.Codigo == txtnombreproducto.Text && p.Estado == true);

                // Validamos que no sea nulo
                if (oProducto != null)
                {
                    // Buscar si el producto ya existe en el DataGridView
                    bool productoExiste = false;
                    foreach (DataGridViewRow row in dtgDetalle.Rows)
                    {

                        if (row.Cells["idProducto"].Value.ToString() == oProducto.ID_Producto.ToString())
                        {
                            productoExiste = true;
                            int cantidadActual = Convert.ToInt32(row.Cells["Cantidad"].Value);
                            //int respuesta1 = new CN_Productos().DescontarCantidad(oProducto, Cantidad, out Mensaje);
                            row.Cells["Cantidad"].Value = cantidadActual + 1;
                            decimal precioTotal = (cantidadActual + 1) * oProducto.PrecioConIVA;
                            row.Cells["Sub_Total"].Value = precioTotal.ToString("0.00");
                            Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                            //Eliminamos la cantidad del producto
                            int respuesta = new CN_Productos().DescontarCantidad(oProducto, 1, out Mensaje);
                            break;
                        }
                    }
                    if (!productoExiste)
                    {
                        // Insertamos el producto al DataGridView
                        dtgDetalle.Rows.Add(new object[]
                        {
                            oProducto.ID_Producto.ToString(),
                            oProducto.Codigo.ToString(),
                            oProducto.NombreProducto,
                            oProducto.PrecioConIVA.ToString(),
                            1,
                            (oProducto.PrecioConIVA).ToString("0.00"),
                            oProducto.Iva.ToString(),
                        });

                        // Obtener la fila recién agregada
                        DataGridViewRow nuevaFila = dtgDetalle.Rows[dtgDetalle.Rows.Count - 1];
                        // Obtener la cantidad de la fila recién agregada
                        int cantidadActual = Convert.ToInt32(nuevaFila.Cells["Cantidad"].Value);
                        // Descontar la cantidad del producto
                        int respuesta = new CN_Productos().DescontarCantidad(oProducto, cantidadActual, out Mensaje);
                    }
                    CalcularTotal();
                    txtnombreproducto.Text = "";
                    txtnombreproducto.Select();
                }
                else
                {
                    txtidproducto.Text = "0";
                    MessageBox.Show("Producto no encontrado");
                }
            }
        }


        private void txtnombreCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Cliente oCliente = new CN_Cliente().Listar()
                .Where(p => p.Nombre.ToUpper() == txtnombreCliente.Text.ToUpper() && p.Estado == true)
                .FirstOrDefault();

                if (oCliente != null)
                {
                    txtnombreCliente.BackColor = Color.Honeydew;
                    txtidcliente.Text = oCliente.Cedula.ToString();
                    txtnombreCliente.Text = oCliente.Nombre.ToString();
                    txtcedula.Text = oCliente.Telefono.ToString();
                }
                else
                {
                    txtidproducto.Text = "0";
                    MessageBox.Show("Cliente no encontrado");
                }
            }
        }
        /// <summary>
        /// Metodo por el cual voy a calcular el total de las celdas del datagriedview
        /// </summary>
        private void CalcularTotal()
        {
            decimal total = 0;
            //Validamos si hay registros en el datagridview
            if (dtgDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgDetalle.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["Sub_Total"].Value);
                }
                txttotal.Text = total.ToString("0");
            }
        }
        /// <summary>
        /// Metodo en el cual va a limpiar todos los camos del producto
        /// </summary>
        public void LimpiarProducto()
        {
            txtnombreproducto.Text = "";
            txtidproducto.Text = "0";
            txtcantidad.Value = 1;
            txtprecioVenta.Text = "";
            dtgDetalle.Rows.Clear();
        }

        private void dtgDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgDetalle.Columns[e.ColumnIndex].Name == "Column1")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    int Cantidad = Convert.ToInt32(dtgDetalle.Rows[indice].Cells["Cantidad"].Value);
                    int id = Convert.ToInt32(dtgDetalle.Rows[indice].Cells["idProducto"].Value);

                    Producto producto = new Producto()
                    {
                        ID_Producto = id,
                    };
                    int respuesta = new CN_Productos().AumentarCantidad(producto, Cantidad);
                    dtgDetalle.Rows.RemoveAt(indice);
                    CalcularTotal();
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

        private void txtpago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtpago.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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
        /// <summary>
        /// Metodo por el cual vamos a calcular el cambio
        /// </summary>
        private void CalcularCambio()
        {
            //Validamos que le total a pagar no este vacio
            if (txttotal.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos a la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagacon;
            decimal total = Convert.ToDecimal(txttotal.Text);


            //Convertimos el texto en la variable
            if (decimal.TryParse(txtpago.Text.Trim(), out pagacon))
            {
                //Valido que el valor de paga debe ser mayor al total
                if (pagacon < total)
                {
                    txtcambio.Text = "0";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtcambio.Text = cambio.ToString();
                    MessageBox.Show("Tu cambio es de " + cambio.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtpago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CalcularCambio();
            }
        }

        private void bntRegistrar_Click(object sender, EventArgs e)
        {
            if (txtcedula.Text == "")
            {
                MessageBox.Show("Debes seleccionar un cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Validamos que haya productos en el datagridview
            if (dtgDetalle.Rows.Count < 1)
            {
                MessageBox.Show("Debes ingresar un producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RegistroDeVentas();
        }

        public void RegistroDeVentas()
        {
            using (FrmPago formularioSecundario = new FrmPago())
            {
                // Obtener el total del formulario principal
                decimal total = Convert.ToDecimal(txttotal.Text);
                // Pasar el total al formulario secundario
                formularioSecundario.Total = total;
                // Mostrar el formulario secundario y esperar hasta que se cierre
                if (formularioSecundario.ShowDialog() == DialogResult.OK)
                {
                    // Obtener el monto de pago del formulario secundario
                    decimal montoPagoFormulario = formularioSecundario.MontoPago;
                    string tipoPago = formularioSecundario.TipoPago;
                    decimal Cambio = formularioSecundario.Cambio;
                    txtcambio.Text = Cambio.ToString();
                    txtpago.Text = montoPagoFormulario.ToString();
                    // Creamos un datatable para guardar en base  de datos
                    DataTable detalle_Compra = new DataTable();
                    //Le aagregamos las columnas a el dataTable
                    detalle_Compra.Columns.Add("Info_Producto", typeof(int));
                    detalle_Compra.Columns.Add("PrecioVenta", typeof(decimal));
                    detalle_Compra.Columns.Add("Cantidad", typeof(int));
                    detalle_Compra.Columns.Add("SubTotal", typeof(decimal));
                    //Vamos a leer cada fila del datagridview
                    foreach (DataGridViewRow row in dtgDetalle.Rows)
                    {
                        detalle_Compra.Rows.Add(new object[]
                        {
                        Convert.ToInt32(row.Cells["idProducto"].Value.ToString()),
                        row.Cells["Precio_Venta"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["Sub_Total"].Value.ToString()
                        });
                    }

                    // Lista  de los precios para restarles el iva
                    List<decimal> preciosFinales = new List<decimal>();
                    decimal totalPrecioSinIVA = 0;
                    //Tabla para imprimir la informacion en la factura
                    DataTable ImprimirFactura = new DataTable();
                    //Le aagregamos las columnas a el dataTable
                    ImprimirFactura.Columns.Add("Info_Producto", typeof(string));
                    ImprimirFactura.Columns.Add("PrecioVenta", typeof(decimal));
                    ImprimirFactura.Columns.Add("Cantidad", typeof(int));
                    ImprimirFactura.Columns.Add("SubTotal", typeof(decimal));
                    ImprimirFactura.Columns.Add("IVA", typeof(string));
                    //Vamos a leer cada fila del datagridview
                    foreach (DataGridViewRow row in dtgDetalle.Rows)
                    {
                        ImprimirFactura.Rows.Add(new object[]
                        {
                        row.Cells["Nombre"].Value.ToString(),
                        row.Cells["Precio_Venta"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["Sub_Total"].Value.ToString(),
                        row.Cells["IVA"].Value.ToString()
                        });

                        // Obtener el valor de la columna "Precio_Venta" de cada fila
                        decimal precioVenta = Convert.ToDecimal(row.Cells["Sub_Total"].Value);
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
                    //Creamos el numero de documento
                    int idCorrectivo = new CN_Venta().ObtenerCorrelativo();
                    string numeroDocumento = string.Format("{0:00000}", idCorrectivo);
                    //Creamos el objeto compra para poder registrar la compra
                    Venta Oventa = new Venta()
                    {
                        ID_Usuario_Venta = new Usuariocs() { ID_Usuarios = _Usuario.ID_Usuarios },
                        Info_Cliente = new Cliente() { Cedula = txtcedula.Text },
                        TipoDocumento = ((OpcionCombo)cmbtipodocumento.SelectedItem).Texto,
                        Info_Pago = tipoPago,
                        NumeroDocumento = numeroDocumento,
                        MontoPago = Convert.ToDecimal(txtpago.Text),
                        MontoCambio = Cambio,
                        MontoTotal = Convert.ToDecimal(txttotal.Text),
                    };

                    string Mensaje = string.Empty;
                    bool respueta = new CN_Venta().Registrar(Oventa, detalle_Compra, out Mensaje);

                    if (respueta)
                    {
                        var result = MessageBox.Show("Quieres imprimir la factura", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            Clipboard.SetText(numeroDocumento);
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
                                e1.Graphics.DrawString($"Fecha: {fechaActual.ToString("dd/MM/yyyy HH:mm:ss")}", fontContenido, Brushes.Black, 10, 90);
                                // Detalles de la factura
                                e1.Graphics.DrawString($"Número de factura: {numeroDocumento}", fontContenido, Brushes.Black, 10, 100);

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
                                    string nombreProducto = row["Info_Producto"].ToString();
                                    decimal precio = Convert.ToDecimal(row["PrecioVenta"]);
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
                                e1.Graphics.DrawString($"Total + IVA: ₡{total:N2}", fontContenido, Brushes.Black, 10, posY);
                                //posY += 15;
                                // e1.Graphics.DrawString($"IVA: ₡{sumaDiferencias:N2}", fontContenido, Brushes.Black, 10, posY);
                                posY += 15;
                                e1.Graphics.DrawString($"Pago: ₡{montoPagoFormulario:N2}", fontContenido, Brushes.Black, 10, posY);
                                posY += 15;
                                e1.Graphics.DrawString($"Cambio: ₡{Cambio:N2}", fontContenido, Brushes.Black, 10, posY);
                            };

                            // Imprime el documento en la impresora predeterminada
                            printDocument.Print();

                            dtgDetalle.Rows.Clear();
                            CalcularTotal();
                            txtcambio.Text = "";
                            txtpago.Text = "";
                            LimpiarProducto();
                        }
                        LimpiarProducto();
                        txttotal.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void btncredito_Click(object sender, EventArgs e)
        {
            if (txtcedula.Text == "")
            {
                MessageBox.Show("Debes seleccionar un cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dtgDetalle.Rows.Count < 1)
            {
                MessageBox.Show("Debes ingresar productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Validamos que el tipo de cambio este lleno
            if (txtpago.Text.Trim() == "")
            {
                txtpago.Text = "0";
            }
            RegistroDeVentas();
            //Creamos un objeto de tipo credito
            Credito_Cliente credito_ = new Credito_Cliente()
            {
                Info_cliente = new Cliente() { Cedula = txtcedula.Text },
                Monto_Credito = Convert.ToDecimal(txttotal.Text)
            };
            string mensaje = string.Empty;
            bool respueta = new CN_Venta().RegistrarCredito(credito_, out mensaje);

            if (respueta)
            {
                var result = MessageBox.Show("Credito agregado", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    dtgDetalle.Rows.Clear();
                    CalcularTotal();
                    txtcambio.Text = "";
                    txtpago.Text = "";
                    LimpiarCliente();
                }
            }
            else
            {
                MessageBox.Show("Credito actualizado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void LimpiarCliente()
        {
            txtidcliente.Text = "";
            txtnombreCliente.Text = "";
            txtcedula.Text = "";
        }

        private void dtgDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string mensaje = string.Empty;
            if (e.ColumnIndex == dtgDetalle.Columns["Cantidad"].Index && e.RowIndex >= 0)
            {
                // Obtener el nuevo valor editado
                int nuevoValorCantidad = Convert.ToInt32(dtgDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    int cantidad = nuevoValorCantidad;
                    int id = Convert.ToInt32(dtgDetalle.Rows[indice].Cells["idProducto"].Value);
                    // Buscamos el producto con el código y validando que esté activo
                    Producto oProducto = new CN_Productos().Listar()
                        .FirstOrDefault(p => p.ID_Producto == id && p.Estado == true);
                    foreach (DataGridViewRow row in dtgDetalle.Rows)
                    {

                        if (row.Cells["idProducto"].Value.ToString() == oProducto.ID_Producto.ToString())
                        {
                            int cantidadActual = Convert.ToInt32(row.Cells["Cantidad"].Value);
                            decimal precioTotal = (cantidadActual ) * oProducto.PrecioConIVA;
                            row.Cells["Sub_Total"].Value = precioTotal.ToString("0.00");
                            Producto producto = new Producto()
                            {
                                ID_Producto = id,
                            };
                            int respuesta = new CN_Productos().AumentarCantidad(producto, valorAnteriorCantidad);
                            if (respuesta >= 0)
                            {
                                int respuestaDescontar = new CN_Productos().DescontarCantidad(producto, cantidad, out mensaje);
                                // Aquí puedes realizar cualquier acción adicional que necesites, como recalcular el total, etc.
                                CalcularTotal();
                            }
                        }
                    }
                }
            }
        }

        private void dtgDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == dtgDetalle.Columns["Cantidad"].Index && e.RowIndex >= 0)
            {
                valorAnteriorCantidad = Convert.ToInt32(dtgDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }
    }
}
