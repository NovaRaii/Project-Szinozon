using System;
using System.Collections.Generic;
using System.Linq;

namespace szinozon
{
    internal class Program
    {
        static Random rnd = new Random();
        static int kivalasztott;

        static void Belepes(List<string> megoldas, List<string> tippek, string[] szinek)
        {
            kivalasztott = 0;
            string[] menupontok = { "Új Játék", "Játékszabályok" };
            ConsoleKeyInfo lenyomott;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Navigálás a nyilakkal történik:\n");

                for (int i = 0; i < menupontok.Length; i++)
                {
                    Console.ForegroundColor = (i == kivalasztott) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine("\t" + (i + 1) + ") " + menupontok[i]);
                }

                lenyomott = Console.ReadKey();
                if (lenyomott.Key == ConsoleKey.UpArrow && kivalasztott > 0) kivalasztott--;
                if (lenyomott.Key == ConsoleKey.DownArrow && kivalasztott < menupontok.Length - 1) kivalasztott++;

            } while (lenyomott.Key != ConsoleKey.Enter);

            Console.Clear();
            if (kivalasztott == 0)
            {
                Generalas(megoldas, szinek);
                Tippeles(tippek, megoldas, szinek);
            }
            else if (kivalasztott == 1)
            {
                Jatekszabaly(megoldas, tippek, szinek);
            }
        }

        static void Jatekszabaly(List<string> megoldas, List<string> tippek, string[] szinek)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Kilépés az Escape gombbal!\n\nA játék lényege, hogy a gép által elrejtett 4 golyó színét és ezek sorrendjét kitaláljuk.\n" +
                "Helyes jelentése: egy golyó színét és helyét is eltaláltuk.\n" +
                "Van benne jelentése: egy golyó színét eltaláltuk, de a helyét nem.\n" +
                "Nincs benne jelentése: nem találtuk el a golyó színét.\n" +
                "Azt, hogy melyik golyóra vonatkoznak azt a gép nem árulja el, ezt nekünk kell kitalálnunk az egyes tippekre adott válaszokból.");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                Belepes(megoldas, tippek, szinek);
            }
        }
        static void Generalas(List<string> megoldas, string[] szinek)
        {
            while (megoldas.Count != 4)
            {
                int sorszam = rnd.Next(9, szinek.Length);
                if (!megoldas.Contains(szinek[sorszam]))
                {
                    megoldas.Add(szinek[sorszam]);
                }
            }
        }
        static void Tippeles(List<string> tippek, List<string> megoldas, string[] szinek)
        {
            string[] eredmeny = new string[4];
            Console.WriteLine("Parancsok: \nKilépés - a játék során bármikor ki lehet lépni a főmenübe\nSzabad a gazda - a játék véget ér és a gép felfedi a megoldást\nMégse - utolsó szín visszavonása\nParancsok végrehajtása - Írd be a tippelésnél a parancsot!");
            int probak = 1;
            int db = 1;

            while (probak < 11)
            {

                tippek.Clear();
                Array.Clear(eredmeny, 0, eredmeny.Length);
                Console.WriteLine($"\n{probak}. próba - Add meg a színeket: (Kék, Zöld, Cián, Piros, Magenta, Sárga, Fehér)\n");

                while (tippek.Count != 4)
                {
                    Console.WriteLine($"{db}. szín:");
                    string tipp = Console.ReadLine().Trim().Split(' ')[0];

                    if (tipp == "Kilépés" || tipp == "kilépés")
                    {
                        Belepes(megoldas, tippek, szinek);
                    }
                    if (tipp == "Mégse" || tipp == "mégse")
                    {
                        if (tippek.Count > 0)
                        {
                            tippek.RemoveAt(tippek.Count - 1);
                            db--;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Nincs mit visszavonni!");
                            continue;
                        }
                    }
                    if (tipp == "Szabad" || tipp == "szabad")
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            for (int x = 0; x < 4; x++)
                            {
                                int index = 0;
                                for (int j = 0; j < szinek.Length; j++)
                                {
                                    if (megoldas[x] == szinek[j])
                                    {
                                        index = j;
                                        break;
                                    }
                                }

                                Console.ForegroundColor = (ConsoleColor)index;

                                if (y == 0 || y == 3)
                                {
                                    Console.Write("****  ");
                                }
                                else
                                {
                                    Console.Write("*  *  ");
                                }
                            }
                            Console.WriteLine();
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    else
                    {
                        tippek.Add(tipp.ToLower());
                        db++;
                    }
                }

                for (int i = 0; i < megoldas.Count; i++)
                {
                    if (megoldas[i] == tippek[i])
                    {
                        eredmeny[i] = "Helyes";
                    }
                    else if (megoldas.Contains(tippek[i]))
                    {
                        eredmeny[i] = "Van benne";
                    }
                    else
                    {
                        eredmeny[i] = "Nincs benne";
                    }
                }

                var sortedEredmeny = eredmeny.OrderBy(e => e == "Helyes" ? 0 : e == "Van benne" ? 1 : 2).ToArray();
                Console.WriteLine();
                Console.Write("Találatok: ");
                foreach (var e in sortedEredmeny)
                {
                    Console.Write($"{e}  ");
                }
                Console.WriteLine();
                Console.WriteLine();

                for (int y = 0; y < 4; y++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int index = 0;
                        for (int j = 0; j < szinek.Length; j++)
                        {
                            if (tippek[i] == szinek[j])
                            {
                                index = j;
                                break;
                            }
                        }

                        Console.ForegroundColor = (ConsoleColor)index;

                        if (y == 0 || y == 3)
                        {
                            Console.Write("****  ");
                        }
                        else
                        {
                            Console.Write("*  *  ");
                        }
                    }
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.White;
                if (eredmeny.All(e => e == "Helyes"))
                {
                    Console.WriteLine("\nGratulálok, eltaláltad a megoldást!");
                    break;
                }
                probak++;
                db = 1;

            }

            if (probak > 10 && !eredmeny.All(e => e == "Helyes"))
            {
                Console.WriteLine("Sajnálom, nem találtad el a megoldást.");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            List<string> megoldas = new List<string>();
            List<string> tippek = new List<string>();
            string[] szinek = new string[16];
            szinek[9] = "kék";
            szinek[10] = "zöld";
            szinek[11] = "cián";
            szinek[12] = "piros";
            szinek[13] = "magenta";
            szinek[14] = "sárga";
            szinek[15] = "fehér";

            Console.WriteLine();

            Belepes(megoldas, tippek, szinek);

            Console.WriteLine("Új játék?: Igen/Nem");
            string valasz = Console.ReadLine();
            if (valasz == "Igen")
            {
                megoldas.Clear();
                Generalas(megoldas, szinek);
                Tippeles(tippek, megoldas, szinek);
            }
            if (valasz == "Nem")
            {
                Belepes(megoldas, tippek, szinek);
            }

            Console.ReadKey();
        }
    }
}