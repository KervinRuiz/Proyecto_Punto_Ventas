using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperMercado_Entidades;
using SuperMercado_Negocio;

namespace SuperMercado_Presentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public void LimpiarFormulario()
        {
            txtusuario.Text = "";
            txtcontra.Text = "";
        }
        /// <summary>
        /// Funcion en la cual validamos si los campos estan vacios
        /// </summary>
        /// <returns>True si estan vacios</returns>
        public bool ValidarCamposVacios()
        {
            if (txtusuario.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar un usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusuario.Focus();
                return false;
            }
            else if (txtcontra.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar una contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtcontra.Focus();
                return false;

            }
            else
            {
                return true;
            }

        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposVacios())
            {
                //Nos va a retonar un usuarion en especifico de nuestra lista de usuarios, con respecto a nuestras cajas de texto
                List<Usuariocs> Test = new CN_Usuario().Listar();
                Usuariocs ousuarios = new CN_Usuario().Listar().Where(u => u.No_Usuario == txtusuario.Text && u.Contra
                == txtcontra.Text).FirstOrDefault();
                //Validamos si el usuario y la contraseña son correctas
                if (ousuarios != null)
                {
                    //Instanciamos el formulario para poder abrirlo cuando haga click
                    frmmenu Menu = new frmmenu(ousuarios);
                    Menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LimpiarFormulario();
                }

            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtusuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtusuario.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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
