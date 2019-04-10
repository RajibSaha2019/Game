using System;

namespace GRPS
{

    public interface IWriter
    {
        void WriteFile(string Message);
    }
       public class FileLogging: IWriter
          {

            private readonly IWriter _IWriter;

        public FileLogging()
        {
        }

        public FileLogging(IWriter IWriter)
            {
                this._IWriter = IWriter;

            }
            public void WriteFile(string Message)
            {
                System.IO.File.WriteAllText(@"c:\Error.txt", Message);
            }
        }

    enum Selection
    {
        Rock=0,
        Paper=1,
        Scissors=2
    }
    abstract class Contestant
    {
        public int wins { get; set; }
       
        protected Selection selection;
        public abstract Selection Select();
    }

    class Computer : Contestant
    {

        IWriter Logger = new FileLogging();
        public override Selection Select()
        {
            try
            {
                Random rand = new Random();
                selection = (Selection)rand.Next(0, Enum.GetValues(typeof(Selection)).Length);
            }
            catch (Exception ex)
            {
                Logger.WriteFile(ex.ToString());
            }
            finally
            {
                Logger.WriteFile("Computer Class");
            }
            return selection;

        }
    }
    class Player : Contestant
    {
        IWriter Logger = new FileLogging();
        public override Selection Select()
        {
            bool isValid;
            string input;
            try
            {
                do
                {
                    int myInt;
                    Console.Write("Select 0 for Rock, 1 for Paper, 2 For Scissors.");
                    input = Console.ReadLine().Trim();
                    bool isNumerical = int.TryParse(input, out myInt);
                    if (isNumerical && myInt > 2)
                    {
                        isValid = false;
                    }
                    else
                    {
                        isValid = Enum.TryParse<Selection>(input, true, out selection);
                    }
                } while (!isValid);
            }
            catch (Exception ex)
            {
                Logger.WriteFile(ex.ToString());
            }
            finally
                {
                Logger.WriteFile("Player Class");
            }
            return selection;
        }
    }

    public class RPS
    {
        public static int GamesPlayed;
        public static void Main()
        {
            IWriter Logger = new FileLogging();
            try
                {
                  gamesplayed();
                }
                catch (Exception ex)
                {
                Logger.WriteFile(ex.ToString());
                }
            finally
            {
                Logger.WriteFile("Main Class");
            }

        }

        private static void gamesplayed()
        {
            IWriter Logger = new FileLogging();
            try { 
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Contestant computer = new Computer();
            Contestant player = new Player();
            Selection computerSelection;
            Selection playerSelection;
            ConsoleKeyInfo input;
            bool repeat;

            do
            {
                Console.Clear();
                computerSelection = computer.Select();
                playerSelection = player.Select();
                Console.Clear();

                Console.WriteLine("Player Selected: " + playerSelection);
                Console.WriteLine("\n" + "Computer Selected: " + computerSelection);

                switch (determineWinner((int)computerSelection, (int)playerSelection))
                {
                    case null:
                        Console.Write("\nMatch Tie");
                        break;

                    case true:
                        Console.Write("\nPlayer won!");
                        player.wins++;
                        break;

                    default:
                        Console.Write("\nPlayer lost");
                        computer.wins++;
                        break;
                }

                RPS.GamesPlayed++;
                Console.WriteLine("\n" + "Play again? <y/n>");
                Console.WriteLine("\n");

                int resetPosY = Console.CursorTop;
                int resetPosX = Console.CursorLeft;
                determineChampion(RPS.GamesPlayed, player.wins, computer.wins);
                Console.SetCursorPosition(resetPosX, resetPosY);
                input = Console.ReadKey(true);
                repeat = input.KeyChar == 'y';

            } while (repeat);
            }
            catch (Exception ex)
            {
                Logger.WriteFile(ex.ToString());
            }
            finally
            {
                Logger.WriteFile("gamesplayed method");
            }
        }

        private static void determineChampion(int GamesPlayed, int playerwins, int computerwins)
        {
            IWriter Logger = new FileLogging();
            try
            {
                if (GamesPlayed == 3)
                {
                    if (playerwins == computerwins)
                    {
                        Console.WriteLine("-------Final Result------ ");
                        Console.WriteLine("Match Drawn");
                        Console.ReadKey();
                    }

                    else if (playerwins < computerwins)
                    {
                        Console.WriteLine("-------Final Result------ ");
                        Console.WriteLine("Computer Wins");
                        Console.ReadKey();
                    }
                    else if (playerwins > computerwins)
                    {
                        Console.WriteLine("-------Final Result------ ");
                        Console.WriteLine("Player Wins");
                        Console.ReadKey();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteFile(ex.ToString());
            }
            finally
            {
                Logger.WriteFile("determineChampion method");
            }
            return;
        }

        public static bool? determineWinner(int computerSelection, int playerSelection)
        {
            IWriter Logger = new FileLogging();
            try
            {
                bool?[,] winMatrix = {
            {null, false, true },
            {true, null, false },
            {false, true, null}
        };


                if (winMatrix[playerSelection, computerSelection] == null)
                    return null;
                return (winMatrix[playerSelection, computerSelection] == true) ? true : false;
            }
            catch (Exception ex)
            {
                Logger.WriteFile(ex.ToString());
                return null; 
            }
            finally
            {
                Logger.WriteFile("determineWinner method");
            }
        }
    }

}
