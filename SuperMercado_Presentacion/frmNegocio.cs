using SuperMercado_Entidades;
using SuperMercado_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metodo que convierte el array de byte a una imagen
        /// </summary>
        /// <returns></returns>
        public Image ByteToImagen(byte[] imagenBytes)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imagenBytes, 0, imagenBytes.Length);
            //Creamos la imagen
            Image image = new Bitmap(ms);

            return image;
        }

        private void frmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            //Obtenemos el logo de base de datos
            byte[] byteImagen = new CN_Negocio().ObtenerLogo(out obtenido);

            if (obtenido)
                pitlogo.Image = ByteToImagen(byteImagen);

            //Instaciamos de negocio para poder obtener los datos
            Negocio datos = new CN_Negocio().ObtenerDatos();
            txtnombre.Text = datos.Nombre;
            txtruc.Text = datos.RUC;
            txtDireccion.Text = datos.Direccion;
        }

        private void btnsubirlogo_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Ventana de dialogo que nos va a permitir seleccionar una imagen
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //Solo mostramos los que tengan formato de imagen
            openFileDialog.FileName = "Files|*.jpg;*.jpeg;*png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Mostramos la imagen en el piccture box
                byte[] byteimagen = File.ReadAllBytes(openFileDialog.FileName);
                //Actualizaremos nuestra imagen en base de datos
                bool repuesta = new CN_Negocio().ActualizarLogo(byteimagen, out mensaje);
                if (repuesta)
                {
                    pitlogo.Image = ByteToImagen(byteimagen);

                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la imagen", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnguardarcambios_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio obj = new Negocio()
            {
                Nombre = txtnombre.Text,
                RUC = txtruc.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Cambios del negocio guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            // Ejemplo de contenido para imprimir
            string numeroFactura = "123";
            string nombreCliente = "Juan Pérez";
            decimal total = 100.00m;

            // Crear una DataTable con la información de los productos
            DataTable tablaProductos = new DataTable();
            tablaProductos.Columns.Add("Nombre", typeof(string));
            tablaProductos.Columns.Add("Precio", typeof(decimal));
            tablaProductos.Columns.Add("Cantidad", typeof(int));

            // Agregar algunos datos de ejemplo a la tabla
            tablaProductos.Rows.Add("Producto 1", 30.00m, 2);
            tablaProductos.Rows.Add("Producto 2", 20.00m, 3);
            tablaProductos.Rows.Add("Producto 3", 15.00m, 1);

            try
            {
                // Crea una instancia de PrintDocument
                PrintDocument printDocument = new PrintDocument();

                // Manejador de evento para imprimir
                printDocument.PrintPage += (sender1, e1) =>
                {
                    // Configuración de fuente
                    Font fontTitulo = new Font("Arial", 16, FontStyle.Bold);
                    Font fontContenido = new Font("Arial", 12);

                    // Contenido de impresión
                    e1.Graphics.DrawString("Factura", fontTitulo, Brushes.Black, 10, 10);

                    // Línea divisoria
                    e1.Graphics.DrawLine(new Pen(Brushes.Black), 10, 40, 390, 40);

                    // Detalles de la factura
                    e1.Graphics.DrawString($"Número de factura: {numeroFactura}", fontContenido, Brushes.Black, 10, 50);
                    e1.Graphics.DrawString($"Cliente: {nombreCliente}", fontContenido, Brushes.Black, 10, 70);

                    // Lista de productos
                    int posY = 90;
                    foreach (DataRow row in tablaProductos.Rows)
                    {
                        string nombreProducto = row["Nombre"].ToString();
                        decimal precio = Convert.ToDecimal(row["Precio"]);
                        int cantidad = Convert.ToInt32(row["Cantidad"]);

                        string detalleProducto = $"{cantidad} x {nombreProducto}: ${precio * cantidad:N2}";
                        e1.Graphics.DrawString(detalleProducto, fontContenido, Brushes.Black, 10, posY);
                        posY += 20;
                    }

                    // Total
                    e1.Graphics.DrawString($"Total: ${total:N2}", fontContenido, Brushes.Black, 10, posY);

                    // Línea divisoria
                    e1.Graphics.DrawLine(new Pen(Brushes.Black), 10, posY + 20, 390, posY + 20);
                };

                // Imprime el documento en la impresora predeterminada
                printDocument.Print();

                Console.WriteLine("Factura impresa correctamente en la impresora predeterminada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al imprimir: " + ex.Message);
            }
        }
    }
}
