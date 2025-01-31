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
    public partial class frmProvedores : Form
    {
        public frmProvedores()
        {
            InitializeComponent();
        }

        private void frmProvedores_Load(object sender, EventArgs e)
        {
            //Llenamos el comobo box de estado, para mostrarlo en el datagriewd
            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            //Definimos lo que tiene que verse en el comobo box
            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;

            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dtprovedor.Columns)
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
            List<Provedores> listaProvedores = new CN_Provedores().Listar();

            foreach (Provedores item in listaProvedores)
            {
                dtprovedor.Rows.Add(new object[] {"",item.ID_Provedores,item.NOmbre_Provedor,item.Telefono_Provedor,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "Inactivo"
                });
            }
        }

        private void dtprovedor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Validamos que si ha hecho click en solo es columna se muestre la informacion
            if (dtprovedor.Columns[e.ColumnIndex].Name == "Nombre")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtidusuario.Text = dtprovedor.Rows[indice].Cells["id"].Value.ToString();
                    txtnombre.Text = dtprovedor.Rows[indice].Cells["Nombre"].Value.ToString();
                    txttelefono.Text = dtprovedor.Rows[indice].Cells["Telefono"].Value.ToString();
                    //Mostramos el valor del estado en el combo box
                    foreach (OpcionCombo oc in cmbestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dtprovedor.Rows[indice].Cells["Estado_Valor"].Value))
                        {
                            int indice_Combo = cmbestado.Items.IndexOf(oc);
                            cmbestado.SelectedIndex = indice_Combo;
                            break;
                        }
                    }
                }

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposVacios())
            {
                //Limpiamos el data grid para volverlo a cargar
                dtprovedor.Rows.Clear();
                string mensaje = string.Empty;
                //Instaciamos de la capa de negocio
                Provedores objusuario = new Provedores()
                {
                    ID_Provedores = Convert.ToInt32(txtidusuario.Text),
                    NOmbre_Provedor = txtnombre.Text,
                    Telefono_Provedor = txttelefono.Text,
                    Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false

                };

                int idusuariogenerado = new CN_Provedores().Registrar(objusuario, out mensaje);
                if (idusuariogenerado == 0)
                {
                    cargar();
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Provedor Agreagado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar();
                    LimpiarCampos();
                }
            }
        }

        public bool ValidarCamposVacios()
        {
            if(txtnombre.Text == string.Empty)
            {
                MessageBox.Show("Debes ingresar el nombre");
                return false;
            }else if (txttelefono.Text == string.Empty)
            {
                MessageBox.Show("Debes ingresar el telefono");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LimpiarCampos()
        {
            txttelefono.Text = string.Empty;
            txtnombre.Text = string.Empty;
            txtindice.Text = "-1";
            txtidusuario.Text = "0";
            cmbestado.SelectedIndex = 0;
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidusuario.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas editar a este provedor?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dtprovedor.Rows.Clear();
                    string mensaje = string.Empty;
                    //Instaciamos de la capa de negocio
                    Provedores objusuario = new Provedores()
                    {
                        ID_Provedores = Convert.ToInt32(txtidusuario.Text),
                        NOmbre_Provedor = txtnombre.Text,
                        Telefono_Provedor = txttelefono.Text,
                        Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,

                    };

                    bool resultado = new CN_Provedores().Editar(objusuario, out mensaje);

                    if (resultado == false)
                    {
                        try
                        {
                            DataGridViewRow row = dtprovedor.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["id"].Value = txtidusuario.Text;
                            row.Cells["Nombre"].Value = txtnombre.Text;
                            row.Cells["Telefono"].Value = txttelefono.Text;
                            row.Cells["Estado_Valor"].Value = ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString();
                        }catch (Exception ex)
                        {
                            MessageBox.Show("Error, no se pudo actualizar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cargar();
                            LimpiarCampos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Provedor actulizado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        cargar();
                    }
                }

            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidusuario.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas eliminar a este usuario?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    //Instaciamos de la capa de negocio
                    Provedores objusuario = new Provedores()
                    {
                        ID_Provedores = Convert.ToInt32(txtidusuario.Text)

                    };
                    bool respuesta = new CN_Provedores().Eliminar(objusuario, out mensaje);

                    //Si la respuesta es true, eliminara a el usuario
                    if (respuesta )
                    {
                        dtprovedor.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
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
            if (dtprovedor.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dtprovedor.Rows)
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
            foreach (DataGridViewRow row in dtprovedor.Rows)
            {
                row.Visible = true;
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
                if (txttelefono.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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
