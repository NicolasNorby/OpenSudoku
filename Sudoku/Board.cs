using System;

namespace OpenSudoku
{
    public enum PrintType { StatisCiphers, InvalidCiphers, ValidCiphers, InvalidCiphersCount, ValidCiphersCount}

    public class Board
    {
        public Cell[,] cells;

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
            cells= new Cell[9,9];
            int rowIndex = 0;
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                rowIndex = 0;
                while (rowIndex < 9)
                {
                    cells[rowIndex, columnIndex] = new Cell();
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
                    b = (cells[rowIndex, columnIndex].GetNumberOfCiphers(true) == 1);
                    rowIndex++;
                }
                columnIndex++;
            }
            return (b);
        }

        public bool SetCellCipher(int rowIndex, int columnIndex, int cipher, bool stat)
        {
            bool succes = false;
            if (cells[rowIndex, columnIndex].SetCipher(cipher, stat))
            {
                InvalidateCipherInRow(rowIndex, cipher);
                InvalidateCipherInColumn(columnIndex, cipher);
                InvalidateCipherInSquare(rowIndex, columnIndex, cipher);

                CleanRows();
                CleanColumns();
                CleanSquares();

                succes = true;
            }
            return (succes);
        }

        public void CleanSquares()
        {
            int rowIndex = 0;
            while (rowIndex < 9)
            {
                int columnIndex = 0;
                while (columnIndex < 9)
                {
                    int cipher = 1;
                    while (cipher <= 9)
                    {
                        int uniqueRowIndex = -1;
                        int uniqueColumnIndex = -1;
                        if (GetUniqueCellForCipher(cipher, rowIndex, columnIndex, ref uniqueRowIndex, ref uniqueColumnIndex))
                        {
                            if (cells[uniqueRowIndex, uniqueColumnIndex].GetNumberOfCiphers(true) > 1)
                            {
                                SetCellCipher(uniqueRowIndex, uniqueColumnIndex, cipher, false);
                            }
                        }
                        cipher++;
                    }
                    columnIndex = columnIndex + 3;
                }
                rowIndex = rowIndex + 3;
            }
        }

        public void CleanColumns() // Fix cells where a cipher hos no other possible locations in the column
        {
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                int cipher = 1;
                while (cipher <= 9)
                {
                    int uniqueRowIndex = GetUniqueRowForCipher(columnIndex, cipher);
                    if (uniqueRowIndex >= 0 && cells[uniqueRowIndex, columnIndex].GetNumberOfCiphers(true) > 1)
                    {
                        SetCellCipher(uniqueRowIndex, columnIndex, cipher, false);
                    }
                    cipher++;
                }
                columnIndex++;
            }
        }

        public void CleanRows() // Fix cells where a cipher hos no other possible locations in the row
        {
            int rowIndex = 0;
            while (rowIndex < 9)
            {
                int cipher = 1;
                while (cipher <= 9)
                {
                    int uniqueColumnIndex = GetUniqueColumnForCipher(rowIndex, cipher);
                    if (uniqueColumnIndex >= 0 && cells[rowIndex, uniqueColumnIndex].GetNumberOfCiphers(true) > 1)
                    {
                        SetCellCipher(rowIndex, uniqueColumnIndex, cipher, false);
                    }
                    cipher++;
                }
                rowIndex++;
            }
        }

        public void InvalidateCipherInRow(int rowIndex, int cipher)
        {
            int columnIndex = 0;
            while (columnIndex < 9)
            {
                cells[rowIndex, columnIndex].InvalidateValue(cipher);
                columnIndex++;
            }
        }

        public int GetUniqueRowForCipher(int columnIndex, int cipher) // Returns -1 if not unique
        {
            // Count occurrances of cipher in column
            int rowIndex = 0;
            int uniqueRowIndex = -1;
            while (rowIndex < 9)
            {
                if (cells[rowIndex, columnIndex].ciphers[cipher - 1]) // Cipher is valid in row
                {
                    if (uniqueRowIndex == -1) // Is it the first occurrance
                    {
                        uniqueRowIndex = rowIndex;
                        rowIndex++;
                    }
                    else // It is not the first occurrance so set to -1 and abort
                    {
                        uniqueRowIndex = -1;
                        rowIndex = 9;
                    }
                }
                else
                {
                    rowIndex++;
                }
            }
            return (uniqueRowIndex);
        }

        public void InvalidateCipherInColumn(int columnIndex, int cipher)
        {
            int rowIndex = 0;
            while (rowIndex < 9)
            {
                cells[rowIndex, columnIndex].InvalidateValue(cipher);
                rowIndex++;
            }
        }

        public int GetUniqueColumnForCipher(int rowIndex, int cipher) // Returns -1 if not unique
        {
            // Count occurrances of cipher in column
            int columnIndex = 0;
            int uniqueColumnIndex = -1;
            while (columnIndex < 9)
            {
                if (cells[rowIndex, columnIndex].ciphers[cipher - 1]) // Cipher is valid in row
                {
                    if (uniqueColumnIndex == -1) // It is the first occurrance
                    {
                        uniqueColumnIndex = columnIndex;
                        columnIndex++;
                    }
                    else // It is not the first occurrance so set to -1 and abort while loop
                    {
                        uniqueColumnIndex = -1;
                        columnIndex = 9;
                    }
                }
                else
                {
                    columnIndex++;
                }
            }
            return (uniqueColumnIndex);
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
                    cells[squareRowIndex, squareColumnIndex].InvalidateValue(cipher);
                    squareColumnIndex++;
                }
                squareColumnIndex = squareColumnIndex - 3;
                squareRowIndex++;
            }
        }

        public bool GetUniqueCellForCipher(int cipher, int startRowIndex, int startColumnIndex, ref int uniqueRowIndex, ref int uniqueColumnIndex)
        {
            int locations = 0;
            uniqueRowIndex = -1;
            uniqueColumnIndex = -1;

            int rowIndex = startRowIndex;
            while (rowIndex < startRowIndex + 3 && locations <= 1)
            {
                int columnIndex = 0;
                while (columnIndex < startColumnIndex + 3 && locations <= 1)
                {
                    if (cells[rowIndex, columnIndex].ciphers[cipher - 1])
                    {
                        locations++;
                        uniqueRowIndex = rowIndex;
                        uniqueColumnIndex = columnIndex;
                    }
                    columnIndex++;
                }
                rowIndex++;
            }
            return (locations == 1);
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
                    if (rowIndex == 0) { s += "|"; }
                    switch(printType)
                    {
                        case PrintType.StatisCiphers:
                            {
                                if (cells[rowIndex, columnIndex].GetNumberOfCiphers(true)==1)
                                {
                                    if (cells[rowIndex, columnIndex].isStatic) { s += "*"; }
                                    else { s += " "; }
                                    s += (cells[rowIndex, columnIndex].GetFirstValidNumber()).ToString();
                                }
                                else { s += "  "; }
                                s += "|";
                                break;
                            }
                        case PrintType.InvalidCiphers:
                            {
                                s += IntArrayToString(cells[rowIndex, columnIndex].GetNumbers(false)) + "|";
                                break;
                            }
                        case PrintType.ValidCiphers:
                            {
                                s += IntArrayToString(cells[rowIndex, columnIndex].GetNumbers(true)) + "|";
                                break;
                            }
                        case PrintType.InvalidCiphersCount:
                            {
                                s += cells[rowIndex, columnIndex].GetNumberOfCiphers(false) + "|";
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
