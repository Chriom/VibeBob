using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET ÉTOILE DE MER - LA BALLE DEVIENT INVINCIBLE COMME STARFISH-MAN!
    public class EffetEtoileDeMer : EffetAbstrait
    {
        private readonly Color couleurEtoile = Color.FromArgb(255, 255, 150, 0); // Orange vif
        private int rayonRotation = 0;

        public override string NomEffet => "ÉTOILE DE MER! ⭐";

        public EffetEtoileDeMer() : base(8000) // 8 secondes d'invincibilité d'étoile!
        {
        }

        public override void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            // L'étoile ne change pas vraiment la vitesse mais affecte le rendu
            rayonRotation += 5; // Rotation de l'étoile
            if (rayonRotation >= 360) rayonRotation = 0;

            // Légère accélération - LES ÉTOILES SONT PUISSANTES!
            if (Math.Abs(vitesseX) < 10)
            {
                vitesseX = vitesseX < 0 ? vitesseX - 1 : vitesseX + 1;
            }
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse)
        {
            int centreX = balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2;
            int centreY = balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2;
            float rayon = balleDeMeduseJoyeuse.Width / 2;

            // Dessiner une étoile brillante - COMME PATRICK QUAND IL EST UN SUPER-HÉROS!

            // Points de l'étoile à 5 branches
            PointF[] pointsEtoile = new PointF[10];

            for (int i = 0; i < 10; i++)
            {
                // Alterner entre rayon externe et interne
                float r = (i % 2 == 0) ? rayon : rayon * 0.4f;
                float angle = (float)(Math.PI / 5 * i + Math.PI / 180 * rayonRotation); // Rotation

                pointsEtoile[i] = new PointF(
                    centreX + r * (float)Math.Cos(angle),
                    centreY + r * (float)Math.Sin(angle));
            }

            // Remplir l'étoile avec dégradé
            using (PathGradientBrush pinceauEtoile = new PathGradientBrush(pointsEtoile))
            {
                pinceauEtoile.CenterColor = Color.Yellow;
                pinceauEtoile.SurroundColors = new Color[] { couleurEtoile };

                dessinateurPatrick.FillPolygon(pinceauEtoile, pointsEtoile);
            }

            // Contour de l'étoile
            using (Pen styloContour = new Pen(Color.FromArgb(200, 255, 200, 0), 1))
            {
                dessinateurPatrick.DrawPolygon(styloContour, pointsEtoile);
            }

            // Éclat central
            using (GraphicsPath cheminEclat = new GraphicsPath())
            {
                cheminEclat.AddEllipse(
                    centreX - rayon * 0.3f,
                    centreY - rayon * 0.3f,
                    rayon * 0.6f,
                    rayon * 0.6f);

                using (PathGradientBrush pinceauEclat = new PathGradientBrush(cheminEclat))
                {
                    pinceauEclat.CenterColor = Color.White;
                    pinceauEclat.SurroundColors = new Color[] { Color.FromArgb(0, Color.White) };

                    dessinateurPatrick.FillEllipse(pinceauEclat,
                        centreX - rayon * 0.3f,
                        centreY - rayon * 0.3f,
                        rayon * 0.6f,
                        rayon * 0.6f);
                }
            }
        }
    }
}