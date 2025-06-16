using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // CLASSE DE BASE POUR TOUS LES BONUS - COMME LES TRÉSORS AU FOND DE L'OCÉAN!
    public abstract class BonusAbstrait : IBonus
    {
        // Propriétés de base pour tous les bonus
        public Rectangle Position { get; set; }
        public bool EstActif { get; set; }
        public abstract string NomBonus { get; }

        // Durée typique des effets - COMME MA PATIENCE QUAND J'ATTENDS UN KRABBY PATTY!
        protected int dureeEffetStandard = 10000; // 10 secondes

        // Générateur aléatoire pour tous les bonus - COMME LES SURPRISES DANS MA VIE!
        protected static Random aléatoireBonusMarin = new Random();

        // Constructeur avec position aléatoire - COMME QUAND JE ME RÉVEILLE N'IMPORTE OÙ!
        protected BonusAbstrait(int largeurEcran, int hauteurEcran)
        {
            // Position aléatoire mais pas trop près des bords
            int x = aléatoireBonusMarin.Next(50, largeurEcran - 70);
            int y = aléatoireBonusMarin.Next(50, hauteurEcran - 70);

            Position = new Rectangle(x, y, 30, 30);
            EstActif = true;
        }

        // Méthodes abstraites à implémenter par les bonus spécifiques
        public abstract void Dessiner(Graphics dessinateurPatrick);
        public abstract IEffet Collecter();
    }
}