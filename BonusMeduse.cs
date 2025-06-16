using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // BONUS MÉDUSE - COMME LES MÉDUSES QUE JE CHASSE À JELLYFISH FIELDS!
    public class BonusMeduse : BonusAbstrait
    {
        private int compteurPulsation = 0;
        private readonly Color couleurMeduse = Color.FromArgb(255, 210, 105, 255); // Rose/violet méduse

        public override string NomBonus => "MÉDUSE!";

        public BonusMeduse(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            dureeEffetStandard = 10000; // 10 secondes d'effet méduse!
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            // Faire pulser la méduse - COMME QUAND ELLES NAGENT!
            compteurPulsation = (compteurPulsation + 3) % 360;
            float facteurPulsation = 1.0f + (float)Math.Sin(compteurPulsation * Math.PI / 180) * 0.1f;

            // Cloche de la méduse
            using (GraphicsPath cheminMeduse = new GraphicsPath())
            {
                Rectangle zoneMeduse = new Rectangle(
                    Position.X, Position.Y,
                    (int)(Position.Width * facteurPulsation),
                    (int)(Position.Height * facteurPulsation));

                cheminMeduse.AddEllipse(zoneMeduse);

                using (PathGradientBrush pinceauMeduse = new PathGradientBrush(cheminMeduse))
                {
                    pinceauMeduse.CenterColor = Color.FromArgb(230, couleurMeduse);
                    pinceauMeduse.SurroundColors = new Color[] {
                        Color.FromArgb(180, couleurMeduse.R - 30, couleurMeduse.G - 30, couleurMeduse.B)
                    };

                    dessinateurPatrick.FillEllipse(pinceauMeduse, zoneMeduse);
                }

                // Reflet sur la méduse
                using (GraphicsPath cheminReflet = new GraphicsPath())
                {
                    Rectangle zoneReflet = new Rectangle(
                        Position.X + Position.Width / 4,
                        Position.Y + Position.Height / 4,
                        Position.Width / 2,
                        Position.Height / 3);

                    cheminReflet.AddEllipse(zoneReflet);

                    using (PathGradientBrush pinceauReflet = new PathGradientBrush(cheminReflet))
                    {
                        pinceauReflet.CenterColor = Color.FromArgb(100, 255, 255, 255);
                        pinceauReflet.SurroundColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };

                        dessinateurPatrick.FillEllipse(pinceauReflet, zoneReflet);
                    }
                }
            }

            // Tentacules ondulantes
            int centreX = Position.X + Position.Width / 2;
            int basY = Position.Y + Position.Height;

            using (Pen styleTentacule = new Pen(couleurMeduse, 2))
            {
                // Dessiner plusieurs tentacules ondulantes
                for (int i = -2; i <= 2; i++)
                {
                    int decalageX = i * Position.Width / 6;

                    // Points de contrôle pour une courbe ondulante
                    Point[] points = new Point[4];
                    points[0] = new Point(centreX + decalageX, basY);

                    // Calculer les autres points avec un effet d'ondulation
                    for (int j = 1; j < points.Length; j++)
                    {
                        double ondulation = Math.Sin(compteurPulsation * Math.PI / 180 + i + j) * 5;
                        points[j] = new Point(
                            centreX + decalageX + (int)ondulation,
                            basY + j * Position.Height / 4);
                    }

                    dessinateurPatrick.DrawCurve(styleTentacule, points);
                }
            }
        }

        public override IEffet Collecter()
        {
            EstActif = false;
            return new EffetMeduse();
        }
    }

    
}