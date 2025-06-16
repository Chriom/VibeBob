using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // BONUS IMAGINATION - UN ARC-EN-CIEL D'IMAGINATION!
    public class BonusImagination : BonusAbstrait
    {
        private int compteurRotation = 0;
        private readonly Color[] couleursArcEnCiel = {
            Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet
        };

        public override string NomBonus => "IMAGINATION!";

        public BonusImagination(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            dureeEffetStandard = 15000; // 15 secondes d'imagination!
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            compteurRotation = (compteurRotation + 2) % 360;

            // Dessiner un arc-en-ciel tournant
            for (int i = 0; i < couleursArcEnCiel.Length; i++)
            {
                using (SolidBrush pinceauCercle = new SolidBrush(Color.FromArgb(200, couleursArcEnCiel[i])))
                {
                    int rayon = Position.Width + i * 2;
                    Rectangle cercle = new Rectangle(
                        Position.X - i * 2,
                        Position.Y - i * 2,
                        rayon, rayon);

                    dessinateurPatrick.FillEllipse(pinceauCercle, cercle);
                }
            }

            // Dessiner un nuage au centre
            using (GraphicsPath cheminNuage = new GraphicsPath())
            {
                cheminNuage.AddEllipse(
                    Position.X + Position.Width / 4,
                    Position.Y + Position.Height / 4,
                    Position.Width / 2,
                    Position.Height / 2);

                using (PathGradientBrush pinceauNuage = new PathGradientBrush(cheminNuage))
                {
                    pinceauNuage.CenterColor = Color.White;
                    pinceauNuage.SurroundColors = new Color[] { Color.FromArgb(100, 200, 200, 255) };

                    dessinateurPatrick.FillEllipse(pinceauNuage,
                        Position.X + Position.Width / 4,
                        Position.Y + Position.Height / 4,
                        Position.Width / 2,
                        Position.Height / 2);
                }
            }

            // Dessiner le mot "IMAGINATION" au centre
            using (Font police = new Font("Arial", 8, FontStyle.Bold))
            using (SolidBrush pinceauTexte = new SolidBrush(Color.DeepPink))
            {
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                dessinateurPatrick.DrawString("IMAGINATION", police, pinceauTexte,
                    Position.X + Position.Width / 2,
                    Position.Y + Position.Height / 2, format);
            }
        }

        public override IEffet Collecter()
        {
            EstActif = false;
            return new EffetImagination();
        }
    }
}