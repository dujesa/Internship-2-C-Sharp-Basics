using System;
using System.Collections.Generic;

namespace PlaylistApp
{
    static class Constants
    {
        public const int songNotFoundNumber = -1;
        public const string songNotFoundTitle = "Not found";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var usersInput = 0;
            Dictionary<int, string> playlist = new Dictionary<int, string>();
            playlist.Add(1, "abdb");
            playlist.Add(2, "a4vgtvdb");
            playlist.Add(3, "oifvhcdb");

            Console.WriteLine("Dobrodošli u Playlist aplikaciju.");

            do
            {
                usersInput = FetchUsersInputFromMenu();
                Console.Clear();

                switch (usersInput)
                {
                    case 1:
                        DisplayPlaylist(playlist);
                        break;
                    case 2:
                        DisplaySongByNumber(playlist);
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
                Console.WriteLine();

            } while (usersInput != 0);

            Console.WriteLine("Hvala na korištenju Playlist aplikacije.");
        }

        private static void DisplayPlaylist(Dictionary<int, string> playlist)
        {
            Console.WriteLine("Ispis pjesama na listi:");
            foreach ((int songnumber, string songTitle) in playlist)
            {
                Console.WriteLine($"{songnumber}. - {songTitle}");
            }
        }

        private static void DisplaySongByNumber(Dictionary<int, string> playlist)
        {
            Console.WriteLine("Unesite redni broj tražene pjesme: ");
            var inputNumber = int.Parse(Console.ReadLine());

            var (_, title) = ProvideSongByUsersInput(inputNumber, "", playlist);

            if (title.Equals(Constants.songNotFoundTitle))
            {
                Console.WriteLine($"Pjesma sa rednim brojem {inputNumber} nije pronađena.");
                Console.WriteLine("Ukoliko želite ponoviti pretragu rednim brojem unesite: 0");
                Console.WriteLine("Ukoliko želite povratak na početni menu unesite: 1");

                var inputOption = int.Parse(Console.ReadLine());

                if (inputOption == 0) DisplaySongByNumber(playlist);
                
                //return za input-case povratka na početni menu
                return;
            }

            Console.WriteLine($"Naziv tražene pjesme je: {title}");
        }

        private static void DisplaySongNumberByTitle(Dictionary<int, string> playlist)
        { 
            
        }

        private static (int number, string title) ProvideSongByUsersInput(int searchingNumber, string searchingTitle, Dictionary<int, string> playlist)
        {
            var song = (Constants.songNotFoundNumber, Constants.songNotFoundTitle);

            foreach ((int songNumber, string songTitle) in playlist)
            {
                if (searchingNumber == songNumber || searchingTitle == songTitle)
                {
                    song = (songNumber, songTitle);
                    break;
                }
            }

            return song;
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
