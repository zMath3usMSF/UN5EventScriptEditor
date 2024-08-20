namespace UN5EventScriptEditor
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openCCSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCCSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCCSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToTXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCCSToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openCCSToolStripMenuItem
            // 
            this.openCCSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCCSToolStripMenuItem1,
            this.saveCCSToolStripMenuItem,
            this.exportToTXTToolStripMenuItem});
            this.openCCSToolStripMenuItem.Name = "openCCSToolStripMenuItem";
            this.openCCSToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openCCSToolStripMenuItem.Text = "File";
            // 
            // openCCSToolStripMenuItem1
            // 
            this.openCCSToolStripMenuItem1.Name = "openCCSToolStripMenuItem1";
            this.openCCSToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.openCCSToolStripMenuItem1.Text = "Open CCS";
            this.openCCSToolStripMenuItem1.Click += new System.EventHandler(this.openCCSToolStripMenuItem1_Click);
            // 
            // saveCCSToolStripMenuItem
            // 
            this.saveCCSToolStripMenuItem.Name = "saveCCSToolStripMenuItem";
            this.saveCCSToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.saveCCSToolStripMenuItem.Text = "Save CCS";
            this.saveCCSToolStripMenuItem.Click += new System.EventHandler(this.saveCCSToolStripMenuItem_Click);
            // 
            // exportToTXTToolStripMenuItem
            // 
            this.exportToTXTToolStripMenuItem.Name = "exportToTXTToolStripMenuItem";
            this.exportToTXTToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exportToTXTToolStripMenuItem.Text = "Export to TXT";
            this.exportToTXTToolStripMenuItem.Click += new System.EventHandler(this.exportToTXTToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(800, 426);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::UN5EventScriptEditor.Properties.Resources.BackgroundImage;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 426);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "UN5 Event Script Editor v1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openCCSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCCSToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveCCSToolStripMenuItem;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem exportToTXTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

