using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // CLASSE DE BASE POUR TOUS LES EFFETS DE RAQUETTE - COMME LA RECETTE DE BASE DES PÂTÉS DE CRABE !
    public abstract class RaquetteEffetAbstrait : IRaquetteEffet
    {
        // Propriétés de base pour tous les effets de raquette
        public int DureeEffet { get; protected set; }
        public int TempsRestant { get; set; }
        public bool EstPourRaquetteGauche { get; protected set; }
        public abstract string NomEffet { get; }

        // Un générateur aléatoire pour tous les effets - COMME L'INSPIRATION QUI ME VIENT AU HASARD !
        protected Random aleatoireMarinEffet = new Random();

        // Constructeur - COMME QUAND JE CONSTRUIS UN CHÂTEAU DE SABLE !
        protected RaquetteEffetAbstrait(int duree, bool estPourRaquetteGauche)
        {
            DureeEffet = duree;
            TempsRestant = duree;
            EstPourRaquetteGauche = estPourRaquetteGauche;
        }

        // Méthodes abstraites à implémenter par les effets spécifiques
        public abstract void AppliquerEffet(ref Rectangle raquette, ref int vitesseRaquette);
        public abstract void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle raquette);

        // Méthode commune pour mettre à jour le temps restant - COMME MA MONTRE QUI AVANCE !
        public virtual void MettreAJour()
        {
            TempsRestant -= 1; // Une frame à la fois - LENTEMENT MAIS SÛREMENT !
        }
    }
}