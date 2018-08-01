namespace Captcha
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblVersion = new System.Windows.Forms.Label();
            this.cmdTrabajoRobot = new System.Windows.Forms.Button();
            this.cmdActualizar = new System.Windows.Forms.Button();
            this.cmdApagar = new System.Windows.Forms.Button();
            this.txtProgreso = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modificacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarRutasDeArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarDatosParaUsoDePortalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informesDelProveedorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informesDelRobotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtplaca = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 38);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Version";
            // 
            // cmdTrabajoRobot
            // 
            this.cmdTrabajoRobot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmdTrabajoRobot.Location = new System.Drawing.Point(16, 144);
            this.cmdTrabajoRobot.Name = "cmdTrabajoRobot";
            this.cmdTrabajoRobot.Size = new System.Drawing.Size(346, 69);
            this.cmdTrabajoRobot.TabIndex = 1;
            this.cmdTrabajoRobot.Text = "Realizar trabajo";
            this.cmdTrabajoRobot.UseVisualStyleBackColor = true;
            this.cmdTrabajoRobot.Click += new System.EventHandler(this.cmdTrabajoRobot_Click);
            // 
            // cmdActualizar
            // 
            this.cmdActualizar.Location = new System.Drawing.Point(16, 228);
            this.cmdActualizar.Name = "cmdActualizar";
            this.cmdActualizar.Size = new System.Drawing.Size(164, 28);
            this.cmdActualizar.TabIndex = 2;
            this.cmdActualizar.Text = "Actualizar versión";
            this.cmdActualizar.UseVisualStyleBackColor = true;
            this.cmdActualizar.Click += new System.EventHandler(this.cmdActualizar_Click);
            // 
            // cmdApagar
            // 
            this.cmdApagar.Location = new System.Drawing.Point(199, 228);
            this.cmdApagar.Name = "cmdApagar";
            this.cmdApagar.Size = new System.Drawing.Size(164, 28);
            this.cmdApagar.TabIndex = 3;
            this.cmdApagar.Text = "Apagar";
            this.cmdApagar.UseVisualStyleBackColor = true;
            this.cmdApagar.Click += new System.EventHandler(this.cmdApagar_Click);
            // 
            // txtProgreso
            // 
            this.txtProgreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txtProgreso.Location = new System.Drawing.Point(16, 262);
            this.txtProgreso.Name = "txtProgreso";
            this.txtProgreso.Size = new System.Drawing.Size(539, 30);
            this.txtProgreso.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 301);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(543, 25);
            this.dataGridView1.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificacionesToolStripMenuItem,
            this.informesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(567, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modificacionesToolStripMenuItem
            // 
            this.modificacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarRutasDeArchivoToolStripMenuItem,
            this.cambiarDatosParaUsoDePortalToolStripMenuItem});
            this.modificacionesToolStripMenuItem.Name = "modificacionesToolStripMenuItem";
            this.modificacionesToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.modificacionesToolStripMenuItem.Text = "Modificaciones";
            // 
            // cambiarRutasDeArchivoToolStripMenuItem
            // 
            this.cambiarRutasDeArchivoToolStripMenuItem.Name = "cambiarRutasDeArchivoToolStripMenuItem";
            this.cambiarRutasDeArchivoToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.cambiarRutasDeArchivoToolStripMenuItem.Text = "Cambiar rutas de archivo";
            this.cambiarRutasDeArchivoToolStripMenuItem.Click += new System.EventHandler(this.cambiarRutasDeArchivoToolStripMenuItem_Click);
            // 
            // cambiarDatosParaUsoDePortalToolStripMenuItem
            // 
            this.cambiarDatosParaUsoDePortalToolStripMenuItem.Name = "cambiarDatosParaUsoDePortalToolStripMenuItem";
            this.cambiarDatosParaUsoDePortalToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.cambiarDatosParaUsoDePortalToolStripMenuItem.Text = "Cambiar datos para uso de portal";
            this.cambiarDatosParaUsoDePortalToolStripMenuItem.Click += new System.EventHandler(this.cambiarDatosParaUsoDePortalToolStripMenuItem_Click);
            // 
            // informesToolStripMenuItem
            // 
            this.informesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informesDelProveedorToolStripMenuItem,
            this.informesDelRobotToolStripMenuItem});
            this.informesToolStripMenuItem.Name = "informesToolStripMenuItem";
            this.informesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.informesToolStripMenuItem.Text = "Informes";
            // 
            // informesDelProveedorToolStripMenuItem
            // 
            this.informesDelProveedorToolStripMenuItem.Name = "informesDelProveedorToolStripMenuItem";
            this.informesDelProveedorToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.informesDelProveedorToolStripMenuItem.Text = "Informes del proveedor";
            this.informesDelProveedorToolStripMenuItem.Click += new System.EventHandler(this.informesDelProveedorToolStripMenuItem_Click_1);
            // 
            // informesDelRobotToolStripMenuItem
            // 
            this.informesDelRobotToolStripMenuItem.Name = "informesDelRobotToolStripMenuItem";
            this.informesDelRobotToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.informesDelRobotToolStripMenuItem.Text = "Informes del robot";
            this.informesDelRobotToolStripMenuItem.Click += new System.EventHandler(this.informesDelRobotToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Captcha.Properties.Resources.Leyendo;
            this.pictureBox1.Location = new System.Drawing.Point(369, 105);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(186, 151);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ingresa el numero de placa a consutlar";
            // 
            // txtplaca
            // 
            this.txtplaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtplaca.Location = new System.Drawing.Point(16, 105);
            this.txtplaca.Name = "txtplaca";
            this.txtplaca.Size = new System.Drawing.Size(346, 26);
            this.txtplaca.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 345);
            this.Controls.Add(this.txtplaca);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtProgreso);
            this.Controls.Add(this.cmdApagar);
            this.Controls.Add(this.cmdActualizar);
            this.Controls.Add(this.cmdTrabajoRobot);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Consulta Vehicular";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button cmdTrabajoRobot;
        private System.Windows.Forms.Button cmdActualizar;
        private System.Windows.Forms.Button cmdApagar;
        private System.Windows.Forms.TextBox txtProgreso;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modificacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarRutasDeArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarDatosParaUsoDePortalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informesDelProveedorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informesDelRobotToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtplaca;
    }
}

