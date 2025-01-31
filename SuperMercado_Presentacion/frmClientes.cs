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
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            //Llenamos el comobo box de estado, para mostrarlo en el datagriewd
            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            //Definimos lo que tiene que verse en el comobo box
            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;

            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dtcliente.Columns)
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

            //Metodo por el cual llenamos el datagriedw
            cargar();
        }
        private void cargar()
        {
            // Mostramos todos los usuarios
            List<Cliente> listaClientes = new CN_Cliente().Listar();

            foreach (Cliente item in listaClientes)
            {
                dtcliente.Rows.Add(new object[] {"",item.ID_Cliente,item.Cedula,item.Nombre,item.Apellidos,item.Telefono,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "Inactivo"
                });
            }
        }

        private void dtcliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Validamos que si ha hecho click en solo es columna se muestre la informacion
            if (dtcliente.Columns[e.ColumnIndex].Name == "Cedula")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtidusuario.Text = dtcliente.Rows[indice].Cells["id"].Value.ToString();
                    txtidentificacion.Text = dtcliente.Rows[indice].Cells["Cedula"].Value.ToString();
                    txtnombre.Text = dtcliente.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtapellidos.Text = dtcliente.Rows[indice].Cells["Apellidos"].Value.ToString();
                    txttelefono.Text = dtcliente.Rows[indice].Cells["Telefono"].Value.ToString();

                    //Mostramos el valor del estado en el combo box
                    foreach (OpcionCombo oc in cmbestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dtcliente.Rows[indice].Cells[6].Value))
                        {
                            int indice_Combo = cmbestado.Items.IndexOf(oc);
                            cmbestado.SelectedIndex = indice_Combo;
                            break;
                        }
                    }
                }
            }
        }

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
            else if (txtapellidos.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtapellidos.Focus();
                return false;
            }
            else if (txttelefono.Text == String.Empty)
            {
                MessageBox.Show("Debe Ingresar un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txttelefono.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        public void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtidusuario.Text = "0";
            txtidentificacion.Text = "";
            txtnombre.Text = "";
            txtapellidos.Text = "";
            txttelefono.Text = "";
            cmbestado.SelectedIndex = 0;

            txtidentificacion.Select();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposVacios())
            {
                //Limpiamos el data grid para volverlo a cargar
                dtcliente.Rows.Clear();
                string mensaje = string.Empty;
                //Instaciamos de la capa de negocio
                Cliente obj = new Cliente()
                {
                    ID_Cliente = Convert.ToInt32(txtidusuario.Text),
                    Cedula = txtidentificacion.Text,
                    Nombre = txtnombre.Text,
                    Apellidos = txtapellidos.Text,
                    Telefono = txttelefono.Text,
                    Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,

                };

                int idgenerado = new CN_Cliente().Registrar(obj, out mensaje);
                if (idgenerado != 0)
                {
                    MessageBox.Show("Cliente agregado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar();
                    LimpiarCampos();
                }
                else
                {
                    cargar();
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                }
            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidusuario.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas editar a este Cliente?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dtcliente.Rows.Clear();
                    string mensaje = string.Empty;
                    //Instaciamos de la capa de negocio
                    Cliente objusuario = new Cliente()
                    {
                        ID_Cliente = Convert.ToInt32( txtidusuario.Text),
                        Cedula = txtidentificacion.Text,
                        Nombre = txtnombre.Text,
                        Apellidos = txtapellidos.Text,
                        Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,
                        Telefono = txttelefono.Text

                    };

                    bool resultado = new CN_Cliente().Editar(objusuario, out mensaje);
                        if (resultado)
                        {

                            MessageBox.Show("Cliente actulizado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            cargar();
                        }
                        else
                        {
                            DataGridViewRow row = dtcliente.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["id"].Value = txtidusuario.Text;
                            row.Cells["Cedula"].Value = txtidentificacion.Text;
                            row.Cells["Nombre"].Value = txtnombre.Text;
                            row.Cells["Apellidos"].Value = txtapellidos.Text;
                            row.Cells["Telefono"].Value = txttelefono.Text;
                            row.Cells["Estado_Valor"].Value = ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString();

                            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        } 
                }
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidusuario.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas eliminar a este cliente?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    //Instaciamos de la capa de negocio
                   Cliente obj = new Cliente()
                    {
                        Cedula = txtidentificacion.Text,

                    };
                    bool respuesta = new CN_Cliente().Eliminar(obj, out mensaje);

                    //Si la respuesta es true, eliminara a el usuario
                    if (respuesta)
                    {
                        dtcliente.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
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
            //Obtengo la columna para hacer la filtracion de busqueda
            string columnFiltro = ((OpcionCombo)cmbuscar.SelectedItem).Valor.ToString();

            //Validacion si existen las filas dentro del datagridw
            if (dtcliente.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dtcliente.Rows)
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
            foreach (DataGridViewRow row in dtcliente.Rows)
            {
                row.Visible = true;
            }
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

        private void txttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txttelefono.Text.Trim().Length == 0 && e.KeyChar.ToString() == "-")
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
    }
}
