using System;
using System.Collections.Generic;

namespace PlaylistApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var usersInput = 0;
            Dictionary<int, string> playlist = new Dictionary<int, string>();

            Console.WriteLine("Dobrodošli u Playlist aplikaciju.");

            do
            {
                Console.Clear();
                usersInput = FetchUsersInputFromMenu();

                switch (usersInput)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                }

            } while (usersInput != 0);

            Console.WriteLine("Hvala na korištenju Playlist aplikacije.");
        }

        static int FetchUsersInputFromMenu()
        {
            DisplayMenu();
            return int.Parse(Console.ReadLine());
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Odaberite akciju:");
            Console.WriteLine("1 - Ispis cijele liste");
            Console.WriteLine("2 - Ispis imena pjesme unosom pripadajućeg rednog broja");
            Console.WriteLine("3 - Ispis rednog broja pjesme unosom pripadajućeg broja");
            Console.WriteLine("4 - Unos nove pjesme");
            Console.WriteLine("5 - Brisanje pjesme po rednom broju");
            Console.WriteLine("6 - Brisanje pjesme po imenu");
            Console.WriteLine("7 - Brisanje cijele liste");
            Console.WriteLine("8 - Uređivanje imena pjesme");
            Console.WriteLine("9 - Uređivanje rednog broja pjesme");
            Console.WriteLine("10 - Shuffle pjesama");
            Console.WriteLine("11 - Izvoz liste pjesama u eksternu datoteku");
            Console.WriteLine("0 - Izlaz iz aplikacije");
        }
    }
}
