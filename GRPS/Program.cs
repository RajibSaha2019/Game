using System;

namespace GRPS
{

    public interface IWriter
    {
        void WriteFile(string Message);
    }
   class EventLogWriter : IWriter
    {
        public void WriteFile(string Message)
        {
            System.IO.File.WriteAllText(@"c:\Error.txt", Message);
        }

    }

    class LogInFile 
    {
        IWriter _IWriter;
        public LogInFile(IWriter IWriter)
        {
            this._IWriter = IWriter;
        }
        public void WriteFile(string Message)
        {
            _IWriter.WriteFile(Message);
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
        LogInFile LogInFile = new LogInFile(new EventLogWriter());


        //IWriter Logger = new FileLogging();

        public override Selection Select()
        {
            try
            {
                Random rand = new Random();
                selection = (Selection)rand.Next(0, Enum.GetValues(typeof(Selection)).Length);
            }
            catch (Exception ex)
            {
                LogInFile.WriteFile(ex.ToString());
            }
            finally
            {
                LogInFile.WriteFile("Computer Class");
            }
            return selection;

        }
    }
    class Player : Contestant
    {
        LogInFile LogInFilePlayer = new LogInFile(new EventLogWriter());
        //IWriter Logger = new FileLogging();


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
                LogInFilePlayer.WriteFile(ex.ToString());
            }
            finally
                {
                LogInFilePlayer.WriteFile("Player Class");
            }
            return selection;
        }
    }

    public class RPS
    {
      
        public static int GamesPlayed;

        public static void Main()
        {
            
             gamesplayed();
                
        }

        public static void gamesplayed()
        {
            LogInFile LogInFileo = new LogInFile(new EventLogWriter());
            try { 
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Contestant computer = new Computer();
            Contestant player = new Player();
            Selection computerSelection;
            Selection playerSelection;
            ConsoleKeyInfo input;
            bool repeat=true;

            do
            {
               
                computerSelection = computer.Select();
                playerSelection = player.Select();
               

                Console.WriteLine("\n" + "Player Selected: " + playerSelection);
                Console.WriteLine("\n" + "Computer Selected: " + computerSelection);
                    

                    switch (determineWinner((int)computerSelection, (int)playerSelection))
                {
                    case null:
                        Console.Write("\nMatch Tie");
                            Console.Write("\n");
                            Console.ReadKey();
                            break;

                    case true:
                        Console.Write("\nPlayer won!");
                            Console.Write("\n");
                            player.wins++;
                            Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nPlayer lost");
                            Console.Write("\n");
                            computer.wins++;
                            Console.ReadKey();
                        break;
                }

                RPS.GamesPlayed++;
                    if (RPS.GamesPlayed==3)
                    { 
                determineChampion(player.wins, computer.wins);
                   
                   Console.WriteLine("\n" + "Play again? <y/n>");
                  Console.WriteLine("\n");
                        input = Console.ReadKey(true);
                        
                        repeat = input.KeyChar == 'y';
                        if (repeat == true)
                        {
                            GamesPlayed = 0;
                        }
                        Console.Clear();
                    }
                    int resetPosY = Console.CursorTop;
                    int resetPosX = Console.CursorLeft;
                    Console.SetCursorPosition(resetPosX, resetPosY);
               
            } while (repeat);
            }
            catch (Exception ex)
            {
                LogInFileo.WriteFile(ex.ToString());
            }
            finally
            {
                LogInFileo.WriteFile("gamesplayed method");
            }
        }

        public static void determineChampion(int playerwins, int computerwins)
        {
            LogInFile LogInFileo = new LogInFile(new EventLogWriter());
            try
            {
                    if (playerwins == computerwins)
                    {
                    Console.WriteLine("\n");
                        Console.WriteLine("-------Final Result------ ");
                        Console.WriteLine("Match Drawn");
                        Console.ReadKey();
                    }

                    else if (playerwins < computerwins)
                    {
                    Console.WriteLine("\n");
                    Console.WriteLine("-------Final Result------ ");
                        Console.WriteLine("Computer Wins");
                        Console.ReadKey();
                    }
                    else if (playerwins > computerwins)
                    {
                    Console.WriteLine("\n");
                    Console.WriteLine("-------Final Result------ ");
                        Console.WriteLine("Player Wins");
                        Console.ReadKey();
                    }

            }
            catch (Exception ex)
            {
                LogInFileo.WriteFile(ex.ToString());
            }
            finally
            {
                LogInFileo.WriteFile("determineChampion method");
                
            }
            return;
        }

        public static bool? determineWinner(int computerSelection, int playerSelection)
        {
            LogInFile LogInFileo = new LogInFile(new EventLogWriter());
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
                LogInFileo.WriteFile(ex.ToString());
                return null; 
            }
            finally
            {
                LogInFileo.WriteFile("determineWinner method");
            }
        }
    }

}
