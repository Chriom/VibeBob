using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET RAQUETTE MÉDUSE - COMME MA COLLECTION DE MÉDUSES DANSANTES !
    public class EffetRaquetteMeduse : RaquetteEffetAbstrait
    {
        private int compteurPulsation = 0;

        public override string NomEffet => "RAQUETTE MÉDUSE !";

        public EffetRaquetteMeduse(bool estPourRaquetteGauche) : base(500, estPourRaquetteGauche) // 8.33 secondes
        {
        }

        public override void AppliquerEffet(ref Rectangle raquette, ref int vitesseRaquette)
        {
            // Les méduses sont très agiles - COMME MOI QUAND JE CHASSE LES MÉDUSES !
            vitesseRaquette = 15;

            // Animation de pulsation
            compteurPulsation = (compteurPulsation + 1) % 60;

            // Faire pulser légèrement la raquette pour imiter le mouvement de la méduse
            int pulsation = (int)(Math.Sin(compteurPulsation * Math.PI / 30) * 5);

            // Ajuster la hauteur en fonction de la pulsation
            int hauteurOrigine = 100; // Hauteur normale de la raquette
            int nouvelleHauteur = hauteurOrigine + pulsation;

            // Centrer la pulsation
            int decalageY = (nouvelleHauteur - hauteurOrigine) / 2;

            raquette.Height = nouvelleHauteur;
            raquette.Y -= decalageY;
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle raquette)
        {
            // Dessiner le corps transparent de la méduse
            using (GraphicsPath cheminMeduse = new GraphicsPath())
            {
                cheminMeduse.AddRectangle(raquette);

                using (PathGradientBrush pinceauMeduse = new PathGradientBrush(cheminMeduse))
                {
                    // Couleur rose transparent pour la méduse
                    pinceauMeduse.CenterColor = Color.FromArgb(200, 255, 100, 255);
                    pinceauMeduse.SurroundColors = new Color[] { Color.FromArgb(150, 150, 0, 150) };

                    dessinateurPatrick.FillRectangle(pinceauMeduse, raquette);
                }

                // Tentacules qui pendent en bas de la raquette méduse
                for (int i = 0; i < 8; i++)
                {
                    int x = raquette.X + (i + 1) * raquette.Width / 9;
                    int y = raquette.Y + raquette.Height;
                    int longueur = 10 + aleatoireMarinEffet.Next(15);

                    double ondulation = Math.Sin((compteurPulsation + i * 10) * Math.PI / 30) * 3;

                    // Dessiner une tentacule ondulante
                    Point[] pointsTentacule = new Point[4];
                    pointsTentacule[0] = new Point(x, y);
                    pointsTentacule[1] = new Point(x + (int)ondulation, y + longueur / 3);
                    pointsTentacule[2] = new Point(x - (int)ondulation, y + 2 * longueur / 3);
                    pointsTentacule[3] = new Point(x, y + longueur);

                    using (Pen pinceauTentacule = new Pen(Color.FromArgb(200, 255, 100, 255), 2))
                    {
                        dessinateurPatrick.DrawCurve(pinceauTentacule, pointsTentacule, 0.5f);
                    }
                }
            }

            // Petites bulles autour de la méduse
            for (int i = 0; i < 5; i++)
            {
                int x = raquette.X + aleatoireMarinEffet.Next(-10, raquette.Width + 10);
                int y = raquette.Y + aleatoireMarinEffet.Next(-10, raquette.Height + 10);
                int tailleBulle = 2 + aleatoireMarinEffet.Next(4);

                using (SolidBrush pinceauBulle = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
                {
                    dessinateurPatrick.FillEllipse(pinceauBulle, x, y, tailleBulle, tailleBulle);
                }
            }
        }
    }
}