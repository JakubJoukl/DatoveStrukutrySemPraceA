namespace DatoveStrukutrySemPraceA
{
    partial class Form2
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
            this.prvniZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.vyhybka = new System.Windows.Forms.TextBox();
            this.prvniDo = new System.Windows.Forms.TextBox();
            this.zrusit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.druhyZ = new System.Windows.Forms.TextBox();
            this.druhyDo = new System.Windows.Forms.TextBox();
            this.potvrdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // prvniZ
            // 
            this.prvniZ.Location = new System.Drawing.Point(71, 44);
            this.prvniZ.Name = "prvniZ";
            this.prvniZ.Size = new System.Drawing.Size(100, 20);
            this.prvniZ.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Z vrcholu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Výhybka";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Do Vrcholu";
            // 
            // vyhybka
            // 
            this.vyhybka.Location = new System.Drawing.Point(139, 69);
            this.vyhybka.Name = "vyhybka";
            this.vyhybka.Size = new System.Drawing.Size(100, 20);
            this.vyhybka.TabIndex = 4;
            // 
            // prvniDo
            // 
            this.prvniDo.Location = new System.Drawing.Point(71, 95);
            this.prvniDo.Name = "prvniDo";
            this.prvniDo.Size = new System.Drawing.Size(100, 20);
            this.prvniDo.TabIndex = 5;
            // 
            // zrusit
            // 
            this.zrusit.Location = new System.Drawing.Point(71, 140);
            this.zrusit.Name = "zrusit";
            this.zrusit.Size = new System.Drawing.Size(75, 23);
            this.zrusit.TabIndex = 6;
            this.zrusit.Text = "Zrušit";
            this.zrusit.UseVisualStyleBackColor = true;
            this.zrusit.Click += new System.EventHandler(this.zrusit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "První cesta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(231, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Druhá cesta";
            // 
            // druhyZ
            // 
            this.druhyZ.Location = new System.Drawing.Point(212, 43);
            this.druhyZ.Name = "druhyZ";
            this.druhyZ.Size = new System.Drawing.Size(100, 20);
            this.druhyZ.TabIndex = 9;
            // 
            // druhyDo
            // 
            this.druhyDo.Location = new System.Drawing.Point(212, 95);
            this.druhyDo.Name = "druhyDo";
            this.druhyDo.Size = new System.Drawing.Size(100, 20);
            this.druhyDo.TabIndex = 10;
            // 
            // potvrdit
            // 
            this.potvrdit.Location = new System.Drawing.Point(237, 140);
            this.potvrdit.Name = "potvrdit";
            this.potvrdit.Size = new System.Drawing.Size(75, 23);
            this.potvrdit.TabIndex = 11;
            this.potvrdit.Text = "Potvrdit";
            this.potvrdit.UseVisualStyleBackColor = true;
            this.potvrdit.Click += new System.EventHandler(this.potvrdit_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 175);
            this.Controls.Add(this.potvrdit);
            this.Controls.Add(this.druhyDo);
            this.Controls.Add(this.druhyZ);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zrusit);
            this.Controls.Add(this.prvniDo);
            this.Controls.Add(this.vyhybka);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prvniZ);
            this.Name = "Form2";
            this.Text = "Přidání výlučné cesty";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox prvniZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox vyhybka;
        private System.Windows.Forms.TextBox prvniDo;
        private System.Windows.Forms.Button zrusit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox druhyZ;
        private System.Windows.Forms.TextBox druhyDo;
        private System.Windows.Forms.Button potvrdit;
    }
}