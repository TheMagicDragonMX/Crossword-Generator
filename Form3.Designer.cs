namespace Crucigrama_2._0
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.Valideichon = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.Win = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Valideichon
            // 
            this.Valideichon.Tick += new System.EventHandler(this.Valideichon_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PeachPuff;
            this.button1.Font = new System.Drawing.Font("Palatino Linotype", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(666, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 77);
            this.button1.TabIndex = 0;
            this.button1.Text = "Seleccionar dificultad diferente";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Win
            // 
            this.Win.AutoSize = true;
            this.Win.BackColor = System.Drawing.Color.Gold;
            this.Win.Font = new System.Drawing.Font("Segoe Print", 25.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Win.ForeColor = System.Drawing.Color.Snow;
            this.Win.Location = new System.Drawing.Point(186, 163);
            this.Win.Name = "Win";
            this.Win.Size = new System.Drawing.Size(378, 79);
            this.Win.TabIndex = 1;
            this.Win.Text = "COMPLETADO!";
            this.Win.Visible = false;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::Crucigrama_2._0.Properties.Resources.Definiciones;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Win);
            this.Controls.Add(this.button1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Valideichon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Win;
    }
}