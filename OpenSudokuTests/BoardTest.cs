using System;
using OpenSudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenSudokuTests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestBoardCells()
        {
            Board board = new Board();
            Assert.AreEqual(board.cells[0, 0].GetNumberOfCiphers(true), 9);
        }
    }
}
