using System;

namespace GRPS
{
    class FileLogger
    {
        public void Handle(string error)
        {
            System.IO.File.WriteAllText(@"c:\Error.txt", error);
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
        
        FileLogger objlogger = new FileLogger();
        public override Selection Select()
        {
            try
            {
                Random rand = new Random();
                selection = (Selection)rand.Next(0, Enum.GetValues(typeof(Selection)).Length);
            }
            catch (Exception ex)
            {
                objlogger.Handle(ex.ToString());
            }
            return selection;

        }
    }
    class Player : Contestant
    {
        FileLogger objlogger = new FileLogger();
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
                objlogger.Handle(ex.ToString());
            }
            return selection;
        }
    }

    public class RPS
    {
        public static int GamesPlayed;
        public static void Main()
        {
            FileLogger objlogger = new FileLogger();
            try
                {
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

                        if (RPS.GamesPlayed == 3)
                        {
                            if (player.wins == computer.wins)
                            {
                                Console.WriteLine("-------Final Result------ ");
                                Console.WriteLine("Match Drawn");
                                Console.ReadKey();
                            }

                            else if (player.wins < computer.wins)
                            {
                                Console.WriteLine("-------Final Result------ ");
                                Console.WriteLine("Computer Wins");
                                Console.ReadKey();
                            }
                            else if (player.wins > computer.wins)
                            {
                                Console.WriteLine("-------Final Result------ ");
                                Console.WriteLine("Player Wins"); 
                                Console.ReadKey();
                            }
                            break;
                        }
                        Console.SetCursorPosition(resetPosX, resetPosY);
                        input = Console.ReadKey(true);
                        repeat = input.KeyChar == 'y';

                    } while (repeat);
                }
                catch (Exception ex)
                {
                objlogger.Handle(ex.ToString());
                }
                
        }
        public static bool? determineWinner(int computerSelection, int playerSelection)
        {
            FileLogger objlogger = new FileLogger();
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
                objlogger.Handle(ex.ToString());
                return null; 
            }
        }
    }

}
