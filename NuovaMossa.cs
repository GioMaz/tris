using System;
using System.Collections.Generic;

namespace tris
{
    public class NuovaMossa
    {
        public int[,] Configurazione { get; set; }
        public int Giocatore { get; set; }
        public Nodo NodoSimile;

        public int[,] DimmiConfigurazione(Nodo Radice)
        {
            NodoSimile = Radice.CercaConfigurazione(Configurazione);
            if (NodoSimile.Equals(new Nodo()))
            {
                Console.WriteLine("NON TROVATO");
                return FiglioRandom().Configurazione;
            }

            // NUOVO
            else if (NodoSimile.HaFigliVincenti())
            {
                NodoSimile.PrintFigli();
                return FiglioVincente().Configurazione;
            }
            else if (!NodoSimile.HaTuttiRami())
            {
                NodoSimile.PrintFigli();
                return FiglioRandom().Configurazione;
            }
            else
            {
                NodoSimile.PrintFigli();
                return FiglioMigliore().Configurazione;
            }

            // VECCHIO
            // Nodo NodoVincente = FiglioVincente(true);
            // if (NodoVincente.Equals(new Nodo()))
            // {
            //     NodoVincente = FiglioRandom();
            // }
            // if (NodoVincente.Equals(new Nodo()))
            // {
            //     NodoVincente = FiglioVincente(false);
            // }
            // return NodoVincente.Configurazione;
        }

        public Nodo FiglioRandom() // SERVE PER DIVERSIFICARE L'APPRENDIMENTO
        {
            List<int[,]> ConfigurazioniPossibili = new List<int[,]>();
            for (int i = 0; i < Configurazione.Length; i++)
            {
                int[,] NuovaConfigurazione = Configurazione.Clone() as int[,];
                if (NuovaConfigurazione[i/3, i%3] == 0)
                {
                    NuovaConfigurazione[i/3, i%3] = Giocatore;
                    if (!NodoSimile.IsFiglio(NuovaConfigurazione))
                    {
                        ConfigurazioniPossibili.Add(NuovaConfigurazione);
                    }
                }
            }
            if (ConfigurazioniPossibili.Count != 0)
            {
                Random Rand = new Random();
                int NRand = Rand.Next(0, ConfigurazioniPossibili.Count);
                return new Nodo() {Configurazione = ConfigurazioniPossibili[NRand]};
            }
            else
            {
                return new Nodo();
            }
        }

        // ritorna nodo figlio con punteggio più alto
        // maggioreDiZero == true -> n.Punteggio >= 0
        // maggioreDiZero == false -> n.Punteggio > (tutti altri n.Punteggio)

        // public Nodo FiglioVincente(bool maggioreDiZero)
        // {
        //     Nodo NodoVincente = new Nodo();
        //     foreach (Nodo n in NodoSimile.ListaFigli)
        //     {
        //         if (maggioreDiZero && n.Punteggio >= 0 && (n.Punteggio > NodoVincente.Punteggio || NodoVincente.Punteggio == null))
        //         {
        //             NodoVincente = new Nodo() {Punteggio = n.Punteggio, Configurazione = n.Configurazione.Clone() as int[,]};
        //         }
        //         else if (!maggioreDiZero && (n.Punteggio > NodoVincente.Punteggio || NodoVincente.Punteggio == null))
        //         {
        //             NodoVincente = new Nodo() {Punteggio = n.Punteggio, Configurazione = n.Configurazione.Clone() as int[,]};
        //         }
        //     }
        //     return NodoVincente;
        // }

        public Nodo FiglioVincente()
        {
            Nodo Vincente = new Nodo();
            foreach (Nodo n in NodoSimile.ListaFigli)
            {
                if (Vincente.Equals(new Nodo()) || n.Punteggio >= 0)
                {
                    Vincente = n;
                }
            }
            return Vincente;
        }

        public Nodo FiglioMigliore()
        {
            Nodo Migliore = new Nodo();
            foreach (Nodo n in NodoSimile.ListaFigli)
            {
                if (Migliore.Equals(new Nodo()) || n.Punteggio > Migliore.Punteggio)
                {
                     Migliore = n;
                }
            }
            return Migliore;
        }
    }
}
