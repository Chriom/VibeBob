using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // BONUS POOOGNON - COMME MONSIEUR KRABS QUI ADORE L'ARGENT! 💰
    public class BonusPooognon : BonusAbstrait
    {
        private float rotationArgent = 0;

        public override string NomBonus => "POOOGNNNNOOONN!";

        public BonusPooognon(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            dureeEffetStandard = 7000; // 7 secondes de vitesse DOLLAR DOLLAR!
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            // Faire tourner le dollar - COMME MONSIEUR KRABS QUI COMPTE SES SOUS!
            rotationArgent = (rotationArgent + 3) % 360;

            // Dessin du fond doré - RICHESSE RICHESSE!
            using (GraphicsPath cheminArgent = new GraphicsPath())
            {
                cheminArgent.AddEllipse(Position);
                using (PathGradientBrush pinceauDollar = new PathGradientBrush(cheminArgent))
                {
                    pinceauDollar.CenterColor = Color.Gold;
                    pinceauDollar.SurroundColors = new Color[] { Color.FromArgb(150, 180, 150, 0) };

                    dessinateurPatrick.FillEllipse(pinceauDollar, Position);
                }
            }

            // Ajout du symbole dollar au milieu - COMME LES YEUX DE MONSIEUR KRABS!
            using (Font police = new Font("Arial", 16, FontStyle.Bold))
            using (SolidBrush pinceauTexte = new SolidBrush(Color.FromArgb(200, 0, 100, 0)))
            {
                // Sauvegarder la transformation actuelle
                Matrix matriceOriginale = dessinateurPatrick.Transform.Clone();

                // Effectuer une rotation autour du centre du bonus
                dessinateurPatrick.TranslateTransform(
                    Position.X + Position.Width / 2,
                    Position.Y + Position.Height / 2);
                dessinateurPatrick.RotateTransform(rotationArgent);

                // Dessiner le symbole dollar
                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                dessinateurPatrick.DrawString("$", police, pinceauTexte,
                    0, 0, format);

                // Restaurer la transformation
                dessinateurPatrick.Transform = matriceOriginale;
            }

            // Petites pièces qui brillent autour - COMME L'ENTHOUSIASME DE MONSIEUR KRABS!
            int nombrePieces = 5;
            for (int i = 0; i < nombrePieces; i++)
            {
                double angle = (rotationArgent + i * (360.0 / nombrePieces)) * Math.PI / 180;
                float x = Position.X + Position.Width / 2 + (float)Math.Cos(angle) * Position.Width * 0.7f;
                float y = Position.Y + Position.Height / 2 + (float)Math.Sin(angle) * Position.Height * 0.7f;

                using (SolidBrush pinceauPiece = new SolidBrush(Color.FromArgb(180, 255, 215, 0)))
                {
                    dessinateurPatrick.FillEllipse(pinceauPiece, x - 3, y - 3, 6, 6);
                }
            }
        }

        public override IEffet Collecter()
        {
            EstActif = false;
            return new EffetPooognon(5, 5); // Les valeurs seront remplacées dans le jeu
        }
    }

}