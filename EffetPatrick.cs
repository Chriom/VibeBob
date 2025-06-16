using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperMegaJeuDuPatrickPong
{
    // EFFET THIS IS PATRICK - LA BALLE DEVIENT GROSSE ET LENTE COMME PATRICK!
    public class EffetPatrick : EffetAbstrait
    {
        private readonly int tailleOriginale;
        private readonly int vitesseOriginaleX;
        private readonly int vitesseOriginaleY;
        private readonly Image imagePatrick;

        public override string NomEffet => "THIS IS PATRICK! 🌟";

        public EffetPatrick(Rectangle balleDeMeduseJoyeuse, int vx, int vy) : base(8000) // 8 secondes de Patrick!
        {
            tailleOriginale = balleDeMeduseJoyeuse.Width;
            vitesseOriginaleX = vx;
            vitesseOriginaleY = vy;

            // On utilise une ellipse rose au lieu d'une image
            imagePatrick = null;
        }

        public override void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY)
        {
            // GROSSIR LA BALLE COMME PATRICK - GROS ET FORT!
            int nouvelletaille = tailleOriginale * 2;

            // Centrer la balle élargie pour éviter les téléportations
            balleDeMeduseJoyeuse.X = balleDeMeduseJoyeuse.X - (nouvelletaille - balleDeMeduseJoyeuse.Width) / 2;
            balleDeMeduseJoyeuse.Y = balleDeMeduseJoyeuse.Y - (nouvelletaille - balleDeMeduseJoyeuse.Height) / 2;

            balleDeMeduseJoyeuse.Width = nouvelletaille;
            balleDeMeduseJoyeuse.Height = nouvelletaille;

            // Ralentir la balle - PATRICK EST LENT COMME UNE LIMACE DE MER!
            float facteur = 0.7f;
            vitesseX = (int)(vitesseOriginaleX * facteur);
            vitesseY = (int)(vitesseOriginaleY * facteur);
        }

        public override void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse)
        {
            // Dessiner une étoile de mer rose - COMME PATRICK!
            using (SolidBrush pinceauPatrick = new SolidBrush(Color.HotPink))
            {
                dessinateurPatrick.FillEllipse(pinceauPatrick, balleDeMeduseJoyeuse);
            }

            // Dessiner des petits points roses foncés comme les taches de Patrick
            using (SolidBrush pinceauTaches = new SolidBrush(Color.DeepPink))
            {
                int tailleTache = balleDeMeduseJoyeuse.Width / 10;

                for (int i = 0; i < 5; i++)
                {
                    // Position aléatoire à l'intérieur de la balle
                    int x = balleDeMeduseJoyeuse.X + aléatoireMarinBonus.Next(balleDeMeduseJoyeuse.Width - tailleTache);
                    int y = balleDeMeduseJoyeuse.Y + aléatoireMarinBonus.Next(balleDeMeduseJoyeuse.Height - tailleTache);

                    dessinateurPatrick.FillEllipse(pinceauTaches, x, y, tailleTache, tailleTache);
                }
            }

            // Dessiner les yeux de Patrick
            int tailleOeil = balleDeMeduseJoyeuse.Width / 4;
            int posYYeux = balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 4;

            // Le blanc des yeux
            using (SolidBrush pinceauBlanc = new SolidBrush(Color.White))
            {
                dessinateurPatrick.FillEllipse(pinceauBlanc,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 4 - tailleOeil / 2,
                    posYYeux,
                    tailleOeil, tailleOeil);

                dessinateurPatrick.FillEllipse(pinceauBlanc,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width * 3 / 4 - tailleOeil / 2,
                    posYYeux,
                    tailleOeil, tailleOeil);
            }

            // Les pupilles
            using (SolidBrush pinceauNoir = new SolidBrush(Color.Black))
            {
                int taillePupille = tailleOeil / 2;

                dessinateurPatrick.FillEllipse(pinceauNoir,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 4 - taillePupille / 2,
                    posYYeux + tailleOeil / 4,
                    taillePupille, taillePupille);

                dessinateurPatrick.FillEllipse(pinceauNoir,
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width * 3 / 4 - taillePupille / 2,
                    posYYeux + tailleOeil / 4,
                    taillePupille, taillePupille);
            }
        }

        public override void MettreAJour()
        {
            base.MettreAJour();
        }
    }
}