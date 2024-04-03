using Checkers_Game.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    public class Cell
    {
        private int _row;
        private int _column;
        private Piece _piece;
        private PieceColorEnum _backgroundColor;

        public Cell(int row, int column, PieceColorEnum _backgroundColor, Piece piece=null)
        {
            Row = row;
            Column = column;
            Piece = piece;
            this._backgroundColor = _backgroundColor;
        }

        public int Row { get => _row; set => _row = value; }
        public int Column { get => _column; set => _column = value; }
        public Piece Piece { get => _piece; set => _piece = value; }
        public string BackgroundColor 
        {
            get
            {
                if (_backgroundColor == PieceColorEnum.BLACK)
                    return Helper.BlackColor;
                if (_backgroundColor == PieceColorEnum.WHITE)
                    return Helper.WhiteColor;

                //error check
                return "";
            }
            set
            {
                _backgroundColor = PieceColorEnum.BLUE;
            }
        }

        public string Icon
        {
            get 
            {
                if (_piece is null)
                    return Helper.TransparentPath;
                return Piece.PathToIcon;
            }

            set
            {

            }
        }
    }
}
