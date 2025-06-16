namespace SuperMegaJeuDuPatrickPong
{
    partial class FormulaireDuKrabsKrusty
    {
        private System.ComponentModel.IContainer composants = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (composants != null)
                    composants.Dispose();

                // Nettoyer les ressources audio
                if (musiqueDuFondMarin != null)
                    musiqueDuFondMarin.Nettoyer();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.composants = new System.ComponentModel.Container();
            this.zoneDeBikiniBas = new System.Windows.Forms.PictureBox();
            this.minuteurDuMedusePatient = new System.Windows.Forms.Timer(this.composants);
            this.scorePatrickEtoile = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.zoneDeBikiniBas)).BeginInit();
            this.SuspendLayout();
            // 
            // zoneDeBikiniBas
            // 
            this.zoneDeBikiniBas.BackColor = System.Drawing.Color.Black;
            this.zoneDeBikiniBas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoneDeBikiniBas.Location = new System.Drawing.Point(0, 0);
            this.zoneDeBikiniBas.Name = "zoneDeBikiniBas";
            this.zoneDeBikiniBas.Size = new System.Drawing.Size(800, 450);
            this.zoneDeBikiniBas.TabIndex = 0;
            this.zoneDeBikiniBas.TabStop = false;
            this.zoneDeBikiniBas.Paint += new System.Windows.Forms.PaintEventHandler(this.zoneDeBikiniBas_Paint);
            // 
            // minuteurDuMedusePatient
            // 
            this.minuteurDuMedusePatient.Interval = 16;
            this.minuteurDuMedusePatient.Tick += new System.EventHandler(this.minuteurDuMedusePatient_Tick);
            // 
            // scorePatrickEtoile
            // 
            this.scorePatrickEtoile.AutoSize = true;
            this.scorePatrickEtoile.BackColor = System.Drawing.Color.Black;
            this.scorePatrickEtoile.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.scorePatrickEtoile.ForeColor = System.Drawing.Color.White;
            this.scorePatrickEtoile.Location = new System.Drawing.Point(12, 9);
            this.scorePatrickEtoile.Name = "scorePatrickEtoile";
            this.scorePatrickEtoile.Size = new System.Drawing.Size(76, 26);
            this.scorePatrickEtoile.TabIndex = 1;
            this.scorePatrickEtoile.Text = "0 - 0";
            // 
            // FormulaireDuKrabsKrusty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scorePatrickEtoile);
            this.Controls.Add(this.zoneDeBikiniBas);
            this.Name = "FormulaireDuKrabsKrusty";
            this.Text = "Pong de Bob l\'Éponge";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormulaireDuKrabsKrusty_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormulaireDuKrabsKrusty_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.zoneDeBikiniBas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox zoneDeBikiniBas;
        private System.Windows.Forms.Timer minuteurDuMedusePatient;
        private System.Windows.Forms.Label scorePatrickEtoile;
    }
}