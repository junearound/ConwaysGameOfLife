using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConwaysGameOfLife.Logic;

namespace ConwaysGameOfLife.Tests
{
    [TestClass]
    class CellTest
    {

        [TestMethod]
        public void Equals_When_Null_Returns_False()
        {
            Cell cell = new Cell(0, 0, 0);

            bool eq = cell.Equals(null);

            Assert.IsFalse(eq);
        }

        [TestMethod]
        public void Equals_When_StateDiffers_Returns_False()
        {
            Cell cell = new Cell(1, 0, 0);
            Cell other = new Cell(0, 0, 0);


            bool eq = cell.Equals(other);

            Assert.IsFalse(eq);
        }

        [TestMethod]
        public void Equals_When_CoordsDiffers_Returns_False()
        {
            Cell cell = new Cell(0, 0, 0);
            Cell other = new Cell(0, 1, 1);

            bool eq = cell.Equals(other);

            Assert.IsFalse(eq);
        }

        [TestMethod]
        public void Equals_When_Same_Returns_True()
        {
            Cell cell = new Cell(0, 1, 1);
            Cell other = new Cell(0, 1, 1);

            bool eq = cell.Equals(other);

            Assert.IsTrue(eq);
        }



    }
}
