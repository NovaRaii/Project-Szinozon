using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                Console.ResetColor();
                Console.WriteLine("A játék lényege, hogy a gép által elrejtett 4 golyó színét és ezek sorrendjét kitaláljuk. Minden egyes tippünk után a gép " +
                    "fekete és/vagy fehér \"tüskékkel\" válaszol. A fehér tüske jelentése: egy golyó színét eltaláltuk (de a helyét nem), a fekete tüske " +
                    "jelentése: egy golyó színét és helyét is eltaláltuk. Azt, hogy melyik golyóra vonatkoznak az egyes tüskék azt nem árulja " +
                    "el a gép, ezt nekünk kell kitalálnunk az egyes tippekre adott válaszokból.");
            }
        }

        static void Generalas(List<string> megoldas, string[] szinek)
        {
            for (int i = 0; i < 4; i++)
            {
                int sorszam = rnd.Next(9, szinek.Length);
                megoldas.Add(szinek[sorszam]);
            }
        }

        static void Tippeles(List<string> tippek, List<string> megoldas, string[] szinek)
        {
            
            string[] eredmeny = new string[4];
            Console.WriteLine("Parancsok: \nKilépés - a játék során bármikor ki lehet lépni a főmenübe\nSzabad a gazda - a játék véget ér és a gép felfedi a megoldást\nParancsok végrehajtása - Írd be a tippelésnél a parancsot!");
            Console.WriteLine();

            while (!(eredmeny[0] == "jó" && eredmeny[1] == "jó" && eredmeny[2] == "jó" && eredmeny[3] == "jó"))
            {
                tippek.Clear();
                Array.Clear(eredmeny, 0, eredmeny.Length);

                Console.WriteLine("\nAdd meg a színeket: (Blue,Green,Cyan,Red,Magenta,Yellow,White)");
                for (int i = 1; i < 5; i++)
                {
                    Console.WriteLine($"{i}. szín:");
                    string tipp = Console.ReadLine();
                    if (tipp == "Kilépés")
                    {
                        Belepes(megoldas, tippek, szinek);
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
                        Console.ResetColor();
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
                        eredmeny[i] = "jó";
                    }
                    else if (megoldas.Contains(tippek[i]))
                    {
                        eredmeny[i] = "van benne";
                    }
                    else
                    {
                        eredmeny[i] = "nincs benne";
                    }
                }


                for (int i = 0; i < eredmeny.Length; i++)
                {
                    Console.Write($"{eredmeny[i]}  ");
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

                Console.ResetColor(); 
            }

            Console.WriteLine("Gratulálok, eltaláltad a megoldást!");
        }


        static void Main(string[] args)
        {
            List<string> megoldas = new List<string>();
            List<string> tippek = new List<string>();
            string[] szinek = new string[16];
            szinek[9] = "Blue";
            szinek[10] = "Green";
            szinek[11] = "Cyan";
            szinek[12] = "Red";
            szinek[13] = "Magenta";
            szinek[14] = "Yellow";
            szinek[15] = "White";

            Console.WriteLine();


            Belepes(megoldas, tippek, szinek);

            Console.ReadKey();
        }
    }

}