using Microsoft.VisualStudio.TestTools.UnitTesting;
using GRPS;
using System;

//test Methods
namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckWinnerNotNull0()//with values 1,0
        {
            //checking for null values
            Assert.IsNull(RPS.determineWinner(1, 1));
        }

        [TestMethod]
        public void CheckWinnerNotNull1()//with values 1,1
        {
            //checking for null values
            Assert.IsNull(RPS.determineWinner(0, 0));
        }

        [TestMethod]
        public void CheckWinnerNotNull2()//with values 2,2
        {
            //checking for null values
            Assert.IsNull(RPS.determineWinner(2, 2));
        }

        [TestMethod]
        public void CheckWinnerpositive0()//with values 0,1
        {
            //checking for return result as true
            bool? result= RPS.determineWinner(0, 1);
            bool expected = (bool)result;
            Assert.IsTrue(expected);
        }

        public void CheckWinnerpositive1()//with values 1,2
        {
            //checking for return result as true
            bool? result = RPS.determineWinner(1, 2);
            bool expected = (bool)result;
            Assert.IsTrue(expected);
        }
        public void CheckWinnerpositive2()//with values 2,0
        {
            //checking for return result as true
            bool? result = RPS.determineWinner(2, 0);
            bool expected = (bool)result;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void CheckWinnerNegative0()//with values 0,2
        {
            //checking for return result as true
            bool? result = RPS.determineWinner(0, 2);
            bool expected = (bool)result;
            Assert.IsFalse(expected);
        }
        [TestMethod]
        public void CheckWinnerNegative1()//with values 1,0
        {
            //checking for return result as true
            bool? result = RPS.determineWinner(1, 0);
            bool expected = (bool)result;
            Assert.IsFalse(expected);
        }

        [TestMethod]
        public void CheckWinnerNegative2()//with values 2,1
        {
            //checking for return result as true
            bool? result = RPS.determineWinner(2, 1);
            bool expected = (bool)result;
            Assert.IsFalse(expected);
        }


        [TestMethod]
        public void CheckAbstractMethodComputer()
        {

           Computer computer = new Computer();
           var returnvalue= computer.Select();
            Assert.IsNotNull(returnvalue);
        }

        [TestMethod]
        public void CheckAbstractMethodContestant()
        {
            Player player = new Player();
            var returnvalue = player.Select();
            Assert.IsNotNull(returnvalue);
        }

        [TestMethod]
        public void CheckAbstractMethodValContestant()
        {
            Player player = new Player();
            var returnvalue = player.Select();
            var expectedresult = "Rock";
            Assert.AreEqual(expectedresult.ToString(), returnvalue.ToString());
        }


    }

    }

