using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET POOOGNON - LA BALLE DEVIENT SUPER RAPIDE! COMME QUAND MR KRABS COURT APRÈS DE L'ARGENT!
    public class EffetPooognon : EffetAbstrait
    {
        private readonly int vitesseOriginaleX;
        private readonly int vitesseOriginaleY;

        public override string NomEffet => "POOOGNNNNNOOOONN! 💰";

        public EffetPooognon(int vx, int vy) : base(7000) // 7 secondes de POOOGNON!
        {
            vitesseOriginaleX = vx;
            vitesseOriginaleY = vy;
        }

        public override void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            // SUPER ACCÉLÉRATION - COMME MR. KRABS QUI COURT VERS UN SOU!
            float facteurVitesse = 1.8f;

            // Garder le signe mais augmenter la magnitude
            int directionX = Math.Sign(vitesseX);
            int directionY = Math.Sign(vitesseY);

            vitesseX = (int)(Math.Abs(vitesseOriginaleX) * facteurVitesse) * directionX;
            vitesseY = (int)(Math.Abs(vitesseOriginaleY) * facteurVitesse) * directionY;
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse)
        {
            // Dessiner des pièces d'or autour de la balle
            using (SolidBrush pinceauOr = new SolidBrush(Color.Gold))
            using (Pen contourPiece = new Pen(Color.FromArgb(200, 205, 133, 0), 1))
            {
                // Petites pièces qui suivent la balle
                for (int i = 0; i < 5; i++)
                {
                    int decalage = i * 6 + 5;
                    int taillePiece = 10;

                    // Position aléatoire autour de la balle
                    double angle = aléatoireMarinBonus.NextDouble() * Math.PI * 2;
                    int pieceX = (int)(balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2 + Math.Cos(angle) * decalage - taillePiece / 2);
                    int pieceY = (int)(balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2 + Math.Sin(angle) * decalage - taillePiece / 2);

                    // Dessiner la pièce d'or
                    dessinateurPatrick.FillEllipse(pinceauOr, pieceX, pieceY, taillePiece, taillePiece);
                    dessinateurPatrick.DrawEllipse(contourPiece, pieceX, pieceY, taillePiece, taillePiece);

                    // Dessiner le $ sur la pièce
                    using (Font police = new Font("Arial", 7, FontStyle.Bold))
                    using (SolidBrush pinceauTexte = new SolidBrush(Color.FromArgb(200, 139, 69, 19)))
                    {
                        dessinateurPatrick.DrawString("$", police, pinceauTexte, pieceX + 2, pieceY);
                    }
                }
            }

            // Colorer la balle en vert dollar - COMME L'ARGENT QUE MR. KRABS ADORE!
            using (SolidBrush pinceauDollar = new SolidBrush(Color.FromArgb(255, 17, 153, 17)))
            {
                dessinateurPatrick.FillEllipse(pinceauDollar, balleDeMeduseJoyeuse);
            }

            // Dessiner le symbole $ sur la balle
            using (Font police = new Font("Arial", balleDeMeduseJoyeuse.Width / 2, FontStyle.Bold))
            using (SolidBrush pinceauTexte = new SolidBrush(Color.FromArgb(230, 255, 215, 0)))
            {
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                dessinateurPatrick.DrawString("$", police, pinceauTexte,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2, format);
            }
        }
    }
}