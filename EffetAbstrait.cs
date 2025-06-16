using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // CLASSE DE BASE POUR TOUS LES EFFETS - COMME LES RECETTES DE BASE DU KRUSTY KRAB!
    public abstract class EffetAbstrait : IEffet
    {
        // Propriétés de base pour tous les effets - COMME LES INGRÉDIENTS DE BASE D'UN KRABBY PATTY!
        public int DureeEffet { get; protected set; }
        public int TempsRestant { get; set; }
        public abstract string NomEffet { get; }

        // Un générateur aléatoire pour tous les effets marins - COMME LE HASARD DE LA VIE SOUS L'OCÉAN!
        protected Random aléatoireMarinBonus = new Random();

        // Constructeur - COMME LA CONSTRUCTION DE MON CHÂTEAU DE SABLE!
        protected EffetAbstrait(int duree)
        {
            DureeEffet = duree;
            TempsRestant = duree;
        }

        // Méthodes abstraites à implémenter par les effets spécifiques
        public abstract void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY);
        public abstract void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse);

        // Méthode commune pour mettre à jour le temps restant - COMME MA MONTRE QUI AVANCE TOUJOURS!
        public virtual void MettreAJour()
        {
            TempsRestant -= 16; // 16ms par frame à 60fps - RAPIDE COMME UNE MÉDUSE QUI ME PIQUE!
        }
    }
}