using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // BONUS THIS IS PATRICK - COMME L'ÉTOILE DE MER QUI RÉPOND AU TÉLÉPHONE!
    public class BonusPatrick : BonusAbstrait
    {
        private int oscillation = 0;

        public override string NomBonus => "THIS IS PATRICK!";

        public BonusPatrick(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            dureeEffetStandard = 8000; // 8 secondes d'effet Patrick!
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            oscillation = (oscillation + 5) % 360;
            float facteurOscillation = (float)Math.Sin(oscillation * Math.PI / 180) * 0.1f + 1.0f;

            // Dessiner une étoile rose comme Patrick
            using (SolidBrush pinceauPatrick = new SolidBrush(Color.HotPink))
            {
                // Corps de Patrick (étoile simplifiée en cercle qui oscille)
                Rectangle corpsOscillant = new Rectangle(
                    Position.X - (int)(Position.Width * facteurOscillation - Position.Width) / 2,
                    Position.Y - (int)(Position.Height * facteurOscillation - Position.Height) / 2,
                    (int)(Position.Width * facteurOscillation),
                    (int)(Position.Height * facteurOscillation));

                dessinateurPatrick.FillEllipse(pinceauPatrick, corpsOscillant);
            }

            // Dessiner les taches sur Patrick
            using (SolidBrush pinceauTaches = new SolidBrush(Color.DeepPink))
            {
                int tailleTache = Position.Width / 8;

                for (int i = 0; i < 5; i++)
                {
                    // Position relative fixe pour les taches
                    int x = Position.X + Position.Width / 5 * (i % 3 + 1);
                    int y = Position.Y + Position.Height / 4 * (i % 2 + 1);

                    dessinateurPatrick.FillEllipse(pinceauTaches, x, y, tailleTache, tailleTache);
                }
            }

            // Dessiner les yeux de Patrick
            int tailleOeil = Position.Width / 5;
            int posYYeux = Position.Y + Position.Height / 4;

            // Le blanc des yeux
            using (SolidBrush pinceauBlanc = new SolidBrush(Color.White))
            {
                dessinateurPatrick.FillEllipse(pinceauBlanc,
                    Position.X + Position.Width / 4,
                    posYYeux,
                    tailleOeil, tailleOeil);

                dessinateurPatrick.FillEllipse(pinceauBlanc,
                    Position.X + Position.Width * 2 / 3,
                    posYYeux,
                    tailleOeil, tailleOeil);
            }

            // Les pupilles
            using (SolidBrush pinceauNoir = new SolidBrush(Color.Black))
            {
                int taillePupille = tailleOeil / 2;

                dessinateurPatrick.FillEllipse(pinceauNoir,
                    Position.X + Position.Width / 4 + tailleOeil / 4,
                    posYYeux + tailleOeil / 4,
                    taillePupille, taillePupille);

                dessinateurPatrick.FillEllipse(pinceauNoir,
                    Position.X + Position.Width * 2 / 3 + tailleOeil / 4,
                    posYYeux + tailleOeil / 4,
                    taillePupille, taillePupille);
            }
        }

        public override IEffet Collecter()
        {
            EstActif = false;
            return new EffetPatrick(new Rectangle(0, 0, 20, 20), 5, 5); // Valeurs temporaires remplacées dans le jeu
        }
    }
}