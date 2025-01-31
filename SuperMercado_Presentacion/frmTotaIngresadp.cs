using SuperMercado_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmTotaIngresadp : Form
    {
        public frmTotaIngresadp()
        {
            InitializeComponent();
        }

        private void frmTotaIngresadp_Load(object sender, EventArgs e)
        {
            CN_Productos CnProductros = new CN_Productos();
            // Usando el formato "N" para separadores de miles y TrimEnd para eliminar los ceros adicionales
            label1.Text = "₡ " + CnProductros.TotalDeProductos().ToString("N").TrimEnd('0').TrimEnd(',');
            lbltotalConIva.Text = " ₡ " + CnProductros.TotalDeProductosConIva().ToString("N").TrimEnd('0').TrimEnd(',');
        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            try
            {
                // Ruta completa del archivo .bat o .sql
                string filePath = @"C:\Users\USER\OneDrive\Documents\Respaldos\RespaldosDataBase.bat"; 


                // Configurar el proceso para ejecutar el archivo
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                // Iniciar el proceso
                process.Start();

                // Leer la salida del proceso
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // Esperar a que el proceso termine
                process.WaitForExit();
                //Mostrar mensaje de confirmacion
                MessageBox.Show("Respaldo realizado con exito","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                // Mostrar la salida
                Console.WriteLine("Output: " + output);
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar el archivo: " + ex.Message);
            }
        }
    }
}
