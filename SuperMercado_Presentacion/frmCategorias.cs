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
    public partial class frmCategorias : Form
    {
        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            //Llenamos el comobo box de estado, para mostrarlo en el datagriewd
            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            //Definimos lo que tiene que verse en el comobo box
            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;

            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dgtcategorias.Columns)
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
        /// <summary>
        /// Metodo por el cual vamos a cargar los datos en el datagriedw
        /// </summary>
        private void cargar()
        {
            // Mostramos todos los usuarios
            List<Categoria_Producto> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria_Producto item in listaCategoria)
            {
                dgtcategorias.Rows.Add(new object[] {"",item.ID_Categoria,item.Tipo_Categoria,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "Inactivo"
                });
            }
        }

        public bool ValidarCampos()
        {
            if (txtcategoria.Text == String.Empty)
            {
                MessageBox.Show("Debe ingresar un tipo de categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtcategoria.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LimpiarCampos()
        {
            txtcategoria.Text = "";
            txtindiceData.Text = "-1";
            txtid.Text = "0";
        }


        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                //Limpiamos el data grid para volverlo a cargar
                dgtcategorias.Rows.Clear();
                string mensaje = string.Empty;
                //Instaciamos de la capa de negocio
                Categoria_Producto obj = new Categoria_Producto()
                {
                    ID_Categoria = Convert.ToInt32(txtid.Text),
                    Tipo_Categoria = txtcategoria.Text,
                    Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,

                };

                
                    bool idusuariogenerado = new CN_Categoria().Registrar(obj, out mensaje);
                    if (idusuariogenerado == false)
                    {
                        cargar();
                        MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Categoria agregada", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargar();
                        LimpiarCampos();
                    }
            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                if (Convert.ToInt32(txtid.Text) != 0)
                {
                    if (MessageBox.Show("¿Deseas editar esta categoria?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Limpiamos el data grid para volverlo a cargar
                        dgtcategorias.Rows.Clear();
                        string mensaje = string.Empty;
                        Categoria_Producto objcategoria = new Categoria_Producto()
                        {
                            ID_Categoria = Convert.ToInt32(txtid.Text),
                            Tipo_Categoria = txtcategoria.Text,
                            Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,

                        };
                        bool resultado = new CN_Categoria().Editar(objcategoria, out mensaje);

                        if (resultado)
                        {

                            MessageBox.Show("Categoria actulizada", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            cargar();

                        }
                        else
                        {
                            DataGridViewRow row = dgtcategorias.Rows[Convert.ToInt32(txtindiceData.Text)];
                            row.Cells["id"].Value = txtid.Text;
                            row.Cells["tipoCategoria"].Value = txtcategoria.Text;
                            row.Cells["Estado_Valor"].Value = ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString();
                            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if(MessageBox.Show("¿Deseas eliminar a esta categoria?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    string mensaje = string.Empty;
                    Categoria_Producto objcategoria = new Categoria_Producto()
                    {
                        ID_Categoria = Convert.ToInt32(txtid.Text),
                        
                    };

                    bool respuesta = new CN_Categoria().Eliminar(objcategoria, out mensaje);

                    //Si la respuesta es true, eliminara a el usuario
                    if (respuesta)
                    {
                        
                        dgtcategorias.Rows.RemoveAt(Convert.ToInt32(txtindiceData.Text));
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                        LimpiarCampos();

                    }
                }
            }

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            //Obtengo la columna para hacer la filtracion de busqueda
            string columnFiltro = ((OpcionCombo)cmbuscar.SelectedItem).Valor.ToString();

            //Validacion si existen las filas dentro del datagridw
            if (dgtcategorias.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgtcategorias.Rows)
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
            foreach (DataGridViewRow row in dgtcategorias.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgtcategorias_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            //Validamos que si ha hecho click en solo es columna se muestre la informacion
            if (dgtcategorias.Columns[e.ColumnIndex].Name == "tipoCategoria")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtindiceData.Text = indice.ToString();
                    txtid.Text = dgtcategorias.Rows[indice].Cells["id"].Value.ToString();
                    txtcategoria.Text = dgtcategorias.Rows[indice].Cells["tipoCategoria"].Value.ToString();
                    //Mostramos el valor del estado en el combo box
                    foreach (OpcionCombo oc in cmbestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgtcategorias.Rows[indice].Cells["Estado_Valor"].Value))
                        {
                            int indice_Combo = cmbestado.Items.IndexOf(oc);
                            cmbestado.SelectedIndex = indice_Combo;
                            break;
                        }
                    }
                }
            }
        }
    }
}
