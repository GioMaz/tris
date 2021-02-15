using System;
using System.Collections.Generic;

namespace tris
{
    class Partita
    {
        public int Vincitore { get; set; }
        public List<int[,]> Cronologia { get; set; }
        
        public void AggiungiTutteCronologie(Nodo radice)
        {
            Aggiungi
            // for (int i = 0; i < 4; i ++)
            // {

            // }
        }

        public void AggiungiCronologia(Nodo radice)
        {
            Nodo NodoPadre = radice;
            foreach (int[,] Conf in Cronologia)
            {
                Nodo NodoFiglio = new Nodo() {Punteggio = Vincitore, Configurazione = Conf};
                NodoPadre.AggiungiFiglio(NodoFiglio);
                NodoPadre = NodoFiglio;
            }
        }

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

        public void PrintCronologia(List<int[,]> cron)
        {
            foreach (int [,] c in cron)
            {
                new Tris().PrintTabella(c);
            }
        }
    }
}
