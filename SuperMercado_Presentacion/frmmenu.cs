using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SuperMercado_Entidades;
using FontAwesome.Sharp;
using SuperMercado_Presentacion.Modales;
using SuperMercado_Negocio;
using System.Diagnostics;

namespace SuperMercado_Presentacion
{
    public partial class frmmenu : Form
    {
        private static Usuariocs UsuarioActual;
        private static Roles UsuarioActualRole;
        public frmmenu(Usuariocs objusuario )
        {
            //Sutear lo que tenga la variable de objUsuario
            UsuarioActual = objusuario;
            InitializeComponent();
            if (UsuarioActual.IdRol.Id_Rol != 1)
            {
                btnProvedores.Enabled = false;
                btusuarios.Enabled = false;
                btnProductos.Enabled = false;
                iconButton2.Enabled = false;
            }
        }
        // Nos permite el movimiento del formulario
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd,int wmsg,int wparam,int hparam);
        private void btnclose_Click(object sender, EventArgs e)
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
            //Cerramos la ventada totalmente
            Application.Exit();
        }

        private void btnmaximo_Click(object sender, EventArgs e)
        {
            //Se hara a pantalla completa
            this.WindowState = FormWindowState.Maximized;
            //Alternamos la visibilidad de los botones
            btnrestaurar.Visible = true;
            btnmaximo.Visible = false;
        }

        private void btnrestaurar_Click(object sender, EventArgs e)
        {
            //La ventana pasara a normal
            this.WindowState = FormWindowState.Normal;
            // Alternamos la visibilidad de los botones
            btnmaximo.Visible = true;
            btnrestaurar.Visible=false;
        }

        private void bntminimo_Click(object sender, EventArgs e)
        {
            //La ventana se minimizara
            this.WindowState = FormWindowState.Minimized;
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            //Llamamos los eventos a la barra del titulo
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void AbrirFormulario( object Formulario)
        {
            if(this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
                Form fh = Formulario as Form;
                fh.TopLevel = false;
                fh.Dock= DockStyle.Fill;
                this.panelContenedor.Controls.Add(fh);
                this.panelContenedor.Tag = fh;
                fh.Show();
            
        }

        private void panelmenuvertical_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Metodo en el cual vamos a devolver un submenu
        /// </summary>
        /// <param name="subMenu"></param>
        private void AbrirSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        
        private void btnreportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmdetalleVentas());
        }



        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            //Cuando se muestre el formulario, nos mostrara el nombre de la persona que hizo el logueo
            lblUsuario.Text = UsuarioActual.Nombre_Usuario;
        }

        private void btusuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario( new Usuarios());
        }

        private void btnProvedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario( new frmProvedores());
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmVentas(UsuarioActual));
        }

        private void BtnCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmCompras(UsuarioActual));
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmClientes());
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmProducto());
        }

        private void btnAcercade_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new CierreCaja());
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmCategorias());
        }
        private void btnverventas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmdetalleVentas());
        }

        private void btnvercompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmDetalleCompra());
        }

        private void btnfiados_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmPagoCreditos());
        }

        private void btnreportepersona_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmreporteCliente());
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToString("h:mm:ss").ToUpper();
            lblfecha.Text = DateTime.Now.ToLongDateString().ToUpper();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmTotaIngresadp());
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmClientes());
        }
    }
}
