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
                {1,0,1},
                {0,2,0},
                {0,0,2}
            };
            int[,] ConfProva1 = new int[,] {
                {1,0,1},
                {0,2,0},
                {2,0,0}
            };

            Tris tris = new Tris();

            // radice.CaricaFigli();
            while (true)
            {
                tris = new Tris();
                tris.GiocaPartita(radice);
                tris.PassaAPartita().AggiungiTutteCronologie(radice);
                // Console.WriteLine(radice.CercaConfigurazione(ConfProva).Equals(new Nodo()));
                // Console.WriteLine(radice.CercaConfigurazione(ConfProva1).Equals(new Nodo()));
                Console.WriteLine("INIZIO NUOVA PARTITA\n");
            }
            // radice.SalvaFigli();
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
