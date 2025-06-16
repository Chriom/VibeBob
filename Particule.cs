using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // CLASSE POUR LES PARTICULES ÉCLABOUSSANTES - SPLISH SPLASH!
    public class Particule
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VitesseX { get; set; }
        public float VitesseY { get; set; }
        public Color Couleur { get; set; }
        public float Taille { get; set; }
        public int Duree_De_Vie { get; set; }

        public Particule(float x, float y, float vx, float vy, Color couleur, float taille, int duree)
        {
            X = x;
            Y = y;
            VitesseX = vx;
            VitesseY = vy;
            Couleur = couleur;
            Taille = taille;
            Duree_De_Vie = duree;
        }

        public void Mettre_A_Jour()
        {
            X += VitesseX;
            Y += VitesseY;
            VitesseY += 0.1f; // Gravité légère - COMME MES PENSÉES QUI TOMBENT VERS MON ESTOMAC!
            Duree_De_Vie--; // LA VIE EST COURTE COMME UN PÂTÉ DE CRABE!
        }

        public void Dessiner(Graphics g)
        {
            // Plus c'est vieux, plus c'est transparent - COMME LES FANTAISIES DE SQUIDWARD!
            int alpha = Math.Min(255, Duree_De_Vie * 8);
            using (SolidBrush pinceau = new SolidBrush(Color.FromArgb(alpha, Couleur)))
            {
                g.FillEllipse(pinceau, X - Taille / 2, Y - Taille / 2, Taille, Taille);
            }
        }
    }
}