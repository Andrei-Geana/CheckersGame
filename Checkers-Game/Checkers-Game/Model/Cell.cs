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
            BackgroundColor = _backgroundColor;
        }

        public int Row { get => _row; set => _row = value; }
        public int Column { get => _column; set => _column = value; }
        public Piece Piece { get => _piece; set => _piece = value; }
        public PieceColorEnum BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }

        public string Icon
        {
            get 
            {
                if (_piece is null)
                    return null;
                return Piece.PathToIcon;
            }

            set
            {

            }
        }
    }
}
