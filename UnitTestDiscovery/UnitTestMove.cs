using MstDiscovery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MstDiscovery.Tests
{
    [TestClass()]
    public class UnitTestMove
    {
        [TestMethod()]
        public void MoveRovers()
        {
            RoverController rover = new RoverController();

            string input = "5 5" + Environment.NewLine +
                "1 2 N" + Environment.NewLine +
                "LMLMLMLMM" + Environment.NewLine +
                "3 3 E" + Environment.NewLine +
                "MMRMMRMRRM";

            string output= rover.MoveRovers(input);

            string expectedOut = "1 3 N" + Environment.NewLine +
            "5 1 E" + Environment.NewLine;

            Assert.AreEqual(expectedOut, output);
        }
    }
}
 