namespace SuperMercado_Presentacion
{
    partial class FrmPago
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMontoPago = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnpago = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbtipopago = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txttotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtcambio = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // txtMontoPago
            // 
            this.txtMontoPago.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMontoPago.DefaultText = "";
            this.txtMontoPago.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMontoPago.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMontoPago.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMontoPago.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMontoPago.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMontoPago.Font = new System.Drawing.Font("Gill Sans MT", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoPago.ForeColor = System.Drawing.Color.Black;
            this.txtMontoPago.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMontoPago.Location = new System.Drawing.Point(566, 248);
            this.txtMontoPago.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMontoPago.Name = "txtMontoPago";
            this.txtMontoPago.PasswordChar = '\0';
            this.txtMontoPago.PlaceholderText = "";
            this.txtMontoPago.SelectedText = "";
            this.txtMontoPago.Size = new System.Drawing.Size(572, 62);
            this.txtMontoPago.TabIndex = 1;
            this.txtMontoPago.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMontoPago_KeyDown);
            // 
            // btnpago
            // 
            this.btnpago.BackColor = System.Drawing.Color.Honeydew;
            this.btnpago.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnpago.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnpago.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnpago.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnpago.FillColor = System.Drawing.Color.HotPink;
            this.btnpago.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpago.ForeColor = System.Drawing.Color.Black;
            this.btnpago.Location = new System.Drawing.Point(1107, 572);
            this.btnpago.Name = "btnpago";
            this.btnpago.Size = new System.Drawing.Size(272, 72);
            this.btnpago.TabIndex = 2;
            this.btnpago.Text = "Registrar";
            this.btnpago.Click += new System.EventHandler(this.btnpago_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26F);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(32, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 51);
            this.label1.TabIndex = 3;
            this.label1.Text = "Paga Con: ";
            // 
            // cmbtipopago
            // 
            this.cmbtipopago.BackColor = System.Drawing.Color.Transparent;
            this.cmbtipopago.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbtipopago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtipopago.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbtipopago.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbtipopago.Font = new System.Drawing.Font("Arial Rounded MT Bold", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbtipopago.ForeColor = System.Drawing.Color.Black;
            this.cmbtipopago.ItemHeight = 30;
            this.cmbtipopago.Location = new System.Drawing.Point(607, 142);
            this.cmbtipopago.Name = "cmbtipopago";
            this.cmbtipopago.Size = new System.Drawing.Size(531, 36);
            this.cmbtipopago.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26F);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(51, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 51);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tipo de pago:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26F);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(41, 422);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 51);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cambio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26F);
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(41, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(351, 51);
            this.label4.TabIndex = 8;
            this.label4.Text = "Total de venta: ";
            // 
            // txttotal
            // 
            this.txttotal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txttotal.DefaultText = "";
            this.txttotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txttotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txttotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttotal.Enabled = false;
            this.txttotal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttotal.Font = new System.Drawing.Font("Arial Rounded MT Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotal.ForeColor = System.Drawing.Color.Black;
            this.txttotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttotal.Location = new System.Drawing.Point(607, 24);
            this.txttotal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txttotal.Name = "txttotal";
            this.txttotal.PasswordChar = '\0';
            this.txttotal.PlaceholderText = "";
            this.txttotal.SelectedText = "";
            this.txttotal.Size = new System.Drawing.Size(543, 63);
            this.txttotal.TabIndex = 9;
            // 
            // txtcambio
            // 
            this.txtcambio.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtcambio.DefaultText = "";
            this.txtcambio.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtcambio.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtcambio.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtcambio.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtcambio.Enabled = false;
            this.txtcambio.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtcambio.Font = new System.Drawing.Font("Arial Rounded MT Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcambio.ForeColor = System.Drawing.Color.Black;
            this.txtcambio.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtcambio.Location = new System.Drawing.Point(516, 415);
            this.txtcambio.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtcambio.Name = "txtcambio";
            this.txtcambio.PasswordChar = '\0';
            this.txtcambio.PlaceholderText = "";
            this.txtcambio.SelectedText = "";
            this.txtcambio.Size = new System.Drawing.Size(520, 58);
            this.txtcambio.TabIndex = 10;
            // 
            // FrmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1486, 711);
            this.Controls.Add(this.txtcambio);
            this.Controls.Add(this.txttotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbtipopago);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnpago);
            this.Controls.Add(this.txtMontoPago);
            this.Name = "FrmPago";
            this.Text = "Pulperia el sueño";
            this.Load += new System.EventHandler(this.FrmPago_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox txtMontoPago;
        private Guna.UI2.WinForms.Guna2Button btnpago;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbtipopago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox txttotal;
        private Guna.UI2.WinForms.Guna2TextBox txtcambio;
    }
}