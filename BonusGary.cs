using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // BONUS GARY - COMME MON ESCARGOT ADORÉ!
    public class BonusGary : BonusAbstrait
    {
        private int compteurMouvement = 0;
        private readonly Color couleurCoquille = Color.FromArgb(255, 64, 224, 208); // Turquoise

        public override string NomBonus => "GARY!";

        public BonusGary(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            dureeEffetStandard = 12000; // 12 secondes d'effet escargot!
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            compteurMouvement = (compteurMouvement + 1) % 360;

            // Corps de Gary (coquille turquoise)
            using (SolidBrush pinceauCoquille = new SolidBrush(couleurCoquille))
            {
                dessinateurPatrick.FillEllipse(pinceauCoquille, Position);
            }

            // Motif en spirale sur la coquille
            using (Pen styloCoquille = new Pen(Color.FromArgb(200, 0, 128, 128), 2))
            {
                int centreX = Position.X + Position.Width / 2;
                int centreY = Position.Y + Position.Height / 2;
                float rayon = Position.Width / 4;

                for (float angle = 0; angle < Math.PI * 4; angle += 0.1f)
                {
                    float r = rayon * (1 - angle / (float)(Math.PI * 4));
                    float x = centreX + r * (float)Math.Cos(angle);
                    float y = centreY + r * (float)Math.Sin(angle);

                    dessinateurPatrick.FillEllipse(Brushes.DarkSlateGray, x - 1, y - 1, 2, 2);
                }
            }

            // Yeux de Gary
            int tailleOeil = Position.Width / 5;

            // Effet de mouvement des yeux
            float decalageYeux = (float)Math.Sin(compteurMouvement * Math.PI / 180) * 3;

            // Position des yeux (en haut)
            int posXYeux = Position.X + Position.Width / 3;

            // Tiges des yeux
            using (SolidBrush pinceauTiges = new SolidBrush(Color.FromArgb(230, 100, 200, 200)))
            {
                dessinateurPatrick.FillEllipse(pinceauTiges,
                    posXYeux,
                    Position.Y - tailleOeil + decalageYeux,
                    tailleOeil,
                    tailleOeil * 2);

                dessinateurPatrick.FillEllipse(pinceauTiges,
                    posXYeux + Position.Width / 3,
                    Position.Y - tailleOeil + decalageYeux,
                    tailleOeil,
                    tailleOeil * 2);
            }

            // Yeux
            using (SolidBrush pinceauYeux = new SolidBrush(Color.White))
            {
                dessinateurPatrick.FillEllipse(pinceauYeux,
                    posXYeux,
                    Position.Y - tailleOeil + decalageYeux,
                    tailleOeil,
                    tailleOeil);

                dessinateurPatrick.FillEllipse(pinceauYeux,
                    posXYeux + Position.Width / 3,
                    Position.Y - tailleOeil + decalageYeux,
                    tailleOeil,
                    tailleOeil);
            }

            // Pupilles
            using (SolidBrush pinceauPupilles = new SolidBrush(Color.Black))
            {
                dessinateurPatrick.FillEllipse(pinceauPupilles,
                    posXYeux + tailleOeil / 2 - 2,
                    Position.Y - tailleOeil + tailleOeil / 2 - 2 + decalageYeux,
                    4,
                    4);

                dessinateurPatrick.FillEllipse(pinceauPupilles,
                    posXYeux + Position.Width / 3 + tailleOeil / 2 - 2,
                    Position.Y - tailleOeil + tailleOeil / 2 - 2 + decalageYeux,
                    4,
                    4);
            }
        }

        public override IEffet Collecter()
        {
            EstActif = false;
            return new EffetGary();
        }
    }

}