using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SuperMegaJeuDuPatrickPong
{
    public partial class FormulaireDuKrabsKrusty : Form
    {
        // Les objets marins du jeu, OH GARY!!
        private Rectangle raquetteDuBobGauche;
        private Rectangle raquetteDuCarlooDroite;
        private Rectangle balleDeMeduseJoyeuse;

        // NOUVEAUTÉ SOUS-MARINE SPECTACULAIRE!
        private List<PointF> traineeCelesteBulle = new List<PointF>();
        private List<Meduse> medusesDansantes = new List<Meduse>();
        private List<Particule> particulesEclaboussantes = new List<Particule>();
        private Color couleurMagiqueDeLaBalle = Color.Yellow;
        private bool effetArcEnCielMarin = true;
        private int compteurRainbowPatrick = 0;

        // LA MUSIQUE DU CRABE CROUSTILLANT! OH YEAAAAH!
        private ChefDOrchestreDuKrustyKrab musiqueDuFondMarin;
        private bool jouerMusiqueEnBoucle = false;

        // Vitesses sous-marines
        private int vitesseBalleMeduse_X = 5;
        private int vitesseBalleMeduse_Y = 5;
        private int vitesseRaquette = 10;

        // Scores des habitants de Bikini Bottom
        private int scoreBobGauche = 0;
        private int scoreCarlooDroite = 0;

        // Contrôle du mouvement
        private bool toucheHautAppuyee = false;
        private bool toucheBasAppuyee = false;

        // Variables pour l'EFFET DE FOLIE quand un but est marqué
        private bool animationButEnCours = false;
        private int compteurAnimationBut = 0;
        private Rectangle zoneFeteVictoire;

        // OH BARNACLES! C'est un générateur aléatoire!
        private Random aléatoireCommePatrick = new Random();

        public FormulaireDuKrabsKrusty()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            InitialiserLeMondeAnanas();

            // PRÉPARER LA MUSIQUE DE LA MER - WAHOOOOO!
            musiqueDuFondMarin = new ChefDOrchestreDuKrustyKrab();

            // JOUER LE GÉNÉRIQUE AU DÉMARRAGE! ÇA VA ÊTRE SUPER COOL!
            Task.Run(() => musiqueDuFondMarin.JouerGeneriqueDeLeponge());

            this.minuteurDuMedusePatient.Start();
            this.Focus();
        }

        private void InitialiserLeMondeAnanas()
        {
            // Création des raquettes - AUSSI ROBUSTES QUE LES SPATULES DU CRABE CROUSTILLANT!
            raquetteDuBobGauche = new Rectangle(20, zoneDeBikiniBas.Height / 2 - 50, 15, 100);
            raquetteDuCarlooDroite = new Rectangle(zoneDeBikiniBas.Width - 35, zoneDeBikiniBas.Height / 2 - 50, 15, 100);

            // Création de la balle au centre - RONDE COMME UNE BELLE BULLE!
            balleDeMeduseJoyeuse = new Rectangle(
                zoneDeBikiniBas.Width / 2 - 10,
                zoneDeBikiniBas.Height / 2 - 10,
                20, 20);

            // GÉNÉRATION DES MÉDUSES DANSANTES, GARY! REGARDE! DES MÉDUSES!
            for (int i = 0; i < 12; i++)
            {
                medusesDansantes.Add(new Meduse(
                    aléatoireCommePatrick.Next(zoneDeBikiniBas.Width),
                    aléatoireCommePatrick.Next(zoneDeBikiniBas.Height),
                    aléatoireCommePatrick.Next(20, 40),
                    Color.FromArgb(
                        aléatoireCommePatrick.Next(150, 255),
                        aléatoireCommePatrick.Next(50, 150),
                        aléatoireCommePatrick.Next(150, 255),
                        150)
                ));
            }
        }

        private void zoneDeBikiniBas_Paint(object sender, PaintEventArgs e)
        {
            Graphics dessinateurPatrick = e.Graphics;
            dessinateurPatrick.SmoothingMode = SmoothingMode.AntiAlias;

            // D'ABORD LES MÉDUSES EN ARRIÈRE-PLAN! OOOOOOOH!
            foreach (var meduse in medusesDansantes)
            {
                meduse.Dessiner(dessinateurPatrick);
            }

            // LA TRAÎNÉE MAGIQUE DE LA BALLE - COMME UNE ÉTOILE FILANTE DE PATRICK!
            if (traineeCelesteBulle.Count > 1)
            {
                using (var traineePinceau = new Pen(Color.FromArgb(100, couleurMagiqueDeLaBalle), 5))
                {
                    for (int i = 1; i < traineeCelesteBulle.Count; i++)
                    {
                        float epaisseur = (float)i / traineeCelesteBulle.Count * 10;
                        traineePinceau.Width = epaisseur;
                        traineePinceau.Color = Color.FromArgb(
                            100 - i * 3,
                            couleurMagiqueDeLaBalle.R,
                            couleurMagiqueDeLaBalle.G,
                            couleurMagiqueDeLaBalle.B);
                        dessinateurPatrick.DrawLine(traineePinceau,
                            traineeCelesteBulle[i - 1],
                            traineeCelesteBulle[i]);
                    }
                }
            }

            // PARTICULES ÉCLABOUSSANTES - COMME LES BULLES DE MON BAIN!
            foreach (var particule in particulesEclaboussantes.ToArray())
            {
                particule.Dessiner(dessinateurPatrick);
            }

            // Ligne centrale pointillée comme des bulles - AUSSI JOLIE QUE LA CRAVATE DE SQUIDWARD!
            using (Pen crayon = new Pen(Color.FromArgb(150, 255, 255, 255), 2))
            {
                crayon.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                dessinateurPatrick.DrawLine(crayon,
                    zoneDeBikiniBas.Width / 2, 0,
                    zoneDeBikiniBas.Width / 2, zoneDeBikiniBas.Height);
            }

            // Dessin des raquettes en couleur corail - COMME MES JOLIS SHORTS CARRÉS!
            using (LinearGradientBrush pinceau = new LinearGradientBrush(
                new Point(0, 0), new Point(15, 0),
                Color.DeepPink, Color.HotPink))
            {
                dessinateurPatrick.FillRectangle(pinceau, raquetteDuBobGauche);
            }

            using (LinearGradientBrush pinceau = new LinearGradientBrush(
                new Point(zoneDeBikiniBas.Width, 0), new Point(zoneDeBikiniBas.Width - 15, 0),
                Color.DeepPink, Color.HotPink))
            {
                dessinateurPatrick.FillRectangle(pinceau, raquetteDuCarlooDroite);
            }

            // Dessin de la balle avec un effet brillant - COMME MON SOURIRE ÉCLATANT!
            using (PathGradientBrush pinceau = new PathGradientBrush(
                new PointF[] {
                    new PointF(balleDeMeduseJoyeuse.X, balleDeMeduseJoyeuse.Y),
                    new PointF(balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width, balleDeMeduseJoyeuse.Y),
                    new PointF(balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width, balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height),
                    new PointF(balleDeMeduseJoyeuse.X, balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height)
                }))
            {
                pinceau.CenterColor = couleurMagiqueDeLaBalle;
                pinceau.SurroundColors = new Color[] { Color.FromArgb(200, couleurMagiqueDeLaBalle) };
                dessinateurPatrick.FillEllipse(pinceau, balleDeMeduseJoyeuse);
            }

            // Reflet sur la balle - BRILLANT COMME MA SPATULE PRÉFÉRÉE!
            using (SolidBrush pinceau = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
            {
                dessinateurPatrick.FillEllipse(pinceau,
                    balleDeMeduseJoyeuse.X + 5,
                    balleDeMeduseJoyeuse.Y + 3,
                    7, 5);
            }

            // Animation spéciale quand un but est marqué - UNE EXPLOSION DE JOIE!
            if (animationButEnCours)
            {
                using (GraphicsPath chemin = new GraphicsPath())
                {
                    chemin.AddEllipse(zoneFeteVictoire);
                    using (PathGradientBrush pinceau = new PathGradientBrush(chemin))
                    {
                        pinceau.CenterColor = Color.FromArgb(
                            150 - compteurAnimationBut * 3,
                            255, 215, 0); // Or
                        pinceau.SurroundColors = new Color[] {
                            Color.FromArgb(0, 255, 215, 0)
                        };

                        dessinateurPatrick.FillEllipse(pinceau, zoneFeteVictoire);
                    }
                }
            }
        }

        private void minuteurDuMedusePatient_Tick(object sender, EventArgs e)
        {
            // MUSIQUE EN BOUCLE - COMME UNE MÉLODIE SANS FIN!
            if (jouerMusiqueEnBoucle && !musiqueDuFondMarin.EstEnTrainDeJouer)
            {
                Task.Run(() => musiqueDuFondMarin.JouerBipJoyeux());
            }

            // MÉDUSES DANSANTES - ELLES FLOTTENT COMME MES RÊVES!
            foreach (var meduse in medusesDansantes)
            {
                meduse.Deplacer(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
            }

            // Animation de but - OH LA LA QUELLE FÊTE!
            if (animationButEnCours)
            {
                compteurAnimationBut++;
                zoneFeteVictoire.Inflate(5, 5);

                if (compteurAnimationBut > 40)
                {
                    animationButEnCours = false;
                    compteurAnimationBut = 0;
                }
            }

            // Mise à jour des particules - BULLES QUI ÉCLATENT!
            MettreAJourParticules();

            // Changement de couleur arc-en-ciel - COMME UN HAMBURGER PÂTÉ DE CRABE MAGIQUE!
            if (effetArcEnCielMarin)
            {
                compteurRainbowPatrick += 5;
                if (compteurRainbowPatrick > 360) compteurRainbowPatrick = 0;

                couleurMagiqueDeLaBalle = CouleurHSVversRVB(compteurRainbowPatrick, 0.8f, 1.0f);
            }

            // Ajout de la position courante à la traînée
            traineeCelesteBulle.Add(new PointF(
                balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2,
                balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2));

            // Limite la taille de la traînée
            if (traineeCelesteBulle.Count > 15) traineeCelesteBulle.RemoveAt(0);

            // Déplacement de la raquette gauche (joueur) - ALLEZ BOB, TU PEUX LE FAIRE!
            if (toucheHautAppuyee && raquetteDuBobGauche.Y > 0)
                raquetteDuBobGauche.Y -= vitesseRaquette;

            if (toucheBasAppuyee && raquetteDuBobGauche.Y + raquetteDuBobGauche.Height < zoneDeBikiniBas.Height)
                raquetteDuBobGauche.Y += vitesseRaquette;

            // Intelligence artificielle simple pour la raquette droite - CARLO EST RUSÉ COMME PLANKTON!
            DeplacementRaquetteCarlooDroite();

            // Déplacement de la balle - ELLE FLOTTE COMME UNE MÉDUSE!
            balleDeMeduseJoyeuse.X += vitesseBalleMeduse_X;
            balleDeMeduseJoyeuse.Y += vitesseBalleMeduse_Y;

            // Rebonds sur les bords haut et bas - BOIIING!
            if (balleDeMeduseJoyeuse.Y <= 0 || balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height >= zoneDeBikiniBas.Height)
            {
                vitesseBalleMeduse_Y = -vitesseBalleMeduse_Y;
                CreerParticules(5);
                Task.Run(() => musiqueDuFondMarin.JouerBipRebond());
            }

            // Rebonds sur les raquettes - SPLAAASH!
            if (balleDeMeduseJoyeuse.IntersectsWith(raquetteDuBobGauche) || balleDeMeduseJoyeuse.IntersectsWith(raquetteDuCarlooDroite))
            {
                vitesseBalleMeduse_X = -vitesseBalleMeduse_X;
                // Accélération progressive - PLUS VITE QUE SANDY ÉCUREUIL!
                if (Math.Abs(vitesseBalleMeduse_X) < 15)
                {
                    vitesseBalleMeduse_X = vitesseBalleMeduse_X < 0 ? vitesseBalleMeduse_X - 1 : vitesseBalleMeduse_X + 1;
                }
                CreerParticules(10);
                Task.Run(() => musiqueDuFondMarin.JouerBipRaquette());
            }

            // But marqué à droite - OH NON BOB A PERDU!
            if (balleDeMeduseJoyeuse.X < 0)
            {
                scoreCarlooDroite++;
                ReinitialiserBalleMeduse();
                DemarrerAnimationBut(0);
                Task.Run(() => musiqueDuFondMarin.JouerBipDefaite());
            }
            // But marqué à gauche - HOURRA POUR BOB!
            else if (balleDeMeduseJoyeuse.X > zoneDeBikiniBas.Width)
            {
                scoreBobGauche++;
                ReinitialiserBalleMeduse();
                DemarrerAnimationBut(zoneDeBikiniBas.Width);
                Task.Run(() => musiqueDuFondMarin.JouerBipVictoire());
            }

            // Mise à jour du score - LES CHIFFRES DE LA VICTOIRE!
            scorePatrickEtoile.Text = $"{scoreBobGauche} - {scoreCarlooDroite}";

            // Rafraîchissement de l'écran - AUSSI RAFRAÎCHISSANT QU'UN PLONGEON DANS L'OCÉAN!
            zoneDeBikiniBas.Invalidate();
        }

        private void DemarrerAnimationBut(int position)
        {
            animationButEnCours = true;
            compteurAnimationBut = 0;
            zoneFeteVictoire = new Rectangle(
                position == 0 ? 0 : zoneDeBikiniBas.Width - 20,
                zoneDeBikiniBas.Height / 2 - 10,
                20, 20);

            // EXPLOSION DE PARTICULES - COMME LA JOIE DANS MON CŒUR!
            for (int i = 0; i < 50; i++)
            {
                float angle = (float)(aléatoireCommePatrick.NextDouble() * Math.PI * 2);
                float vitesse = aléatoireCommePatrick.Next(3, 10);

                particulesEclaboussantes.Add(new Particule(
                    position == 0 ? 10 : zoneDeBikiniBas.Width - 10,
                    zoneDeBikiniBas.Height / 2,
                    (float)Math.Cos(angle) * vitesse,
                    (float)Math.Sin(angle) * vitesse,
                    Color.FromArgb(
                        aléatoireCommePatrick.Next(150, 255),
                        aléatoireCommePatrick.Next(150, 255),
                        aléatoireCommePatrick.Next(150, 255)),
                    aléatoireCommePatrick.Next(3, 8),
                    aléatoireCommePatrick.Next(30, 60)
                ));
            }
        }

        private void DeplacementRaquetteCarlooDroite()
        {
            // L'IA suit la balle mais avec un peu de délai - CARLO EST PRESQUE AUSSI MALIN QUE MOI!
            int centreRaquette = raquetteDuCarlooDroite.Y + raquetteDuCarlooDroite.Height / 2;
            int centreBalle = balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2;

            if (centreBalle < centreRaquette - 10 && raquetteDuCarlooDroite.Y > 0)
                raquetteDuCarlooDroite.Y -= vitesseRaquette / 2;
            else if (centreBalle > centreRaquette + 10 && raquetteDuCarlooDroite.Y + raquetteDuCarlooDroite.Height < zoneDeBikiniBas.Height)
                raquetteDuCarlooDroite.Y += vitesseRaquette / 2;
        }

        private void ReinitialiserBalleMeduse()
        {
            balleDeMeduseJoyeuse.X = zoneDeBikiniBas.Width / 2 - 10;
            balleDeMeduseJoyeuse.Y = zoneDeBikiniBas.Height / 2 - 10;
            traineeCelesteBulle.Clear();

            // Changement aléatoire de la direction - IMPRÉVISIBLE COMME PLANKTON!
            vitesseBalleMeduse_X = aléatoireCommePatrick.Next(0, 2) == 0 ? -5 : 5;
            vitesseBalleMeduse_Y = aléatoireCommePatrick.Next(-3, 4);
        }

        private void CreerParticules(int nombre)
        {
            // Création de particules lors des rebonds - SPLASH SPLASH SPLASH!
            for (int i = 0; i < nombre; i++)
            {
                float angle = (float)(aléatoireCommePatrick.NextDouble() * Math.PI * 2);
                float vitesse = aléatoireCommePatrick.Next(2, 6);

                particulesEclaboussantes.Add(new Particule(
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2,
                    (float)Math.Cos(angle) * vitesse,
                    (float)Math.Sin(angle) * vitesse,
                    couleurMagiqueDeLaBalle,
                    aléatoireCommePatrick.Next(2, 5),
                    aléatoireCommePatrick.Next(10, 30)
                ));
            }
        }

        private void MettreAJourParticules()
        {
            // Mise à jour des particules - COMME DES BULLES QUI DANSENT!
            for (int i = particulesEclaboussantes.Count - 1; i >= 0; i--)
            {
                particulesEclaboussantes[i].Mettre_A_Jour();
                if (particulesEclaboussantes[i].Duree_De_Vie <= 0)
                {
                    particulesEclaboussantes.RemoveAt(i);
                }
            }
        }

        // Conversion HSV vers RGB pour l'arc-en-ciel - COMME LA MAGIE DES ARCS-EN-CIEL MARINS!
        private Color CouleurHSVversRVB(float h, float s, float v)
        {
            int hi = (int)(h / 60) % 6;
            float f = h / 60 - (int)(h / 60);
            float p = v * (1 - s);
            float q = v * (1 - f * s);
            float t = v * (1 - (1 - f) * s);

            float r, g, b;

            switch (hi)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                default: r = v; g = p; b = q; break;
            }

            return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        private void FormulaireDuKrabsKrusty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                toucheHautAppuyee = true;
            else if (e.KeyCode == Keys.Down)
                toucheBasAppuyee = true;
            else if (e.KeyCode == Keys.Space)
                effetArcEnCielMarin = !effetArcEnCielMarin; // BASCULE L'ARC-EN-CIEL AVEC ESPACE!
            else if (e.KeyCode == Keys.M)
            {
                // TOUCHE M POUR DÉMARRER/ARRÊTER LA MUSIQUE EN BOUCLE!
                jouerMusiqueEnBoucle = !jouerMusiqueEnBoucle;
                if (jouerMusiqueEnBoucle)
                {
                    Task.Run(() => musiqueDuFondMarin.JouerGeneriqueDeLeponge());
                }
            }
        }

        private void FormulaireDuKrabsKrusty_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                toucheHautAppuyee = false;
            else if (e.KeyCode == Keys.Down)
                toucheBasAppuyee = false;
        }

    }
}