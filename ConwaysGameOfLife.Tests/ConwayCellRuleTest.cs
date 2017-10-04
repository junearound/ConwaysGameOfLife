using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConwaysGameOfLife.Logic;
using System.Linq;
using System.Collections.Generic;

namespace ConwaysGameOfLife.Tests
{
    [TestClass]
    public class ConwayCellRuleTest
    {
        [TestMethod]
        public void CreateNewCell_When_Empty_Returns_Dead()
        {
            ConwayCellRule r = new ConwayCellRule();
            Cell current = new Cell(0, 0, 0);
            ICell[] empty = new ICell[0];

            ICell newCell = r.CreateNewCell(current, empty);

            Assert.AreEqual(newCell.State, 0);
        }

        [TestMethod]
        public void CreateNewCell_When_DeadNeighborhood_Returns_Dead()
        {
            ConwayCellRule r = new ConwayCellRule();
            Cell current = new Cell(0,0,0);
            ICell[] neigh = Enumerable.Range(0, 7).Select(n => (ICell)(new Cell(0,0,0))).ToArray<ICell>();

            ICell newCell = r.CreateNewCell(current, neigh);

            Assert.AreEqual(newCell.State, 0);
        }

        [TestMethod]
        public void CreateNewCell_When_AliveCountMore3_Returns_Dead()
        {
            ConwayCellRule r = new ConwayCellRule();
            Cell current = new Cell(1, 0, 0);
            ICell[] neigh = Enumerable.Range(0, 7).Select(n => (ICell)(new Cell(0, 0, 0))).ToArray<ICell>();
            neigh[0].State = 1;
            neigh[1].State = 1;
            neigh[2].State = 1;
            neigh[3].State = 1;

            ICell newCell = r.CreateNewCell(current, neigh);

            Assert.AreEqual(newCell.State, 0);
        }

        [TestMethod]
        public void CreateNewCell_When_AliveCountLess2_Returns_Dead()
        {
            ConwayCellRule r = new ConwayCellRule();
            Cell current = new Cell(1, 0, 0);
            ICell[] neigh = Enumerable.Range(0, 7).Select(n => (ICell)(new Cell(0, 0, 0))).ToArray<ICell>();
            neigh[0].State = 1;

            ICell newCell = r.CreateNewCell(current, neigh);

            Assert.AreEqual(newCell.State, 0);
        }

        [TestMethod]
        public void CreateNewCell_When_AliveCountIs3_Returns_Alive()
        {
            ConwayCellRule r = new ConwayCellRule();
            Cell current = new Cell(1, 0, 0);
            ICell[] neigh = Enumerable.Range(0, 7).Select(n => (ICell)(new Cell(0, 0, 0))).ToArray<ICell>();
            neigh[0].State = 1;
            neigh[2].State = 1;
            neigh[3].State = 1;

            ICell newCell = r.CreateNewCell(current, neigh);

            Assert.AreEqual(newCell.State, 1);
        }

       
    }
}

 