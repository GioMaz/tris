using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace tris
{
    [Serializable]
    class Nodo
    {
        public int Punteggio { get; set; }
        public int[,] Configurazione { get; set; }
        public List<Nodo> ListaFigli = new List<Nodo>();

        public void AggiungiFiglio(Nodo unNodo)
        {
            // controlla se un nodo con la stessa configurazione esiste già
            Nodo NodoUguale = CercaConfigurazione(unNodo.Configurazione);

            if (NodoUguale.Equals(new Nodo()))
            {
                // se non esiste aggiunge un nuovo nodo con quella configurazione
                ListaFigli.Add(unNodo);
            }
        }

        // cerca una configurazione in maniera ricorsiva
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

        public Nodo DimmiFiglioVincente()
        {
            Nodo NodoVincente = new Nodo() {Punteggio = -100};
            foreach (Nodo n in ListaFigli)
            {
                if (n.Punteggio > NodoVincente.Punteggio)
                {
                    NodoVincente = n;
                }
            }
            return NodoVincente;
        }

        // override del metodo equals
        public override bool Equals(object n)
        {
            Nodo altroNodo = n as Nodo;
            if (this.Configurazione == null && altroNodo.Configurazione == null)
            {
                return true;
            }
            else if (this.Configurazione == null || altroNodo.Configurazione == null)
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
            // return tuttoUguale;
            return tuttoUguale && this.Punteggio == altroNodo.Punteggio;
        }

        public override int GetHashCode()
        {
            return Configurazione.GetHashCode();
        }

        public void SalvaFigli(string Path)
        {
            StreamWriter file = new StreamWriter(Path);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Nodo>));
            serializer.Serialize(file, ListaFigli);
            file.Close();
        }

        public void CaricaFigli(string Path)
        {
            if (File.Exists(Path))  
            {
                StreamReader file = new StreamReader(Path);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Nodo>));
                ListaFigli = serializer.Deserialize(file) as List<Nodo>;
                file.Close();
            }
        }

        public void PrintConfigurazione(int [,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i,j]);
                    Console.Write(" ");
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
                n.PrintFigli();
            }
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

// public Nodo CercaConfigurazione(int[,] unaConfigurazione)
// {
//     if (this.Equals(new Nodo() {Punteggio = this.Punteggio, Configurazione = unaConfigurazione}))
//     {
//         return this;
//     }
//     foreach (Nodo n in ListaFigli)
//     {
//         Nodo NodoTrovato = n.CercaConfigurazione(unaConfigurazione);
//         if (!NodoTrovato.Equals(new Nodo()))
//         {
//             return NodoTrovato;
//         }
//     }
//     return NodoTrovato;
// }
