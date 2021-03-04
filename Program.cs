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

            Tris tris = new Tris();

            // uncomment se si vuole caricare il nodo RADICE
            // radice.CaricaFigli();
            while (true)
            {
                tris = new Tris();
                tris.GiocaPartita(radice);
                tris.PassaAPartita().AggiungiTutteCronologie(radice);
                Console.WriteLine("INIZIO NUOVA PARTITA\n");
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
