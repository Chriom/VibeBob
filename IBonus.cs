using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // INTERFACE POUR LES BONUS MARINS - COMME LES TRÉSORS CACHÉS DANS L'OCÉAN!
    public interface IBonus
    {
        // Position du bonus - COMME L'EMPLACEMENT DE MON ANANAS!
        Rectangle Position { get; set; }

        // Est-ce que le bonus est actif (visible) - COMME QUAND JE SUIS À L'HEURE AU TRAVAIL!
        bool EstActif { get; set; }

        // Dessiner le bonus - COMME JE DESSINE DES BURGERS KRABBY PATTY!
        void Dessiner(Graphics dessinateurPatrick);

        // Ce qui se passe quand on collecte le bonus - COMME QUAND JE TROUVE UNE MÉDUSE!
        IEffet Collecter();

        // Nom du bonus pour l'affichage - COMME LE NOM DE MA MÉDUSE DE COMPAGNIE!
        string NomBonus { get; }
    }
}