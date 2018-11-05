using System;
using System.Windows.Forms;

namespace OpenSudoku
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
            richTextBoxTask.Text = board.SolutionToString(PrintType.StatisCiphers);
            richTextBoxSolution.Text = board.SolutionToString(PrintType.InvalidCiphersCount);
            richTextBox1.Text = board.SolutionToString(PrintType.ValidCiphers);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (board.SetRandomStaticValue())
            //if (board.SetUpperLeftCornerValue())
            {
                Draw();
                toolStripStatusLabel1.Text = "Success";
            }
            else
            {
                toolStripStatusLabel1.Text = "Failed";
            }
            button1.Enabled = !board.OneSolution();
        }

    }
}
