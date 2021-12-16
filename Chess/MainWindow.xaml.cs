﻿using System;
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
using static System.Math;


namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CreateMap();

            Start();
        }
        public enum ChessSide
        {
            White = 0,
            Black,
            NoSide
        }
        public enum ChessType
        {
            Notype = 0,
            Pawn,
            Rook,
            Knight,
            Bishop,
            Quenn,
            King
        }

        public class NewButton : Button
        {
            public ChessSide Side { get; set; }
            public bool FirstTurn = true;
            public ChessType Type { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public bool Potential(int x2, int y2, ChessSide s)
            {
                switch (Type)
                {
                    case ChessType.Pawn:
                        {
                            int temp = 1;
                            if (Side == 0)
                                temp = -1;
                            if (FirstTurn)
                            {
                                if ((x2 == X + temp || (x2 == X + 2 * temp && buttonMap[X + temp, Y].Type == 0)) && Y == y2 && s == ChessSide.NoSide || (s != Side && s != ChessSide.NoSide && x2 == X + temp && (Y == y2 + 1 || Y == y2 - 1)))
                                    return true;
                                else
                                    return false;
                            }
                            else
                            {
                                if ((x2 == X + temp && Y == y2 && s == ChessSide.NoSide) || (s != Side && s != ChessSide.NoSide && x2 == X + temp && (Y == y2 + 1 || Y == y2 - 1)))
                                    return true;
                                else
                                    return false;
                            }
                        }
                    case ChessType.Rook:
                        return HorizontalVertical(X, Y, x2, y2, Side, true);
                    case ChessType.Knight:
                        {
                            if ((Abs(X - x2) == 2 && Abs(Y - y2) == 1) || (Abs(X - x2) == 1 && Abs(Y - y2) == 2))
                                return true;
                            else
                                return false;
                        }
                    case ChessType.Bishop:
                        return Diagonal(X, Y, x2, y2, Side, true);
                    case ChessType.Quenn:
                        return HorizontalVertical(X, Y, x2, y2, Side, true) || Diagonal(X, Y, x2, y2, Side, true);
                    case ChessType.King:
                            return HorizontalVertical(X, Y, x2, y2, Side, false) || Diagonal(X, Y, x2, y2, Side, false);
                    default:
                        return false;
                }
            }
            public NewButton() { }
            public NewButton(int Side, int Type, int X, int Y)
            {
                this.Side = (ChessSide)Side;
                this.Type = (ChessType)Type;
                this.X = X;
                this.Y = Y;
                string currentDir = Environment.CurrentDirectory;
                switch (this.Side, this.Type)
                {
                    case (ChessSide.Black, ChessType.Pawn):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\pawnBlack.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.Black, ChessType.Rook):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\rookBlack.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.Black, ChessType.Knight):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\knightBlack.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.Black, ChessType.Bishop):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\bishopBlack.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.Black, ChessType.Quenn):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\queenBlack.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.Black, ChessType.King):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\kingBlack.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.White, ChessType.Pawn):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\pawnWhite.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.White, ChessType.Rook):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\rookWhite.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.White, ChessType.Knight):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\knightWhite.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.White, ChessType.Bishop):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\bishopWhite.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.White, ChessType.Quenn):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\queenWhite.png"));
                            this.Content = img;
                            break;
                        }
                    case (ChessSide.White, ChessType.King):
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(currentDir + @"\Fig\kingWhite.png"));
                            this.Content = img;
                            break;
                        }
                }
            }
        }

        readonly int[,] map = new int[8, 8]
        {
           {12,13,14,15,16,14,13,12},
           {11,11,11,11,11,11,11,11},
           {20,20,20,20,20,20,20,20 },
           {20,20,20,20,20,20,20,20 },
           {20,20,20,20,20,20,20,20 },
           {20,20,20,20,20,20,20,20 },
           {1,1,1,1,1,1,1,1},
           {2,3,4,5,6,4,3,2}
        };

        public static NewButton[,] buttonMap = new NewButton[8, 8];

        public NewButton previousButton = new NewButton();

        public int turnNumber = 0;

        public bool themaLight = true;

        //public string[] sideArray = new string[2] { "White", "Black" };

        //string[] alpha = { "A", "B", "C", "D", "E", "F", "G", "H" };

        public void CreateMap()
        {
            for (int i = 0; i < 8; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = (8 - i).ToString();
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                chessNumber1.Children.Add(textBlock);

                TextBlock textBlock1 = new TextBlock();
                //textBlock1.Text = alpha[i];
                textBlock1.Text = ((char)(65 + i)).ToString();
                textBlock1.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                chessLetter1.Children.Add(textBlock1);
            }

            for (int i = 0; i < 8; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = (8 - i).ToString();
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                chessNumber2.Children.Add(textBlock);

                TextBlock textBlock1 = new TextBlock();
                //textBlock1.Text = alpha[i].ToString();
                textBlock1.Text = ((char)(65 + i)).ToString();
                textBlock1.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                chessLetter2.Children.Add(textBlock1);
            }           
        }

        public void Pressed(object sender, RoutedEventArgs e)
        {
            NewButton pressedButton = (NewButton)sender;

            if (pressedButton.Content != null && pressedButton.Side == (ChessSide)(turnNumber % 2))
            {
                if (previousButton != null)
                {
                    previousButton.Effect = null;
                    previousButton.BorderBrush = Brushes.Black;
                }
                pressedButton.BorderBrush = Brushes.Red;                    //***************какой-то эффект нужен более интересный для выделения
                //pressedButton.Foreground = Brushes.Red;
                previousButton = pressedButton;
            }
            else if (previousButton != null && (pressedButton.Content == null || pressedButton.Content != null) && previousButton.Potential(pressedButton.X, pressedButton.Y, pressedButton.Side))
                Turn(previousButton, pressedButton);
        }

        public void Turn(NewButton button1, NewButton button2)
        {
            TextTurn(button1, button2);
            if (button2.Type == ChessType.King)
            {
                MessageBox.Show((ChessSide)(turnNumber % 2) + " Win");  
                DisableButtons();
            }
            button2.Content = button1.Content;
            button2.Type = button1.Type;
            button2.Side = button1.Side;
            button2.FirstTurn = false;
            button1.Content = null;
            button1.Type = ChessType.Notype;
            button1.Side = ChessSide.NoSide;

            turnNumber++;
            TurnTextBox.Text = (turnNumber + 1).ToString();
            SideTextBox.Text = ((ChessSide)(turnNumber % 2)).ToString();
            Brush[] b = { Brushes.White, Brushes.Black };
            FunImage.Background = b[turnNumber % 2];
        }

        public static bool HorizontalVertical(int x1, int y1, int x2, int y2, ChessSide s, bool check)
        {
            if (x1 == x2 || y1 == y2)
            {
                if (x1 == x2)
                {
                    int signY = Sign(y1 - y2), y = y1 - signY;
                    if (check)
                        while (y != y2 && buttonMap[x1, y].Type == 0)
                            y -= signY;
                    if (buttonMap[x1, y].Side != s && y == y2)
                        return true;
                    else
                        return false;
                }
                else
                {
                    int signX = Sign(x1 - x2), x = x1 - signX;
                    if (check)
                        while (x != x2 && buttonMap[x, y1].Type == 0)
                            x -= signX;
                    if (buttonMap[x, y1].Side != s && x == x2)
                        return true;
                    else
                        return false;
                }
            }
            else
                return false;
        }

        public static bool Diagonal(int x1, int y1, int x2, int y2, ChessSide s, bool check)
        {
            if (Abs(x1 - x2) == Abs(y1 - y2))
            {
                int signX = Sign(x1 - x2),
                signY = Sign(y1 - y2),
                x = x1 - signX, y = y1 - signY;
                if (check)
                    while (x != x2 && y != y2 && buttonMap[x, y].Type == 0)
                    {
                        x -= signX;
                        y -= signY;

                    }

                if (buttonMap[x, y].Side != s && x == x2 && y == y2)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public void DisableButtons() //***********************************//
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    buttonMap[i, j].IsEnabled = false;
        }

        public void RestartClick(object sender, RoutedEventArgs e)
        {
            Start();
        }

        public void Start()
        {
            chessBoard.Children.Clear();
            previousButton = null;
            turnNumber = 0;
            TextBox1.Text = null;
            TurnTextBox.Text = "1";
            SideTextBox.Text = "White";
            FunImage.Background = Brushes.White;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    buttonMap[i, j] = new NewButton(map[i, j] / 10, map[i, j] % 10, i, j);
                    buttonMap[i, j].BorderBrush = Brushes.Black;
                    chessBoard.Children.Add(buttonMap[i, j]);
                }

            ThemaChange(themaLight);

            foreach (UIElement c in chessBoard.Children)
                if (c is Button)
                    ((Button)c).Click += Pressed;
        }

        public void LightThema(object sender, RoutedEventArgs e)
        {
            if (!themaLight)
            {
                themaLight = true;
                ThemaChange(themaLight);
            }
        }

        public void DarkThema(object sender, RoutedEventArgs e)
        {
            if (themaLight)
            {
                themaLight = false;
                ThemaChange(themaLight);
            }
        }

        public void ThemaChange(bool temp)
        {
            Brush br1, br2;
            if (temp)
            {
                chessBackBoard.Background = Brushes.Peru;
                TextBox1.Background = Brushes.Tan;
                menu.Background = Brushes.RosyBrown;
                br1 = Brushes.Peru;
                br2 = Brushes.Sienna;
            }
            else
            {
                chessBackBoard.Background = Brushes.Gray;
                TextBox1.Background = Brushes.DimGray;
                menu.Background = Brushes.SlateGray;
                br1 = Brushes.Gray;
                br2 = Brushes.LightGray;
            }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                        buttonMap[i, j].Background = br1;
                    else
                        buttonMap[i, j].Background = br2;
                }
        }


        public void TextTurn(NewButton button1, NewButton button2)   //*************тут подумать с символами
        {
            int kostyl = 1;
            string[] fig =
                {"",
                "\u2659", //whitePawn
                "\u2656", //whiteRook
                "\u2658", //whiteKnight
                "\u2657", //whiteBsihop
                "\u2655", //whiteQueen
                "\u2654", //whiteKing

                "\u265f", //blackPawn
                "\u265c", //blackRook
                "\u265e", //blackKnight
                "\u265d", //blackBsihop
                "\u265b", //blackQueen
                "\u265a", //blackKing
            };
            if (button2.Side == ChessSide.NoSide)
                kostyl = 0;
            //TextBox1.Text += "Turn: " + (turnNumber + 1) + "\t" + fig[button1.Type + 6 * button1.Side]
            //        + " " + ((char)(65 + button1.Y)).ToString().ToLower() + (8 - button1.X)
            //        + " " + alpha[button2.Y].ToLower() + (8 - button2.X)
            //        + " " + fig[button2.Type + 6 * (int)temp * kostyl] + "\n";
            /*string temp = button1.Side.ToString();
            int n = button1.Side;*/
        }
    }
}