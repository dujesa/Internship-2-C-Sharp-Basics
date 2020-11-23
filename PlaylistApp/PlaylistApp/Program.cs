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
            playlist.Add(0, "abdb");
            playlist.Add(1, "a4vgtvdb");
            playlist.Add(2, "oifvhcdb");

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
                        DisplaySongByTitle(playlist);
                        break;
                    case 4:
                        AddNewSong(playlist);
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
                var inputOption = FetchUsersInputForNonExpectedBehaviour();

                if (inputOption == 0) DisplaySongByNumber(playlist);
                
                //return za input-case povratka na početni menu
                return;
            }

            Console.WriteLine($"Naziv tražene pjesme je: {title}");
        }

        private static void DisplaySongByTitle(Dictionary<int, string> playlist)
        {
            Console.WriteLine("Unesite naziv tražene pjesme: ");
            var inputTitle = Console.ReadLine();

            var (number, _) = ProvideSongByUsersInput(Constants.songNotFoundNumber, inputTitle, playlist);

            if (number == Constants.songNotFoundNumber)
            {
                Console.WriteLine($"Pjesma sa nazivom {inputTitle} nije pronađena.");
                var inputOption = FetchUsersInputForNonExpectedBehaviour();

                if (inputOption == 0) DisplaySongByTitle(playlist);

                //return za input-case povratka na početni menu
                return;
            }

            Console.WriteLine($"Redni broj tražene pjesme je: {number}");
        }

        private static void AddNewSong(Dictionary<int, string> playlist)
        {
            int newSongNumber = playlist.Count;
            string newSongTitle;

            Console.WriteLine("Unesite naziv nove pjesme:");
            newSongTitle = Console.ReadLine();

            var isNewSongDataValid = ValidateNewSongData(newSongNumber, newSongTitle, playlist);

            if (isNewSongDataValid)
            {
                playlist.Add(newSongNumber, newSongTitle);
            }
            else
            {
                var inputOption = FetchUsersInputForNonExpectedBehaviour();
                if (inputOption == 0) AddNewSong(playlist);
            }

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

        private static bool ValidateNewSongData(int newSongNumber, string newSongTitle, Dictionary<int, string> playlist)
        {
            var (foundNumber, foundTitle) = ProvideSongByUsersInput(newSongNumber, newSongTitle, playlist);

            if (foundNumber != Constants.songNotFoundNumber)
            {
                Console.WriteLine("Pjesma pod tim nazivom već postoji u listi!");
                return false;
            }

            if (foundTitle.Equals(Constants.songNotFoundTitle) == false)
            {
                Console.WriteLine("Pjesma pod tim rednim brojem već postoji u listi!");
                return false;
            }

            if (playlist.Count != newSongNumber)
            {
                Console.WriteLine($"Unesen broj pjesme nije validan, nesmije biti prazno mjesto u listi nakon zadnje pjesme (koja je pod rbr-om {playlist.Count}.).");
                return false;
            }

            return true;
        }

        static int FetchUsersInputFromMenu()
        {
            DisplayMenu();
            return int.Parse(Console.ReadLine());
        }

        static int FetchUsersInputForNonExpectedBehaviour()
        {
            Console.WriteLine("Ukoliko želite ponoviti prethodnu radnju unesite: 0");
            Console.WriteLine("Ukoliko želite povratak na početni menu unesite: 1");

            return int.Parse(Console.ReadLine());
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Odaberite akciju:");
            Console.WriteLine("1 - Ispis cijele liste");
            Console.WriteLine("2 - Ispis imena pjesme unosom rednog broja pjesme");
            Console.WriteLine("3 - Ispis rednog broja pjesme unosom imena pjesme");
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
