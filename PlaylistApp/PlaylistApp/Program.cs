using System;

namespace PlaylistApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var usersInput = 0;
            
            do
            {
                Console.Clear();
                usersInput = FetchUsersInputFromMenu();
                Console.WriteLine("You have inputed " + usersInput);
            } while (usersInput != 0);
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
