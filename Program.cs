using System;
// using System.Collections.Generic;

namespace tris
{
    class MainClass
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("INIZIO PROGRAMMA");
            Nodo radice = new Nodo() {Punteggio = 0, Configurazione = new int[,] {
                {0,0,0},
                {0,0,0},
                {0,0,0}
            }};

            Tris tris = new Tris();
            tris.GiocaPartita(radice, false);

            Partita p = tris.PassaAPartita();
            p.AggiungiCronologieRuotate(radice);

            radice.PrintAlbero();

            // Console.WriteLine("DIVENTO INTELLIGENTE");
            // tris = new Tris();
            // tris.GiocaPartita(radice, true);
        }
    }
}

// class Partita
// p.Vincitore
// p.Cronologia
//
// p.AggiungiTutteCronologie
// p.AggiungiCronologia
// p.RuotaCronologia


// class Tris
// t.Tabella;                           => int[,]
// t.CronologiaMosse;                   => List<int[,]>

// t.GiocaPartita();
// t.DimmiVincitore();                  => int
// t.PassaAPartita();                   => Partita
// t.PrintTabella(int[,]);
// t.PrintCronologia();


// class Nodo
// n.Punteggio                          => int
// n.Configurazione                     => int[,]
// n.ListaFigli                         => List<Nodo>

// n.AggiungiFiglio(Nodo);
// n.CercaConfigurazione(int[,]);       => Nodo
// n.Equals(object as Nodo) (override)  => bool
// n.PrintConfigurazione(int[,]);
// n.PrintAlbero();
