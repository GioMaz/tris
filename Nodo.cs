using System;
using System.Collections.Generic;

namespace tris
{
    class Nodo
    {
        public int Punteggio { get; set; }
        public int[,] Configurazione { get; set; }
        public List<Nodo> ListaFigli = new List<Nodo>();

        public void AggiungiFiglio(Nodo unNodo)
        {
            // controlla se un nodo con la stessa configurazione esiste già
            // PrintConfigurazione(CercaConfigurazione(unaConfigurazione).Configurazione);
            // Console.WriteLine(CercaConfigurazione(unNodo.Configurazione).Equals(new Nodo()));
            // PrintConfigurazione(unNodo.Configurazione);
            if (CercaConfigurazione(unNodo.Configurazione).Equals(new Nodo()))
            {
                // se non esiste aggiunge un nuovo nodo con quella configurazione
                // Console.WriteLine("AGGIUNGENDO IL NODO...\n");
                ListaFigli.Add(unNodo);
            }
        }

        // cerca una configurazione in maniera ricorsiva
        public Nodo CercaConfigurazione(int[,] unaConfigurazione)
        {
            if (this.Equals(new Nodo() {Punteggio = this.Punteggio, Configurazione = unaConfigurazione}))
            {
                // PrintConfigurazione(this.Configurazione);
                // Console.WriteLine("THIS CONFIGURAZIONE");
                return this;
            }
            foreach (Nodo n in ListaFigli)
            {
                return n.CercaConfigurazione(unaConfigurazione);
            }
            return new Nodo();
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
            return tuttoUguale;
            //return altroNodo.Punteggio == this.Punteggio && altroNodo.Configurazione == this.Configurazione && tuttoUguale;
            //return altroNodo.Configurazione == this.Configurazione;
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
    }
}
