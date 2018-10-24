using System;

namespace Sudoku
{
    public enum PrintType { StatisCiphers, InvalidCiphers, ValidCiphers, InvalidCiphersCount, ValidCiphersCount}
    public class Board
    {

        public Cell[,] fields;

        public bool SetRandomStaticValue()
        {
            Random rnd = new Random();
            int rowIndex = rnd.Next(0,8);
            int columnIndex = rnd.Next(0, 8);
            int cipher = rnd.Next(1, 9);
            bool succes = SetCellCipher(rowIndex, columnIndex, cipher, true);
            return (succes);
        }

        public bool SetUpperLeftCornerValue() // Only for test purposes
        {
            Random rnd = new Random();
            int rowIndex = rnd.Next(0, 0);
            int columnIndex = rnd.Next(0, 0);
            int value = rnd.Next(1, 9);
            bool succes = SetCellCipher(rowIndex, columnIndex, value, true);
            return (succes);
        }

        public Board()
        {
            fields = new Cell[9,9];
            int rowIndex = 0;
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                rowIndex = 0;
                while (rowIndex < 9)
                {
                    fields[rowIndex, columnIndex] = new Cell();
                    rowIndex++;
                }
                columnIndex++;
            }
        }

        public bool OneSolution()
        {
            bool b = true;
            int rowIndex = 0;
            int columnIndex = 0;
            while (columnIndex < 9 && b)
            {
                rowIndex = 0;
                while (rowIndex < 9 && b)
                {
                    b = (fields[rowIndex, columnIndex].GetNumberOfCiphers(true) == 1);
                    rowIndex++;
                }
                columnIndex++;
            }
            return (b);
        }

        public bool SetCellCipher(int rowIndex, int columnIndex, int cipher, bool stat)
        {
            bool succes = false;
            if (fields[rowIndex, columnIndex].SetCipher(cipher, stat))
            {
                InvalidateCipherInRow(rowIndex, cipher);
                InvalidateCipherInColumn(columnIndex, cipher);
                InvalidateCipherInSquare(rowIndex, columnIndex, cipher);
                SetCipherLocationsInRow(rowIndex);
                succes = true;
            }
            return (succes);
        }

        public void InvalidateCipherInRow(int rowIndex, int cipher)
        {
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                fields[rowIndex, columnIndex].InvalidateValue(cipher);
                columnIndex++;
            }
        }

        public void InvalidateCipherInColumn(int columnIndex, int cipher)
        {
            int rowIndex = 0;
            while (rowIndex < 9)
            {
                fields[rowIndex, columnIndex].InvalidateValue(cipher);
                rowIndex++;
            }
        }

        public void InvalidateCipherInSquare(int rowIndex, int columnIndex, int cipher)
        {
            int squareRowIndex = 3 * (rowIndex / 3);
            int squareColumnIndex = 3 * (columnIndex / 3);
            int nextSquareRowIndex = squareRowIndex + 3;
            int nextSquareColumnIndex = squareColumnIndex + 3;
            while (squareRowIndex < nextSquareRowIndex)
            {
                while (squareColumnIndex < nextSquareColumnIndex)
                {
                    fields[squareRowIndex, squareColumnIndex].InvalidateValue(cipher);
                    squareColumnIndex++;
                }
                squareColumnIndex = squareColumnIndex - 3;
                squareRowIndex++;
            }
        }

        public void SetCipherLocationsInRow(int rowIndex)
        {
            int cipher = 1;
            while (cipher < 10)
            {
                int uniqueColumnIndex = 0;
                if ((GetUniqueColumnForCipher(rowIndex, cipher, ref uniqueColumnIndex) ) &&  (fields[rowIndex, uniqueColumnIndex].GetNumberOfCiphers(true) > 1))
                    {
                        SetCellCipher(rowIndex, uniqueColumnIndex, cipher, false);
                        cipher = 1;
                    }
                else { cipher++; }
            }
        }

        public bool GetUniqueColumnForCipher(int rowIndex, int cipher, ref int uniqueColumnIndex)
        {
            int columnCount = 0;
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                if (fields[rowIndex, columnIndex].ciphers[cipher - 1])
                {
                    uniqueColumnIndex = columnIndex;
                    columnCount++;
                }
                columnIndex++;
            }
            return (columnCount==1);
        }

        public override string ToString()
        {
            string s = "";
            int rowIndex = 0;
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                rowIndex = 0;
                while (rowIndex < 9)
                {
                    if (fields[rowIndex, columnIndex].isStatic) { s += "|" + fields[rowIndex, columnIndex].GetFirstValidNumber().ToString() + "|"; }
                    else { s += "| |"; }
                    rowIndex++;
                }
                if (columnIndex < 9 - 1) { s += System.Environment.NewLine; }
                columnIndex++;
            }
            return (s);
        }

        public string SolutionToString(PrintType printType)
        {
            string s = "";
            int rowIndex = 0;
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                rowIndex = 0;
                while (rowIndex < 9)
                {
                    switch(printType)
                    {
                        case PrintType.InvalidCiphers:
                            {
                                s += "|" + IntArrayToString(fields[rowIndex, columnIndex].GetNumbers(false)) + "|";
                                break;
                            }
                        case PrintType.ValidCiphers:
                            {
                                s += "|" + IntArrayToString(fields[rowIndex, columnIndex].GetNumbers(true)) + "|";
                                break;
                            }
                        case PrintType.InvalidCiphersCount:
                            {
                                s += "|" + fields[rowIndex, columnIndex].GetNumberOfCiphers(false) + "|";
                                break;
                            }

                        default: break;
                    }
                    rowIndex++;
                }
                if (columnIndex < 9 - 1) { s += System.Environment.NewLine; }
                columnIndex++;
            }
            return (s);
        }

        private string IntArrayToString(int[] i)
        {
            string s = "";
            int j = 0;
            while (j < i.Length)
            {
                if (j > 0) { s += ", "; }
                s += i[j].ToString();
                j++;
            }
            return (s);
        }
    }
}
