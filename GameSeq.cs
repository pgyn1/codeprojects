using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReturnChar
{
    class GameSeq
    {
        //
        public SaveRestoreFunc StartNewRestore = new SaveRestoreFunc();
        public Valid ValidGameSeq = new Valid();
        public GetStartGameOptions getstart = new GetStartGameOptions();
        private Stopwatch stopwatchtime = new Stopwatch();
        public Menu UserBoxMenu = new Menu();
        public UserBox UserBoxGameSeq = new UserBox();
        public Random RandGameSeq = new Random();
        public Book BookGameSeq = new Book();
        public Player game = new Player();

        //
        public string GameOver;
        public string NoElementsInSequence;
        public string GameModelZero;
        public string GameModelOne;

        public bool IsPlaying { get; set; }
        public int Timer { get; set; }
        public int GameScore { get; set; }
        public char DiffLvl { get; set; }
        public char Mode { get; set; }
        public char NewRestore { get; set; }
        public int RandNumber { get; set; }
        public string[] RestoredSavedArray { get; set; }
        public List<Book> GameArray { get; set; }
        public TimeSpan TimeSpanTimer { get; set; }

        public GameSeq(char newrestore, char difflvl, char mode)
        {

            //if (timespaner.Equals(null)){}TimeSpan timespaner
            NewRestore = newrestore;
            DiffLvl = difflvl;
            Mode = mode; //'X' timer option, 'Y' list==0, 'Z' Target Score
            IsPlaying = true;
            GameArray = new List<Book>();
            GameOver = "\t\t\tGame Over";
            GameModelZero = "\t\t\tGame model zero";
            GameModelOne = "\t\t\tGame model one";
            NoElementsInSequence = "\t\t\t!Any() Sequence";
            RandNumber = RandGameSeq.Next(2); // Return a random number < 2 for the gamemodel cycles
            
            Game();
        }

        static GameSeq()
        {

        }

        public void Game()
        {
            if(NewRestore.Equals('R') || NewRestore.Equals('N'))
            {
                try
                {
                    Player.RestoreComputerScore(0);
                    Player.RestorePlayerScore(0);

                    switch (Mode.ToString().ToLower())
                    {
                        case "x":
                            Timer = GetStartGameOptions.GetTimer();
                            //TimeSpanTimer = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + Timer, DateTime.Now.Second);
                            stopwatchtime.Start();
                            break;
                        case "z":
                            GameScore = GetStartGameOptions.GetGameScore();
                            break;
                        default:
                            break;
                    }

                    //Book.SetReadFromStream(GetStartGameOptions.GetGameFileList());
                    Book.SetDictionary();
                    Valid.SetValidDictionary(Book.GetDictionary());
                    Valid.GetSelectedBooks().Clear();

                    switch (DiffLvl.ToString().ToLower())
                    {
                        case "e":
                        case "m":
                            //Display rules
                            GameArray = BookGameSeq.GetGameArray(DiffLvl);
                            if (GameArray.Count() == 0 || GameArray == null) { } else { GetGameModelZero(); }
                            break;
                        case "h":
                        case "s":
                            //
                            GameArray = BookGameSeq.GetGameArray(DiffLvl);
                            if (GameArray.Count() == 0 || GameArray == null) { } else { GetGameModelOne(); }
                            break;
                        default:
                            //if (BookGameSeq.GetComStateListEasy()) GameArray = Book.ComStateList;
                            GameArray = BookGameSeq.GetGameArray(DiffLvl);
                            if (GameArray.Count() == 0 || GameArray == null) { } else { GetGameModelZero(); }
                            break;
                    }


                }
                catch (ArgumentNullException argexc)
                {
                    Console.WriteLine($"GameSeq -> Game() + {argexc.Message}\n{argexc.InnerException}");
                    Console.WriteLine("Game array = null");
                    Console.WriteLine("Return to menu");
                    Console.ReadKey();
                    Menu.DisplayMenuOptions();
                    //Environment.Exit(Environment.ExitCode);

                }

                catch (Exception e)
                {
                    Console.WriteLine("GameSeq -> Game()");
                    Console.WriteLine($"{e.Message}\n{e.InnerException}");
                    Console.WriteLine("Return to menu");
                    Console.ReadKey();
                    Menu.DisplayMenuOptions();
                }
            }
         
 

        }

        public void GameModelZeroDisplays()
        {
            // repeats until computer list exh, player quits, time limit or score 
            UserBoxGameSeq.SetBorderDisplay();
            Console.WriteLine($"{GameModelZero}");
        }

        public void GameModelOneDisplays()
        {
            // repeats until computer list exh, player quits, time limit or score 
            UserBoxGameSeq.SetBorderDisplay();
            Console.WriteLine($"{GameModelOne}");
        }

        //Model1 // Returning a random char in zero mode  
        public void GetGameModelZero()
        {
            GameModelZeroDisplays();

            if (RandNumber == 0)
            {
                while (IsPlaying)
                {
                    UserBoxGameSeq.SetOne(DiffLvl,Mode);

                    Player.SetComputerInput(GameArray); // Computer set an answer from finite list
                    IfContains(Player.GetComputerInput()); //If contains reducing the GameArray
                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput()); // player responds 

                    if (Mode.ToString().ToUpper().Equals("Z")) { if (!IfScore()) break; }
                    IfContains(Player.GetPlayerInput());

                    UserBoxGameSeq.NextTurn();

                    Player.SetPlayerInput(); // player sets an answer from 'finite' 
                    IfContains(Player.GetPlayerInput());
                    Player.SetComputerInput(GameArray, Player.GetPlayerInput()); // computer responds
                    IfContains(Player.GetComputerInput());

                    if (!UserBoxGameSeq.IsPlayingGameDisplay()) { break; } //If bool return false call the main menu option and break from loop
                    SwitchMode();
                    
                }

                SaveRestoreFunc.WriteScores(Player.GetComputerScore(), Player.GetPlayerScore());
                Console.ReadKey();
                Menu.DisplayMenuOptions();
            }
            else
            {
                while (IsPlaying)
                {
                    UserBoxGameSeq.SetOne(DiffLvl,Mode);

                    Player.SetPlayerInput(); // player sets an answer from 'finite' list
                    IfContains(Player.GetPlayerInput());
                    Player.SetComputerInput(GameArray, Player.GetPlayerInput()); // computer responds
                    if (Mode.ToString().ToUpper().Equals("Z")) { if (!IfScore()) break; }
                    IfContains(Player.GetComputerInput());

                    UserBoxGameSeq.NextTurn();

                    Player.SetComputerInput(GameArray); // Computer set an answer from finite list
                    IfContains(Player.GetComputerInput());
                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput()); // player responds 
                    IfContains(Player.GetPlayerInput());

                    if (!UserBoxGameSeq.IsPlayingGameDisplay()) { break; } //If bool return false call the main menu option and break from loop
                    SwitchMode();
                }

                SaveRestoreFunc.WriteScores(Player.GetComputerScore(), Player.GetPlayerScore());
                Console.ReadKey();
                Menu.DisplayMenuOptions();
            }

        }

        //Model2
        public void GetGameModelOne()
        {
            GameModelOneDisplays();

            if (RandNumber == 0)
            {
                //if 0 the comp turns 1st which means the player must have a random input
                Player.SetPlayerInput(Player.GetAlphabet());
                Console.WriteLine($"\t\t\tReturn Char {Player.GetPlayerInput()}");
                Console.ReadLine();

                while (IsPlaying)
                {
                    UserBoxGameSeq.SetOne(DiffLvl, Mode);
                    Player.SetComputerInput(GameArray, Player.GetPlayerInput()); //computerinput is set based upon the options in the gamearray and last letter of the playerinput
                    IfContains(Player.GetComputerInput());
                    if (Mode.Equals('Z')) if (!IfScore()) break; //If mode, ifscore returns false break loop

                    UserBoxGameSeq.NextTurn();

                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput());
                    IfContains(Player.GetPlayerInput());

                    if (!UserBoxGameSeq.IsPlayingGameDisplay()) { break; } //If bool return false call the main menu option and break from loop
                    SwitchMode();
                }

                SaveRestoreFunc.WriteScores(Player.GetComputerScore(), Player.GetPlayerScore());
                Console.ReadKey();
                Menu.DisplayMenuOptions();
            }
            else
            {
                //else the player 1st which means the computer must have a random input // A pass_ or a invalid response sets the prev to a random char GetAlphabet()
                Player.SetComputerInput(Player.GetAlphabet());
                Console.WriteLine($"\t\t\tReturn Char {Player.GetComputerInput()}");
                Console.ReadLine();

                while (IsPlaying)
                {
                    UserBoxGameSeq.SetOne(DiffLvl, Mode);
                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput()); //player input is based upon the last letter of the computerinput
                    IfContains(Player.GetPlayerInput());
                    if (Mode.Equals('Z')) if (!IfScore()) break;

                    UserBoxGameSeq.NextTurn();

                    Player.SetComputerInput(GameArray, Player.GetPlayerInput());
                    IfContains(Player.GetComputerInput());

                    if (!UserBoxGameSeq.IsPlayingGameDisplay()) { break; } //If bool returns false call the main menu option and break from loop
                    SwitchMode();
                }

                SaveRestoreFunc.WriteScores(Player.GetComputerScore(), Player.GetPlayerScore());
                Console.ReadKey();
                Menu.DisplayMenuOptions();
            }

        }

        // X Y Z  TIME EMP T
        public void SwitchMode()
        {
            switch (Mode.ToString().ToLower())
            {
                case "x":
                    GameSeqTimer();
                    //Mode = timer 
                    break;
                case "y":
                    GameSeqEmpty();
                    // Empty List
                    break;
                case "z":
                    GameSeqTarget();
                    // GameScore / TargetScore
                    break;
                default:
                    GameSeqTarget();
                    break;

            }
        }

        //TIME
        public void GameSeqTimer()
        {
            //Set timer in minutes 
            //consider stopwatch/timespan

            //if (new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) >= (TimeSpanTimer))
            //{
            //    Console.WriteLine($"{GameOver}");
            //    UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
            //    IsPlaying = false;
            //    stopwatchtime.Stop();
            //}

            if (stopwatchtime.ElapsedMilliseconds >= Timer)
            {
                Console.WriteLine($"{GameOver} Final time = {stopwatchtime.Elapsed}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                stopwatchtime.Stop();
                IsPlaying = false;
            }
            else if (!GameArray.Any())
            {
                Console.WriteLine($"{NoElementsInSequence}");
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;
            }
            else
            {
                UserBoxGameSeq.IsPlayingGameDisplays(DiffLvl, Mode, GameScore, Timer, Player.GetPlayerScore(), Player.GetComputerScore(), RandNumber, GameArray);
                DisplayerTimer();
            }
        }

        //EMP
        public void GameSeqEmpty()
        {
            // If list empty
            if (!GameArray.Any())
            {
                Console.WriteLine($"{NoElementsInSequence}");
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;
            }
            else
            {
                UserBoxGameSeq.IsPlayingGameDisplays(DiffLvl, Mode, GameScore, Timer, Player.GetPlayerScore(), Player.GetComputerScore(), RandNumber, GameArray);
            }
        }

        //T
        public void GameSeqTarget()
        {
            
            if (Player.GetComputerScore() == GameScore || Player.GetPlayerScore() == GameScore)
            {
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;

            }
            else if (!GameArray.Any())
            {
                Console.WriteLine($"{NoElementsInSequence}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;

            }
            else
            {
                UserBoxGameSeq.IsPlayingGameDisplays(DiffLvl, Mode, GameScore, Timer, Player.GetPlayerScore(), Player.GetComputerScore(), RandNumber, GameArray);
            }

        }

        public void DisplayerTimer()
        {
          
                switch (Timer)
                {
                    case 60000:
                        Console.Write($"\t\t\tCurrent Time = {stopwatchtime.Elapsed} Time limit = 1 minute");
                        break;
                    case 120000:
                        Console.Write($"\t\t\tCurrent Time = {stopwatchtime.Elapsed} Time limit = 2 minutes");
                        break;
                    case 180000:
                        Console.Write($"\t\t\tCurrent Time = {stopwatchtime.Elapsed} Time limit = 3 minutes");
                        break;
                    case 240000:
                        Console.Write($"\t\t\tCurrent Time = {stopwatchtime.Elapsed} Time limit = 4 minutes");
                        break;
                    case 300000:
                        Console.Write($"\t\t\tCurrent Time = {stopwatchtime.Elapsed} Time limit = 5 minutes ");
                        break;
                    default:
                        Console.Write($"\t\t\tCurrent Time = {stopwatchtime.Elapsed} Time limit = Timer limit n/a");
                        break;
                }

        }

        public bool IfScore()
        {
            if (Player.GetComputerScore() == GameScore || Player.GetPlayerScore() == GameScore)
            {
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                return false;
            }
            else
            {
                return true;
            }

        }

        private void IfContains(string inputx)
        {

            for (int i = 0; i < GameArray.Count(); i++)
            {
                if (GameArray[i].Name.ToLower().Equals(inputx.ToLower())) GameArray.Remove(GameArray[i]);
            }

        }

    }
}
