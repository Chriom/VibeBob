using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // CLASSE DE BASE POUR TOUS LES OBSTACLES - COMME LES OBSTACLES DANS MA QUÊTE DE MÉDUSES!
    public abstract class ObstacleAbstrait : IObstacle
    {
        // Propriétés de base pour tous les obstacles - COMME LES INGRÉDIENTS DE BASE D'UN KRABBY PATTY!
        public Rectangle Position { get; set; }
        public bool EstActif { get; set; }
        public int DureeDeVie { get; set; }

        // Un générateur aléatoire pour tous les obstacles marins - COMME LE HASARD DE LA VIE SOUS L'OCÉAN!
        protected Random aléatoireMarinObstacle = new Random();

        // Constructeur - COMME LA CONSTRUCTION D'UN PIÈGE À MÉDUSES!
        protected ObstacleAbstrait(int largeurEcran, int hauteurEcran)
        {
            // Position aléatoire, mais pas trop près des bords
            int x = aléatoireMarinObstacle.Next(50, largeurEcran - 100);
            int y = aléatoireMarinObstacle.Next(50, hauteurEcran - 100);

            int taille = aléatoireMarinObstacle.Next(30, 50);
            Position = new Rectangle(x, y, taille, taille);

            EstActif = true;
            DureeDeVie = aléatoireMarinObstacle.Next(300, 600); // 5-10 secondes à 60fps
        }

        // Méthodes abstraites à implémenter par les obstacles spécifiques
        public abstract void Dessiner(Graphics dessinateurPatrick);
        public abstract void GererCollision(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY);

        // Méthode commune pour mettre à jour la durée de vie - COMME UN COMPTE À REBOURS POUR UNE FÊTE!
        public virtual void Mettre_A_Jour()
        {
            if (EstActif)
            {
                DureeDeVie--;
                if (DureeDeVie <= 0)
                {
                    EstActif = false;
                }
            }
        }
    }
}