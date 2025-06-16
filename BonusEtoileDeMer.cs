using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // BONUS ÉTOILE DE MER - COMME UNE SUPER ÉTOILE DE PATRICK!
    public class BonusEtoileDeMer : BonusAbstrait
    {
        private float rotationEtoile = 0;
        private readonly Color couleurEtoile = Color.FromArgb(255, 255, 150, 0); // Orange vif

        public override string NomBonus => "ÉTOILE DE MER!";

        public BonusEtoileDeMer(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            dureeEffetStandard = 8000; // 8 secondes de pouvoir d'étoile!
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            // L'étoile tourne - COMME PATRICK QUI TOURNE SUR LUI-MÊME HIHI!
            rotationEtoile = (rotationEtoile + 2) % 360;

            int centreX = Position.X + Position.Width / 2;
            int centreY = Position.Y + Position.Height / 2;
            float rayon = Position.Width / 2;

            // Points de l'étoile à 5 branches
            PointF[] pointsEtoile = new PointF[10];

            for (int i = 0; i < 10; i++)
            {
                // Alterner entre rayon externe et interne
                float r = (i % 2 == 0) ? rayon : rayon * 0.4f;
                float angle = (float)(Math.PI / 5 * i + Math.PI / 180 * rotationEtoile);

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

        public override IEffet Collecter()
        {
            EstActif = false;
            return new EffetEtoileDeMer();
        }
    }

}