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
            int giocatore = 1;
            while (DimmiVincitore() == 0)
            {
                if (giocatore == 1)
                {
                    MossaGiocatore(radice, giocatore);
                }
                else
                {
                    MossaIntelligente(radice, giocatore);
                }
                giocatore = (1 - (giocatore-1) + 1);
            }
            Console.Write("VINCITORE: ");
            Console.WriteLine(DimmiVincitore());
        }

        public void MossaIntelligente(Nodo radice, int giocatore)
        {
            int [,] tabella = Cronologia[Cronologia.Count - 1];
            
            NuovaMossa n = new NuovaMossa
            {
                Configurazione = tabella,
                Giocatore = giocatore
            };
            int[,] NuovaConf = n.DimmiConfigurazione(radice).Clone() as int[,];
            Cronologia.Add(NuovaConf);
            PrintTabella(Cronologia[Cronologia.Count - 1]);
        }

        public void MossaGiocatore(Nodo radice, int giocatore)
        {
            int [,] tabella = Cronologia[Cronologia.Count - 1];
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
                if (NScelto < 9 && -1 < NScelto &&
                        tabella[NScelto/3, NScelto%3] == 0)
                {
                    int[,] NuovaConfigurazione = tabella.Clone() as int[,];
                    NuovaConfigurazione[NScelto/3, NScelto%3] = giocatore;
                    Cronologia.Add(NuovaConfigurazione);
                    Trovato = true;
                    PrintTabella(NuovaConfigurazione);
                }
            }
        }

        public Partita PassaAPartita()
        {
            return new Partita()
            {Vincitore = DimmiVincitore(), Cronologia = Cronologia};
        }

        // serve a stabilire chi ha vinto
        // controlla diagonali, righe e colonne
        //
        // ritorna il numero del giocatore che ha vinto
        // ritorna -1 se c'è un pareggio
        // ritorna 0 se la partita è ancora in corso
        public int DimmiVincitore()
        {
            int[,] tabella = Cronologia[Cronologia.Count - 1];
            int vincitore = 0;
            for (int i = 0; i < tabella.GetLength(0); i++)
            {
                bool ori, ver, dia1, dia2;
                ori = ver = dia1 = dia2 = true;
                int giocatoreVer = tabella[0, i];
                int giocatoreOri = tabella[i, 0];
                int giocatoreDia1 = tabella[0, 0];
                int giocatoreDia2 = tabella[0, tabella.GetLength(1) - 1];
                for (int j = 0; j < tabella.GetLength(1); j++)
                {
                    if (tabella[i, j] != giocatoreOri || tabella[i, j] == 0)
                    {
                        ori = false;
                    }
                    if (tabella[j, i] != giocatoreVer || tabella[j, i] == 0)
                    {
                        ver = false;
                    }
                    if (tabella[j, j] != giocatoreDia1 || tabella[j, j] == 0)
                    {
                        dia1 = false;
                    }
                    if (tabella[j, tabella.GetLength(0) - 1 - j] != 
                            giocatoreDia2 || 
                            tabella[j, tabella.GetLength(0) - 1 - j] == 0)
                    {
                        dia2 = false;
                    }
                }
                if (ori)
                {
                    vincitore = giocatoreOri;
                }
                else if (ver)
                {
                    vincitore = giocatoreVer;
                }
                else if (dia1)
                {
                    vincitore = giocatoreDia1;
                }
                else if (dia2)
                {
                    vincitore = giocatoreDia2;
                }
            }
            bool tuttePiene = true;
            int k = 0;
            while (tuttePiene && k < tabella.GetLength(0))
            {
                int l = 0;
                while (tuttePiene && l < tabella.GetLength(1))
                {
                    if (tabella[k, l] == 0)
                    {
                        tuttePiene = false;
                    }
                    l++;
                }
                k++;
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
