using System;
using System.Linq;

namespace Sudoku
{
    public class Cell
    {
        public bool[] ciphers;
        public bool isStatic; // Meaning given from the creator of the puzzle

        public Cell()
        {
            ciphers = new bool[9];
            int i = 0;
            while (i < 9)
            {
                ciphers[i] = true;
                i++;
            }
            isStatic = false;
        }

        public override string ToString()
        {
            string s = "";
            if (isStatic) { s = "STATIC"; }
            for (int i = 0; i < ciphers.Count(); i++)
            {
                if (ciphers[i])
                {
                    if (i > 0) { s += ", "; }
                    s += (i + 1).ToString();
                }
            }
            return (s);
        }

        public void ResetCell() // Set all ciphers to valid
        {
            int i = 0;
            while (i < ciphers.Count()) { ciphers[i] = true; }
        }

        public void InvalidateValue(int value)
        {
            if (!isStatic && GetNumberOfCiphers(true)>1) { ciphers[value - 1] = false; }
        }

        public bool SetCipher(int value, bool stat)
        {
            bool isValid = ciphers[value - 1];
            if (isValid)
            {
                isStatic = stat;
                int i = 0;
                while (i < ciphers.Count())
                {
                    ciphers[i] = (i == (value - 1));
                    i++;
                } // Set all other values to false
            }
            return (isValid);
        }

        public int GetNumberOfCiphers(bool valid)
        {
            int count = 0;
            int i = 0;
            while (i < ciphers.Count())
            {
                if (ciphers[i] == valid)
                {
                    count++;
                }
                i++;
            }
            return (count);
        }

        public int[] GetNumbers(bool valid)
        {
            int[] numbers = new int[GetNumberOfCiphers(valid)];
            int numbersIndex= 0;
            int i = 0;
            while (i < ciphers.Count())
            {
                if (ciphers[i]==valid)
                {
                    numbers[numbersIndex] = i + 1;
                    numbersIndex++;
                }
                i++;
            }
            return (numbers);
        }

        public int GetFirstValidNumber()
        {
            int i = 0;
            while (i < ciphers.Count())
            {
                if (ciphers[i]) { break; }
                else { i++; }
            }
            return (i + 1);
        }

    }
}
