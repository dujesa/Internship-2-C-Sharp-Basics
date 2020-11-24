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
                        DeleteSongByNumber(playlist);
                        break;
                    case 6:
                        DeleteSongByTitle(playlist);
                        break;
                    case 7:
                        DeletePlaylist(playlist);
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
            int newSongNumber = playlist.Count + 1;
            string newSongTitle;

            Console.WriteLine("Unesite naziv nove pjesme:");
            newSongTitle = Console.ReadLine();

            var newSongDataState = ValidateNewSongData(newSongNumber, newSongTitle, playlist);

            if (newSongDataState == SongValidationStates.Valid)
            {
                playlist.Add(newSongNumber, newSongTitle);
            }
            else
            {
                var inputOption = FetchUsersInputForNonExpectedBehaviour();
                if (inputOption == 0) AddNewSong(playlist);
            }

        }

        private static void DeleteSongByNumber(Dictionary<int, string> playlist)
        {
            Console.WriteLine("Unesite redni broj tražene pjesme: ");
            var inputNumber = int.Parse(Console.ReadLine());

            var (_, title) = ProvideSongByUsersInput(inputNumber, "", playlist);

            if (title.Equals(Constants.songNotFoundTitle))
            {
                Console.WriteLine($"Pjesma sa rednim brojem {inputNumber} nije pronađena.");
                var inputOption = FetchUsersInputForNonExpectedBehaviour();

                if (inputOption == 0) DeleteSongByNumber(playlist);

                //return za input-case povratka na početni menu
                return;
            }

            RemoveSongFromPlaylist(inputNumber, playlist);
        }

        private static void DeleteSongByTitle(Dictionary<int, string> playlist)
        {
            Console.WriteLine("Unesite naziv tražene pjesme: ");
            var inputTitle = Console.ReadLine();

            var (number, _) = ProvideSongByUsersInput(Constants.songNotFoundNumber, inputTitle, playlist);

            if (number == Constants.songNotFoundNumber)
            {
                Console.WriteLine($"Pjesma sa nazivom {inputTitle} nije pronađena.");
                var inputOption = FetchUsersInputForNonExpectedBehaviour();

                if (inputOption == 0) DeleteSongByTitle(playlist);

                //return za input-case povratka na početni menu
                return;
            }

            RemoveSongFromPlaylist(number, playlist);
        }

        private static void DeletePlaylist(Dictionary<int, string> playlist)
        {
            Console.WriteLine($"Jeste li sigurni da želite izbrisati cijelu listu pjesama?");

            var isDeletionConfirmed = FetchUserConfirmation();

            if (isDeletionConfirmed == false)
            {
                return;
            }

            playlist.Clear();

            Console.WriteLine("Lista je pobrisana.");
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

        private static SongValidationStates ValidateNewSongData(int newSongNumber, string newSongTitle, Dictionary<int, string> playlist)
        {
            var (foundNumber, foundTitle) = ProvideSongByUsersInput(newSongNumber, newSongTitle, playlist);

            if (foundNumber != Constants.songNotFoundNumber && foundTitle.Equals(Constants.songNotFoundTitle) == false)
            {
                Console.WriteLine("Pjesma pod tim nazivom i rednim brojem već postoji u listi!");
                return SongValidationStates.ExistentNumberAndTitle;
            }

            if (foundNumber != Constants.songNotFoundNumber)
            {
                Console.WriteLine("Pjesma pod tim nazivom već postoji u listi!");

                return SongValidationStates.ExistentNumber;
            }

            if (foundTitle.Equals(Constants.songNotFoundTitle) == false)
            {
                Console.WriteLine("Pjesma pod tim rednim brojem već postoji u listi!");

                return SongValidationStates.ExistentTitle;
            }

            if (playlist.Count + 1 != newSongNumber)
            {
                Console.WriteLine($"Unesen broj pjesme nije validan, nesmije biti prazno mjesto u listi nakon zadnje pjesme (koja je pod rbr-om {playlist.Count}.).");

                return SongValidationStates.InvalidNumber;
            }


            return SongValidationStates.Valid;
        }

        private static void RemoveSongFromPlaylist(int songNumber, Dictionary<int, string> playlist)
        {
            var title = playlist[songNumber];

            Console.WriteLine($"Jeste li sigurni da želite izbrisati pjesmu '{title}'?");

            var isDeletionConfirmed = FetchUserConfirmation();

            if (isDeletionConfirmed == false)
            {
                return;
            }

            playlist.Remove(songNumber);

            for (var i = songNumber; i <= playlist.Count; i++)
            {
                playlist[i] = playlist[i + 1];
                playlist.Remove(i + 1);
            }

            Console.WriteLine($"Pjesma '{title}' je izbrisana.");
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

        static bool FetchUserConfirmation()
        {
            Console.WriteLine("Unesite:");
            Console.WriteLine("0 - Odustani");
            Console.WriteLine("1 - Potvrdi");

            var confirmationInput = int.Parse(Console.ReadLine());

            return (confirmationInput == 1);
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

    enum SongValidationStates
    { 
        Valid = 0,
        ExistentNumber = 1,
        ExistentTitle = 2,
        ExistentNumberAndTitle = 3,
        InvalidNumber = 4
    }
}
