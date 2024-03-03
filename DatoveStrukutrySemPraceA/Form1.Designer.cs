namespace DatoveStrukutrySemPraceA
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.ovladaciMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.vstupniUzel = new System.Windows.Forms.Button();
            this.prujezdovyUzel = new System.Windows.Forms.Button();
            this.koncovyUzel = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.ovladaciMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // ovladaciMenu
            // 
            this.ovladaciMenu.BackColor = System.Drawing.Color.Navy;
            this.ovladaciMenu.Controls.Add(this.vstupniUzel);
            this.ovladaciMenu.Controls.Add(this.prujezdovyUzel);
            this.ovladaciMenu.Controls.Add(this.koncovyUzel);
            this.ovladaciMenu.Dock = System.Windows.Forms.DockStyle.Right;
            this.ovladaciMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ovladaciMenu.Location = new System.Drawing.Point(965, 0);
            this.ovladaciMenu.Name = "ovladaciMenu";
            this.ovladaciMenu.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);
            this.ovladaciMenu.Size = new System.Drawing.Size(203, 641);
            this.ovladaciMenu.TabIndex = 0;
            this.ovladaciMenu.Click += new System.EventHandler(this.ovladaciMenu_Click);
            this.ovladaciMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.ovladaciMenu_Paint);
            // 
            // vstupniUzel
            // 
            this.vstupniUzel.Location = new System.Drawing.Point(33, 13);
            this.vstupniUzel.Name = "vstupniUzel";
            this.vstupniUzel.Size = new System.Drawing.Size(135, 23);
            this.vstupniUzel.TabIndex = 0;
            this.vstupniUzel.Text = "Vytvoř vstupní uzel";
            this.vstupniUzel.UseVisualStyleBackColor = true;
            this.vstupniUzel.Click += new System.EventHandler(this.vstupniUzel_click);
            // 
            // prujezdovyUzel
            // 
            this.prujezdovyUzel.Location = new System.Drawing.Point(33, 42);
            this.prujezdovyUzel.Name = "prujezdovyUzel";
            this.prujezdovyUzel.Size = new System.Drawing.Size(135, 23);
            this.prujezdovyUzel.TabIndex = 2;
            this.prujezdovyUzel.Text = "Vytvoř průjezdový uzel";
            this.prujezdovyUzel.UseVisualStyleBackColor = true;
            this.prujezdovyUzel.Click += new System.EventHandler(this.prujezdovyUzel_click);
            // 
            // koncovyUzel
            // 
            this.koncovyUzel.Location = new System.Drawing.Point(33, 71);
            this.koncovyUzel.Name = "koncovyUzel";
            this.koncovyUzel.Size = new System.Drawing.Size(135, 23);
            this.koncovyUzel.TabIndex = 1;
            this.koncovyUzel.Text = "Vytvoř koncový uzel";
            this.koncovyUzel.UseVisualStyleBackColor = true;
            this.koncovyUzel.Click += new System.EventHandler(this.koncovyUzel_click);
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.canvas.Location = new System.Drawing.Point(-1, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(972, 641);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 641);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.ovladaciMenu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ovladaciMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ovladaciMenu;
        private System.Windows.Forms.PictureBox canvas;
        public System.Windows.Forms.Button vstupniUzel;
        public System.Windows.Forms.Button koncovyUzel;
        public System.Windows.Forms.Button prujezdovyUzel;
    }
}

