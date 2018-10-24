using System;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {

        public Board board;

        public Form1()
        {
            InitializeComponent();
            board = new Board();
        }

        public void Draw()
        {
            richTextBoxTask.Text = board.ToString();
            richTextBoxSolution.Text = board.SolutionToString(PrintType.InvalidCiphersCount);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (board.SetRandomStaticValue())
            //if (board.SetUpperLeftCornerValue())
            {
                Draw();
                toolStripStatusLabel1.Text = "Det lykkedes";
            }
            else
            {
                toolStripStatusLabel1.Text = "Det lykkedes ikke";
            }
            button1.Enabled = !board.OneSolution();
        }

    }
}
