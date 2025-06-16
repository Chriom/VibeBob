using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // CLASSE POUR LES MÉDUSES DANSANTES - ELLES SONT SI JOLIES!
    public class Meduse
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Taille { get; set; }
        public Color Couleur { get; set; }
        public float VitesseX { get; set; }
        public float VitesseY { get; set; }

        private Random aléatoireMeduse = new Random();

        public Meduse(float x, float y, float taille, Color couleur)
        {
            X = x;
            Y = y;
            Taille = taille;
            Couleur = couleur;
            VitesseX = (float)(aléatoireMeduse.NextDouble() * 2 - 1);
            VitesseY = (float)(aléatoireMeduse.NextDouble() * 2 - 1);
        }

        public void Deplacer(int largeurMax, int hauteurMax)
        {
            X += VitesseX;
            Y += VitesseY;

            // Rebonds sur les bords
            if (X < 0 || X > largeurMax)
            {
                VitesseX = -VitesseX;
                X = X < 0 ? 0 : largeurMax;
            }

            if (Y < 0 || Y > hauteurMax)
            {
                VitesseY = -VitesseY;
                Y = Y < 0 ? 0 : hauteurMax;
            }

            // Mouvement aléatoire occasionnel - COMME MOI QUAND JE CHASSE LES MÉDUSES!
            if (aléatoireMeduse.Next(100) < 5)
            {
                VitesseX += (float)(aléatoireMeduse.NextDouble() * 0.4 - 0.2);
                VitesseY += (float)(aléatoireMeduse.NextDouble() * 0.4 - 0.2);

                // Limite de vitesse - PAS PLUS VITE QUE MOI QUAND JE VAIS AU KRUSTY KRAB!
                VitesseX = Math.Max(-1.5f, Math.Min(1.5f, VitesseX));
                VitesseY = Math.Max(-1.5f, Math.Min(1.5f, VitesseY));
            }
        }

        public void Dessiner(Graphics g)
        {
            // Corps de la méduse - TOUT ROND ET DODU!
            using (SolidBrush pinceau = new SolidBrush(Couleur))
            {
                g.FillEllipse(pinceau, X - Taille / 2, Y - Taille / 2, Taille, Taille * 0.8f);
            }

            // Tentacules de la méduse - COMME LES CHEVEUX DE SANDY MAIS SOUS L'EAU!
            using (Pen crayon = new Pen(Couleur, 2))
            {
                for (int i = 0; i < 5; i++)
                {
                    float decalage = (i - 2) * Taille / 6;
                    g.DrawLine(crayon,
                        X + decalage, Y + Taille * 0.3f,
                        X + decalage + (float)Math.Sin(Y / 10 + i) * 3, Y + Taille * 0.8f);
                }
            }
        }
    }
}