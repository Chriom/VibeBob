using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET RAQUETTE GÉANTE - COMME QUAND J'UTILISE LA LOUPE DE SANDY !
    public class EffetRaquetteGeante : RaquetteEffetAbstrait
    {
        private int hauteurEcran;

        public override string NomEffet => "RAQUETTE GÉANTE !";

        public EffetRaquetteGeante(bool estPourRaquetteGauche) : base(450, estPourRaquetteGauche) // 7.5 secondes
        {
            // On va demander la hauteur de l'écran au moment de l'application de l'effet
        }

        public override void AppliquerEffet(ref Rectangle raquette, ref int vitesseRaquette)
        {
            // La raquette devient ÉNORME - COMME L'APPÉTIT DE PATRICK !
            int largeurOrigine = raquette.Width;
            int hauteurOrigine = raquette.Height;

            // Obtenir la hauteur de l'écran si nous ne l'avons pas encore
            if (hauteurEcran == 0)
            {
                // On estime la hauteur de l'écran en supposant que la raquette est initialement bien positionnée
                hauteurEcran = 600; // Valeur par défaut raisonnable

                // Si la raquette est déjà proche du bas, cela suggère que l'écran n'est pas très grand
                if (raquette.Y + raquette.Height * 2 > hauteurEcran)
                {
                    hauteurEcran = raquette.Y + raquette.Height * 3; // Estimation plus prudente
                }
            }

            // Calculer la hauteur maximale possible pour que la raquette reste sur l'écran
            int hauteurMaxPossible = hauteurEcran - 10; // Laisser une petite marge

            // Déterminer la nouvelle hauteur (au plus double, mais pas plus que la hauteur maximale)
            int nouvelleHauteur = Math.Min(hauteurOrigine * 2, hauteurMaxPossible);

            // Si la nouvelle hauteur est trop grande, on limite à 80% de l'écran
            if (nouvelleHauteur > hauteurEcran * 0.8)
            {
                nouvelleHauteur = (int)(hauteurEcran * 0.8);
            }

            // Centrer la nouvelle raquette
            int decalageY = (nouvelleHauteur - hauteurOrigine) / 2;

            // Ajuster la position Y pour que la raquette reste dans les limites
            int nouveauY = raquette.Y - decalageY;

            // S'assurer que la raquette reste entièrement visible
            if (nouveauY < 0)
            {
                nouveauY = 0;
            }
            else if (nouveauY + nouvelleHauteur > hauteurEcran)
            {
                nouveauY = hauteurEcran - nouvelleHauteur;
            }

            // Appliquer les modifications
            raquette.Height = nouvelleHauteur;
            raquette.Y = nouveauY;

            // La raquette géante est un peu plus lente - PARCE QU'ELLE EST LOURDE !
            vitesseRaquette = 7;
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle raquette)
        {
            // Dessiner la raquette géante avec un effet brillant
            using (LinearGradientBrush pinceauRaquette = new LinearGradientBrush(
                new Point(raquette.X, raquette.Y),
                new Point(raquette.X + raquette.Width, raquette.Y),
                Color.FromArgb(255, 50, 220, 50), Color.FromArgb(255, 0, 180, 0)))
            {
                dessinateurPatrick.FillRectangle(pinceauRaquette, raquette);
            }

            // Ajouter des étoiles pour montrer que c'est GÉANT !
            for (int i = 0; i < 5; i++)
            {
                int x, y;

                if (EstPourRaquetteGauche)
                {
                    x = raquette.X + raquette.Width + aleatoireMarinEffet.Next(5, 15);
                }
                else
                {
                    x = raquette.X - aleatoireMarinEffet.Next(10, 20);
                }

                y = raquette.Y + aleatoireMarinEffet.Next(raquette.Height);

                int tailleEtoile = aleatoireMarinEffet.Next(5, 10);

                // Pour faire une étoile simple
                using (SolidBrush pinceauEtoile = new SolidBrush(Color.Yellow))
                using (Pen contourEtoile = new Pen(Color.Orange, 1))
                {
                    Point[] pointsEtoile = new Point[10];
                    for (int j = 0; j < 10; j++)
                    {
                        double angle = j * Math.PI / 5;
                        double rayon = j % 2 == 0 ? tailleEtoile : tailleEtoile / 2;
                        pointsEtoile[j] = new Point(
                            x + (int)(Math.Cos(angle) * rayon),
                            y + (int)(Math.Sin(angle) * rayon)
                        );
                    }

                    dessinateurPatrick.FillPolygon(pinceauEtoile, pointsEtoile);
                    dessinateurPatrick.DrawPolygon(contourEtoile, pointsEtoile);
                }
            }
        }
    }
}