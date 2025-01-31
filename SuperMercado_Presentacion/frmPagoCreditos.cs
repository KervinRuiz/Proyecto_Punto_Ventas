using SuperMercado_Entidades;
using SuperMercado_Negocio;
using SuperMercado_Presentacion.Modales;
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
    public partial class frmPagoCreditos : Form
    {
        public frmPagoCreditos()
        {
            InitializeComponent();
        }

        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            // Al dar click al boton abriremos el modal donde se muestran
            // todos los provedores en listados
            using (var ModalCliente = new mdCliente())
            {
                var result = ModalCliente.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidcliente.Text = ModalCliente._cliente.Cedula.ToString();
                    txtnombreCliente.Text = ModalCliente._cliente.Nombre;
                    txttelefonocliente.Text = ModalCliente._cliente.Telefono;
                }
                else
                {
                    txtidcliente.Select();
                }
            }
        }

        private void btnbuscarCredito_Click(object sender, EventArgs e)
        {
            // Al dar click al boton abriremos el modal donde se muestran
            // todos los creditos en listados
            using (var ModalCreditos = new modalCreditos())
            {
                var result = ModalCreditos.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidcredito.Text = ModalCreditos._creditocliente.id_Cliente.ToString();
                    txtmonto.Text = ModalCreditos._creditocliente.Monto_Credito.ToString();
                    txtfecha.Text = ModalCreditos._creditocliente.Fecha_Registro;
                }
                else
                {
                    txtidcliente.Select();
                }
            }
        }

        private void frmPagoCreditos_Load(object sender, EventArgs e)
        {

        }

        private void btnPago_Click(object sender, EventArgs e)
        {
            if(txtidcliente.Text == string.Empty)
            {
                MessageBox.Show("Debes tener un cliente seleccionado","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtidcliente.Focus();
                return;
            }
            if(txtmontoPago.Text == string.Empty)
            {
                MessageBox.Show("Debes introducir el monto de pago","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtmontoPago.Focus();
                txtmontoPago.Select();
                return;
            }
            // Creo un objeto de tipo de abonos 
            Abonos abonos = new Abonos()
            {
                Info_Cliente = new Cliente() { Cedula = txtidcliente.Text },
                Monto_Abono = Convert.ToDecimal(txtmontoPago.Text)
            };
            //Declaro una variable para el parametro de mensaje
            string mensaje = string.Empty;
            //Llamado del metodo de insertar en la tabla de abonos
            bool respuesta = new CN_Abonos().RegistrarAbono(abonos, out mensaje);

            //validamos si el registro esta correcto para guardar en la tabla de abonos
            if (respuesta)
            {
                MessageBox.Show("Abono agregado","Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(mensaje,"Alerta",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void LimpiarCampos()
        {
            txtfecha.Text = "";
            txtidcliente.Text = "";
            txtidcredito.Text = "";
            txtmonto.Text = "";
            txtmontoPago.Text = "";
            txtnombreCliente.Text = "";
            txttelefonocliente.Text = "";
        }

        private void txtmontoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //Validamos que no deje ingresar un punto como un primer valor
                if (txtmonto.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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
