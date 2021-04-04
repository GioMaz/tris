using System;
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
            if (File.Exists("RADICE"))
            {
                radice.CaricaFigli();
            }

            while (true)
            {
                tris = new Tris();
                tris.GiocaPartita(radice);
                tris.PassaAPartita().PassaARadice(radice);
                Console.WriteLine("INIZIO NUOVA PARTITA\n");
            }
        }
    }
}
