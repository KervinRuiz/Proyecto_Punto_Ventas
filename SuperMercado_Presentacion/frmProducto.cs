using ClosedXML.Excel;
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
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SuperMercado_Presentacion
{
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            //Obtengo la columna para hacer la filtracion de busqueda
            string columnFiltro = ((OpcionCombo)cmbuscar.SelectedItem).Valor.ToString();

            //Validacion si existen las filas dentro del datagridw
            if (dgtproductos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgtproductos.Rows)
                {
                    //Aqui empezamos con la filtracion de los datos
                    if (row.Cells[columnFiltro].Value.ToString().Trim().ToLower().Contains(txtbuscar.Text.Trim().ToLower()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }
        private void cargar()
        {
            // Mostramos todos los usuarios
            List<Producto> listaProductos = new CN_Productos().Listar();

            foreach (Producto item in listaProductos)
            {
                dgtproductos.Rows.Add(new object[] {"",item.ID_Producto,item.Codigo,item.NombreProducto,
                item.PrecioCompra,
                item.PrecioConIVA,
                item.Info_Categoria.ID_Categoria,
                item.Info_Categoria.Tipo_Categoria,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "Inactivo",
                item.Cantidad,
                item.CantidadAlerta,
                item.Iva,
                item.PorcentajeDeGanancia
                });
            }
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            //Llenamos el comobo box de estado, para mostrarlo en el datagriewd
            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            //Definimos lo que tiene que verse en el comobo box
            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = -1;
            //Llenamos el comboBox del IVA
            cmbIVA.Items.Add(new OpcionCombo() {Valor = "13%" , Texto= "13%" });
            cmbIVA.Items.Add(new OpcionCombo() { Valor = "1%", Texto = "1%" });
            cmbIVA.Items.Add(new OpcionCombo() { Valor = "2%", Texto = "2%" });
            cmbIVA.DisplayMember = "Texto";
            cmbIVA.ValueMember = "Valor";
            cmbIVA.SelectedIndex = 0;
            for (int i = 8; i <= 150;)
            {
                double porcentaje = i;
                string texto = $"{porcentaje}%";
                cmbganancia.Items.Add(new OpcionCombo() { Valor = $"{porcentaje}%", Texto = texto });

                if (i < 100)
                {
                    i++; // Incrementa en 1 si i es menor que 30
                }
                else
                {
                    i += 10; // Suma 10 cuando i es mayor o igual a 30
                }
            }
            cmbganancia.DisplayMember = "Texto";
            cmbganancia.ValueMember = "Valor";
            cmbganancia.SelectedIndex = 0;
            //Llenado del comobo box para buscar conforme a los nombres de la cabecera del datagriedw
            foreach (DataGridViewColumn columna in dgtproductos.Columns)
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
            List<Categoria_Producto> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria_Producto item in listaCategoria)
            {
                cmbCategoria.Items.Add(new OpcionCombo() { Valor = item.ID_Categoria,Texto = item.Tipo_Categoria});
            }
            cmbCategoria.DisplayMember = "Texto";
            cmbCategoria.ValueMember = "Valor";
            cmbCategoria.SelectedIndex = 0;

            cargar();
        }

        public bool ValidarCamposVacios()
        {
            if(txtnombre.Text == string.Empty)
            {
                MessageBox.Show("Debes ingresar el nombre del producto");
                txtnombre.Focus();
                return false;
            }else if (txtprecio.Text == string.Empty)
            {
                MessageBox.Show("Debes ingresar el precio del producto");
                txtprecio.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LimpiarCampos()
        {
            txtid.Text = "0";
            txtnombre.Text = "";
            txtprecio.Text = "";
            txtalerta.Text = "";
            txtcantidad.Text = "";
            cmbCategoria.SelectedIndex = 0;
            cmbestado.SelectedIndex = 0; 
            txtnombre.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(ValidarCamposVacios())
            {
                //Limpiamos el data grid para volverlo a cargar
                dgtproductos.Rows.Clear();
                string mensaje = string.Empty;
                //Instaciamos de la capa de negocio
                Producto obj = new Producto()
                {
                    ID_Producto = Convert.ToInt32(txtid.Text),
                    NombreProducto = txtnombre.Text,
                    PrecioCompra = Convert.ToDecimal(txtprecio.Text),
                    Codigo = txtcodigo.Text,
                    Cantidad = Convert.ToInt32(txtcantidad.Text),
                    CantidadAlerta = Convert.ToInt32(txtalerta.Text),
                    Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,
                    Iva = ((OpcionCombo)cmbIVA.SelectedItem).Texto,
                    PorcentajeDeGanancia = ((OpcionCombo)cmbganancia.SelectedItem).Texto,
                    Info_Categoria = new Categoria_Producto() { ID_Categoria = Convert.ToInt32(((OpcionCombo)cmbCategoria.SelectedItem).Valor)},
                };


                int idgenerado = new CN_Productos().Registrar(obj, out mensaje);
                if (idgenerado == 0)
                {
                    cargar();
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                }
                else
                {
                   // MessageBox.Show("Producto agregada", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar();
                    LimpiarCampos();
                }
            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposVacios())
            {
                if (Convert.ToInt32(txtid.Text) != 0)
                {
                    if (MessageBox.Show("¿Deseas editar este producto?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Limpiamos el data grid para volverlo a cargar
                        dgtproductos.Rows.Clear();
                        string mensaje = string.Empty;
                        Producto objcategoria = new Producto()
                        {
                            ID_Producto = Convert.ToInt32(txtid.Text),
                            NombreProducto = txtnombre.Text,
                            PrecioCompra = Convert.ToDecimal(txtprecio.Text),
                            Info_Categoria = new Categoria_Producto() { ID_Categoria = Convert.ToInt32(((OpcionCombo)cmbCategoria.SelectedItem).Valor)},
                            Codigo = txtcodigo.Text,
                            Cantidad = Convert.ToInt32(txtcantidad.Text),
                            CantidadAlerta = Convert.ToInt32(txtalerta.Text),
                            Iva = ((OpcionCombo)cmbIVA.SelectedItem).Texto,
                            PorcentajeDeGanancia = ((OpcionCombo)cmbganancia.SelectedItem).Texto,
                            Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false,

                        };
                        bool resultado = new CN_Productos().Editar(objcategoria, out mensaje);

                        if (resultado)
                        {

                            //MessageBox.Show("Producto actulizada", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            cargar();

                        }
                        else
                        {
                            DataGridViewRow row = dgtproductos.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["id"].Value = txtid.Text;
                            row.Cells["Nombre"].Value = txtnombre.Text;
                            row.Cells["Precio"].Value = txtprecio.Text;
                            row.Cells["Cantidad"].Value = txtcantidad.Text;
                            row.Cells["CantidadAlerta"].Value = txtalerta.Text;
                            row.Cells["Categoria_Estado"].Value = ((OpcionCombo)cmbCategoria.SelectedItem).Valor.ToString();
                            row.Cells["Categoria"].Value = ((OpcionCombo)cmbCategoria.SelectedItem).Texto.ToString();
                            row.Cells["Codigo"].Value = txtcodigo.Text;
                            row.Cells["Iva"].Value = ((OpcionCombo)cmbIVA.SelectedItem).Texto.ToString();
                            row.Cells["Ganancia"].Value = ((OpcionCombo)cmbganancia.SelectedItem).Texto.ToString();
                            row.Cells["Estado_Valor"].Value = ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString();
                            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void dgtproductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Validamos que si ha hecho click en solo es columna se muestre la informacion
            if (dgtproductos.Columns[e.ColumnIndex].Name == "Nombre")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgtproductos.Rows[indice].Cells["id"].Value.ToString();
                    txtnombre.Text = dgtproductos.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtprecio.Text = dgtproductos.Rows[indice].Cells["Precio"].Value.ToString();
                    //Mostramos el valor del categoria en el combo box
                    foreach (OpcionCombo op in cmbCategoria.Items)
                    {
                        if (Convert.ToInt32(op.Valor) == Convert.ToInt32(dgtproductos.Rows[indice].Cells["Categoria_Estado"].Value))
                        {
                            int indice_combo = cmbCategoria.Items.IndexOf(op);
                            cmbCategoria.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    //Mostramos el valor del estado en el combo box
                    foreach (OpcionCombo oc in cmbestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgtproductos.Rows[indice].Cells["Estado_Valor"].Value))
                        {
                            int indice_Combo = cmbestado.Items.IndexOf(oc);
                            cmbestado.SelectedIndex = indice_Combo;
                            break;
                        }
                    }
                    txtcodigo.Text = dgtproductos.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtcantidad.Text = dgtproductos.Rows[indice].Cells["Cantidad"].Value.ToString();
                    txtalerta.Text = dgtproductos.Rows[indice].Cells["Alerta"].Value.ToString();
                    string valor = dgtproductos.Rows[indice].Cells["Ganancia"].Value.ToString();
                    string iva = dgtproductos.Rows[indice].Cells["Iva"].Value.ToString();
                    //Obtenemos el valor del comoboBox de ganancia
                    foreach (OpcionCombo oc in cmbganancia.Items)
                    {
                        //MessageBox.Show(oc.Valor.ToString());
                        if (oc.Valor.ToString() == valor)
                        {
                            int indice_combo = cmbganancia.Items.IndexOf(oc);
                            cmbganancia.SelectedIndex = indice_combo;
                        }
                    }
                    foreach (OpcionCombo oc in cmbIVA.Items)
                    {
                        //MessageBox.Show(oc.Valor.ToString());
                        if (oc.Valor.ToString() == iva)
                        {
                            int indice_combo = cmbIVA.Items.IndexOf(oc);
                            cmbIVA.SelectedIndex = indice_combo;
                        }
                    }
                }
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas eliminar a esta producto?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Producto objproducto = new Producto()
                    {
                        ID_Producto = Convert.ToInt32(txtid.Text)
                    };

                    int respuesta = new CN_Productos().Eliminar(objproducto, out mensaje);

                    //Si la respuesta es true, eliminara a el usuario
                    if (respuesta != 0)
                    {

                        dgtproductos.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
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

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbuscar.Text = "";
            foreach (DataGridViewRow row in dgtproductos.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnexel_Click(object sender, EventArgs e)
        {
            //Validamos si hay datos por mostrar en el exel
            if(dgtproductos.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos por mostrar","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            }
            else
            {
                //Tabla que en la cual vamos almacenar los datos del datagriew
                DataTable dt = new DataTable();
                //Insertamos las columas de la tabla
                foreach (DataGridViewColumn column in dgtproductos.Columns)
                {
                    //Accesedemos a todas las columnas las cuales tengan un encabezado
                    if (column.HeaderText != "" && column.Visible)
                        //Insertamos todas la cabeceras
                        dt.Columns.Add(column.HeaderText, typeof(string));
                }
                    //Insertamos las filas
                    foreach (DataGridViewRow row in dgtproductos.Rows)
                    {
                        //Insertamos solo las filas que estan visibles
                        if (row.Visible)
                            //Insertamos las filas
                            dt.Rows.Add(new object[]
                            {
                                row.Cells[2].Value.ToString(),
                                row.Cells[3].Value.ToString(),
                                row.Cells[4].Value.ToString(),
                                row.Cells[5].Value.ToString(),
                                row.Cells[10].Value.ToString(),
                            });
                    }
                // Dialogo que nos dira donde queremos guardar el archivo
                SaveFileDialog saveFile = new SaveFileDialog();
                //Le damos nombre a archivo
                saveFile.FileName = string.Format("ReporteProducto_{0}.xlsx",DateTime.Now.ToString("ddMMyyyyHHmmss"));
                //Filtro para que solo se muestren archivos con esa extension
                saveFile.Filter = "Excel Files | *.xlsx";

                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //Creamos el archivo exel
                        XLWorkbook wb = new XLWorkbook();
                        //Agregamos una hoja a el archivo exel
                        var hoja = wb.Worksheets.Add(dt,"Informe");
                        //Le ajustamos todo el ancho de las columnas
                        hoja.ColumnsUsed().AdjustToContents();
                        //Guardamos
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }catch (Exception ex)
                    {
                        MessageBox.Show("Error al generar el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtprecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
