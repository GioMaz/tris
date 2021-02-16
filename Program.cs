using System;
// using System.Collections.Generic;

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

            Tris tris;
            
            Console.WriteLine("CALCOLO MOSSE...");
            for (int i = 0; i < 1; i ++)
            {
                tris = new Tris();
                tris.GiocaPartita(radice, true, true);

                // Console.Write("CRONOLOGIA: ");
                // Console.WriteLine(i + 1);
                // tris.PrintCronologia();

                tris.PassaAPartita().AggiungiCronologieRuotate(radice);
                // p.AggiungiCronologieRuotate(radice);
            }

            radice.PrintAlbero();
            // radice.PrintFigli();

            Console.WriteLine("INIZIO GIOCO...");
            
            while (true)
            {
                tris = new Tris();
                tris.GiocaPartita(radice, true, true);
            }

            // Console.WriteLine("DIVENTO INTELLIGENTE");
            // tris = new Tris();
            // tris.GiocaPartita(radice, true);
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
