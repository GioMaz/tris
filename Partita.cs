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
        public void AggiungiCronologieRuotate(Nodo radice)
        {
            for (int i = 0; i < 4; i++)
            {
                Nodo NodoPadre = radice;
                for (int j = 1; j < Cronologia.Count; j ++)
                {
                    Nodo NodoFiglio = new Nodo() {Punteggio = DimmiPunteggio(), Configurazione = Cronologia[j]};
                    NodoPadre.AggiungiFiglio(NodoFiglio);
                    NodoPadre = NodoFiglio;
                }
                RuotaCronologia();
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
                    for (int j = 0; j < m.GetLength(0); j++)
                    {
                        NuovaMatrice[i, j] = m[NuovaMatrice.GetLength(0) - 1 - j, i];
                    }
                }
                NuovaCronologia.Add(NuovaMatrice);
            }
            Cronologia = NuovaCronologia;
        }

        public int DimmiPunteggio()
        {
            switch (Vincitore)
            {
                case 1:
                    return -1;
                case 2:
                    return 1;
                case -1:
                    return 0;
                default:
                    return 0;
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

        public void PrintCronologia(List<int[,]> Cron)
        {
            foreach (int [,] c in Cron)
            {
                PrintConfigurazione(c);
            }
        }
    }
}
