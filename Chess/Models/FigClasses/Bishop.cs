﻿using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Chess.Enums;
using Chess.Models.FigClasses.Support;

namespace Chess.FigClasses
{
    public class Bishop : Button, IPiece
    {
        public ChessSide Side { get; }
        public bool FirstTurn { get; set; } = true;
        //public ChessType Type { get; }
        public Position Position { get; set; }
        public bool Possible(Position position)
        {
            //
            return true;
        }
        public void Move()
        {
            //
        }
        //public override bool Potential(int x2, int y2, ChessSide s) =>
        //    Diagonal(X, Y, x2, y2, Side, true);

        public Bishop(ChessSide Side, int X, int Y)
        {
            this.Side = Side;
            //this.Type = Type;
            Position = new Position(X, Y);
            Image img = new Image();
            if (this.Side == ChessSide.Black)
                img.Source = new BitmapImage(new Uri("FigClasses/Support/Fig/bishopBlack.png", UriKind.Relative));
            else
                img.Source = new BitmapImage(new Uri("FigClasses/Support/Fig/bishopWhite.png", UriKind.Relative));
            Content = img;
            Background = null;
        }
    }

}
