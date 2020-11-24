using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                        EditSongTitle(playlist);
                        break;
                    case 9:
                        EditSongNumber(playlist);
                        break;
                    case 10:
                        ShuffleSongs(playlist);
                        break;
                    case 21:
                        ExportPlaylist(playlist);
                        break;
                    case 22:
                        playlist = ImportPlaylist();
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

            var (newSongDataState, validationMessage) = ValidateNewSongData(newSongNumber, newSongTitle, playlist);

            if (newSongDataState == SongValidationStates.Valid)
            {
                playlist.Add(newSongNumber, newSongTitle);
            }
            else
            {
                Console.WriteLine(validationMessage);

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

        private static void EditSongTitle(Dictionary<int, string> playlist)
        {
            Console.WriteLine("Unesite naziv pjesme koji želite urediti:");
            var inputTitle = Console.ReadLine();

            var (number, _) = ProvideSongByUsersInput(Constants.songNotFoundNumber, inputTitle, playlist);

            if (number == Constants.songNotFoundNumber)
            {
                Console.WriteLine($"Pjesma sa nazivom {inputTitle} nije pronađena.");
                var inputOption = FetchUsersInputForNonExpectedBehaviour();

                if (inputOption == 0) EditSongTitle(playlist);

                //return za input-case povratka na početni menu
                return;
            }

            Console.WriteLine("Unesite novi naziv za pjesmu:");
            var newTitle = Console.ReadLine();

            var (newSongDataState, validationMessage) = ValidateNewSongData(number, newTitle, playlist);
            bool isNewSongDataValid = (newSongDataState == SongValidationStates.Valid || newSongDataState == SongValidationStates.ExistentNumber);
            
            if (isNewSongDataValid == false)
            {
                Console.WriteLine(validationMessage);
                Console.WriteLine("Uređivanje naziva pjesme je neuspješno.");
                var inputOption = FetchUsersInputForNonExpectedBehaviour();

                if (inputOption == 0) EditSongTitle(playlist);

                //return za input-case povratka na početni menu
                return;
            }

            playlist[number] = newTitle;
            Console.WriteLine($"Pjesma je uspješno preimenovana iz '{inputTitle}' u '{newTitle}'");
        }

        private static void EditSongNumber(Dictionary<int, string> playlist)
        {
            string secondSongTitle;
            int secondInputNumber;

            Console.WriteLine("Unesite redni broj tražene pjesme: ");
            var firstInputNumber = int.Parse(Console.ReadLine());

            var (_, firstSongTitle) = ProvideSongByUsersInput(firstInputNumber, "", playlist);

            if (firstSongTitle.Equals(Constants.songNotFoundTitle))
            {
                Console.WriteLine($"Pjesma sa rednim brojem {firstInputNumber} nije pronađena.");
                var inputOption = FetchUsersInputForNonExpectedBehaviour();

                if (inputOption == 0) EditSongNumber(playlist);

                //return za input-case povratka na početni menu
                return;
            }

            while (true)
            { 
                Console.WriteLine($"Unesite redni broj pjesme sa kojom želite zamjeniti pjesmu '{firstSongTitle}':");
                var replacementInputNumber = int.Parse(Console.ReadLine());

                var (_, replacementSongTitle) = ProvideSongByUsersInput(replacementInputNumber, "", playlist);

                if (replacementSongTitle.Equals(Constants.songNotFoundTitle))
                {
                    Console.WriteLine($"Pjesma sa rednim brojem {replacementInputNumber} nije pronađena.");
                    var inputOption = FetchUsersInputForNonExpectedBehaviour();

                    if (inputOption != 0) return;
                }
                else
                {
                    secondSongTitle = replacementSongTitle;
                    secondInputNumber = replacementInputNumber;
                    break;
                }

            }

            playlist[firstInputNumber] = secondSongTitle;
            playlist[secondInputNumber] = firstSongTitle;

            Console.WriteLine($"Mjesta pjesama '{firstSongTitle}' i '{secondSongTitle}' su uspješno zamjenjena.");
        }

        private static void ShuffleSongs(Dictionary<int, string> playlist)
        {
            List<int> numberList = new List<int>(playlist.Count);
            Random random = new Random();

            foreach (var song in playlist)
            {
                numberList.Add(song.Key);                
            }

            for (int i = 1; i < playlist.Count; i++)
            {
                int randomNumber = random.Next(0, numberList.Count);

                var shuffleIndex = numberList[randomNumber];

                var helperSongContainer = playlist[i];
                playlist[i] = playlist[shuffleIndex];
                playlist[shuffleIndex] = helperSongContainer;

                numberList.RemoveAt(randomNumber);
            }

            Console.WriteLine("Lista pjesama izmješana!");
            DisplayPlaylist(playlist);
        }

        private static void ExportPlaylist(Dictionary<int, string> playlist)
        {
            String playlistCsv = String.Join(
                Environment.NewLine,
                playlist.Select(
                    song => $"{song.Key};{song.Value};"
                )
            );

            var playlistCsvPath = System.IO.Directory.GetCurrentDirectory() + "/playlist.csv";
            
            System.IO.File.WriteAllText(playlistCsvPath, playlistCsv);
            Console.WriteLine($"Spremljeno u: {playlistCsvPath}");
        }

        private static Dictionary<int, string> ImportPlaylist()
        {
            var playlistCsvPath = System.IO.Directory.GetCurrentDirectory() + "/playlist.csv";

            var playlist = File.ReadLines(path: playlistCsvPath).Select(song => song.Split(';')).ToDictionary(song => int.Parse(song[0]), song => song[1]);

            Console.WriteLine($"Lista pjesama uspješno učitana iz: {playlistCsvPath}");
            DisplayPlaylist(playlist);

            return playlist;
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

        private static (SongValidationStates, string) ValidateNewSongData(int newSongNumber, string newSongTitle, Dictionary<int, string> playlist)
        {
            if (newSongTitle.Length == 0)
            {
                return (SongValidationStates.InvalidTitle, "Pjesma nemože imati prazan naziv!");
            }

            var (foundNumber, foundTitle) = ProvideSongByUsersInput(newSongNumber, newSongTitle, playlist);

            if (foundNumber != Constants.songNotFoundNumber && foundTitle.Equals(newSongTitle))
            {
                return (SongValidationStates.ExistentNumberAndTitle, "Pjesma pod tim nazivom i rednim brojem već postoji u listi!");
            }

            if (foundNumber != Constants.songNotFoundNumber)
            {
                return (SongValidationStates.ExistentNumber, "Pjesma pod tim rednim brojem već postoji u listi!");
            }

            if (foundTitle.Equals(newSongTitle))
            {
                return (SongValidationStates.ExistentTitle, "Pjesma pod tim nazivom već postoji u listi!");
            }

            if (playlist.Count + 1 != newSongNumber)
            {
                return (SongValidationStates.InvalidNumber, $"Unesen broj pjesme nije validan, nesmije biti prazno mjesto u listi nakon zadnje pjesme (koja je pod rbr-om {playlist.Count}.).");
            }


            return (SongValidationStates.Valid, "Podaci za pjesmu su ispravni");
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
            Console.WriteLine("0 - Izlaz iz aplikacije");
            Console.WriteLine("---- Akcije za olakšavanje developmenta i testiranja ----");
            Console.WriteLine("21 - Save playlist (Izvoz liste pjesama u eksternu datoteku)");
            Console.WriteLine("22 - Load playlist (Uvoz liste pjesama iz eksternu datoteku)");
        }
    }

    enum SongValidationStates
    { 
        Valid = 0,
        ExistentNumber = 1,
        ExistentTitle = 2,
        ExistentNumberAndTitle = 3,
        InvalidNumber = 4,
        InvalidTitle = 5
    }
}
