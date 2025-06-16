using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // BONUS AUTO ÉCOLE - COMME BOB QUI ESSAIE D'APPRENDRE À CONDUIRE!
    public class BonusAutoEcole : BonusAbstrait
    {
        private int compteurVolant = 0;
        private readonly Color couleurVoiture = Color.Red;

        public override string NomBonus => "AUTO ÉCOLE!";

        public BonusAutoEcole(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            dureeEffetStandard = 12000; // 12 secondes de conduite folle!
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            compteurVolant = (compteurVolant + 5) % 360;

            // Corps de la voiture (cercle rouge)
            using (SolidBrush pinceauVoiture = new SolidBrush(couleurVoiture))
            {
                dessinateurPatrick.FillEllipse(pinceauVoiture, Position);
            }

            // Lettre L (learner/apprenti) sur la voiture
            using (Font police = new Font("Arial", Position.Width / 2, FontStyle.Bold))
            using (SolidBrush pinceauTexte = new SolidBrush(Color.White))
            {
                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                dessinateurPatrick.DrawString("L", police, pinceauTexte,
                    Position.X + Position.Width / 2,
                    Position.Y + Position.Height / 2,
                    format);
            }

            // Roues de la voiture
            int tailleRoue = Position.Width / 5;
            using (SolidBrush pinceauRoues = new SolidBrush(Color.Black))
            {
                // Quatre roues
                dessinateurPatrick.FillEllipse(pinceauRoues,
                    Position.X - tailleRoue / 2,
                    Position.Y - tailleRoue / 2,
                    tailleRoue, tailleRoue);

                dessinateurPatrick.FillEllipse(pinceauRoues,
                    Position.X + Position.Width - tailleRoue / 2,
                    Position.Y - tailleRoue / 2,
                    tailleRoue, tailleRoue);

                dessinateurPatrick.FillEllipse(pinceauRoues,
                    Position.X - tailleRoue / 2,
                    Position.Y + Position.Height - tailleRoue / 2,
                    tailleRoue, tailleRoue);

                dessinateurPatrick.FillEllipse(pinceauRoues,
                    Position.X + Position.Width - tailleRoue / 2,
                    Position.Y + Position.Height - tailleRoue / 2,
                    tailleRoue, tailleRoue);
            }

            // Un peu de fumée derrière la voiture
            using (Pen styloFumee = new Pen(Color.FromArgb(150, 100, 100, 100), 3))
            {
                styloFumee.DashStyle = DashStyle.Dot;

                for (int i = 1; i <= 3; i++)
                {
                    int decalageY = (int)(Math.Sin(compteurVolant * Math.PI / 180 + i) * 5);

                    dessinateurPatrick.DrawLine(styloFumee,
                        Position.X - 5,
                        Position.Y + Position.Height / 2 + decalageY,
                        Position.X - 15 - i * 5,
                        Position.Y + Position.Height / 2 + decalageY);
                }
            }
        }

        public override IEffet Collecter()
        {
            EstActif = false;
            return new EffetAutoEcole();
        }
    }

}