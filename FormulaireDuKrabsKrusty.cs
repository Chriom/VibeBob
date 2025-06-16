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

        // NOUVEAUT� SOUS-MARINE SPECTACULAIRE!
        private List<PointF> traineeCelesteBulle = new List<PointF>();
        private List<Meduse> medusesDansantes = new List<Meduse>();
        private List<Particule> particulesEclaboussantes = new List<Particule>();
        private Color couleurMagiqueDeLaBalle = Color.Yellow;
        private bool effetArcEnCielMarin = true;
        private int compteurRainbowPatrick = 0;

        // SYST�ME DE BONUS ET EFFETS - COMME LES SURPRISES DANS LES BO�TES DE C�R�ALES!
        private List<IBonus> bonusMarins = new List<IBonus>();
        private List<IEffet> effetsActifs = new List<IEffet>();
        private string nomEffetEnCours = "";
        private int compteurProchainBonus = 200; // D�lai avant le premier bonus
        private int dureeAffichageNomEffet = 0;

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

        // Contr�le du mouvement
        private bool toucheHautAppuyee = false;
        private bool toucheBasAppuyee = false;

        // Variables pour l'EFFET DE FOLIE quand un but est marqu�
        private bool animationButEnCours = false;
        private int compteurAnimationBut = 0;
        private Rectangle zoneFeteVictoire;

        // OH BARNACLES! C'est un g�n�rateur al�atoire!
        private Random al�atoireCommePatrick = new Random();


        private List<IObstacle> obstaclesSousMarins = new List<IObstacle>();
        private int compteurProchainObstacle = 300; // D�lai avant le premier obstacle


        private bool fondPsychedeliqueActif = false;
        private int dureeAffichagePsychedelique = 0;
        private int compteurCouleurPsychedelique = 0;
        private int delaiProchainPsychedelique = 600; // 10 secondes � 60fps
        private Color[] couleursPsychedeliques = new Color[] {
    Color.Magenta, Color.BlueViolet, Color.DeepPink,
    Color.Gold, Color.Lime, Color.Cyan, Color.Orange
};


        public FormulaireDuKrabsKrusty()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            InitialiserLeMondeAnanas();

            // PR�PARER LA MUSIQUE DE LA MER - WAHOOOOO!
            musiqueDuFondMarin = new ChefDOrchestreDuKrustyKrab();

            // JOUER LE G�N�RIQUE AU D�MARRAGE! �A VA �TRE SUPER COOL!
            Task.Run(() => musiqueDuFondMarin.JouerGeneriqueDeLeponge());

            this.minuteurDuMedusePatient.Start();
            this.Focus();
        }

        private void InitialiserLeMondeAnanas()
        {
            // Cr�ation des raquettes - AUSSI ROBUSTES QUE LES SPATULES DU CRABE CROUSTILLANT!
            raquetteDuBobGauche = new Rectangle(20, zoneDeBikiniBas.Height / 2 - 50, 15, 100);
            raquetteDuCarlooDroite = new Rectangle(zoneDeBikiniBas.Width - 35, zoneDeBikiniBas.Height / 2 - 50, 15, 100);

            // Cr�ation de la balle au centre - RONDE COMME UNE BELLE BULLE!
            balleDeMeduseJoyeuse = new Rectangle(
                zoneDeBikiniBas.Width / 2 - 10,
                zoneDeBikiniBas.Height / 2 - 10,
                20, 20);

            // G�N�RATION DES M�DUSES DANSANTES, GARY! REGARDE! DES M�DUSES!
            for (int i = 0; i < 12; i++)
            {
                medusesDansantes.Add(new Meduse(
                    al�atoireCommePatrick.Next(zoneDeBikiniBas.Width),
                    al�atoireCommePatrick.Next(zoneDeBikiniBas.Height),
                    al�atoireCommePatrick.Next(20, 40),
                    Color.FromArgb(
                        al�atoireCommePatrick.Next(150, 255),
                        al�atoireCommePatrick.Next(50, 150),
                        al�atoireCommePatrick.Next(150, 255),
                        150)
                ));
            }
        }

        private void zoneDeBikiniBas_Paint(object sender, PaintEventArgs e)
        {
            Graphics dessinateurPatrick = e.Graphics;
            dessinateurPatrick.SmoothingMode = SmoothingMode.AntiAlias;


            // FOND PSYCHEDELIQUE MAGIQUE - COMME LA FOIS O� MOI ET PATRICK SOMMES ALL�S DANS NOTRE BOX D'IMAGINATION !
            if (fondPsychedeliqueActif)
            {
                // Dessiner plusieurs cercles concentriques color�s qui tournent
                using (GraphicsPath cheminPsychedelique = new GraphicsPath())
                {
                    int nombreCercles = 8;
                    for (int i = 0; i < nombreCercles; i++)
                    {
                        // Angle de rotation qui d�pend du compteur
                        double angle = compteurCouleurPsychedelique / 10.0 + i * Math.PI / 4;

                        // Position du cercle qui tourne
                        int centreX = zoneDeBikiniBas.Width / 2 + (int)(Math.Sin(angle) * zoneDeBikiniBas.Width / 4);
                        int centreY = zoneDeBikiniBas.Height / 2 + (int)(Math.Cos(angle) * zoneDeBikiniBas.Height / 4);

                        // Taille du cercle (plus grand pour les premiers)
                        int taille = zoneDeBikiniBas.Width - i * 100;
                        if (taille < 100) taille = 100;

                        // Cercle qui tourne
                        Rectangle zoneColoree = new Rectangle(
                            centreX - taille / 2,
                            centreY - taille / 2,
                            taille, taille);

                        // Index de couleur qui change
                        int indexCouleur = (compteurCouleurPsychedelique / 5 + i) % couleursPsychedeliques.Length;

                        // Cr�er un d�grad� radial
                        cheminPsychedelique.AddEllipse(zoneColoree);
                        using (PathGradientBrush pinceauPsyche = new PathGradientBrush(cheminPsychedelique))
                        {
                            // La couleur centrale est celle de notre tableau
                            pinceauPsyche.CenterColor = Color.FromArgb(
                                50, // Transparence pour voir le jeu derri�re
                                couleursPsychedeliques[indexCouleur]);

                            // Le bord est transparent
                            pinceauPsyche.SurroundColors = new Color[] { Color.FromArgb(0, Color.White) };

                            // Dessiner le cercle color�
                            dessinateurPatrick.FillEllipse(pinceauPsyche, zoneColoree);
                        }

                        // R�initialiser le chemin pour le prochain cercle
                        cheminPsychedelique.Reset();
                    }
                }

                // Dessiner des spirales tournantes
                for (int i = 0; i < 3; i++)
                {
                    // Angle initial de la spirale
                    double angleSpiral = compteurCouleurPsychedelique / 15.0 + i * Math.PI * 2 / 3;
                    int indexCouleur = (compteurCouleurPsychedelique / 10 + i) % couleursPsychedeliques.Length;

                    // Dessiner la spirale
                    using (Pen spiralPen = new Pen(Color.FromArgb(100, couleursPsychedeliques[indexCouleur]), 4))
                    {
                        Point[] pointsSpirales = new Point[36];
                        for (int j = 0; j < 36; j++)
                        {
                            double angle = angleSpiral + j * Math.PI / 9;
                            int rayon = 20 + j * 15;
                            pointsSpirales[j] = new Point(
                                zoneDeBikiniBas.Width / 2 + (int)(Math.Cos(angle) * rayon),
                                zoneDeBikiniBas.Height / 2 + (int)(Math.Sin(angle) * rayon)
                            );
                        }

                        if (pointsSpirales.Length > 1)
                            dessinateurPatrick.DrawCurve(spiralPen, pointsSpirales, 0.5f);
                    }
                }

                // Texte "IMAGINATION" qui tourne autour du centre
                using (Font policePsychedelique = new Font("Comic Sans MS", 24, FontStyle.Bold))
                {
                    string texteImaginaire = "IMAGINATIOOOON";

                    for (int i = 0; i < 5; i++)
                    {
                        double angleTexte = compteurCouleurPsychedelique / 20.0 + i * Math.PI * 2 / 5;
                        int indexCouleur = (compteurCouleurPsychedelique / 15 + i) % couleursPsychedeliques.Length;

                        // Position du texte qui tourne
                        float texteX = zoneDeBikiniBas.Width / 2 + (float)(Math.Cos(angleTexte) * zoneDeBikiniBas.Width / 3) - 100;
                        float texteY = zoneDeBikiniBas.Height / 2 + (float)(Math.Sin(angleTexte) * zoneDeBikiniBas.Height / 3) - 20;

                        // Dessiner le texte avec une ombre
                        using (SolidBrush pinceauOmbre = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                        using (SolidBrush pinceauTexte = new SolidBrush(Color.FromArgb(180, couleursPsychedeliques[indexCouleur])))
                        {
                            // Ombre d'abord
                            dessinateurPatrick.DrawString(texteImaginaire, policePsychedelique, pinceauOmbre,
                                texteX + 3, texteY + 3);

                            // Puis le texte
                            dessinateurPatrick.DrawString(texteImaginaire, policePsychedelique, pinceauTexte,
                                texteX, texteY);
                        }
                    }
                }
            }


            // D'ABORD LES M�DUSES EN ARRI�RE-PLAN! OOOOOOOH!
            foreach (var meduse in medusesDansantes)
            {
                meduse.Dessiner(dessinateurPatrick);
            }

            // Apr�s le dessin des m�duses et avant le dessin des bonus:
            // DESSINER LES OBSTACLES DANGEREUX - COMME LES PI�GES DE PLANKTON!
            foreach (var obstacle in obstaclesSousMarins)
            {
                if (obstacle.EstActif)
                {
                    obstacle.Dessiner(dessinateurPatrick);
                }
            }

            // DESSINER LES BONUS MARINS - COMME DES CADEAUX SOUS-MARINS!
            foreach (var bonus in bonusMarins)
            {
                if (bonus.EstActif)
                {
                    bonus.Dessiner(dessinateurPatrick);
                }
            }

            // LA TRA�N�E MAGIQUE DE LA BALLE - COMME UNE �TOILE FILANTE DE PATRICK!
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

            // PARTICULES �CLABOUSSANTES - COMME LES BULLES DE MON BAIN!
            foreach (var particule in particulesEclaboussantes.ToArray())
            {
                particule.Dessiner(dessinateurPatrick);
            }

            // Ligne centrale pointill�e comme des bulles - AUSSI JOLIE QUE LA CRAVATE DE SQUIDWARD!
            using (Pen crayon = new Pen(Color.FromArgb(150, 255, 255, 255), 2))
            {
                crayon.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                dessinateurPatrick.DrawLine(crayon,
                    zoneDeBikiniBas.Width / 2, 0,
                    zoneDeBikiniBas.Width / 2, zoneDeBikiniBas.Height);
            }

            // Dessin des raquettes en couleur corail - COMME MES JOLIS SHORTS CARR�S!
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

            // V�RIFIER SI UN EFFET DOIT DESSINER LA BALLE � SA FA�ON - COMME MA MAISON ANANAS UNIQUE!
            bool balleDessineeParEffet = false;
            foreach (var effet in effetsActifs)
            {
                // L'effet dessine la balle � sa fa�on sp�ciale!
                effet.DessinerEffetSpecial(dessinateurPatrick, balleDeMeduseJoyeuse);
                balleDessineeParEffet = true;
                break;  // On prend juste le premier effet actif pour dessiner la balle
            }

            // Dessin normal de la balle SI AUCUN EFFET NE L'A FAIT - JUSTE AU CAS O�!
            if (!balleDessineeParEffet)
            {
                // Dessin de la balle avec un effet brillant - COMME MON SOURIRE �CLATANT!
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

                // Reflet sur la balle - BRILLANT COMME MA SPATULE PR�F�R�E!
                using (SolidBrush pinceau = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
                {
                    dessinateurPatrick.FillEllipse(pinceau,
                        balleDeMeduseJoyeuse.X + 5,
                        balleDeMeduseJoyeuse.Y + 3,
                        7, 5);
                }
            }

            // Animation sp�ciale quand un but est marqu� - UNE EXPLOSION DE JOIE!
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

            // Affichage du nom de l'effet actif - COMME UNE ANNONCE AU KRUSTY KRAB!
            if (dureeAffichageNomEffet > 0 && !string.IsNullOrEmpty(nomEffetEnCours))
            {
                using (Font police = new Font("Comic Sans MS", 16, FontStyle.Bold))
                using (SolidBrush pinceauTexte = new SolidBrush(Color.Yellow))
                using (SolidBrush pinceauOmbre = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    StringFormat format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    // Ombre du texte
                    dessinateurPatrick.DrawString(nomEffetEnCours, police, pinceauOmbre,
                        zoneDeBikiniBas.Width / 2 + 2,
                        zoneDeBikiniBas.Height / 4 + 2,
                        format);

                    // Texte principal
                    dessinateurPatrick.DrawString(nomEffetEnCours, police, pinceauTexte,
                        zoneDeBikiniBas.Width / 2,
                        zoneDeBikiniBas.Height / 4,
                        format);
                }
            }
        }

        private void minuteurDuMedusePatient_Tick(object sender, EventArgs e)
        {
            // MISE � JOUR DE L'EFFET PSYCH�D�LIQUE - COMME LA DANSE DE M�DUSES DANS MA T�TE !
            if (fondPsychedeliqueActif)
            {
                // Mettre � jour le compteur de couleur
                compteurCouleurPsychedelique += 3;
                if (compteurCouleurPsychedelique > 360) compteurCouleurPsychedelique = 0;

                // D�cr�menter la dur�e d'affichage
                dureeAffichagePsychedelique--;
                if (dureeAffichagePsychedelique <= 0)
                {
                    fondPsychedeliqueActif = false;
                }
            }
            else
            {
                // D�cr�menter le d�lai avant la prochaine apparition
                delaiProchainPsychedelique--;
                if (delaiProchainPsychedelique <= 0)
                {
                    // Temps al�atoire avant la prochaine apparition (entre 10 et 30 secondes)
                    delaiProchainPsychedelique = al�atoireCommePatrick.Next(600, 1800);

                    // Activer l'effet psych�d�lique pour 3-6 secondes
                    fondPsychedeliqueActif = true;
                    dureeAffichagePsychedelique = al�atoireCommePatrick.Next(180, 360);
                    compteurCouleurPsychedelique = al�atoireCommePatrick.Next(0, 360);

                    // Son sp�cial quand l'effet appara�t
                    Task.Run(() => musiqueDuFondMarin.JouerBipVictoire());
                }
            }


            // MUSIQUE EN BOUCLE - COMME UNE M�LODIE SANS FIN!
            if (jouerMusiqueEnBoucle && !musiqueDuFondMarin.EstEnTrainDeJouer)
            {
                Task.Run(() => musiqueDuFondMarin.JouerBipJoyeux());
            }

            // M�DUSES DANSANTES - ELLES FLOTTENT COMME MES R�VES!
            foreach (var meduse in medusesDansantes)
            {
                meduse.Deplacer(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
            }

            // Apr�s la mise � jour des m�duses:
            // MISE � JOUR DES OBSTACLES - COMME LES PLANS DIABOLIQUES DE PLANKTON!
            MettreAJourObstacles();

            // Animation de but - OH LA LA QUELLE F�TE!
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

            // MISE � JOUR DES BONUS ET EFFETS - COMME MES AVENTURES QUOTIDIENNES!
            MettreAJourBonus();
            MettreAJourEffets();

            // D�cr�menter le compteur d'affichage du nom d'effet
            if (dureeAffichageNomEffet > 0)
                dureeAffichageNomEffet--;

            // Mise � jour des particules - BULLES QUI �CLATENT!
            MettreAJourParticules();

            // Changement de couleur arc-en-ciel - COMME UN HAMBURGER P�T� DE CRABE MAGIQUE!
            if (effetArcEnCielMarin)
            {
                compteurRainbowPatrick += 5;
                if (compteurRainbowPatrick > 360) compteurRainbowPatrick = 0;

                couleurMagiqueDeLaBalle = CouleurHSVversRVB(compteurRainbowPatrick, 0.8f, 1.0f);
            }

            // Ajout de la position courante � la tra�n�e
            traineeCelesteBulle.Add(new PointF(
                balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2,
                balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2));

            // Limite la taille de la tra�n�e
            if (traineeCelesteBulle.Count > 15) traineeCelesteBulle.RemoveAt(0);

            // D�placement de la raquette gauche (joueur) - ALLEZ BOB, TU PEUX LE FAIRE!
            if (toucheHautAppuyee && raquetteDuBobGauche.Y > 0)
                raquetteDuBobGauche.Y -= vitesseRaquette;

            if (toucheBasAppuyee && raquetteDuBobGauche.Y + raquetteDuBobGauche.Height < zoneDeBikiniBas.Height)
                raquetteDuBobGauche.Y += vitesseRaquette;

            // Intelligence artificielle simple pour la raquette droite - CARLO EST RUS� COMME PLANKTON!
            DeplacementRaquetteCarlooDroite();

            // D�placement de la balle - ELLE FLOTTE COMME UNE M�DUSE!
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
                // Acc�l�ration progressive - PLUS VITE QUE SANDY �CUREUIL!
                if (Math.Abs(vitesseBalleMeduse_X) < 15)
                {
                    vitesseBalleMeduse_X = vitesseBalleMeduse_X < 0 ? vitesseBalleMeduse_X - 1 : vitesseBalleMeduse_X + 1;
                }
                CreerParticules(10);
                Task.Run(() => musiqueDuFondMarin.JouerBipRaquette());
            }

            // But marqu� � droite - OH NON BOB A PERDU!
            if (balleDeMeduseJoyeuse.X < 0)
            {
                scoreCarlooDroite++;
                ReinitialiserBalleMeduse();
                DemarrerAnimationBut(0);
                Task.Run(() => musiqueDuFondMarin.JouerBipDefaite());
            }
            // But marqu� � gauche - HOURRA POUR BOB!
            else if (balleDeMeduseJoyeuse.X > zoneDeBikiniBas.Width)
            {
                scoreBobGauche++;
                ReinitialiserBalleMeduse();
                DemarrerAnimationBut(zoneDeBikiniBas.Width);
                Task.Run(() => musiqueDuFondMarin.JouerBipVictoire());
            }

            // Mise � jour du score - LES CHIFFRES DE LA VICTOIRE!
            scorePatrickEtoile.Text = $"{scoreBobGauche} - {scoreCarlooDroite}";

            // Rafra�chissement de l'�cran - AUSSI RAFRA�CHISSANT QU'UN PLONGEON DANS L'OC�AN!
            zoneDeBikiniBas.Invalidate();
        }


        // Ajouter la m�thode pour g�rer les obstacles
        private void MettreAJourObstacles()
        {
            // Faire appara�tre un nouvel obstacle de temps en temps
            compteurProchainObstacle--;
            if (compteurProchainObstacle <= 0)
            {
                // Temps al�atoire avant le prochain obstacle (entre 5 et 15 secondes)
                compteurProchainObstacle = al�atoireCommePatrick.Next(300, 900);

                // Cr�er un obstacle al�atoire - COMME LES SURPRISES DANS L'OC�AN!
                AjouterObstacleAleatoire();
            }

            // V�rifier les collisions avec la balle
            foreach (var obstacle in obstaclesSousMarins.ToArray())
            {
                // Mettre � jour l'obstacle
                obstacle.Mettre_A_Jour();

                // G�rer la collision avec la balle
                obstacle.GererCollision(ref balleDeMeduseJoyeuse, ref vitesseBalleMeduse_X, ref vitesseBalleMeduse_Y);
            }

            // Supprimer les obstacles inactifs - COMME QUAND JE NETTOIE MON ANANAS!
            obstaclesSousMarins.RemoveAll(obstacle => !obstacle.EstActif);
        }

        private void AjouterObstacleAleatoire()
        {
            // Choisir un type d'obstacle al�atoire - COMME LES DANGERS DE LA MER!
            int typeObstacle = al�atoireCommePatrick.Next(2); // 0 ou 1 pour Plankton ou PateDeCrabe
            IObstacle nouvelObstacle = null;

            switch (typeObstacle)
            {
                case 0:
                    nouvelObstacle = new Plankton(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
                case 1:
                    nouvelObstacle = new PateDeCrabe(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
            }

            if (nouvelObstacle != null)
            {
                obstaclesSousMarins.Add(nouvelObstacle);
            }
        }
        private void MettreAJourBonus()
        {
            // Faire appara�tre un nouveau bonus de temps en temps
            compteurProchainBonus--;
            if (compteurProchainBonus <= 0)
            {
                // Temps al�atoire avant le prochain bonus (entre 10 et 20 secondes)
                compteurProchainBonus = al�atoireCommePatrick.Next(600, 1200);

                // Cr�er un bonus al�atoire - COMME LES CADEAUX D'ANNIVERSAIRE SURPRISES!
                AjouterBonusAleatoire();
            }

            // V�rifier si la balle collecte un bonus - COMME QUAND JE TROUVE UN SOU PAR TERRE!
            foreach (var bonus in bonusMarins.ToArray())
            {
                if (bonus.EstActif && balleDeMeduseJoyeuse.IntersectsWith(bonus.Position))
                {
                    // Collecter le bonus et appliquer son effet
                    IEffet nouvelEffet = bonus.Collecter();

                    // Supprimer les effets pr�c�dents (un seul � la fois)
                    effetsActifs.Clear();

                    // Ajouter le nouvel effet
                    effetsActifs.Add(nouvelEffet);

                    // Afficher le nom de l'effet - COMME QUAND JE CRIE "JE SUIS PR���T!"
                    nomEffetEnCours = nouvelEffet.NomEffet;
                    dureeAffichageNomEffet = 120; // Afficher pendant 2 secondes (120 frames � 60fps)

                    // Jouer un son joyeux - COMME QUAND JE TROUVE UN DOLLAR!
                    Task.Run(() => musiqueDuFondMarin.JouerBipVictoire());
                }
            }

            // Supprimer les bonus inactifs - COMME QUAND JE NETTOIE MON ANANAS!
            bonusMarins.RemoveAll(bonus => !bonus.EstActif);
        }

        private void MettreAJourEffets()
        {
            // Mettre � jour et appliquer tous les effets actifs
            foreach (var effet in effetsActifs.ToArray())
            {
                effet.MettreAJour();

                // Appliquer l'effet � la balle
                effet.AppliquerEffet(ref balleDeMeduseJoyeuse, ref vitesseBalleMeduse_X, ref vitesseBalleMeduse_Y);

                // Supprimer les effets termin�s
                if (effet.TempsRestant <= 0)
                {
                    effetsActifs.Remove(effet);

                    // R�initialiser la taille de la balle si n�cessaire
                    if (balleDeMeduseJoyeuse.Width != 20 || balleDeMeduseJoyeuse.Height != 20)
                    {
                        // Recentrer la balle pour �viter les t�l�portations
                        int centreX = balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2;
                        int centreY = balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2;

                        balleDeMeduseJoyeuse.Width = 20;
                        balleDeMeduseJoyeuse.Height = 20;

                        balleDeMeduseJoyeuse.X = centreX - 10;
                        balleDeMeduseJoyeuse.Y = centreY - 10;
                    }

                    // Annoncer que l'effet est termin�
                    nomEffetEnCours = "Effet termin�!";
                    dureeAffichageNomEffet = 60; // 1 seconde
                }
            }
        }

        private void AjouterBonusAleatoire()
        {
            // Choisir un type de bonus al�atoire - COMME LES SAVEURS DE GLACE CHEZ GOOFY GOOBER!
            int typeBonus = al�atoireCommePatrick.Next(7); // 0 � 6 pour 7 types de bonus
            IBonus nouveauBonus = null;

            switch (typeBonus)
            {
                case 0:
                    nouveauBonus = new BonusImagination(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
                case 1:
                    nouveauBonus = new BonusPatrick(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
                case 2:
                    nouveauBonus = new BonusPooognon(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
                case 3:
                    nouveauBonus = new BonusEtoileDeMer(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
                case 4:
                    nouveauBonus = new BonusAutoEcole(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
                case 5:
                    nouveauBonus = new BonusMeduse(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
                case 6:
                    nouveauBonus = new BonusGary(zoneDeBikiniBas.Width, zoneDeBikiniBas.Height);
                    break;
            }

            if (nouveauBonus != null)
            {
                bonusMarins.Add(nouveauBonus);
            }
        }

        private void DemarrerAnimationBut(int position)
        {
            animationButEnCours = true;
            compteurAnimationBut = 0;
            zoneFeteVictoire = new Rectangle(
                position == 0 ? 0 : zoneDeBikiniBas.Width - 20,
                zoneDeBikiniBas.Height / 2 - 10,
                20, 20);

            // EXPLOSION DE PARTICULES - COMME LA JOIE DANS MON C�UR!
            for (int i = 0; i < 50; i++)
            {
                float angle = (float)(al�atoireCommePatrick.NextDouble() * Math.PI * 2);
                float vitesse = al�atoireCommePatrick.Next(3, 10);

                particulesEclaboussantes.Add(new Particule(
                    position == 0 ? 10 : zoneDeBikiniBas.Width - 10,
                    zoneDeBikiniBas.Height / 2,
                    (float)Math.Cos(angle) * vitesse,
                    (float)Math.Sin(angle) * vitesse,
                    Color.FromArgb(
                        al�atoireCommePatrick.Next(150, 255),
                        al�atoireCommePatrick.Next(150, 255),
                        al�atoireCommePatrick.Next(150, 255)),
                    al�atoireCommePatrick.Next(3, 8),
                    al�atoireCommePatrick.Next(30, 60)
                ));
            }
        }

        private void DeplacementRaquetteCarlooDroite()
        {
            // L'IA suit la balle mais avec un peu de d�lai - CARLO EST PRESQUE AUSSI MALIN QUE MOI!
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

            // Changement al�atoire de la direction - IMPR�VISIBLE COMME PLANKTON!
            vitesseBalleMeduse_X = al�atoireCommePatrick.Next(0, 2) == 0 ? -5 : 5;
            vitesseBalleMeduse_Y = al�atoireCommePatrick.Next(-3, 4);
        }

        private void CreerParticules(int nombre)
        {
            // Cr�ation de particules lors des rebonds - SPLASH SPLASH SPLASH!
            for (int i = 0; i < nombre; i++)
            {
                float angle = (float)(al�atoireCommePatrick.NextDouble() * Math.PI * 2);
                float vitesse = al�atoireCommePatrick.Next(2, 6);

                particulesEclaboussantes.Add(new Particule(
                    balleDeMeduseJoyeuse.X + balleDeMeduseJoyeuse.Width / 2,
                    balleDeMeduseJoyeuse.Y + balleDeMeduseJoyeuse.Height / 2,
                    (float)Math.Cos(angle) * vitesse,
                    (float)Math.Sin(angle) * vitesse,
                    couleurMagiqueDeLaBalle,
                    al�atoireCommePatrick.Next(2, 5),
                    al�atoireCommePatrick.Next(10, 30)
                ));
            }
        }

        private void MettreAJourParticules()
        {
            // Mise � jour des particules - COMME DES BULLES QUI DANSENT!
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
                // TOUCHE M POUR D�MARRER/ARR�TER LA MUSIQUE EN BOUCLE!
                jouerMusiqueEnBoucle = !jouerMusiqueEnBoucle;
                if (jouerMusiqueEnBoucle)
                {
                    Task.Run(() => musiqueDuFondMarin.JouerGeneriqueDeLeponge());
                }
            }
            // TOUCHE B POUR CR�ER UN BONUS IMM�DIATEMENT - POUR LE TEST!
            else if (e.KeyCode == Keys.B)
            {
                AjouterBonusAleatoire();
            }
            else if (e.KeyCode == Keys.O)
            {
                AjouterObstacleAleatoire();
            }
            // TOUCHE P POUR ACTIVER L'EFFET PSYCH�D�LIQUE - COMME QUAND J'APPUIE SUR LE BOUTON DE MON IMAGINATION !
            else if (e.KeyCode == Keys.P)
            {
                fondPsychedeliqueActif = true;
                dureeAffichagePsychedelique = 300; // 5 secondes
                compteurCouleurPsychedelique = al�atoireCommePatrick.Next(0, 360);
                Task.Run(() => musiqueDuFondMarin.JouerBipVictoire());
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