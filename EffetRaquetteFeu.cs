using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET RAQUETTE FEU - LA RAQUETTE EST EN FEU COMME QUAND J'OUBLIE UN PÂTÉ SUR LE GRILL !
    public class EffetRaquetteFeu : RaquetteEffetAbstrait
    {
        private int compteurFlammes = 0;

        public override string NomEffet => "RAQUETTE EN FEU !";

        public EffetRaquetteFeu(bool estPourRaquetteGauche) : base(300, estPourRaquetteGauche) // 5 secondes
        {
        }

        public override void AppliquerEffet(ref Rectangle raquette, ref int vitesseRaquette)
        {
            // La raquette en feu est plus rapide - COMME QUAND JE COURS POUR ÉTEINDRE UN INCENDIE DE CUISINE !
            vitesseRaquette = 18;
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle raquette)
        {
            compteurFlammes = (compteurFlammes + 5) % 360;

            // Dessiner d'abord la raquette en rouge ardent
            using (LinearGradientBrush pinceauRaquette = new LinearGradientBrush(
                new Point(raquette.X, raquette.Y),
                new Point(raquette.X + raquette.Width, raquette.Y),
                Color.Orange, Color.Red))
            {
                dessinateurPatrick.FillRectangle(pinceauRaquette, raquette);
            }

            // Ajouter des flammes qui sortent de la raquette
            int nombreFlammes = 10;
            for (int i = 0; i < nombreFlammes; i++)
            {
                // Position de la flamme le long de la raquette
                int baseY = raquette.Y + i * (raquette.Height / nombreFlammes);
                int hauteurFlamme = aleatoireMarinEffet.Next(10, 30);

                // Position horizontale de la flamme (côté opposé à la balle)
                int baseX = EstPourRaquetteGauche ?
                    raquette.X + raquette.Width :
                    raquette.X - hauteurFlamme;

                // Variation sinusoïdale pour l'animation des flammes
                double variation = Math.Sin((compteurFlammes + i * 30) * Math.PI / 180) * 5;

                // Points pour la flamme
                Point[] pointsFlamme = new Point[]
                {
                    new Point(EstPourRaquetteGauche ? raquette.X + raquette.Width : raquette.X, baseY),
                    new Point(baseX + (EstPourRaquetteGauche ? hauteurFlamme : 0) + (int)variation, baseY - 5),
                    new Point(baseX + (EstPourRaquetteGauche ? hauteurFlamme : 0), baseY + 5),
                    new Point(baseX + (EstPourRaquetteGauche ? hauteurFlamme : 0) - (int)variation, baseY + 15)
                };

                // Couleur de flamme
                using (PathGradientBrush pinceauFlamme = new PathGradientBrush(pointsFlamme))
                {
                    pinceauFlamme.CenterColor = Color.Yellow;
                    pinceauFlamme.SurroundColors = new Color[] {
                        Color.FromArgb(200, 255, 100, 0),
                        Color.FromArgb(150, 255, 0, 0),
                        Color.FromArgb(100, 255, 50, 0)
                    };

                    dessinateurPatrick.FillPolygon(pinceauFlamme, pointsFlamme);
                }
            }

            // Particules de fumée au-dessus des flammes
            for (int i = 0; i < 5; i++)
            {
                int baseX = EstPourRaquetteGauche ?
                    raquette.X + raquette.Width + 15 + i * 5 :
                    raquette.X - 15 - i * 5;

                int baseY = raquette.Y + aleatoireMarinEffet.Next(raquette.Height);
                int tailleParticule = 3 + i;

                using (SolidBrush pinceauFumee = new SolidBrush(Color.FromArgb(50 - i * 10, 100, 100, 100)))
                {
                    dessinateurPatrick.FillEllipse(pinceauFumee,
                        baseX, baseY,
                        tailleParticule, tailleParticule);
                }
            }
        }
    }
}