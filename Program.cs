using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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


            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                #region Menü kiírása
                for (int i = 0; i < menupontok.Length; i++)
                {
                    if (i == kivalasztott)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + menupontok[i]);
                }
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < menupontok.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter);


            #endregion
            Console.Clear();
            if (kivalasztott == 0)
            {
                Generalas(megoldas, szinek);
                Tippeles(tippek, megoldas, szinek);
            }
            else if (kivalasztott == 1)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("A játék lényege, hogy a gép által elrejtett 4 golyó színét és ezek sorrendjét kitaláljuk.\n" +
                    "Helyes jelentése: egy golyó színét és helyét is eltaláltuk.\n" +
                    "Van benne jelentése: egy golyó színét eltaláltuk, de a helyét nem.\n" +
                    "Nincs benne jelentése: nem találtuk el a golyó színét.\n" +
                    "Azt, hogy melyik golyóra vonatkoznak azt a gép nem árulja el a gép, ezt nekünk kell kitalálnunk az egyes tippekre adott válaszokból.");
            }
        }

        static void Generalas(List<string> megoldas, string[] szinek)
        {
            while (megoldas.Count!=4)
            {
                int sorszam = rnd.Next(9, szinek.Length);
                if(!megoldas.Contains(szinek[sorszam]))
                {
                    megoldas.Add(szinek[sorszam]);
                }
                
            }
        }

        static void Tippeles(List<string> tippek, List<string> megoldas, string[] szinek)
        {

            string[] eredmeny = new string[4];
            Console.WriteLine("Parancsok: \nKilépés - a játék során bármikor ki lehet lépni a főmenübe\nSzabad a gazda - a játék véget ér és a gép felfedi a megoldást\nParancsok végrehajtása - Írd be a tippelésnél a parancsot!");
            Console.WriteLine();
            int probak = 1;

            while (probak < 11)
            {
                tippek.Clear();
                Array.Clear(eredmeny, 0, eredmeny.Length);

                Console.WriteLine($"\n{probak}. próba - Add meg a színeket: (Kék, Zöld, Cián, Piros, Magenta, Sárga, Fehér)");
                Console.WriteLine();
                for (int i = 1; i < 5; i++)
                {
                    Console.WriteLine($"{i}. szín:");
                    string tipp = Console.ReadLine();
                    if (tipp == "Kilépés")
                    {
                        Belepes(megoldas, tippek, szinek);
                    }
                    if (tipp == "vissza") 
                    {
                        
                    }
                    if (tipp == "szabad a gazda")
                    {
                        Console.WriteLine("\nA megoldás:");

                        for (int line = 0; line < 4; line++)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                int index = 0;
                                for (int j = 0; j < szinek.Length; j++)
                                {
                                    if (megoldas[k] == szinek[j])
                                    {
                                        index = j;
                                        break;
                                    }
                                }

                                Console.ForegroundColor = (ConsoleColor)index;

                                if (line == 0 || line == 3)
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
                        tippek.Add(tipp);
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


                for (int line = 0; line < 4; line++)
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


                        if (line == 0 || line == 3)
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
                    Console.WriteLine();
                    Console.WriteLine("Gratulálok, eltaláltad a megoldást!");
                    break;
                }

                probak++;
            }

            if (probak > 11 && !eredmeny.All(e => e == "jó"))
            {
                Console.WriteLine("Sajnálom, nem találtad el a megoldást.");
            }
            Console.WriteLine();
            Console.WriteLine("Új játék?: Igen/Nem");
            string valasz = Console.ReadLine();
            if (valasz == "Igen")
            {
                Generalas(megoldas, szinek);
                Tippeles(tippek, megoldas, szinek);
            }
            if (valasz == "Nem")
            {
                Belepes(megoldas, tippek, szinek);
            }
        }


        static void Main(string[] args)
        {
            List<string> megoldas = new List<string>();
            List<string> tippek = new List<string>();
            string[] szinek = new string[16];
            szinek[9] = "Kék";
            szinek[10] = "Zöld";
            szinek[11] = "Cián";
            szinek[12] = "Piros";
            szinek[13] = "Magenta";
            szinek[14] = "Sárga";
            szinek[15] = "Fehér";

            Console.WriteLine();


            Belepes(megoldas, tippek, szinek);

            Console.ReadKey();
        }
    }

}
