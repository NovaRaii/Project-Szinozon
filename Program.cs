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
        //feladat: 11 próba, kilépés gomb, szabad a gazda gomb, utasítások

        static Random rnd = new Random();
        //static string[] szinek = { "piros", "fehér", "kék", "rózsaszín", "narancs", "lila", "sárga", "zöld" };
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
            for (int i = 0; i < megoldas.Count; i++)
            {
                Console.WriteLine(megoldas[i]);
            }
        }

        static void Tippeles(List<string> tippek, List<string> megoldas, string[] szinek)
        {
            string[] eredmeny = new string[4];
            bool van = false;
            Console.WriteLine();
            Console.WriteLine("Parancsok: \nkilépés - a játék során bármior ki lehet lépni (ESC gomb)\nszabad a gazda - a játék véget ér és a gép felfedi a megoldást");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Add meg a színeket:");
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine($"{i}. szín");
                string tipp = Console.ReadLine();
                tippek.Add(tipp);
                
            }
            int y = 0;
            for (int i = 0; i < megoldas.Count; i++)
            {
                for (y = 0; y < tippek.Count; y++)
                {
                    
                    if (megoldas[i] == tippek[i]) 
                    {
                        eredmeny[i] = "jó";
                        van = true;
                        continue;
                        
                    }
                    else if (megoldas[i] == tippek[y])
                    {
                        eredmeny[i] = "van benne";
                        van = true;
                        continue;
                    }
                    if (van == false)
                    {
                        Console.WriteLine("nincs benne");
                        continue;
                    }
                    van = false;
                   
                    
                }
                y = 0;
            }
            for (int i = 0; i < eredmeny.Length; i++)
            {
                Console.WriteLine(eredmeny[i]);
            }



            int index = 0;
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                for (int x = 0; x < tippek.Count; x++)
                {
                    for (int j = 0; j < szinek.Length; j++)
                    {
                        if (tippek[i] == szinek[j])
                        {
                            index = j;
                        }
                    }
                }
                Console.ForegroundColor = (ConsoleColor)index;
                Console.WriteLine("****");
                Console.WriteLine("*  *");
                Console.WriteLine("*  *");
                Console.WriteLine("****");
                

                index = 0;
               
            }


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

        }
    }

}
