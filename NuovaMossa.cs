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
            if (NodoSimile == null)
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
        }

        public Nodo FiglioRandom() // SERVE PER DIVERSIFICARE L'APPRENDIMENTO
        {
            List<int[,]> configurazioniPossibili = new List<int[,]>();
            for (int i = 0; i < Configurazione.Length; i++)
            {
                int[,] NuovaConfigurazione = Configurazione.Clone() as int[,];
                if (NuovaConfigurazione[i/3, i%3] == 0)
                {
                    NuovaConfigurazione[i/3, i%3] = Giocatore;
                    if (NodoSimile == null ||
                            !NodoSimile.IsFiglio(NuovaConfigurazione))
                    {
                        configurazioniPossibili.Add(NuovaConfigurazione);
                    }
                }
            }
            if (configurazioniPossibili.Count != 0)
            {
                Random Rand = new Random();
                int NRand = Rand.Next(0, configurazioniPossibili.Count);
                return new Nodo()
                {
                    Configurazione = configurazioniPossibili[NRand]
                };
            }
            else
            {
                return null;
            }
        }

        public Nodo FiglioVincente()
        {
            Nodo vincente = null;
            foreach (Nodo n in NodoSimile.ListaFigli)
            {
                if (vincente == null || n.Punteggio >= 0)
                {
                    vincente = n;
                }
            }
            return vincente;
        }

        public Nodo FiglioMigliore()
        {
            Nodo migliore = null;
            foreach (Nodo n in NodoSimile.ListaFigli)
            {
                if (migliore == null || n.Punteggio > migliore.Punteggio)
                {
                     migliore = n;
                }
            }
            return migliore;
        }
    }
}
