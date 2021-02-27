using System;
using System.Collections.Generic;

namespace tris
{
    class Partita
    {
        public int Vincitore { get; set; }
        public List<int[,]> Cronologia { get; set; }

        // aggiunge cronologia a nodo (radice)
        // ruota le configurazioni per imparare più in fretta
        public void AggiungiTutteCronologie(Nodo radice)
        {
            List<int[,]> GiaSommato = new List<int[,]>();
            // Console.Write("PUNTEGGIO PARTITA: ");
            // Console.WriteLine(DimmiPunteggio());
            // int? NuovoPunteggio = DimmiPunteggio(0);
            // specchia cronologia
            for (int i = 0; i < 2; i++) // 2
            {
                // ruota cronologia
                for (int j = 0; j < 4; j++) // 4
                {
                    Nodo NodoPadre = radice;
                    for (int k = 1; k < Cronologia.Count; k++)
                    {
                        Nodo NodoSimile = NodoPadre.CercaConfigurazione(Cronologia[k]);

                        if (NodoSimile.Equals(new Nodo()))
                        {
                            GiaSommato.Add(Cronologia[k]);
                            Nodo NodoFiglio = new Nodo() {Configurazione = Cronologia[k], Punteggio = DimmiPunteggio(0)};
                            NodoPadre.AggiungiFiglio(NodoFiglio);
                            NodoPadre = NodoFiglio;
                        }
                        else
                        {
                            if (!GiaSommato.Contains(NodoSimile.Configurazione))
                            {
                                NodoSimile.Punteggio = DimmiPunteggio(NodoSimile.Punteggio);
                                GiaSommato.Add(NodoSimile.Configurazione);
                            }
                            NodoPadre = NodoSimile;
                        }
                    }
                    RuotaCronologia();
                }
                SpecchiaCronologia();
            }
        }

        // ruota tutte le configurazioni nella cronologia
        public void RuotaCronologia()
        {
            List<int[,]> NuovaCronologia = new List<int[,]> {};
            foreach (int[,] m in Cronologia)
            {
                int [,] NuovaMatrice = new int[,] {
                    {0,0,0},
                    {0,0,0},
                    {0,0,0}
                };
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    for (int j = 0; j < m.GetLength(1); j++)
                    {
                        NuovaMatrice[i, j] = m[m.GetLength(0) - 1 - j, i];
                    }
                }
                NuovaCronologia.Add(NuovaMatrice);
            }
            Cronologia = NuovaCronologia;
        }

        // specchia tutte le configurazioni nella cronologia
        public void SpecchiaCronologia()
        {
            List<int[,]> NuovaCronologia = new List<int[,]> {};
            foreach (int[,] m in Cronologia)
            {
                int [,] NuovaMatrice = new int[,] {
                    {0,0,0},
                    {0,0,0},
                    {0,0,0}
                };
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    for (int j = 0; j < m.GetLength(1); j++)
                    {
                        NuovaMatrice[i, j] = m[i, m.GetLength(0) - 1 - j];
                    }
                }
                NuovaCronologia.Add(NuovaMatrice);
            }
            Cronologia = NuovaCronologia;
        }

        public int? DimmiPunteggio(int? vecchioPunteggio)
        {
            int n = Cronologia.Count;
            // se è vinta, poche mosse è meglio
            // se è persa, tante mosse è meglio
            switch (Vincitore)
            {
                case 1:
                    return vecchioPunteggio + n - 11;
                case 2:
                    return 11 - n;
                case -1:
                    return 0;
                default:
                    return 0;
            }
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
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintCronologia(List<int[,]> tab)
        {
            // Console.WriteLine("FIGLI: ");
            for (int i = 0; i < 3; i++)
            {
                foreach (int[,] n in tab)
                {
                    for (int j = 0; j < n.GetLength(0); j++)
                    {
                        Console.Write(n[i, j]);
                    }
                    Console.Write("   ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

