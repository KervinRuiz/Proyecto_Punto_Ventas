namespace SuperMercado_Presentacion
{
    partial class frmPagoCreditos
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtidcliente = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnbuscarcliente = new FontAwesome.Sharp.IconButton();
            this.txttelefonocliente = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnombreCliente = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnbuscarCredito = new FontAwesome.Sharp.IconButton();
            this.txtidcredito = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtmonto = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtfecha = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmontoPago = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPago = new FontAwesome.Sharp.IconButton();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtidcliente);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnbuscarcliente);
            this.groupBox2.Controls.Add(this.txttelefonocliente);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtnombreCliente);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Location = new System.Drawing.Point(176, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1518, 129);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informacion de cliente";
            // 
            // txtidcliente
            // 
            this.txtidcliente.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtidcliente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtidcliente.DefaultText = "";
            this.txtidcliente.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtidcliente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtidcliente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtidcliente.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtidcliente.Enabled = false;
            this.txtidcliente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtidcliente.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtidcliente.ForeColor = System.Drawing.Color.Black;
            this.txtidcliente.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtidcliente.Location = new System.Drawing.Point(10, 74);
            this.txtidcliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtidcliente.Name = "txtidcliente";
            this.txtidcliente.PasswordChar = '\0';
            this.txtidcliente.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtidcliente.PlaceholderText = "";
            this.txtidcliente.SelectedText = "";
            this.txtidcliente.Size = new System.Drawing.Size(295, 36);
            this.txtidcliente.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cedula";
            // 
            // btnbuscarcliente
            // 
            this.btnbuscarcliente.BackColor = System.Drawing.Color.Silver;
            this.btnbuscarcliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbuscarcliente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnbuscarcliente.IconChar = FontAwesome.Sharp.IconChar.Sistrix;
            this.btnbuscarcliente.IconColor = System.Drawing.Color.Black;
            this.btnbuscarcliente.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnbuscarcliente.IconSize = 30;
            this.btnbuscarcliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnbuscarcliente.Location = new System.Drawing.Point(1161, 74);
            this.btnbuscarcliente.Name = "btnbuscarcliente";
            this.btnbuscarcliente.Size = new System.Drawing.Size(178, 36);
            this.btnbuscarcliente.TabIndex = 5;
            this.btnbuscarcliente.Text = "Buscar";
            this.btnbuscarcliente.UseVisualStyleBackColor = false;
            this.btnbuscarcliente.Click += new System.EventHandler(this.btnbuscarcliente_Click);
            // 
            // txttelefonocliente
            // 
            this.txttelefonocliente.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txttelefonocliente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txttelefonocliente.DefaultText = "";
            this.txttelefonocliente.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txttelefonocliente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txttelefonocliente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttelefonocliente.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttelefonocliente.Enabled = false;
            this.txttelefonocliente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttelefonocliente.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttelefonocliente.ForeColor = System.Drawing.Color.Black;
            this.txttelefonocliente.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttelefonocliente.Location = new System.Drawing.Point(658, 76);
            this.txttelefonocliente.Margin = new System.Windows.Forms.Padding(4);
            this.txttelefonocliente.Name = "txttelefonocliente";
            this.txttelefonocliente.PasswordChar = '\0';
            this.txttelefonocliente.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txttelefonocliente.PlaceholderText = "";
            this.txttelefonocliente.SelectedText = "";
            this.txttelefonocliente.Size = new System.Drawing.Size(260, 34);
            this.txttelefonocliente.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(654, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Telefono";
            // 
            // txtnombreCliente
            // 
            this.txtnombreCliente.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtnombreCliente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtnombreCliente.DefaultText = "";
            this.txtnombreCliente.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtnombreCliente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtnombreCliente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtnombreCliente.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtnombreCliente.Enabled = false;
            this.txtnombreCliente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtnombreCliente.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombreCliente.ForeColor = System.Drawing.Color.Black;
            this.txtnombreCliente.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtnombreCliente.Location = new System.Drawing.Point(344, 76);
            this.txtnombreCliente.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtnombreCliente.Name = "txtnombreCliente";
            this.txtnombreCliente.PasswordChar = '\0';
            this.txtnombreCliente.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtnombreCliente.PlaceholderText = "";
            this.txtnombreCliente.SelectedText = "";
            this.txtnombreCliente.Size = new System.Drawing.Size(260, 34);
            this.txtnombreCliente.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(340, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nombre";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnbuscarCredito);
            this.groupBox1.Controls.Add(this.txtidcredito);
            this.groupBox1.Controls.Add(this.txtmonto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtfecha);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(176, 253);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1518, 137);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion de Credito";
            // 
            // btnbuscarCredito
            // 
            this.btnbuscarCredito.BackColor = System.Drawing.Color.Silver;
            this.btnbuscarCredito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbuscarCredito.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnbuscarCredito.IconChar = FontAwesome.Sharp.IconChar.Sistrix;
            this.btnbuscarCredito.IconColor = System.Drawing.Color.Black;
            this.btnbuscarCredito.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnbuscarCredito.IconSize = 30;
            this.btnbuscarCredito.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnbuscarCredito.Location = new System.Drawing.Point(970, 87);
            this.btnbuscarCredito.Name = "btnbuscarCredito";
            this.btnbuscarCredito.Size = new System.Drawing.Size(178, 36);
            this.btnbuscarCredito.TabIndex = 5;
            this.btnbuscarCredito.Text = "Buscar";
            this.btnbuscarCredito.UseVisualStyleBackColor = false;
            this.btnbuscarCredito.Click += new System.EventHandler(this.btnbuscarCredito_Click);
            // 
            // txtidcredito
            // 
            this.txtidcredito.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtidcredito.DefaultText = "";
            this.txtidcredito.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtidcredito.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtidcredito.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtidcredito.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtidcredito.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtidcredito.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtidcredito.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtidcredito.Location = new System.Drawing.Point(1122, 20);
            this.txtidcredito.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtidcredito.Name = "txtidcredito";
            this.txtidcredito.PasswordChar = '\0';
            this.txtidcredito.PlaceholderText = "";
            this.txtidcredito.SelectedText = "";
            this.txtidcredito.Size = new System.Drawing.Size(53, 24);
            this.txtidcredito.TabIndex = 4;
            this.txtidcredito.Visible = false;
            // 
            // txtmonto
            // 
            this.txtmonto.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtmonto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtmonto.DefaultText = "";
            this.txtmonto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtmonto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtmonto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtmonto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtmonto.Enabled = false;
            this.txtmonto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtmonto.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmonto.ForeColor = System.Drawing.Color.Black;
            this.txtmonto.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtmonto.Location = new System.Drawing.Point(427, 87);
            this.txtmonto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.PasswordChar = '\0';
            this.txtmonto.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtmonto.PlaceholderText = "";
            this.txtmonto.SelectedText = "";
            this.txtmonto.Size = new System.Drawing.Size(279, 36);
            this.txtmonto.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Monto";
            // 
            // txtfecha
            // 
            this.txtfecha.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtfecha.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtfecha.DefaultText = "";
            this.txtfecha.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtfecha.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtfecha.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtfecha.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtfecha.Enabled = false;
            this.txtfecha.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtfecha.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfecha.ForeColor = System.Drawing.Color.Black;
            this.txtfecha.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtfecha.Location = new System.Drawing.Point(23, 87);
            this.txtfecha.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.PasswordChar = '\0';
            this.txtfecha.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtfecha.PlaceholderText = "";
            this.txtfecha.SelectedText = "";
            this.txtfecha.Size = new System.Drawing.Size(281, 36);
            this.txtfecha.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fecha";
            // 
            // txtmontoPago
            // 
            this.txtmontoPago.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtmontoPago.BorderColor = System.Drawing.Color.Black;
            this.txtmontoPago.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtmontoPago.DefaultText = "";
            this.txtmontoPago.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtmontoPago.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtmontoPago.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtmontoPago.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtmontoPago.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtmontoPago.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmontoPago.ForeColor = System.Drawing.Color.Black;
            this.txtmontoPago.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtmontoPago.Location = new System.Drawing.Point(176, 525);
            this.txtmontoPago.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtmontoPago.Name = "txtmontoPago";
            this.txtmontoPago.PasswordChar = '\0';
            this.txtmontoPago.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtmontoPago.PlaceholderText = "";
            this.txtmontoPago.SelectedText = "";
            this.txtmontoPago.Size = new System.Drawing.Size(247, 36);
            this.txtmontoPago.TabIndex = 7;
            this.txtmontoPago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtmontoPago_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(172, 479);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "Monto de pago";
            // 
            // btnPago
            // 
            this.btnPago.BackColor = System.Drawing.Color.DarkGray;
            this.btnPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPago.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPago.IconChar = FontAwesome.Sharp.IconChar.Donate;
            this.btnPago.IconColor = System.Drawing.Color.Black;
            this.btnPago.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPago.IconSize = 30;
            this.btnPago.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPago.Location = new System.Drawing.Point(520, 518);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(317, 43);
            this.btnPago.TabIndex = 40;
            this.btnPago.Text = "Realizar Pago";
            this.btnPago.UseVisualStyleBackColor = false;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(738, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(226, 33);
            this.label7.TabIndex = 41;
            this.label7.Text = "Pago de credito";
            // 
            // frmPagoCreditos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1761, 787);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnPago);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtmontoPago);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPagoCreditos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fiados";
            this.Load += new System.EventHandler(this.frmPagoCreditos_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private FontAwesome.Sharp.IconButton btnbuscarcliente;
        private Guna.UI2.WinForms.Guna2TextBox txtidcliente;
        private Guna.UI2.WinForms.Guna2TextBox txttelefonocliente;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox txtnombreCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton btnbuscarCredito;
        private Guna.UI2.WinForms.Guna2TextBox txtidcredito;
        private Guna.UI2.WinForms.Guna2TextBox txtmonto;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtfecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtmontoPago;
        private System.Windows.Forms.Label label6;
        private FontAwesome.Sharp.IconButton btnPago;
        private System.Windows.Forms.Label label7;
    }
}