using System;
// using System.Collections.Generic;

namespace tris
{
    class MainClass
    { 
        public static void Main(string[] args)
        {
            Nodo radice = new Nodo() {Punteggio = 1, Configurazione = new int[,] {
                {0,0,0},
                {0,0,0},
                {0,0,0}
            }};

            Tris tris;

            tris = new Tris();
            tris.GiocaPartita(radice, false);
            Partita p = tris.CreaPartita();
            p.AggiungiTutteCronologie(radice);
            radice.PrintAlbero();
            // tris = new Tris();
            // tris.GiocaPartita(true, radice);
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
// t.Tabella;           => int[,]
// t.CronologiaMosse;   => List<int[,]>

// t.FaiPartita();
// t.DimmiVincitore();  => int
// t.PrintTabella(int[,]);
// t.PrintCronologia();


// class Nodo
// n.Punteggio          => int
// n.Configurazione     => int[,]
// n.ListaFigli         => List<Nodo>

// n.AggiungiFiglio(Nodo);
// n.CercaConfigurazione(int[,]);       => Nodo
// n.Equals(object as Nodo) (override)  => bool
// n.PrintConfigurazione(int[,]);
// n.PrintAlbero();
