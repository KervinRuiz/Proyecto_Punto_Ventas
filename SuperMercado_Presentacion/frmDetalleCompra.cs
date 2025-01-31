using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using SuperMercado_Entidades;
using SuperMercado_Negocio;
using SuperMercado_Presentacion.Modales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMercado_Presentacion
{
    public partial class frmDetalleCompra : Form
    {
        public frmDetalleCompra()
        {
            InitializeComponent();
        }

        private void btnbuscarprovedor_Click(object sender, EventArgs e)
        {
            if(txtbusqueda.Text == "")
            {
                MessageBox.Show("Debes ingresar el numero de documento");
                return;
            }
            Compras Ocompra = new CN_Compra().ObtenerCompra(txtbusqueda.Text);
            //Validamos que el id de la compra sea exitosa
                if (Ocompra.ID_Compra != 0)
                {
                // Mostramos los dastos en los campos de texto
                    txtnumeroDocumento.Text = Ocompra.Numero_Documento;
                    txtfecha.Text = Ocompra.Fecha_Registro;
                    txttipoDocumento.Text = Ocompra.TipoDocumento;
                    txtusuario.Text = Ocompra.ID_Usuario_Compra.Nombre_Usuario;
                    txtnombreprovedor.Text = Ocompra.Info_Provedor.NOmbre_Provedor;
                    txtTelefono.Text = Ocompra.Info_Provedor.Telefono_Provedor;

                  //Limiamos las filas del dataGried
                    dgtdetalle.Rows.Clear();

                // Recorremos la lista de las clases
                foreach (Detalle_Compra dc in Ocompra.oDetalleCompra)
                {
                    dgtdetalle.Rows.Add(new object[]
                    {
                        dc.Info_Producto.NombreProducto,
                        dc.PrecioCompra,
                        dc.cantidad,
                        dc.Monto_Total
                    });
                }
                txtmontototal.Text = Ocompra.MontoTotal.ToString("0.00");
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtfecha.Text = "";
            txttipoDocumento.Text = "";
            txtTelefono.Text = "";
            txtnumeroDocumento.Text = "";
            txtnombreprovedor.Text = "";
            txtusuario.Text = "";
            txtbusqueda.Text = "";

            dgtdetalle.Rows.Clear();
            txtmontototal.Text = "0.00";
        }

        private void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            //Validamos si hay datos para poder crear pdf
            if(txttipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontro informacion","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            //Almacenamos nuestra plantilla de html
            string Texto_HTML = Properties.Resources.PlantillaCompra.ToString();
            Negocio DatosNegocio = new CN_Negocio().ObtenerDatos();

            //Remplazamos los textos existentes por otros textos
            Texto_HTML = Texto_HTML.Replace("@nombrenegocio", DatosNegocio.Nombre.ToUpper());
            Texto_HTML = Texto_HTML.Replace("@docnegocio", DatosNegocio.RUC);
            Texto_HTML = Texto_HTML.Replace("@direcnegocio", DatosNegocio.Direccion);
            Texto_HTML = Texto_HTML.Replace("@tipodocumento", txttipoDocumento.Text.ToUpper());
            Texto_HTML = Texto_HTML.Replace("@numerodocumento",txtnumeroDocumento.Text);
            Texto_HTML = Texto_HTML.Replace("@telproveedor", txtTelefono.Text);
            Texto_HTML = Texto_HTML.Replace("@nombreproveedor", txtnombreprovedor.Text);
            Texto_HTML = Texto_HTML.Replace("@usuarioregistro", txtusuario.Text);
            Texto_HTML = Texto_HTML.Replace("@fecharegistro", txtfecha.Text);
            
            //Creamos una variable para alacenarlo en la tabla de html
            string filas = string.Empty;
            foreach(DataGridViewRow row in dgtdetalle.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio_Compra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_HTML = Texto_HTML.Replace("@filas", filas);
            Texto_HTML = Texto_HTML.Replace("@montototal", txtmontototal.Text);

            //Preguntamos a donde queremos descargar el archivo
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Compra_{0}.pdf",txtnumeroDocumento.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            //Validamos si hemos escogido una ruta valida
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                    //Creamos un archivo en memoria
                    using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                    {
                        //Cramos documento en pdf
                        iTextSharp.text.Document pdfdocument = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 25, 25);
                        //Devolvemos una instancia de el documento
                        PdfWriter writer = PdfWriter.GetInstance(pdfdocument, stream);
                        pdfdocument.Open();
                        //Insertamos el logo del negocio dentro del pdf
                        bool obtenido = true;
                        byte[] imagen = new CN_Negocio().ObtenerLogo(out obtenido);

                        //Validamos si se pudo obtener el logo
                        if (obtenido)
                        {
                            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagen);
                            img.ScaleToFit(60, 60);
                            img.Alignment = iTextSharp.text.Image.UNDERLYING;
                            img.SetAbsolutePosition(pdfdocument.Left, pdfdocument.GetTop(51));
                            pdfdocument.Add(img);
                        }

                        //Pegamos nuestros textos sobre el htlml
                        using (StringReader sr = new StringReader(Texto_HTML))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfdocument, sr);
                        }
                        pdfdocument.Close();
                        stream.Close();

                        MessageBox.Show("Documento generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
        }

        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {

        }

        private void btnlistado_Click(object sender, EventArgs e)
        {
            // Al dar click al boton abriremos el modal donde se muestran
            // todos los provedores en listados
            using (var ModalCompras = new modalCompras())
            {
                var result = ModalCompras.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtbusqueda.Text = ModalCompras._compra.Numero_Documento;
                }
                else
                {
                    txtnumeroDocumento.Select();
                }
            }
        }

        private void txtmontototal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtmontototal.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtTelefono.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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
