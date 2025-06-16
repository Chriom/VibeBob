using System;
using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // INTERFACE POUR LES EFFETS SOUS-MARINS - COMME LES TOURS DE MAGIE DE BOB!
    public interface IEffet
    {
        // Durée de l'effet en millisecondes - COMME LE TEMPS QUE JE PRENDS POUR ME PRÉPARER LE MATIN!
        int DureeEffet { get; }

        // Temps restant avant que l'effet disparaisse - COMME MON ENTHOUSIASME QUI NE S'ÉPUISE JAMAIS!
        int TempsRestant { get; set; }

        // Appliquer l'effet à la balle - COMME LA SAUCE SECRÈTE SUR UN PÂTÉ DE CRABE!
        void AppliquerEffet(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY);

        // Dessiner l'effet spécial - COMME LES BULLES QUI SORTENT DE MES TROUS!
        void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle balleDeMeduseJoyeuse);

        // Mettre à jour l'effet - COMME QUAND JE CHANGE MES SOUS-VÊTEMENTS!
        void MettreAJour();

        // Nom de l'effet pour l'affichage - COMME MON NOM ÉCRIT SUR MON BADGE AU KRUSTY KRAB!
        string NomEffet { get; }
    }
}