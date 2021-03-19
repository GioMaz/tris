using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace tris
{
    public class NuovaMossa
    {
        public int[,] Configurazione { get; set; }
        public int Giocatore { get; set; }
        public Nodo NodoSimile;

        public int[,] DimmiConfigurazioneVincente(Nodo Radice)
        {
            NodoSimile = Radice.CercaConfigurazione(Configurazione);
            if (NodoSimile.Equals(new Nodo()))
            {
                Console.WriteLine("NON TROVATO");
                return DimmiFiglioRandom(Giocatore).Configurazione;
            }
            Nodo NodoVincente = DimmiFiglioVincente(true);
            if (NodoVincente.Equals(new Nodo()))
            {
                NodoVincente = DimmiFiglioRandom(Giocatore);
            }
            if (NodoVincente.Equals(new Nodo()))
            {
                NodoVincente = DimmiFiglioVincente(false);
            }
            return NodoVincente.Configurazione;
        }

        // ***
        // public Nodo DimmiFiglioRandom(int giocatore, bool diversoDaFigli)
        public Nodo DimmiFiglioRandom(int giocatore) // SERVE PER DIVERSIFICARE L'APPRENDIMENTO
        {
            List<int[,]> ConfigurazioniPossibili = new List<int[,]>();
            for (int i = 0; i < Configurazione.Length; i++)
            {
                int[,] NuovaConfigurazione = Configurazione.Clone() as int[,];
                if (NuovaConfigurazione[i/3, i%3] == 0)
                {
                    NuovaConfigurazione[i/3, i%3] = giocatore;
                    // ***
                    // if (diversoDaFigli && !IsFiglio(NuovaConfigurazione))
                    if (!NodoSimile.IsFiglio(NuovaConfigurazione))
                    {
                        ConfigurazioniPossibili.Add(NuovaConfigurazione);
                    }
                    // ***
                    // else if (!diversoDaFigli)
                    // {
                    //     ConfigurazioniPossibili.Add(NuovaConfigurazione);
                    // }
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
        public Nodo DimmiFiglioVincente(bool maggioreDiZero)
        {
            Nodo NodoVincente = new Nodo();
            foreach (Nodo n in NodoSimile.ListaFigli)
            {
                if (maggioreDiZero && n.Punteggio >= 0 && (n.Punteggio > NodoVincente.Punteggio || NodoVincente.Punteggio == null))
                {
                    NodoVincente = new Nodo() {Punteggio = n.Punteggio, Configurazione = n.Configurazione.Clone() as int[,]};
                }
                else if (!maggioreDiZero && (n.Punteggio > NodoVincente.Punteggio || NodoVincente.Punteggio == null))
                {
                    NodoVincente = new Nodo() {Punteggio = n.Punteggio, Configurazione = n.Configurazione.Clone() as int[,]};
                }
            }
            return NodoVincente;
        }
    }
}
