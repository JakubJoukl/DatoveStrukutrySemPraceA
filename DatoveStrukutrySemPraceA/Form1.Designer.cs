﻿using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ovladaciMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.vstupniUzel = new System.Windows.Forms.Button();
            this.prujezdovyUzel = new System.Windows.Forms.Button();
            this.koncovyUzel = new System.Windows.Forms.Button();
            this.vstupniKoncovy = new System.Windows.Forms.Button();
            this.presunUzel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.seznamL = new System.Windows.Forms.Button();
            this.seznamR = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.exportSeznamLDoSouboru = new System.Windows.Forms.Button();
            this.exportSeznamRDoSouboru = new System.Windows.Forms.Button();
            this.exportGraf = new System.Windows.Forms.Button();
            this.importGraf = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.náhledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vzhledStránkyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seznamTiskarenComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.vystupList = new System.Windows.Forms.ListBox();
            this.ovladaciMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ovladaciMenu
            // 
            this.ovladaciMenu.AutoSize = true;
            this.ovladaciMenu.BackColor = System.Drawing.Color.Navy;
            this.ovladaciMenu.Controls.Add(this.vstupniUzel);
            this.ovladaciMenu.Controls.Add(this.prujezdovyUzel);
            this.ovladaciMenu.Controls.Add(this.koncovyUzel);
            this.ovladaciMenu.Controls.Add(this.vstupniKoncovy);
            this.ovladaciMenu.Controls.Add(this.presunUzel);
            this.ovladaciMenu.Controls.Add(this.button1);
            this.ovladaciMenu.Controls.Add(this.seznamL);
            this.ovladaciMenu.Controls.Add(this.seznamR);
            this.ovladaciMenu.Controls.Add(this.vystupList);
            this.ovladaciMenu.Dock = System.Windows.Forms.DockStyle.Right;
            this.ovladaciMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ovladaciMenu.Location = new System.Drawing.Point(1025, 27);
            this.ovladaciMenu.Name = "ovladaciMenu";
            this.ovladaciMenu.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);
            this.ovladaciMenu.Size = new System.Drawing.Size(234, 741);
            this.ovladaciMenu.TabIndex = 0;
            this.ovladaciMenu.WrapContents = false;
            this.ovladaciMenu.Click += new System.EventHandler(this.ovladaciMenu_Click);
            this.ovladaciMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.ovladaciMenu_Paint);
            // 
            // vstupniUzel
            // 
            this.vstupniUzel.Location = new System.Drawing.Point(33, 13);
            this.vstupniUzel.Name = "vstupniUzel";
            this.vstupniUzel.Size = new System.Drawing.Size(168, 23);
            this.vstupniUzel.TabIndex = 0;
            this.vstupniUzel.Text = "Vytvoř vstupní uzel";
            this.vstupniUzel.UseVisualStyleBackColor = true;
            this.vstupniUzel.Click += new System.EventHandler(this.vstupniUzel_click);
            // 
            // prujezdovyUzel
            // 
            this.prujezdovyUzel.Location = new System.Drawing.Point(33, 42);
            this.prujezdovyUzel.Name = "prujezdovyUzel";
            this.prujezdovyUzel.Size = new System.Drawing.Size(168, 23);
            this.prujezdovyUzel.TabIndex = 2;
            this.prujezdovyUzel.Text = "Vytvoř průjezdový uzel";
            this.prujezdovyUzel.UseVisualStyleBackColor = true;
            this.prujezdovyUzel.Click += new System.EventHandler(this.prujezdovyUzel_click);
            // 
            // koncovyUzel
            // 
            this.koncovyUzel.Location = new System.Drawing.Point(33, 71);
            this.koncovyUzel.Name = "koncovyUzel";
            this.koncovyUzel.Size = new System.Drawing.Size(168, 23);
            this.koncovyUzel.TabIndex = 1;
            this.koncovyUzel.Text = "Vytvoř koncový uzel";
            this.koncovyUzel.UseVisualStyleBackColor = true;
            this.koncovyUzel.Click += new System.EventHandler(this.koncovyUzel_click);
            // 
            // vstupniKoncovy
            // 
            this.vstupniKoncovy.Location = new System.Drawing.Point(33, 100);
            this.vstupniKoncovy.Name = "vstupniKoncovy";
            this.vstupniKoncovy.Size = new System.Drawing.Size(168, 39);
            this.vstupniKoncovy.TabIndex = 7;
            this.vstupniKoncovy.Text = "Vytvoř vstupní koncový uzel";
            this.vstupniKoncovy.UseVisualStyleBackColor = true;
            this.vstupniKoncovy.Click += new System.EventHandler(this.vstupniKoncovy_Click);
            // 
            // presunUzel
            // 
            this.presunUzel.Location = new System.Drawing.Point(33, 145);
            this.presunUzel.Name = "presunUzel";
            this.presunUzel.Size = new System.Drawing.Size(168, 23);
            this.presunUzel.TabIndex = 3;
            this.presunUzel.Text = "Přesuň existující uzel";
            this.presunUzel.UseVisualStyleBackColor = true;
            this.presunUzel.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Přidej výlučnou cestu...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // seznamL
            // 
            this.seznamL.BackColor = System.Drawing.Color.Orange;
            this.seznamL.Location = new System.Drawing.Point(33, 203);
            this.seznamL.Name = "seznamL";
            this.seznamL.Size = new System.Drawing.Size(168, 23);
            this.seznamL.TabIndex = 4;
            this.seznamL.Text = "Dej seznam L";
            this.seznamL.UseVisualStyleBackColor = false;
            this.seznamL.Click += new System.EventHandler(this.dejSeznamL_click);
            // 
            // seznamR
            // 
            this.seznamR.BackColor = System.Drawing.Color.Orange;
            this.seznamR.Location = new System.Drawing.Point(33, 232);
            this.seznamR.Name = "seznamR";
            this.seznamR.Size = new System.Drawing.Size(168, 23);
            this.seznamR.TabIndex = 5;
            this.seznamR.Text = "Dej seznam R";
            this.seznamR.UseVisualStyleBackColor = false;
            this.seznamR.Click += new System.EventHandler(this.seznamR_click);
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.canvas.Location = new System.Drawing.Point(-1, 27);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1029, 705);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            this.canvas.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseWheel);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Navy;
            this.flowLayoutPanel1.Controls.Add(this.exportSeznamLDoSouboru);
            this.flowLayoutPanel1.Controls.Add(this.exportSeznamRDoSouboru);
            this.flowLayoutPanel1.Controls.Add(this.exportGraf);
            this.flowLayoutPanel1.Controls.Add(this.importGraf);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-1, 728);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1029, 40);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // exportSeznamLDoSouboru
            // 
            this.exportSeznamLDoSouboru.Location = new System.Drawing.Point(6, 6);
            this.exportSeznamLDoSouboru.Name = "exportSeznamLDoSouboru";
            this.exportSeznamLDoSouboru.Size = new System.Drawing.Size(145, 25);
            this.exportSeznamLDoSouboru.TabIndex = 0;
            this.exportSeznamLDoSouboru.Text = "Exportovat seznam L";
            this.exportSeznamLDoSouboru.UseVisualStyleBackColor = true;
            this.exportSeznamLDoSouboru.Click += new System.EventHandler(this.exportSeznamLDoSouboru_click);
            // 
            // exportSeznamRDoSouboru
            // 
            this.exportSeznamRDoSouboru.Location = new System.Drawing.Point(157, 6);
            this.exportSeznamRDoSouboru.Name = "exportSeznamRDoSouboru";
            this.exportSeznamRDoSouboru.Size = new System.Drawing.Size(123, 25);
            this.exportSeznamRDoSouboru.TabIndex = 1;
            this.exportSeznamRDoSouboru.Text = "Exportovat seznam R";
            this.exportSeznamRDoSouboru.UseVisualStyleBackColor = true;
            this.exportSeznamRDoSouboru.Click += new System.EventHandler(this.exportSeznamRDoSouboru_Click);
            // 
            // exportGraf
            // 
            this.exportGraf.Location = new System.Drawing.Point(286, 6);
            this.exportGraf.Name = "exportGraf";
            this.exportGraf.Size = new System.Drawing.Size(75, 25);
            this.exportGraf.TabIndex = 2;
            this.exportGraf.Text = "Exportuj graf";
            this.exportGraf.UseVisualStyleBackColor = true;
            this.exportGraf.Click += new System.EventHandler(this.exportGraf_Click);
            // 
            // importGraf
            // 
            this.importGraf.Location = new System.Drawing.Point(367, 6);
            this.importGraf.Name = "importGraf";
            this.importGraf.Size = new System.Drawing.Size(81, 25);
            this.importGraf.TabIndex = 3;
            this.importGraf.Text = "Importuj graf";
            this.importGraf.UseVisualStyleBackColor = true;
            this.importGraf.Click += new System.EventHandler(this.importGraf_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem,
            this.seznamTiskarenComboBox});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1259, 27);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.náhledToolStripMenuItem,
            this.vzhledStránkyToolStripMenuItem});
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(48, 23);
            this.sToolStripMenuItem.Text = "Tisk...";
            this.sToolStripMenuItem.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // tiskToolStripMenuItem
            // 
            this.tiskToolStripMenuItem.Name = "tiskToolStripMenuItem";
            this.tiskToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tiskToolStripMenuItem.Text = "Tisk...";
            this.tiskToolStripMenuItem.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
            // 
            // náhledToolStripMenuItem
            // 
            this.náhledToolStripMenuItem.Name = "náhledToolStripMenuItem";
            this.náhledToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.náhledToolStripMenuItem.Text = "Náhled";
            this.náhledToolStripMenuItem.Click += new System.EventHandler(this.náhledToolStripMenuItem_Click);
            // 
            // vzhledStránkyToolStripMenuItem
            // 
            this.vzhledStránkyToolStripMenuItem.Name = "vzhledStránkyToolStripMenuItem";
            this.vzhledStránkyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.vzhledStránkyToolStripMenuItem.Text = "Vzhled stránky...";
            this.vzhledStránkyToolStripMenuItem.Click += new System.EventHandler(this.vzhledStránkyToolStripMenuItem_Click);
            // 
            // seznamTiskarenComboBox
            // 
            this.seznamTiskarenComboBox.Name = "seznamTiskarenComboBox";
            this.seznamTiskarenComboBox.Size = new System.Drawing.Size(121, 23);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // vystupList
            // 
            this.vystupList.FormattingEnabled = true;
            this.vystupList.Location = new System.Drawing.Point(33, 261);
            this.vystupList.Name = "vystupList";
            this.vystupList.Size = new System.Drawing.Size(168, 446);
            this.vystupList.TabIndex = 10;
            this.vystupList.SelectedIndexChanged += new System.EventHandler(this.vystupList_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 768);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.ovladaciMenu);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ovladaciMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ovladaciMenu;
        private System.Windows.Forms.PictureBox canvas;
        public System.Windows.Forms.Button vstupniUzel;
        public System.Windows.Forms.Button koncovyUzel;
        public System.Windows.Forms.Button prujezdovyUzel;
        private Button presunUzel;
        private Button seznamL;
        private Button seznamR;
        private Button vstupniKoncovy;
        private Button button1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button exportSeznamLDoSouboru;
        private Button exportSeznamRDoSouboru;
        private Button exportGraf;
        private Button importGraf;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem sToolStripMenuItem;
        private PrintDialog printDialog;
        private ToolStripMenuItem tiskToolStripMenuItem;
        private ToolStripMenuItem náhledToolStripMenuItem;
        private ToolStripMenuItem vzhledStránkyToolStripMenuItem;
        private ToolStripComboBox seznamTiskarenComboBox;
        private PageSetupDialog pageSetupDialog;
        private PrintPreviewDialog printPreviewDialog;
        private ListBox vystupList;
    }
}

