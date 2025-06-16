using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET RAQUETTE BOB - LA RAQUETTE DEVIENT ABSORBANTE COMME UNE ÉPONGE !
    public class EffetRaquetteBob : RaquetteEffetAbstrait
    {
        private int compteurAnimation = 0;

        public override string NomEffet => "RAQUETTE ÉPONGE !";

        public EffetRaquetteBob(bool estPourRaquetteGauche) : base(600, estPourRaquetteGauche) // 10 secondes à 60fps
        {
        }

        public override void AppliquerEffet(ref Rectangle raquette, ref int vitesseRaquette)
        {
            // La raquette éponge est plus grande - COMME MON COEUR PLEIN D'AMOUR !
            int largeurOrigine = raquette.Width;
            int hauteurOrigine = raquette.Height;

            // Agrandir la hauteur de la raquette
            int nouvelleHauteur = 140;

            // Centrer la nouvelle raquette
            int decalageY = (nouvelleHauteur - hauteurOrigine) / 2;

            raquette.Height = nouvelleHauteur;
            raquette.Y -= decalageY;

            // Garder la raquette dans les limites de l'écran
            if (raquette.Y < 0)
                raquette.Y = 0;

            // L'éponge est un peu plus lente - ELLE ABSORBE TOUT !
            vitesseRaquette = 8;
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle raquette)
        {
            compteurAnimation = (compteurAnimation + 1) % 60;

            // Créer un motif d'éponge poreuse !
            using (GraphicsPath cheminEponge = new GraphicsPath())
            {
                cheminEponge.AddRectangle(raquette);

                using (PathGradientBrush pinceauEponge = new PathGradientBrush(cheminEponge))
                {
                    // Couleur éponge jaune !
                    pinceauEponge.CenterColor = Color.Yellow;
                    pinceauEponge.SurroundColors = new Color[] { Color.FromArgb(255, 255, 220, 0) };

                    dessinateurPatrick.FillRectangle(pinceauEponge, raquette);
                }

                // Ajouter des trous d'éponge !
                using (SolidBrush pinceauTrous = new SolidBrush(Color.FromArgb(100, 150, 100, 0)))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        int x = raquette.X + aleatoireMarinEffet.Next(raquette.Width - 4);
                        int y = raquette.Y + aleatoireMarinEffet.Next(raquette.Height - 4);
                        int taille = aleatoireMarinEffet.Next(2, 5);

                        dessinateurPatrick.FillEllipse(pinceauTrous, x, y, taille, taille);
                    }
                }

                // Dessin des yeux et du sourire de Bob !
                if (compteurAnimation < 30) // Clignement des yeux !
                {
                    using (SolidBrush pinceauYeux = new SolidBrush(Color.White))
                    {
                        int tailleOeil = raquette.Width - 4;

                        // Les yeux
                        dessinateurPatrick.FillEllipse(pinceauYeux,
                            raquette.X + 2,
                            raquette.Y + raquette.Height / 4,
                            tailleOeil, tailleOeil);

                        dessinateurPatrick.FillEllipse(pinceauYeux,
                            raquette.X + 2,
                            raquette.Y + raquette.Height * 2 / 3,
                            tailleOeil, tailleOeil);

                        // Les pupilles
                        using (SolidBrush pinceauPupilles = new SolidBrush(Color.Blue))
                        {
                            int taillePupille = tailleOeil / 2;

                            dessinateurPatrick.FillEllipse(pinceauPupilles,
                                raquette.X + 4 + tailleOeil / 4,
                                raquette.Y + raquette.Height / 4 + tailleOeil / 4,
                                taillePupille, taillePupille);

                            dessinateurPatrick.FillEllipse(pinceauPupilles,
                                raquette.X + 4 + tailleOeil / 4,
                                raquette.Y + raquette.Height * 2 / 3 + tailleOeil / 4,
                                taillePupille, taillePupille);
                        }
                    }
                }
            }
        }
    }
}