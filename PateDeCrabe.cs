using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // PÂTÉ DE CRABE - OH JE SUIS PRÊT ! DÉLICIEUX MAIS SUPER MÉGA GRAISSEUX ! LA BALLE VA GLISSER COMME SUR LE SOL DU KRUSTY KRAB !
    public class PateDeCrabe : ObstacleAbstrait
    {
        // Durée de l'effet graisseux sur la balle - COMME MA SPATULE APRÈS UNE JOURNÉE DE FRITURE !
        private int dureeEffetGraisseux = 180; // 3 secondes à 60fps - LE TEMPS DE DIRE "JE SUIS PRÊÊÊT !"


        public PateDeCrabe(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            // Le pâté est ÉNORME et DÉLICIEUX comme ceux que je prépare pour M. KRABS !
            int taillePateMagique = aléatoireMarinObstacle.Next(40, 60);
            Position = new Rectangle(
                Position.X,
                Position.Y,
                taillePateMagique,
                taillePateMagique / 2); // Forme ovale - COMME LA FORME DE GARY !
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            // Dessiner le pâté de crabe SUPER DÉLICIEUX - MEILLEUR QUE TOUT CE QUE FAIT CARLO AU SEAU À CHÂTAIGNES !
            using (GraphicsPath chemin = new GraphicsPath())
            {
                chemin.AddEllipse(Position);

                // Dégradé pour le pâté - LES COULEURS SECRÈTES DE LA FORMULE !
                using (PathGradientBrush pinceauPate = new PathGradientBrush(chemin))
                {
                    pinceauPate.CenterColor = Color.FromArgb(230, 240, 150, 50); // Marron-orange au centre - COMME UN PÂTÉ FRAÎCHEMENT GRILLÉ !
                    pinceauPate.SurroundColors = new Color[] { Color.FromArgb(230, 190, 120, 30) }; // Plus foncé sur les bords - COMME QUAND JE LES FAIS BIEN DORER !

                    dessinateurPatrick.FillEllipse(pinceauPate, Position);
                }
            }

            // Contour du pâté - AUSSI NET QUE MON UNIFORME DU KRUSTY KRAB !
            using (Pen pinceauContour = new Pen(Color.FromArgb(200, 150, 90, 10), 2))
            {
                dessinateurPatrick.DrawEllipse(pinceauContour, Position);
            }

            // Ajouter des détails au pâté - COMME LES INGRÉDIENTS SECRETS DE LA FORMULE KRABS QUE PLANKTON N'AURA JAMAIS !
            using (SolidBrush pinceauDetails = new SolidBrush(Color.FromArgb(180, 150, 90, 10)))
            {
                // Petits morceaux dans le pâté - LES PARTIES CROQUANTES QUE TOUT LE MONDE ADORE !
                for (int i = 0; i < 5; i++)
                {
                    int tailleDetail = aléatoireMarinObstacle.Next(3, 6);
                    int x = Position.X + aléatoireMarinObstacle.Next(Position.Width - tailleDetail);
                    int y = Position.Y + aléatoireMarinObstacle.Next(Position.Height - tailleDetail);

                    dessinateurPatrick.FillEllipse(pinceauDetails, x, y, tailleDetail, tailleDetail);
                }
            }

            // Ajouter l'effet graisseux (brillant) - AUSSI BRILLANT QUE MON FRONT APRÈS UNE JOURNÉE AU GRILL !
            using (SolidBrush pinceauBrillant = new SolidBrush(Color.FromArgb(100, 255, 255, 200)))
            {
                int brillanceX = Position.X + Position.Width / 4;
                int brillanceY = Position.Y + Position.Height / 4;
                int brillanceTaille = Position.Width / 3;

                dessinateurPatrick.FillEllipse(pinceauBrillant,
                    brillanceX, brillanceY,
                    brillanceTaille, brillanceTaille / 2);
            }
        }

        public override void GererCollision(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            if (EstActif && balleDeMeduseJoyeuse.IntersectsWith(Position))
            {
                // Rebondir normalement mais avec un effet glissant - COMME MOI QUAND JE GLISSE SUR LE SOL DU KRUSTY KRAB !
                if (Math.Abs(balleDeMeduseJoyeuse.X - Position.X) < Math.Abs(balleDeMeduseJoyeuse.Y - Position.Y))
                {
                    // Rebond vertical - BOÏNG COMME MON CORPS SPONGIEUX !
                    vitesseY = -vitesseY;
                }
                else
                {
                    // Rebond horizontal - WOOSH COMME MA MAIN QUAND JE RETOURNE UN PÂTÉ !
                    vitesseX = -vitesseX;
                }

                // Effet graisseux - La balle devient "glissante" - GLISSANTE COMME QUAND J'OUBLIE DE NETTOYER LA CUISINE !
                vitesseX = (int)(vitesseX * 0.85f);
                vitesseY = (int)(vitesseY * 0.85f);

                // Assurer une vitesse minimale - JAMAIS PLUS LENT QUE M. KRABS ALLANT CHERCHER UN SOU !
                if (Math.Abs(vitesseX) < 3) vitesseX = Math.Sign(vitesseX) * 3;
                if (Math.Abs(vitesseY) < 2) vitesseY = Math.Sign(vitesseY) * 2;

                // Créer un effet visuel de graisse sur la balle ici - COMME QUAND JE METS TROP DE SAUCE SPÉCIALE !
                // (Dans le jeu complet, il faudrait ajouter un effet à la balle qui dure dureeEffetGraisseux frames)

                // Le pâté de crabe disparaît après avoir été touché - MIAM MIAM DANS MON VENTRE ! C'ÉTAIT DÉLICIEUX !
                EstActif = false;
            }
        }
    }
}