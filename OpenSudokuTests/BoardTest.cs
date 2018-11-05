using System;
using OpenSudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenSudokuTests
{
    [TestClass]
    public class BoardTest
    {

        public Board GetTestBoard()
        {
            // | | |2| | |8| | |1|
            // | |6| | | |1| |8|2|
            // | |5| | |7| | |3| |
            // | | |9|5| | |4| | |
            // | |2| | | | | |1| |
            // | | |4| | |6|8| | |
            // | |3| | |9| | |6| |
            // |2|9| |4| | | |5| |
            // |1| | |3| | | | | |

            Board board = new Board();
            board.SetCellCipher(0, 7, 2, true);
            board.SetCellCipher(0, 8, 1, true);
            board.SetCellCipher(1, 1, 6, true);
            board.SetCellCipher(1, 2, 5, true);
            board.SetCellCipher(1, 4, 2, true);
            board.SetCellCipher(1, 6, 3, true);
            board.SetCellCipher(1, 7, 9, true);
            board.SetCellCipher(2, 3, 9, true);
            board.SetCellCipher(2, 5, 4, true);
            board.SetCellCipher(3, 3, 5, true);
            board.SetCellCipher(3, 7, 4, true);
            board.SetCellCipher(3, 8, 3, true);
            board.SetCellCipher(4, 2, 7, true);
            board.SetCellCipher(4, 6, 9, true);
            board.SetCellCipher(5, 0, 8, true);
            board.SetCellCipher(5, 1, 1, true);
            board.SetCellCipher(5, 5, 6, true);
            board.SetCellCipher(6, 3, 4, true);
            board.SetCellCipher(6, 5, 8, true);
            board.SetCellCipher(7, 1, 8, true);
            board.SetCellCipher(7, 2, 3, true);
            board.SetCellCipher(7, 4, 1, true);
            board.SetCellCipher(7, 6, 6, true);
            board.SetCellCipher(7, 7, 5, true);
            board.SetCellCipher(8, 0, 1, true);
            board.SetCellCipher(8, 1, 2, true);
            return (board);
        }

        [TestMethod]
        public void TestBoardCells()
        {
            Board board = new Board();
            Assert.AreEqual(board.cells[0, 0].GetNumberOfCiphers(true), 9);
        }
    }
}
