using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ActiveUser { get; set; }
        public int XScore { get; set; }
        public int OScore { get; set; }
        public string Turn { get; set; }
        public int TurnValue { get; set; }
        public int[,] ScoreArray { get; set; }
        public List<string> btnName { get; set; }
    
        public MainWindow()
        {
            InitializeComponent();

            XScore = 0;
            OScore = 0;
            Turn = "x";
            ScoreArray = new int[3, 3];
            btnName = new List<string> { "topLeft", "topMiddle", "topRight", "left", "middle", "right", "bottomLeft", "bottomMiddle", "bottomRight" };

            UpdateScoreBoard();
            UpdateTurn();
        }

        private void UpdateTurn()
        {
            if (Turn == "x")
            {
                Turn = "o";
                TurnValue = -1;
                turnTextBlock.Text = "o's turn";
            }
            else
            {
                Turn = "x";
                TurnValue = 1;
                turnTextBlock.Text = "x's turn";
            }
        }

        private void UpdateScoreBoard()
        {
            scoreTextBlock.Text = $"Score: x: {XScore} and o: {OScore}";
        }

        private void topLeftTile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CheckWinner()
        {
            int temp;
            List<int> sum = new List<int>();

            for (int i = 0; i <= ScoreArray.GetUpperBound(0); i++)
            {
                temp = 0;
                for (int j = 0; j <= ScoreArray.GetUpperBound(1); j++) // check rows
                    temp += ScoreArray[i, j];
                sum.Add(temp);
            }

            for (int i = 0; i <= ScoreArray.GetUpperBound(1); i++) // check columns
            {
                temp = 0;
                for (int j = 0; j <= ScoreArray.GetUpperBound(0); j++)
                    temp += ScoreArray[j, i];
                sum.Add(temp);
            }

            // top left to bottom right diagonal
            temp = ScoreArray[0, 0] + ScoreArray[1, 1] + ScoreArray[2, 2];
            sum.Add(temp);
            // bottom left to top right diagonal 
            temp = ScoreArray[2, 0] + ScoreArray[1, 1] + ScoreArray[0, 2];
            sum.Add(temp);

            if (sum.Contains(3))
            {
                MessageBox.Show("x is the winner");
                XScore += 1;
                ResetBoard();
            }
            else if (sum.Contains(-3))
            {
                MessageBox.Show("o is the winner");
                OScore += 1;
                ResetBoard();
            }

            List<int> allArrayValues = new List<int>();
            foreach (int elem in ScoreArray)
                allArrayValues.Add(elem);

            if (!allArrayValues.Contains(0))
            {
                MessageBox.Show("Cats game");
                ResetBoard();
            }
        }

        private void ResetBoard()
        {
            Array.Clear(ScoreArray, 0, ScoreArray.Length);
            foreach (string btn in btnName)
            {
                Button btnGeneric = (Button)this.FindName(btn);
                btnGeneric.IsEnabled = true;
                btnGeneric.Content = "Fill this Tile";
                
                TextBlock textBlockGeneric = (TextBlock)this.FindName(btn + "TextBlock");
                textBlockGeneric.Text = "[]";
            }
            UpdateScoreBoard();

        }

        private void tile_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button) sender;

            if (btn.Content.ToString() != "Filled")
            {
                switch (btn.Name.ToString())
                {
                    case "topLeft":
                        topLeftTextBlock.Text = Turn;
                        ScoreArray[0, 0] = TurnValue;
                        break;
                    case "topMiddle":
                        topMiddleTextBlock.Text = Turn;
                        ScoreArray[0, 1] = TurnValue;
                        break;
                    case "topRight":
                        topRightTextBlock.Text = Turn;
                        ScoreArray[0, 2] = TurnValue;
                        break;
                    case "left":
                        leftTextBlock.Text = Turn;
                        ScoreArray[1, 0] = TurnValue;
                        break;
                    case "middle":
                        middleTextBlock.Text = Turn;
                        ScoreArray[1, 1] = TurnValue;
                        break;
                    case "right":
                        rightTextBlock.Text = Turn;
                        ScoreArray[1, 2] = TurnValue;
                        break;
                    case "bottomLeft":
                        bottomLeftTextBlock.Text = Turn;
                        ScoreArray[2, 0] = TurnValue;
                        break;
                    case "bottomMiddle":
                        bottomMiddleTextBlock.Text = Turn;
                        ScoreArray[2, 1] = TurnValue;
                        break;
                    case "bottomRight":
                        bottomRightTextBlock.Text = Turn;
                        ScoreArray[2, 2] = TurnValue;
                        break;
                }
                btn.Content = "Filled";
                btn.IsEnabled = false;
                UpdateTurn();
                CheckWinner();
            }

           
            UpdateScoreBoard();
        }
    }
}
