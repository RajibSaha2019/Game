using System;
using System.Collections.Generic;
using System.Text;
using System;

namespace UnitTestProject2
{
    enum Selection
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }
    abstract class Contestant
    {
        public int wins { get; set; }

        protected Selection selection;
        public abstract Selection Select();
    }
    class Computer : Contestant
    {
        public override Selection Select()
        {
            Random rand = new Random();
            selection = (Selection)rand.Next(0, Enum.GetValues(typeof(Selection)).Length);
            return selection;
        }
    }

    class Player : Contestant
    {
        public override Selection Select()
        {
            bool isValid;
            string input;

            do
            {
                int myInt;
                //Console.Write("Select 0 for Rock, 1 for Paper, 2 For Scissors.");
                input = "Rock";
                bool isNumerical = int.TryParse(input, out myInt);
                if (isNumerical && myInt>2)
                {
                    isValid = false;
                }
                else
                {
                    isValid = Enum.TryParse<Selection>(input, true, out selection);
                }
            } while (!isValid);
            
            return selection;
        }
    }
}
