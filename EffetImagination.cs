using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET D'IMAGINATION - DES ARCS-EN-CIEL ET DES LICORNES MARINES PARTOUT!
    public class EffetImagination : EffetAbstrait
    {
        private int compteurMulticolore = 0;
        private Color couleurActuelle = Color.Yellow;

        public override string NomEffet => "IMAGINATION! 🌈";

        public EffetImagination() : base(15000) // 15 secondes d'imagination débordante!
        {
        }

        public override void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            // L'IMAGINATION CHANGE LA COULEUR ET FAIT ZIGZAGUER LA BALLE!
            compteurMulticolore += 10;
            if (compteurMulticolore > 360) compteurMulticolore = 0;

            // Arc-en-ciel magique!
            couleurActuelle = CouleurArcEnCiel(compteurMulticolore);

            // Zigzags aléatoires pour la direction - COMME MES PENSÉES QUAND JE SUIS EXCITÉ!
            if (aléatoireMarinBonus.Next(100) < 10)
            {
                vitesseY += aléatoireMarinBonus.Next(-2, 3);
                // Limiter pour ne pas devenir fou
                vitesseY = Math.Clamp(vitesseY, -10, 10);
            }
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse)
        {
            // TRAÎNÉE MULTICOLORE DERRIÈRE LA BALLE - COMME UN ARC-EN-CIEL MARIN!
            for (int i = 1; i <= 5; i++)
            {
                using (GraphicsPath chemin = new GraphicsPath())
                {
                    Rectangle zone = new Rectangle(
                        balleDeMeduseJoyeuse.X - i * 3,
                        balleDeMeduseJoyeuse.Y - i * 3,
                        balleDeMeduseJoyeuse.Width + i * 6,
                        balleDeMeduseJoyeuse.Height + i * 6);

                    chemin.AddEllipse(zone);

                    using (PathGradientBrush pinceau = new PathGradientBrush(chemin))
                    {
                        Color couleurDecalée = CouleurArcEnCiel((compteurMulticolore + i * 30) % 360);
                        pinceau.CenterColor = Color.FromArgb(100 - i * 15, couleurDecalée);
                        pinceau.SurroundColors = new Color[] { Color.FromArgb(0, couleurDecalée) };

                        dessinateurPatrick.FillEllipse(pinceau, zone);
                    }
                }
            }

            // Remplacer la couleur normale de la balle
            using (SolidBrush pinceau = new SolidBrush(couleurActuelle))
            {
                dessinateurPatrick.FillEllipse(pinceau, balleDeMeduseJoyeuse);
            }

            // Reflet sur la balle - COMME MON SOURIRE ÉCLATANT!
            using (SolidBrush pinceau = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
            {
                dessinateurPatrick.FillEllipse(pinceau,
                    balleDeMeduseJoyeuse.X + 5,
                    balleDeMeduseJoyeuse.Y + 3,
                    7, 5);
            }
        }

        // Méthode pour générer des couleurs arc-en-ciel - COMME L'IMAGINATION DE BOB ET PATRICK!
        private Color CouleurArcEnCiel(int angle)
        {
            int hi = (int)(angle / 60) % 6;
            float f = angle / 60f - (int)(angle / 60);
            float v = 1.0f;  // Valeur
            float s = 0.8f;  // Saturation
            float p = v * (1 - s);
            float q = v * (1 - f * s);
            float t = v * (1 - (1 - f) * s);

            float r, g, b;

            switch (hi)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                default: r = v; g = p; b = q; break;
            }

            return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
        }
    }
}