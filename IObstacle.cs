using System.Drawing;

namespace SuperMegaJeuDuPatrickPong
{
    // INTERFACE POUR LES OBSTACLES SOUS-MARINS - COMME LES DANGERS DE L'OCÉAN!
    public interface IObstacle
    {
        // Position de l'obstacle - COMME L'EMPLACEMENT DE PLANKTON DANS SON RESTAURANT RATÉ!
        Rectangle Position { get; set; }

        // Est-ce que l'obstacle est actif (visible) - COMME QUAND PLANKTON PRÉPARE UN PLAN DIABOLIQUE!
        bool EstActif { get; set; }

        // Dessiner l'obstacle - COMME UN DESSIN DE GARY!
        void Dessiner(Graphics dessinateurPatrick);

        // Mettre à jour l'obstacle - COMME LA MISE À JOUR DU MENU DU KRUSTY KRAB!
        void Mettre_A_Jour();

        // Ce qui se passe quand la balle touche l'obstacle - COMME UN COUP DE SPATULE SUR UN PÂTÉ!
        void GererCollision(ref Rectangle balleDeMeduseJoyeuse, ref int vitesseX, ref int vitesseY);
    }
}