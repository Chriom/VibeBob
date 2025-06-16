using System;
using System.Collections.Generic;
using System.Threading;

namespace SuperMegaJeuDuPatrickPong
{
    // NOUVEAU! MUSIQUE DU GÉNÉRIQQUE AVEC DES BIPS - COMME AU KRUSTY KRAB!
    public class ChefDOrchestreDuKrustyKrab
    {
        // Les notes du Krusty Krab - COMME LA MUSIQUE QUE JOUE SQUIDWARD SUR SA CLARINETTE!
        private readonly Dictionary<string, int> notesDuBikiniBas = new Dictionary<string, int>
        {
            {"C4", 262}, {"C#4", 277}, {"D4", 294}, {"D#4", 311},
            {"E4", 330}, {"F4", 349}, {"F#4", 370}, {"G4", 392},
            {"G#4", 415}, {"A4", 440}, {"A#4", 466}, {"B4", 494},
            {"C5", 523}, {"C#5", 554}, {"D5", 587}, {"D#5", 622},
            {"E5", 659}, {"F5", 698}, {"F#5", 740}, {"G5", 784},
            {"G#5", 831}, {"A5", 880}, {"A#5", 932}, {"B5", 988}
        };

        // Un flag pour savoir si une mélodie est en cours - COMME MON CERVEAU EN PLEINE RÉFLEXION!
        public bool EstEnTrainDeJouer { get; private set; } = false;

        // Jouer une note - COMME UN PETIT POISSON QUI CHANTE!
        public void JouerNote(string note, int duree)
        {
            if (notesDuBikiniBas.TryGetValue(note, out int frequence))
            {
                Console.Beep(frequence, duree);
            }
            else
            {
                // Si c'est une pause, on attend juste - ATTENDONS PATRICK QUI RÉFLÉCHIT...
                Thread.Sleep(duree);
            }
        }

        // Le VRAI générique complet - QUI VIT DANS UN ANANAS DANS LA MER?
        public void JouerGeneriqueDeLeponge()
        {
            EstEnTrainDeJouer = true;

            try
            {
                int tempo = 150; // Vitesse de base pour des notes plus dynamiques!

                // Intro Captain - "VOUS ÊTES PRÊTS, LES ENFANTS?"
                JouerNote("G4", tempo); JouerNote("E5", tempo); JouerNote("D5", tempo);
                JouerNote("G4", tempo * 2); Thread.Sleep(tempo);

                // Enfants - "OUI, CAPITAINE!"
                JouerNote("G4", tempo); JouerNote("E5", tempo); JouerNote("G5", tempo * 2);

                // Captain - "J'AI RIEN ENTENDU!"
                JouerNote("E5", tempo); JouerNote("G5", tempo); JouerNote("A5", tempo);
                JouerNote("G5", tempo); JouerNote("E5", tempo); JouerNote("C5", tempo * 2);

                // Enfants - "OUI, CAPITAINE!!!"
                JouerNote("G4", tempo); JouerNote("E5", tempo); JouerNote("G5", tempo * 2);
                Thread.Sleep(tempo * 2);

                // "QUI VIT DANS UN ANANAS DANS LA MER?"
                JouerNote("C5", tempo); JouerNote("F5", tempo); JouerNote("G5", tempo);
                JouerNote("C6", tempo * 2); JouerNote("A5", tempo); JouerNote("G5", tempo);
                JouerNote("F5", tempo * 2); Thread.Sleep(tempo / 2);

                // "BOB L'ÉPONGE CARRÉE!"
                JouerNote("F5", tempo); JouerNote("F5", tempo); JouerNote("F5", tempo);
                JouerNote("G5", tempo); JouerNote("A5", tempo); JouerNote("G5", tempo);
                JouerNote("F5", tempo); JouerNote("E5", tempo); JouerNote("D5", tempo);
                JouerNote("C5", tempo * 2); JouerNote("C5", tempo);
                JouerNote("D5", tempo); JouerNote("F5", (int)(tempo * 2.5));
                Thread.Sleep((int)(tempo * 1.5));

                // "ABSORBANT ET JAUNE ET POREUX EST-IL!"
                JouerNote("C5", tempo); JouerNote("F5", tempo); JouerNote("G5", tempo);
                JouerNote("C6", tempo * 2); JouerNote("A5", tempo); JouerNote("G5", tempo);
                JouerNote("F5", tempo * 2); Thread.Sleep(tempo / 2);

                // "BOB L'ÉPONGE CARRÉE!"
                JouerNote("F5", tempo); JouerNote("F5", tempo); JouerNote("F5", tempo);
                JouerNote("G5", tempo); JouerNote("A5", tempo); JouerNote("G5", tempo);
                JouerNote("F5", tempo); JouerNote("E5", tempo); JouerNote("D5", tempo);
                JouerNote("C5", tempo * 2); JouerNote("C5", tempo);
                JouerNote("D5", tempo); JouerNote("F5", (int)(tempo * 2.5));
                Thread.Sleep((int)(tempo * 1.5));

                // "SI DES SOTTISES SOUS-MARINES SONT TON SOUHAIT..."
                JouerNote("C5", tempo); JouerNote("F5", tempo); JouerNote("G5", tempo);
                JouerNote("C6", tempo * 2); JouerNote("A5", tempo); JouerNote("G5", tempo);
                JouerNote("F5", tempo * 2); Thread.Sleep(tempo / 2);

                // "BOB L'ÉPONGE CARRÉE!"
                JouerNote("F5", tempo); JouerNote("F5", tempo); JouerNote("F5", tempo);
                JouerNote("G5", tempo); JouerNote("A5", tempo); JouerNote("G5", tempo);
                JouerNote("F5", tempo); JouerNote("E5", tempo); JouerNote("D5", tempo);

                // Finale - "ALORS JETTE-TOI SUR LE PONT ET AGITE-TOI COMME UN POISSON!"
                JouerNote("C5", tempo); JouerNote("D5", (int)(tempo * 1.3));
                JouerNote("F5", tempo); JouerNote("E5", tempo);
                JouerNote("D5", tempo); JouerNote("C5", tempo * 2);
                JouerNote("D5", tempo); JouerNote("E5", (int)(tempo * 1.3));

                // "BOB L'ÉPONGE CARRÉÉÉÉÉE!"
                JouerNote("F5", tempo); JouerNote("G5", tempo);
                JouerNote("A5", tempo); JouerNote("G5", tempo);
                JouerNote("F5", tempo); JouerNote("E5", tempo);
                JouerNote("D5", tempo); JouerNote("C5", tempo * 3);

                // Rire du pirate! MOUAHAHA!
                JouerNote("G5", tempo / 2); JouerNote("F5", tempo / 2);
                JouerNote("G5", tempo / 2); JouerNote("F5", tempo / 2);
                JouerNote("G5", tempo / 2); JouerNote("A5", tempo / 2);
                JouerNote("G5", tempo); JouerNote("C5", tempo * 3);

                // Flûte finale
                JouerNote("F5", tempo / 2); JouerNote("F5", tempo / 2);
                JouerNote("F5", tempo / 2); JouerNote("F5", tempo / 2);
                JouerNote("F5", tempo / 2); JouerNote("G5", tempo / 2);
                JouerNote("F5", tempo * 3);
            }
            finally
            {
                EstEnTrainDeJouer = false;
            }
        }

        // Bip joyeux pour quand la balle rebondit sur une raquette - SPLISH!
        public void JouerBipRaquette()
        {
            EstEnTrainDeJouer = true;
            try
            {
                Console.Beep(659, 50); // Mi (E5) - HAUT COMME MES ESPOIRS!
                Console.Beep(784, 50); // Sol (G5) - BRILLANT COMME MON SOURIRE!
            }
            finally
            {
                EstEnTrainDeJouer = false;
            }
        }

        // Le reste des méthodes est inchangé...
        public void JouerBipRebond()
        {
            EstEnTrainDeJouer = true;
            try
            {
                Console.Beep(523, 30); // Do (C5) - COURT COMME L'ATTENTION DE PATRICK!
            }
            finally
            {
                EstEnTrainDeJouer = false;
            }
        }

        public void JouerBipVictoire()
        {
            EstEnTrainDeJouer = true;
            try
            {
                Console.Beep(523, 100); // Do (C5)
                Console.Beep(587, 100); // Ré (D5)
                Console.Beep(659, 100); // Mi (E5)
                Console.Beep(784, 300); // Sol (G5) - AUSSI HAUT QUE MA JOIE!
                Console.Beep(659, 100); // Mi (E5)
                Console.Beep(784, 500); // Sol (G5) - TELLEMENT CONTENT!
            }
            finally
            {
                EstEnTrainDeJouer = false;
            }
        }

        public void JouerBipDefaite()
        {
            EstEnTrainDeJouer = true;
            try
            {
                Console.Beep(494, 100); // Si (B4)
                Console.Beep(466, 100); // La# (A#4)
                Console.Beep(440, 100); // La (A4) - COMME LES LARMES DE BOB!
                Console.Beep(392, 500); // Sol (G4) - AUSSI BAS QUE MES CHAUSSETTES!
            }
            finally
            {
                EstEnTrainDeJouer = false;
            }
        }

        public void JouerBipJoyeux()
        {
            EstEnTrainDeJouer = true;
            try
            {
                JouerNote("C4", 150); // Do
                JouerNote("E4", 150); // Mi
                JouerNote("G4", 150); // Sol
                JouerNote("C5", 300); // Do - COMME UN RIRE DE MÉDUSE!
            }
            finally
            {
                EstEnTrainDeJouer = false;
            }
        }
    }
}