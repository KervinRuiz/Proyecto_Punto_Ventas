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
using System.Windows.Controls;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }


        private void Usuarios_Load(object sender, EventArgs e)
        {
            //Llenamos el comobo box de estado, para mostrarlo en el datagriewd
            cmbestado.Items.Add(new OpcionCombo() {Valor = 1, Texto= "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            //Definimos lo que tiene que verse en el comobo box
            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;

            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach(DataGridViewColumn columna in dtusuarios.Columns)
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
            //Llenamos el comboBox de categorias
            List<Roles> listaRoles= new CN_Productos().ListarRoles();

            foreach (Roles item in listaRoles)
            {
                cmbRol.Items.Add(new OpcionCombo() { Valor = item.Id_Rol, Texto = item.NombreRol });
            }
            cmbRol.DisplayMember = "Texto";
            cmbRol.ValueMember = "Valor";
            cmbRol.SelectedIndex = 0;
            //Metodo por el cual llenamos el datagriedw
            cargar();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposVacios())
            {
                //Limpiamos el data grid para volverlo a cargar
                dtusuarios.Rows.Clear();
                string mensaje  = string.Empty;
                //Instaciamos de la capa de negocio
                Usuariocs objusuario = new Usuariocs()
                {
                    ID_Usuarios = Convert.ToInt32(txtidusuario.Text),
                    No_Usuario = txtidentificacion.Text,
                    Nombre_Usuario = txtnombre.Text,
                    Contra = txtcontra.Text,
                    Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true:false,
                    Correo = txtcorreo.Text,
                    IdRol = new Roles() { Id_Rol = Convert.ToInt32(((OpcionCombo)cmbRol.SelectedItem).Valor)},
                };

                    int idusuariogenerado = new CN_Usuario().Registrar(objusuario, out mensaje);
                    if (idusuariogenerado != 0)
                    {
                        cargar();
                        MessageBox.Show(mensaje,"Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //MessageBox.Show("Usuario agregado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargar();
                        LimpiarCampos();
                    }

            }
        }

        /// <summary>
        /// Metodo para limpiar todas las cajas de texto
        /// </summary>
        public void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtidusuario.Text = "0";
            txtidentificacion.Text = "";
            txtnombre.Text = "";
            txtcorreo.Text = "";
            txtcontra.Text = "";
            cmbestado.SelectedIndex = 0;

            txtidentificacion.Select();
        }

        /// <summary>
        /// Funcion para validar espacios vacios
        /// </summary>
        /// <returns>True si no hay espacios vacios, false si encuentra espacios vacios</returns>
        public bool ValidarCamposVacios()
        {
            if (txtidentificacion.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar una Cedula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtidentificacion.Focus();
                return false;
            }
            else if (txtnombre.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtnombre.Focus();
                return false;

            }
            else if(txtcorreo.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtcorreo.Focus();
                return false;
            }
            else if(txtcontra.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               txtcontra.Focus();
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Metodo por el cual vamos a cargar los datos en el datagriedw
        /// </summary>
        private void cargar()
        {
            // Mostramos todos los usuarios
            List<Usuariocs> listaUsuarios = new CN_Usuario().Listar();

            foreach (Usuariocs item in listaUsuarios)
            {
                dtusuarios.Rows.Add(new object[] {"",item.ID_Usuarios,item.No_Usuario,item.Nombre_Usuario,item.Correo,item.Contra,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "Inactivo",
                item.IdRol.Id_Rol,
                });
            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidusuario.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas editar a este usuario?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dtusuarios.Rows.Clear();
                    string mensaje = string.Empty;
                    //Instaciamos de la capa de negocio
                    Usuariocs objusuario = new Usuariocs()
                    {
                        ID_Usuarios = Convert.ToInt32(txtidusuario.Text),
                        No_Usuario = txtidentificacion.Text,
                        Nombre_Usuario = txtnombre.Text,
                        Contra = txtcontra.Text,
                        Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,
                        Correo = txtcorreo.Text,
                        IdRol = new Roles() { Id_Rol = Convert.ToInt32(((OpcionCombo)cmbRol.SelectedItem).Valor) },

                    };

                    bool resultado = new CN_Usuario().Editar(objusuario, out mensaje);

                    if (resultado)
                    {
                        DataGridViewRow row = dtusuarios.Rows[Convert.ToInt32(txtindice.Text)];
                        row.Cells["id"].Value = txtidusuario.Text;
                        row.Cells["NumeroUsuario"].Value = txtidentificacion.Text;
                        row.Cells["Nombre"].Value = txtnombre.Text;
                        row.Cells["Correo"].Value = txtcorreo.Text;
                        row.Cells["Clave"].Value = txtcontra.Text;
                        row.Cells["Estado_Valor"].Value = ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString();
                        row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString();

                        MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //MessageBox.Show("Usuario actulizado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        cargar();
                    }
                }

            }


        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtidusuario.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas eliminar a este usuario?","Pregunta",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    //Instaciamos de la capa de negocio
                    Usuariocs objusuario = new Usuariocs()
                    {
                        No_Usuario = txtidentificacion.Text,

                    };
                    int respuesta = new CN_Usuario().Eliminar(objusuario, out mensaje);

                    //Si la respuesta es true, eliminara a el usuario
                    if (respuesta != 0)
                    {
                        dtusuarios.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                        
                    }
                }
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtengo la columna para hacer la filtracion de busqueda
                string columnFiltro = ((OpcionCombo)cmbuscar.SelectedItem).Valor.ToString();

                //Validacion si existen las filas dentro del datagridw
                if (dtusuarios.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dtusuarios.Rows)
                    {
                        //Aqui empezamos con la filtracion de los datos
                        if (row.Cells[columnFiltro].Value.ToString().Trim().ToLower().Contains(txtbuscar.Text.Trim().ToLower()))
                            row.Visible = true;
                        else
                            row.Visible = false;
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbuscar.Text = "";
            foreach (DataGridViewRow row in dtusuarios.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnvercontra_Click(object sender, EventArgs e)
        {
            txtcontra.PasswordChar = '\0';
        }
        private void txtcontra_Click(object sender, EventArgs e)
        {
            btnvercontra.Visible = true;
        }

        private void txtidentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtidentificacion.Text.Trim().Length == 0 && e.KeyChar.ToString() == "-")
                {
                    e.Handled = true;
                }
                else
                {
                    //Validacion que permite ingresar el punto
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == "-")
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

        private void dtusuarios_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Validamos que si ha hecho click en solo es columna se muestre la informacion
            if (dtusuarios.Columns[e.ColumnIndex].Name == "Cedula")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtidusuario.Text = dtusuarios.Rows[indice].Cells["id"].Value.ToString();
                    txtidentificacion.Text = dtusuarios.Rows[indice].Cells["Cedula"].Value.ToString();
                    txtnombre.Text = dtusuarios.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtcorreo.Text = dtusuarios.Rows[indice].Cells["Correo"].Value.ToString();
                    txtcontra.Text = dtusuarios.Rows[indice].Cells["Clave"].Value.ToString();

                    //Mostramos el valor del estado en el combo box
                    foreach (OpcionCombo oc in cmbestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dtusuarios.Rows[indice].Cells[6].Value))
                        {
                            int indice_Combo = cmbestado.Items.IndexOf(oc);
                            cmbestado.SelectedIndex = indice_Combo;
                            break;
                        }
                    }
                    //Mostramos el valor del estado en el combo box
                    foreach (OpcionCombo oc in cmbRol.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dtusuarios.Rows[indice].Cells[8].Value))
                        {
                            int indice_Combo = cmbRol.Items.IndexOf(oc);
                            cmbRol.SelectedIndex = indice_Combo;
                            break;
                        }
                    }
                }

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
