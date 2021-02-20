using System;
using System.Collections.Generic;
using System.IO;

namespace tris
{
    class MainClass
    { 
        public static void Main(string[] args)
        {
            Nodo radice = new Nodo() {Punteggio = 0, Configurazione = new int[,] {
                {0,0,0},
                {0,0,0},
                {0,0,0}
            }};
            int[,] ConfProva = new int[,] {
                {1,0,0},
                {0,0,0},
                {0,0,0}
            };

            Tris tris = new Tris();
           
            // // APPRENDIMENTO AUTOMATICO 
            // if (!File.Exists("radice"))
            // {
            //     Console.WriteLine("CALCOLO MOSSE...");
            //     for (int i = 0; i < 100; i ++)
            //     {
            //         tris.GiocaPartita(radice, false);
            //         tris.PassaAPartita().AggiungiTutteCronologie(radice);
            //         tris = new Tris();
            //     }
            //     radice.SalvaFigli();
            // }

            while (true)
            {
                Console.WriteLine("INIZIO PARTITA");
                tris = new Tris();
                tris.GiocaPartita(radice);
                tris.PassaAPartita().AggiungiTutteCronologie(radice);
            }
        }
    }
}

// class Nodo
// n.Punteggio                          => int
// n.Configurazione                     => int[,]
// n.ListaFigli                         => List<Nodo>
// 
// n.AggiungiFiglio(Nodo);
// n.CercaConfigurazione(int[,]);       => Nodo
// n.Equals(object as Nodo) (override)  => bool
// n.PrintConfigurazione(int[,]);
// n.PrintAlbero();


// class Partita
// p.Vincitore                          => int
// p.Cronologia                         => List<int[,]>
// 
// p.AggiungiTutteCronologie
// p.AggiungiCronologia
// p.RuotaCronologia


// class Tris
// t.Tabella;                           => int[,]
// t.CronologiaMosse;                   => List<int[,]>
// 
// t.GiocaPartita();
// t.DimmiVincitore();                  => int
// t.PassaAPartita();                   => Partita
// t.PrintTabella(int[,]);
// t.PrintCronologia();
