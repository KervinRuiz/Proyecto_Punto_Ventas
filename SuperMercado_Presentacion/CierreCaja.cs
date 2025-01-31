using SuperMercado_Datos;
using SuperMercado_Negocio;
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
    public partial class CierreCaja : Form
    {
        public CierreCaja()
        {
            InitializeComponent();
        }
        private void CierreCaja_Load(object sender, EventArgs e)
        {
            MostrarTotalesEnVentana();

        }

        private void MostrarTotalesEnVentana()
        {
            // Obtener la fecha de ayer
            DateTime fechaAyer = DateTime.Today.AddDays(-1);

            // Verificar si la fecha actual es diferente a la fecha de ayer
            if (DateTime.Today != fechaAyer )
            {
                // Si la fecha ha cambiado, limpiamos el DataGridView
                dgtdetalle.Rows.Clear();
            }

            CN_Venta CnVenta = new CN_Venta();
            Dictionary<string, decimal> totalesPorTipoPago = CnVenta.ObtenerTotalesPorTipoPago();

            if (totalesPorTipoPago != null && totalesPorTipoPago.Count > 0)
            {
                decimal totalGeneral = 0; // Variable para almacenar el total general

                foreach (var kvp in totalesPorTipoPago)
                {
                    dgtdetalle.Rows.Add(kvp.Key, kvp.Value.ToString("C"));
                    totalGeneral += kvp.Value;  // Sumar al total general
                }

                // Agregar una fila al final con el total general
                dgtdetalle.Rows.Add("Total General", totalGeneral.ToString("C"));
            }
        }



        private void btnimprimir_Click(object sender, EventArgs e)
        {
            DataTable ImprimirFactura = new DataTable();
            //Le aagregamos las columnas a el dataTable
            ImprimirFactura.Columns.Add("TipoPago", typeof(string));
            ImprimirFactura.Columns.Add("Total", typeof(string));
            //Vamos a leer cada fila del datagridview
            foreach (DataGridViewRow row in dgtdetalle.Rows)
            {
                ImprimirFactura.Rows.Add(new object[]
                {
                        row.Cells["TipoPago"].Value.ToString(),
                        row.Cells["Total"].Value.ToString(),

                });
            }
            // Crea una instancia de PrintDocument
            PrintDocument printDocument = new PrintDocument();

            // Manejador de evento para imprimir
            printDocument.PrintPage += (sender1, e1) =>
            {
                // Configuración de fuente
                Font fontTitulo = new Font("Arial", 14, FontStyle.Bold);
                Font fontSubtitulo = new Font("Arial", 8);
                Font fontContenido = new Font("Arial", 8);
                // Línea divisoria
                Pen penLineaDivisoria = new Pen(Brushes.Black, 2);
                penLineaDivisoria.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e1.Graphics.DrawLine(penLineaDivisoria, 10, 80, 290, 80); // Línea divisoria horizontal

                // Fecha
                DateTime fechaActual = DateTime.Now;
                e1.Graphics.DrawString($"Fecha: {fechaActual.ToString("dd/MM/yyyy HH:mm:ss")}", fontContenido, Brushes.Black, 10, 90);

                // Títulos fijos
                e1.Graphics.DrawString("Tipo de pago", fontSubtitulo, Brushes.Black, 10, 130);
                e1.Graphics.DrawString("Total", fontSubtitulo, Brushes.Black, 100, 130);

                // Línea divisoria
                e1.Graphics.DrawLine(penLineaDivisoria, 10, 150, 290, 150); // Línea divisoria horizontal

                // Lista de productos
                int posY = 160;
                foreach (DataRow row in ImprimirFactura.Rows)
                {
                    string nombreProducto = row["TipoPago"].ToString();
                    string precio = row["Total"].ToString();

                    // Contenido por producto
                    e1.Graphics.DrawString(nombreProducto, fontContenido, Brushes.Black, 10, posY);
                    e1.Graphics.DrawString(precio.ToString(), fontContenido, Brushes.Black, 100, posY);
                    posY += 15;
                }

                // Línea divisoria
                e1.Graphics.DrawLine(penLineaDivisoria, 10, posY, 290, posY); // Línea divisoria horizontal

                // Total
               // posY += 15;
                //e1.Graphics.DrawString($"Total: ₡{totalGeneral:N2}", fontContenido, Brushes.Black, 10, posY);
            };

            // Imprime el documento en la impresora predeterminada
            printDocument.Print();
        }
    }
}
