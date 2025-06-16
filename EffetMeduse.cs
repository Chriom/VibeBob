using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET MÉDUSE - LA BALLE DEVIENT IMPRÉVISIBLE ET PEUT PIQUER LES RAQUETTES!
    public class EffetMeduse : EffetAbstrait
    {
        private readonly Color couleurMeduse = Color.FromArgb(255, 210, 105, 255); // Rose/violet méduse

        public override string NomEffet => "MÉDUSE! 🎐";

        public EffetMeduse() : base(10000) // 10 secondes d'effet méduse!
        {
        }

        public override void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            // Mouvement ondulant comme une méduse - COMME QUAND JE DANSE!
            int phaseOndulation = (int)(TempsRestant / 200.0) % 6; // Cycle d'ondulation

            if (phaseOndulation == 0)
            {
                // Petit ajustement aléatoire à la trajectoire
                vitesseY += aléatoireMarinBonus.Next(-2, 3);

                // Limites raisonnables
                vitesseY = Math.Clamp(vitesseY, -10, 10);
            }
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse)
        {
            // Dessiner une méduse élégante - AUSSI BELLE QUE CELLES QUE JE CHASSE!

            // Corps semi-transparent de la méduse
            using (GraphicsPath cheminMeduse = new GraphicsPath())
            {
                // Créer un dôme arrondi pour la méduse
                cheminMeduse.AddEllipse(
                    balleDeMeduseJoyeuse.X,
                    balleDeMeduseJoyeuse.Y,
                    balleDeMeduseJoyeuse.Width,
                    balleDeMeduseJoyeuse.Height);

                using (PathGradientBrush pinceauMeduse = new PathGradientBrush(cheminMeduse))
                {
                    // Créer un gradient doux pour la méduse
                    pinceauMeduse.CenterColor = Color.FromArgb(230, couleurMeduse);
                    pinceauMeduse.SurroundColors = new Color[] {
                        Color.FromArgb(180, couleurMeduse.R - 30, couleurMeduse.G - 30, couleurMeduse.B)
                    };

                    dessinateurPatrick.FillEllipse(pinceauMeduse, balleDeMeduseJoyeuse);
                }
            }

            // Tentacules ondulantes - COMME LES CHEVEUX DE MÉDUSE!
            using (Pen styloTentacule = new Pen(Color.FromArgb(200, couleurMeduse), 2))
            {
                int centreX = balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2;
                int basY = balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height;

                // Phase pour l'ondulation
                double phase = TempsRestant / 500.0;

                // Dessiner plusieurs tentacules
                for (int i = -3; i <= 3; i++)
                {
                    int decalageX = i * balleDeMeduseJoyeuse.Width / 8;

                    // Points de contrôle pour la courbe ondulante
                    Point[] points = new Point[4];
                    points[0] = new Point(centreX + decalageX, basY);

                    // Tentacules ondulantes
                    for (int j = 1; j < points.Length; j++)
                    {
                        double ondulation = Math.Sin(phase + i + j) * 5;
                        points[j] = new Point(
                            centreX + decalageX + (int)ondulation,
                            basY + j * balleDeMeduseJoyeuse.Height / 3);
                    }

                    dessinateurPatrick.DrawCurve(styloTentacule, points);
                }
            }

            // Reflet lumineux sur la méduse
            using (SolidBrush pinceauReflet = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
            {
                dessinateurPatrick.FillEllipse(pinceauReflet,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 3,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 3,
                    balleDeMeduseJoyeuse.Width / 4,
                    balleDeMeduseJoyeuse.Height / 6);
            }
        }
    }
}