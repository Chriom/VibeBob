using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // INTERFACE POUR LES EFFETS DE RAQUETTE - COMME LES SUPER POUVOIRS DE MERMAID MAN ET BARNACLE BOY !
    public interface IRaquetteEffet
    {
        // Nom de l'effet - COMME MON NOM SUR MON BADGE DU KRUSTY KRAB !
        string NomEffet { get; }

        // Durée de l'effet en frames - COMME MA PATIENCE QUAND J'ATTENDS UN PÂTÉ DE CRABE !
        int DureeEffet { get; }

        // Temps restant avant la fin de l'effet - COMME MON COMPTE À REBOURS AVANT LA FIN DU TRAVAIL !
        int TempsRestant { get; set; }

        // Est-ce que l'effet est appliqué à la raquette gauche (joueur) - COMME MOI, BOB L'ÉPONGE !
        bool EstPourRaquetteGauche { get; }

        // Appliquer l'effet à la raquette - COMME QUAND JE TRANSFORME UN SIMPLE PAIN EN DÉLICIEUX PÂTÉ !
        void AppliquerEffet(ref Rectangle raquette, ref int vitesseRaquette);

        // Dessiner l'effet spécial - COMME QUAND JE DÉCORE MON ANANAS !
        void DessinerEffetSpecial(Graphics dessinateurPatrick, Rectangle raquette);

        // Mettre à jour l'effet - COMME QUAND JE RAFRAÎCHIS MA COLLECTION DE MÉDUSES !
        void MettreAJour();
    }
}