using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // PLANKTON - OH NEPTUNE ! C'EST MON ENNEMI JURÉ QUI VEUT TOUJOURS VOLER MA PRÉCIEUSE FORMULE SECRÈTE !
    public class Plankton : ObstacleAbstrait
    {
        private int angleRotation = 0;

        public Plankton(int largeurEcran, int hauteurEcran) : base(largeurEcran, hauteurEcran)
        {
            // Plankton est MINUSCULE mais TERRIBLEMENT MALÉFIQUE ! MUAHAHAHA !
            int taillePlanktonMechant = aléatoireMarinObstacle.Next(20, 35);
            Position = new Rectangle(
                Position.X,
                Position.Y,
                taillePlanktonMechant,
                taillePlanktonMechant);
        }

        public override void Dessiner(Graphics dessinateurPatrick)
        {
            if (!EstActif) return;

            // Rotation de Plankton - IL TOURNE COMME SES PLANS DIABOLIQUES DANS LE CHAUDRON DU CHUM BUCKET !
            angleRotation = (angleRotation + 3) % 360;

            // Sauvegarde de l'état du dessinateur - AUSSI PRÉCIEUSE QUE MA FORMULE SECRÈTE !
            Matrix transformationOriginale = dessinateurPatrick.Transform.Clone();

            // Appliquer la rotation - WHOOSH COMME QUAND JE LANCE DES PÂTÉS EN L'AIR !
            dessinateurPatrick.TranslateTransform(
                Position.X + Position.Width / 2,
                Position.Y + Position.Height / 2);
            dessinateurPatrick.RotateTransform(angleRotation);
            dessinateurPatrick.TranslateTransform(
                -(Position.X + Position.Width / 2),
                -(Position.Y + Position.Height / 2));

            // Corps de Plankton (vert) - VERT COMME LES ALGUES QUE JE NETTOIE AU KRUSTY KRAB !
            using (SolidBrush pinceauCorps = new SolidBrush(Color.FromArgb(200, 50, 180, 50)))
            {
                dessinateurPatrick.FillEllipse(pinceauCorps, Position);
            }

            // Œil de Plankton (rouge) - ROUGE ET MALVEILLANT COMME QUAND IL ESPIONNE MA CUISINE !
            using (SolidBrush pinceauOeil = new SolidBrush(Color.FromArgb(220, 220, 0, 0)))
            {
                int tailleOeil = Position.Width / 3;
                dessinateurPatrick.FillEllipse(pinceauOeil,
                    Position.X + Position.Width / 2 - tailleOeil / 2,
                    Position.Y + Position.Height / 3 - tailleOeil / 2,
                    tailleOeil, tailleOeil);
            }

            // Antennes de Plankton - AUSSI POINTUES QUE SES REMARQUES SARCASTIQUES À CARLO !
            using (Pen pinceauAntenne = new Pen(Color.FromArgb(200, 30, 150, 30), 2))
            {
                dessinateurPatrick.DrawLine(pinceauAntenne,
                    Position.X + Position.Width / 2,
                    Position.Y,
                    Position.X + Position.Width / 2,
                    Position.Y - Position.Height / 2);

                dessinateurPatrick.DrawLine(pinceauAntenne,
                    Position.X + Position.Width / 2,
                    Position.Y - Position.Height / 2,
                    Position.X + Position.Width / 2 - Position.Width / 4,
                    Position.Y - Position.Height / 2 - Position.Height / 4);

                dessinateurPatrick.DrawLine(pinceauAntenne,
                    Position.X + Position.Width / 2,
                    Position.Y - Position.Height / 2,
                    Position.X + Position.Width / 2 + Position.Width / 4,
                    Position.Y - Position.Height / 2 - Position.Height / 4);
            }

            // Restaurer la transformation originale - COMME JE REMETS MA CUISINE EN ORDRE APRÈS UNE JOURNÉE DE FRITURE !
            dessinateurPatrick.Transform = transformationOriginale;
        }

        public override void GererCollision(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            if (EstActif && balleDeMeduseJoyeuse.IntersectsWith(Position))
            {
                // FIHIHIHI ! Plankton est petit mais INCROYABLEMENT maléfique ! Il inverse COMPLÈTEMENT la direction !
                vitesseX = -vitesseX;
                vitesseY = -vitesseY;

                // Plankton disparaît après avoir été touché - COMME QUAND M. KRABS LE JETTE HORS DU KRUSTY KRAB !
                EstActif = false;
            }
        }
    }
}