using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace tris
{
    [Serializable]
    public class Nodo
    {
        // public int? Punteggio { get; set; } = null;
        public int Punteggio { get; set; }
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
            Nodo NodoTrovato = null;
            bool Trovato = false;
            int i = 0;
            while (!Trovato && i < ListaFigli.Count)
            {
                if (HaTutteCelleInComune(unaConfigurazione))
                {
                    NodoTrovato = ListaFigli[i].CercaConfigurazione(unaConfigurazione);
                    if (NodoTrovato != null)
                    {
                        Trovato = true;
                    }
                }
                i ++;
            }
            return NodoTrovato;
        }

        public bool HaTutteCelleInComune(int[,] unaConfigurazione)
        {
            bool tuttoUguale = true;
            int i = 0;
            while (tuttoUguale && i < Configurazione.GetLength(0))
            {
                int j = 0;
                while (tuttoUguale && j < Configurazione.GetLength(1))
                {
                    if (this.Configurazione[i, j] != 0 && this.Configurazione[i, j] != unaConfigurazione[i, j])
                    {
                        tuttoUguale = false;
                    }
                    j++;
                }
                i++;
            }
            return tuttoUguale;
        }

        // override del metodo equals
        public override bool Equals(object n)
        {
            Nodo altroNodo = n as Nodo;
            // // if (this.Configurazione == null && altroNodo.Configurazione == null && this.Punteggio == null && altroNodo.Punteggio == null)
            // // {
            // //     return true;
            // // }
            // // else if (this.Configurazione == null || altroNodo.Configurazione == null || this.Punteggio == null || altroNodo.Punteggio == null)
            // // {
            // //     return false;
            // // }
            // if (this.Configurazione == null && altroNodo.Configurazione == null)
            // {
            //     return true;
            // }
            // else if (this.Configurazione == null || altroNodo.Configurazione == null)
            // {
            //     return false;
            // }
            bool tuttoUguale = true;
            int i = 0;
            while (tuttoUguale && i < Configurazione.GetLength(0))
            {
                int j = 0;
                while (tuttoUguale && j < Configurazione.GetLength(1))
                {
                    if (Configurazione[i, j] != altroNodo.Configurazione[i, j])
                    {
                        tuttoUguale = false;
                    }
                    j++;
                }
                i++;
            }
            return tuttoUguale && this.Punteggio == altroNodo.Punteggio;
        }

        // true se un figlio ha la configurazione specificata nell'argomento
        public bool IsFiglio(int[,] conf)
        {
            bool Trovato = false;
            int i = 0;
            while (!Trovato && i < ListaFigli.Count)
            {
                if (ListaFigli[i].Equals(new Nodo() {Punteggio = ListaFigli[i].Punteggio, Configurazione = conf}))
                {
                    Trovato = true;
                }
                i ++;
            }
            return Trovato;
        }

        // controlla se ha figli con punteggio >= 0
        public bool HaFigliVincenti()
        {
            bool HaVincenti= false;
            int i = 0;
            while (!HaVincenti && i < ListaFigli.Count)
            {
                if (ListaFigli[i].Punteggio >= 0)
                {
                    HaVincenti = true;
                }
                i ++;
            }
            return HaVincenti;
        }

        // controlla se tutte le mosse possibili (ListaFigli)
        // sono state già eseguite
        public bool HaTuttiRami()
        {
            int ZeroCount = 0;
            foreach (int n in Configurazione)
            {
                if (n == 0)
                {
                    ZeroCount ++;
                }
            }
            return ListaFigli.Count == ZeroCount;
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
