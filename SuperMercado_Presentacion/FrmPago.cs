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
    public partial class FrmPago : Form
    {
        public decimal Total { get; set; }
        public decimal MontoPago { get;  set; }
        public string TipoPago { get; set; }
        public decimal Cambio {  get; set; }
        public FrmPago()
        {
            InitializeComponent();
        }

        private void FrmPago_Load(object sender, EventArgs e)
        {
            cmbtipopago.Items.Add(new OpcionCombo() { Valor = "Efectivo", Texto = "Efectivo" });
            cmbtipopago.Items.Add(new OpcionCombo() { Valor = "Sinpe", Texto = "Sinpe" });
            cmbtipopago.Items.Add(new OpcionCombo() { Valor = "Tarjeta", Texto = "Tarjeta" });
            cmbtipopago.DisplayMember = "Texto";
            cmbtipopago.ValueMember = "Valor";
            cmbtipopago.SelectedIndex = 0;
            txttotal.Text = Total.ToString("0");
        }

        private void btnpago_Click(object sender, EventArgs e)
        {
            // Validar y asignar el monto de pago
            if (decimal.TryParse(txtMontoPago.Text, out decimal montoPago))
            {
                MontoPago = montoPago;
               
                // Indicar que la operación fue exitosa y cerrar el formulario
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Ingrese un monto de pago válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Metodo por el cual vamos a calcular el cambio
        /// </summary>
        private void CalcularCambio()
        {
            string totalText = txttotal.Text.Replace(",", "").Replace(".", "");
            if (decimal.TryParse(totalText, out decimal total))
            {
                Total = total;
            }
            if (decimal.TryParse(txtMontoPago.Text, out decimal montoPago))
            {
                MontoPago = montoPago;
            }

            // Validamos que el monto de pago sea mayor o igual al total
            if (montoPago < total)
            {
                MessageBox.Show("Pago inferior al total", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtcambio.Text = "0";
            }
            else
            {
                Cambio = montoPago - total;
                txtcambio.Text = Cambio.ToString();
                TipoPago = ((OpcionCombo)cmbtipopago.SelectedItem).Texto;
            }
        }


        private void txtMontoPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CalcularCambio();
            }
        }
    }
}
