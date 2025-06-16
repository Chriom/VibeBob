using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET GARY - LA BALLE LAISSE UNE TRAÎNÉE DE BAVE D'ESCARGOT!
    public class EffetGary : EffetAbstrait
    {
        private List<Point> traineeDeBave = new List<Point>();
        private int compteurFrames = 0;
        private int directionRegardGary = 1; // Direction où Gary regarde (1 = droite, -1 = gauche)

        public override string NomEffet => "GARY! 🐌";

        public EffetGary() : base(12000) // 12 secondes d'effet escargot!
        {
        }

        public override void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            // Gary est lent mais stable
            compteurFrames++;

            // Sauvegarder la direction pour les yeux
            directionRegardGary = Math.Sign(vitesseX);

            // Enregistrer la position pour la traînée de bave
            if (compteurFrames % 3 == 0) // Toutes les 3 frames
            {
                traineeDeBave.Add(new Point(
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2));

                // Limiter la longueur de la traînée
                if (traineeDeBave.Count > 20)
                {
                    traineeDeBave.RemoveAt(0);
                }
            }

            // Stabiliser légèrement la vitesse - GARY EST STABLE ET CONFIANT!
            if (Math.Abs(vitesseY) > 6)
            {
                vitesseY = (vitesseY > 0) ? 6 : -6;
            }
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse)
        {
            // Dessiner la traînée de bave de Gary
            if (traineeDeBave.Count > 1)
            {
                using (Pen styloTrainee = new Pen(Color.FromArgb(150, 200, 255, 200), 5))
                {
                    styloTrainee.StartCap = LineCap.Round;
                    styloTrainee.EndCap = LineCap.Round;

                    for (int i = 1; i < traineeDeBave.Count; i++)
                    {
                        // Faire varier l'opacité selon l'âge de la traînée
                        float opacite = (float)i / traineeDeBave.Count;
                        styloTrainee.Color = Color.FromArgb((int)(150 * opacite), 200, 255, 200);
                        styloTrainee.Width = 5 * opacite + 1;

                        dessinateurPatrick.DrawLine(styloTrainee, traineeDeBave[i - 1], traineeDeBave[i]);
                    }
                }
            }

            // Dessiner Gary (un escargot marin)

            // Corps de Gary (coquille turquoise)
            using (SolidBrush pinceauCoquille = new SolidBrush(Color.FromArgb(255, 64, 224, 208))) // Turquoise
            {
                dessinateurPatrick.FillEllipse(pinceauCoquille, balleDeMeduseJoyeuse);
            }

            // Motif en spirale sur la coquille
            using (Pen styloCoquille = new Pen(Color.FromArgb(200, 0, 128, 128), 2))
            {
                int centreX = balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2;
                int centreY = balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2;
                float rayon = balleDeMeduseJoyeuse.Width / 4;

                for (float angle = 0; angle < Math.PI * 4; angle += 0.1f)
                {
                    float r = rayon * (1 - angle / (float)(Math.PI * 4));
                    float x = centreX + r * (float)Math.Cos(angle);
                    float y = centreY + r * (float)Math.Sin(angle);

                    dessinateurPatrick.FillEllipse(Brushes.DarkSlateGray, x - 1, y - 1, 2, 2);
                }
            }

            // Yeux de Gary
            int tailleOeil = balleDeMeduseJoyeuse.Width / 5;

            // Utiliser la direction sauvegardée - LES YEUX DE GARY SUIVENT LE MOUVEMENT!
            int posXYeux = balleDeMeduseJoyeuse.X + (directionRegardGary > 0 ?
                balleDeMeduseJoyeuse.Width - tailleOeil : 0);

            // Tiges des yeux
            using (SolidBrush pinceauTiges = new SolidBrush(Color.FromArgb(230, 100, 200, 200)))
            {
                dessinateurPatrick.FillEllipse(pinceauTiges,
                    posXYeux,
                    balleDeMeduseJoyeuse.Y - tailleOeil,
                    tailleOeil,
                    tailleOeil * 2);

                dessinateurPatrick.FillEllipse(pinceauTiges,
                    posXYeux,
                    balleDeMeduseJoyeuse.Y - tailleOeil * 1.5f,
                    tailleOeil,
                    tailleOeil * 2);
            }

            // Yeux
            using (SolidBrush pinceauYeux = new SolidBrush(Color.White))
            {
                dessinateurPatrick.FillEllipse(pinceauYeux,
                    posXYeux,
                    balleDeMeduseJoyeuse.Y - tailleOeil * 1.5f,
                    tailleOeil,
                    tailleOeil);

                dessinateurPatrick.FillEllipse(pinceauYeux,
                    posXYeux,
                    balleDeMeduseJoyeuse.Y - tailleOeil * 0.7f,
                    tailleOeil,
                    tailleOeil);
            }

            // Pupilles
            using (SolidBrush pinceauPupilles = new SolidBrush(Color.Black))
            {
                dessinateurPatrick.FillEllipse(pinceauPupilles,
                    posXYeux + tailleOeil / 2 - 2,
                    balleDeMeduseJoyeuse.Y - tailleOeil * 1.25f,
                    4,
                    4);

                dessinateurPatrick.FillEllipse(pinceauPupilles,
                    posXYeux + tailleOeil / 2 - 2,
                    balleDeMeduseJoyeuse.Y - tailleOeil * 0.45f,
                    4,
                    4);
            }
        }
    }
}