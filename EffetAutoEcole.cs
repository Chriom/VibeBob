using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET AUTO ÉCOLE - LA BALLE DEVIENT IMPRÉVISIBLE COMME BOB DANS UNE VOITURE!
    public class EffetAutoEcole : EffetAbstrait
    {
        private int compteurChaos = 0;

        public override string NomEffet => "AUTO ÉCOLE! 🚗";

        public EffetAutoEcole() : base(12000) // 12 secondes de conduite chaotique!
        {
        }

        public override void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            // Comportement erratique - COMME BOB QUI CONDUIT!
            compteurChaos++;

            // Changer de direction de façon aléatoire toutes les 20 frames environ
            if (compteurChaos % 20 == 0)
            {
                // 30% de chances de changer de direction X ou Y
                if (aléatoireMarinBonus.Next(100) < 30)
                {
                    vitesseX = -vitesseX;
                }

                if (aléatoireMarinBonus.Next(100) < 30)
                {
                    vitesseY = -vitesseY;
                }

                // Petits ajustements aléatoires
                vitesseY += aléatoireMarinBonus.Next(-1, 2);

                // Limites pour éviter que ça devienne trop fou
                vitesseY = Math.Clamp(vitesseY, -12, 12);
            }
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse)
        {
            // Dessiner une balle avec des roues et un volant - COMME UNE VOITURE D'AUTO-ÉCOLE!

            // Corps de la voiture (balle)
            using (SolidBrush pinceauVoiture = new SolidBrush(Color.Red))
            {
                dessinateurPatrick.FillEllipse(pinceauVoiture, balleDeMeduseJoyeuse);
            }

            // Roues
            int tailleRoue = balleDeMeduseJoyeuse.Width / 4;

            using (SolidBrush pinceauRoues = new SolidBrush(Color.Black))
            {
                // Roue avant
                dessinateurPatrick.FillEllipse(pinceauRoues,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width - tailleRoue / 2,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height - tailleRoue,
                    tailleRoue, tailleRoue);

                // Roue arrière
                dessinateurPatrick.FillEllipse(pinceauRoues,
                    balleDeMeduseJoyeuse.X - tailleRoue / 2,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height - tailleRoue,
                    tailleRoue, tailleRoue);
            }

            // Lettre "L" de learner (apprenti) sur la voiture
            using (Font police = new Font("Arial", balleDeMeduseJoyeuse.Width / 2, FontStyle.Bold))
            using (SolidBrush pinceauTexte = new SolidBrush(Color.White))
            {
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                dessinateurPatrick.DrawString("L", police, pinceauTexte,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2 - 2, format);
            }

            // Traînée de fumée aléatoire derrière la voiture
            using (Pen styloFumee = new Pen(Color.FromArgb(150, 100, 100, 100), 3))
            {
                styloFumee.DashStyle = DashStyle.Dot;

                int xDebut = balleDeMeduseJoyeuse.X;
                int yDebut = balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2;

                // Dessiner plusieurs lignes de fumée
                for (int i = 1; i <= 4; i++)
                {
                    int decalageX = i * 15;
                    int decalageY = aléatoireMarinBonus.Next(-10, 11);

                    dessinateurPatrick.DrawLine(styloFumee,
                        xDebut, yDebut,
                        xDebut - decalageX, yDebut + decalageY);
                }
            }
        }
    }
}