using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace tris
{
    [Serializable]
    public class Nodo
    {
        public int? Punteggio { get; set; } = null;
        public int[,] Configurazione { get; set; }
        public List<Nodo> ListaFigli = new List<Nodo>();

        public void AggiungiFiglio(Nodo unNodo)
        {
            ListaFigli.Add(unNodo);
        }

        // cerca una configurazione in maniera ricorsiva
        // partendo dal nodo corrente
        public Nodo CercaConfigurazione(int[,] unaConfigurazione)
        {
            if (this.Equals(new Nodo() {Punteggio = this.Punteggio, Configurazione = unaConfigurazione}))
            {
                return this;
            }
            Nodo NodoTrovato = new Nodo();
            bool Trovato = false;
            int i = 0;
            while (!Trovato && i < ListaFigli.Count)
            {
                NodoTrovato = ListaFigli[i].CercaConfigurazione(unaConfigurazione);
                if (!NodoTrovato.Equals(new Nodo()))
                {
                    Trovato = true;
                }
                i ++;
            }
            return NodoTrovato;
        }

        // override del metodo equals
        public override bool Equals(object n)
        {
            Nodo altroNodo = n as Nodo;
            if (this.Configurazione == null && altroNodo.Configurazione == null && this.Punteggio == null && altroNodo.Punteggio == null)
            {
                return true;
            }
            else if (this.Configurazione == null || altroNodo.Configurazione == null || this.Punteggio == null || altroNodo.Punteggio == null)
            {
                return false;
            }
            bool tuttoUguale = true;
            for (int i = 0; i < Configurazione.GetLength(0); i++)
            {
                for (int j = 0; j < Configurazione.GetLength(1); j++)
                {
                    if (Configurazione[i, j] != altroNodo.Configurazione[i, j])
                    {
                        tuttoUguale = false;
                    }
                }
            }
            return tuttoUguale && this.Punteggio == altroNodo.Punteggio;
        }

        // ritorna nodo con configurazione mossa preferibile data quella attuale
        public Nodo DimmiConfigurazioneVincente(int giocatore)
        {
            Nodo NodoVincente = DimmiFiglioVincente(true);
            if (NodoVincente.Equals(new Nodo()))
            {
                // ***
                // NodoVincente = DimmiFiglioRandom(giocatore, true);
                NodoVincente = DimmiFiglioRandom(giocatore);
            }
            if (NodoVincente.Equals(new Nodo()))
            {
                NodoVincente = DimmiFiglioVincente(false);
            }
            // ***
            // if (NodoVincente.Equals(new Nodo()))
            // {
            //     NodoVincente = DimmiFiglioRandom(giocatore, false);
            // }
            return NodoVincente;
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
                    if (!IsFiglio(NuovaConfigurazione))
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
            foreach (Nodo n in ListaFigli)
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

        // true se un figlio ha la configurazione specificata nell'argomento
        public bool IsFiglio(int[,] conf)
        {
            bool IsFiglio = false;
            foreach (Nodo n in ListaFigli)
            {
                if (n.Equals(new Nodo() {Punteggio = n.Punteggio, Configurazione = conf}))
                {
                    IsFiglio = true;
                }
            }
            return IsFiglio;
        }

        // ritorna il numero di 0 (caselle non specificate) nella configurazione attuale
        public int DimmiMosseDisponibili()
        {
            int MosseDisponibili = 0;
            for (int i = 0; i < Configurazione.GetLength(0); i++)
            {
                for (int j = 0; j < Configurazione.GetLength(1); j++)
                {
                    if (Configurazione[i, j] == 0)
                    {
                        MosseDisponibili ++;
                    }
                }
            }
            return MosseDisponibili;
        }

        // salva figli in un file binario
        public void SalvaFigli()
        {
            Stream file = File.Create("RADICE");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(file, ListaFigli);
            file.Close();
        }

        public void CaricaFigli()
        {
            Stream file = File.OpenRead("RADICE");
            BinaryFormatter serializer = new BinaryFormatter();
            ListaFigli = serializer.Deserialize(file) as List<Nodo>;
            file.Close();
        }

//-----------------------------------------------------
// Print
//-----------------------------------------------------

        public void PrintConfigurazione(int [,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i,j]);
                    // Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintConfigurazioni(List<int[,]> list)
        {
            Console.WriteLine("FIGLI: ");
            for (int i = 0; i < 3; i++)
            {
                foreach (int[,] n in list)
                {
                    for (int j = 0; j < n.GetLength(0); j++)
                    {
                        Console.Write(n[i, j]);
                    }
                    Console.Write("   ");
                }
                Console.WriteLine();
            }
        }

        public void PrintFigli()
        {
            Console.WriteLine("FIGLI: ");
            for (int i = 0; i < 3; i++)
            {
                foreach (Nodo n in ListaFigli)
                {
                    for (int j = 0; j < n.Configurazione.GetLength(0); j++)
                    {
                        Console.Write(n.Configurazione[i, j]);
                    }
                    Console.Write("   ");
                }
                Console.WriteLine();
            }
            foreach (Nodo n in ListaFigli)
            {
                // n.printfigli();
                Console.Write(n.Punteggio);
                Console.Write("    ");
            }
            Console.WriteLine();
        }

        public void PrintAlbero()
        {
            Console.Write("PUNTEGGIO: ");
            Console.WriteLine(Punteggio);
            PrintConfigurazione(Configurazione);
            foreach (Nodo n in ListaFigli)
            {
                n.PrintAlbero();
                Console.WriteLine();
            }
        }
    }
}
