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

        // aggiunge un figlio
        // se non esiste lo crea
        // se esiste già ne modifica il punteggio
        public void AggiungiFiglio(Nodo unNodo)
        {
            // controlla se un nodo con la stessa configurazione esiste già
            Nodo NodoUguale = CercaConfigurazione(unNodo.Configurazione);
            // se non esiste aggiunge un nuovo nodo con quella configurazione
            if (NodoUguale.Equals(new Nodo()))
            {
                // Console.WriteLine("E NUOVO");
                ListaFigli.Add(unNodo);
            }
            // // se esiste allora cambia il punteggio
            // else
            // {
            //     Console.WriteLine("NON E NUOVO PER CUI CAMBIO IL PUNTEGGIO");
            //     NodoUguale.Punteggio = unNodo.Punteggio;
            // }
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

        public Nodo DimmiMossaVincente(int giocatore)
        {
            Nodo NodoVincente = DimmiNodoDiversoDaFigli(giocatore);
            // Console.WriteLine("GIOCATORE MIGLIORE TRA QUELLI PRESENTI");
            if (NodoVincente.Equals(new Nodo()))
            {
                NodoVincente = DimmiNodoMiglioreTraFigli();
                // Console.WriteLine("GIOCATORE DIVERSO DA FIGLI");
            }
            if (NodoVincente.Equals(new Nodo()))
            {
                // Console.WriteLine("GIOCATORE A CASO");
                NodoVincente = DimmiNodoRandom(giocatore);
            }
            NodoVincente.Punteggio = 0;
            return NodoVincente;
        }

        // ATTENZIONE
        // funzione che ritorna nodo non presente tra i figli, serve per diversificare l'apprendimento
        // e non proporre sempre le stesse configurazioni tramite un semplice random
        public Nodo DimmiNodoDiversoDaFigli(int giocatore)
        {
            Nodo NuovoNodo = new Nodo();
            // Console.Write("LISTAFIGLI: ");
            // Console.WriteLine(ListaFigli.Count);
            if (ListaFigli.Count < DimmiMosseDisponibili())
            {
                Random Rand = new Random();
                bool Trovato = false;
                while (!Trovato)
                {
                    // Console.Write(".");
                    int NRand = Rand.Next(0, Configurazione.Length);
                    if (Configurazione[NRand/3, NRand%3] == 0)
                    {
                        int[,] NuovaConfigurazione = Configurazione.Clone() as int[,];
                        NuovaConfigurazione[NRand/3, NRand%3] = giocatore;
                        NuovoNodo = new Nodo() {Punteggio = 0, Configurazione = NuovaConfigurazione};
                        // Console.WriteLine(EUnFiglio(NuovoNodo));
                        if (!EUnFiglio(NuovoNodo))
                        {
                            // Console.WriteLine("SONO ENTRATO NEL COSO");
                            Trovato = true;
                        }
                        // PrintConfigurazione(NuovaConfigurazione);
                    }
                }
            }
            // Console.Write("PUNTEGGIO CONFIGURAZIONE NODO DIVERSO DAGLI ALTRI: ");
            // Console.WriteLine(NuovoNodo.Punteggio);
            // PrintConfigurazione(NuovoNodo.Configurazione);
            // Console.WriteLine("E HO FINITO");
            return NuovoNodo;
        }

        public Nodo DimmiNodoMiglioreTraFigli()
        {
            Nodo NodoVincente = new Nodo();
            if (ListaFigli.Count != 0)
            {
                NodoVincente = new Nodo() {Punteggio = ListaFigli[0].Punteggio, Configurazione = ListaFigli[0].Configurazione.Clone() as int[,]};
                foreach (Nodo n in ListaFigli)
                {
                    // if (n.Punteggio > NodoVincente.Punteggio && n.Punteggio >= 0)
                    if (n.Punteggio > NodoVincente.Punteggio)
                    {
                        NodoVincente = new Nodo() {Punteggio = n.Punteggio, Configurazione = n.Configurazione.Clone() as int[,]};
                    }
                }
            }
            return NodoVincente;
        }

        public Nodo DimmiNodoRandom(int giocatore)
        {
            Nodo NodoVincente = new Nodo();
            Random Rand = new Random();
            bool Trovato = false;
            while (!Trovato)
            {
                int NRand = Rand.Next(0, Configurazione.Length);
                if (Configurazione[NRand/3, NRand%3] == 0)
                {
                    int[,] NuovaConfigurazione = Configurazione.Clone() as int[,];
                    NuovaConfigurazione[NRand/3, NRand%3] = giocatore;
                    NodoVincente = new Nodo() {Punteggio = 0, Configurazione = NuovaConfigurazione};
                    Trovato = true;
                }
            }
            return NodoVincente;
        }

        public bool EUnFiglio(Nodo unNodo)
        {
            bool EUnFiglio = false;
            foreach (Nodo n in ListaFigli)
            {
                if (unNodo.Equals(new Nodo {Punteggio = unNodo.Punteggio, Configurazione = n.Configurazione}))
                {
                    EUnFiglio = true;
                }
            }
            return EUnFiglio;
        }

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

        public int? DimmiProspettivaVittoria(int? valorePartenza)
        {
            int? nuovoValorePartenza = valorePartenza + this.Punteggio;
            foreach (Nodo n in ListaFigli)
            {
                nuovoValorePartenza += n.DimmiProspettivaVittoria(nuovoValorePartenza);
            }
            return nuovoValorePartenza;
        }

        public void SalvaFigli()
        {
            Stream file = File.Create("radice");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(file, ListaFigli);
            file.Close();
        }

        public void CaricaFigli()
        {
            Stream file = File.OpenRead("radice");
            BinaryFormatter serializer = new BinaryFormatter();
            ListaFigli = serializer.Deserialize(file) as List<Nodo>;
            file.Close();
        }

        public override int GetHashCode()
        {
            return Configurazione.GetHashCode();
        }

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

        // cerca una configurazione in maniera ricorsiva
        public void CercaEPrintaConfigurazione(int[,] unaConfigurazione)
        {
            foreach (Nodo n in ListaFigli)
            {
                n.CercaEPrintaConfigurazione(unaConfigurazione);
                if (n.Equals(new Nodo() {Configurazione = unaConfigurazione}))
                {
                    Console.WriteLine("HO TROVATO QUESTA");
                    PrintConfigurazione(n.Configurazione);
                }
            }
        }
    }
}
