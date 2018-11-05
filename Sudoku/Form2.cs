using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSudoku
{
    public partial class Form2 : Form
    {
        public Board board;

        public void TestBoard()
        {
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
            board.SetCellCipher(4, 6, 9, true);
            richTextBox1.Text = board.SolutionToString(PrintType.StatisCiphers);
            richTextBox2.Text = board.SolutionToString(PrintType.InvalidCiphersCount);
        }

        public Form2()
        {
            InitializeComponent();
            board = new Board();
            TestBoard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TestBoard();
                if (board.SetCellCipher(int.Parse(textBoxRow.Text), int.Parse(textBoxColumn.Text), int.Parse(textBoxCipher.Text), true))
                {
                    toolStripStatusLabel1.Text = "Success";
                    //richTextBox1.Text = board.SolutionToString(PrintType.StatisCiphers);
                    //richTextBox2.Text = board.SolutionToString(PrintType.InvalidCiphersCount);
                }
                else
                {
                    toolStripStatusLabel1.Text = "Failure";
                }
                
            }
            catch (Exception f) { toolStripStatusLabel1.Text = f.Message; }
        }
    }
}

