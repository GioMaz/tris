using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace tris
{
    class Tris
    {
        public List<int [,]> Cronologia = new List<int[,]>();
        public Tris()
        {
            Cronologia.Add(new int[,] {
                {0,0,0},
                {0,0,0},
                {0,0,0}
            });
        }

        public void GiocaPartita(Nodo radice)
        {
            int Turno = 0;
            while (DimmiVincitore() == 0)
            {
                int giocatore = Turno%2 + 1;
                if (giocatore == 1)
                {
                    MossaGiocatore(radice, giocatore);
                }
                else
                {
                    MossaIntelligente(radice, giocatore);
                }
                Turno ++;
            }
            Console.Write("VINCITORE: ");
            Console.WriteLine(DimmiVincitore());
        }

        public void MossaIntelligente(Nodo radice, int giocatore)
        {
            int [,] Tabella = Cronologia[Cronologia.Count - 1];
            Nodo ConfigurazioneSimile = radice.CercaConfigurazione(Tabella);
            if (!ConfigurazioneSimile.Equals(new Nodo()))
            {
                // PRINTA FIGLI DISPONIBILI 
                // (tenere per trovare eventuali errori nella creazione dell'albero)
                // UTILE A SCOPO DI DEBUG
                ConfigurazioneSimile.PrintFigli();

                Nodo FiglioMigliore = ConfigurazioneSimile.DimmiConfigurazioneVincente(giocatore);
                Console.Write("PUNTEGGIO: ");
                Console.WriteLine(FiglioMigliore.Punteggio);
                int[,] NuovaConfigurazione = FiglioMigliore.Configurazione.Clone() as int[,];
                Cronologia.Add(NuovaConfigurazione);
                PrintTabella(Cronologia[Cronologia.Count - 1]);
            }
            else
            {
                Console.WriteLine("NON TROVATO");
                MossaRandom(giocatore);
            }
        }

        public void MossaRandom(int giocatore) // NEL CASO NON SI CONOSCA LA CONFIGURAZIONE ATTUALE
        {
            List<int[,]> MossePossibili = new List<int[,]>();
            int [,] Tabella = Cronologia[Cronologia.Count - 1];
            for (int i = 0; i < Tabella.Length; i++)
            {
                int[,] NuovaMossa = Tabella.Clone() as int[,];
                if (NuovaMossa[i/3, i%3] == 0)
                {
                    NuovaMossa[i/3, i%3] = giocatore;
                    MossePossibili.Add(NuovaMossa);
                }
            }
            if (MossePossibili.Count != 0)
            {
                Random Rand = new Random();
                int NRand = Rand.Next(0, MossePossibili.Count);
                Cronologia.Add(MossePossibili[NRand]);
                PrintTabella(MossePossibili[NRand]);
            }
        }

        public void MossaGiocatore(Nodo radice, int giocatore)
        {
            int [,] Tabella = Cronologia[Cronologia.Count - 1];
            bool Trovato = false;
            while (!Trovato)
            {
                string SScelto = Console.ReadLine();
                // UNCOMMENT PER SALVARE RADICE A FINE SESSIONE
                if (!Regex.IsMatch(SScelto, @"^\d+$"))
                {
                    Console.WriteLine("SALVATAGGIO RADICE...");
                    radice.SalvaFigli();
                }
                int NScelto = Convert.ToInt32(SScelto) - 1;
                if (NScelto < 9 && -1 < NScelto && Tabella[NScelto/3, NScelto%3] == 0)
                {
                    int[,] NuovaConfigurazione = Tabella.Clone() as int[,];
                    NuovaConfigurazione[NScelto/3, NScelto%3] = giocatore;
                    Cronologia.Add(NuovaConfigurazione);
                    Trovato = true;
                    PrintTabella(NuovaConfigurazione);
                }
            }
        }

        public Partita PassaAPartita()
        {
            return new Partita() {Vincitore = DimmiVincitore(), Cronologia = Cronologia};
        }

        // serve a stabilire chi ha vinto
        // controlla diagonali, righe e colonne
        //
        // ritorna il numero del giocatore che ha vinto
        // ritorna -1 se c'è un pareggio
        // ritorna 0 se la partita è ancora in corso
        public int DimmiVincitore()
        {
            int[,] Tabella = Cronologia[Cronologia.Count - 1];
            int vincitore = 0;
            for (int i = 0; i <= Tabella.GetLength(0) - 1; i++)
            {
                bool ori  = true;
                bool ver = true;
                bool dia1 = true;
                bool dia2 = true;
                int giocatoreVer = Tabella[0, i];
                int giocatoreOri = Tabella[i, 0];
                int giocatoreDia1 = Tabella[0, 0];
                int giocatoreDia2 = Tabella[0, Tabella.GetLength(1) - 1];
                for (int j = 0; j <= Tabella.GetLength(1) - 1; j++)
                {
                    if (Tabella[i, j] != giocatoreOri || Tabella[i, j] == 0)
                    {
                        ori = false;
                    }
                    if (Tabella[j, i] != giocatoreVer || Tabella[j, i] == 0)
                    {
                        ver = false;
                    }
                    if (Tabella[j, j] != giocatoreDia1 || Tabella[j, j] == 0)
                    {
                        dia1 = false;
                    }
                    if (Tabella[j, Tabella.GetLength(0) - 1 - j] != giocatoreDia2 || Tabella[j, Tabella.GetLength(0) - 1 - j] == 0)
                    {
                        dia2 = false;
                    }
                }
                if (ori == true)
                {
                    vincitore = giocatoreOri;
                }
                else if (ver == true)
                {
                    vincitore = giocatoreVer;
                }
                else if (dia1 == true)
                {
                    vincitore = giocatoreDia1;
                }
                else if (dia2 == true)
                {
                    vincitore = giocatoreDia2;
                }
            }
            bool tuttePiene = true;
            for (int i = 0; i <= Tabella.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= Tabella.GetLength(1) - 1; j++)
                {
                    if (Tabella[i,j] == 0)
                    {
                        tuttePiene = false;
                    }
                }
            }
            if (tuttePiene && vincitore == 0)
            {
                return -1;
            }
            else
            {
                return vincitore;
            }
        }

//-----------------------------------------------------
// Print
//-----------------------------------------------------

        public void PrintTabella(int [,] tab)
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

        public void PrintCronologia()
        {
            foreach (int [,] c in Cronologia)
            {
                PrintTabella(c);
            }
        }
    }
}
