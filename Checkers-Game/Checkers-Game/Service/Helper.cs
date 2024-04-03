using Checkers_Game.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Service
{
    public class Helper
    {
        Helper() { }

        public static string WhitePawnPath = "/Checkers-Game;component/Resource/whitepiece.png";
        public static string BlackPawnPath = "/Checkers-Game;component/Resource/blackpiece.png";
        public static string WhiteKingPath = "/Checkers-Game;component/Resource/whiteking.png";
        public static string BlackKingPath = "/Checkers-Game;component/Resource/blackking.png";
        public static string TransparentPath = "/Checkers-Game;component/Resource/transparent.png";

        public static string BlackColor = "#d18b47";
        public static string WhiteColor = "#ffce9e";

        public static ObservableCollection<ObservableCollection<Cell>> GetNewBoard(int nrRows=8, int nrColumns=8)
        {
            ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
            for (int i = 0; i < nrRows; ++i)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int j = 0; j < nrColumns; ++j)
                {
                    PieceColorEnum backgroundColor = (i + j) % 2 != 0 ? PieceColorEnum.BLACK : PieceColorEnum.WHITE;
                    Cell cell = new Cell(i, j, backgroundColor);

                    if (i < 3 && (i + j) % 2 != 0)
                    {
                        cell.Piece = new Piece(PieceTypeEnum.PAWN, PieceColorEnum.WHITE);
                    }
                    else if (i > 4 && (i + j) % 2 != 0)
                    {
                        cell.Piece = new Piece(PieceTypeEnum.PAWN, PieceColorEnum.BLACK);
                    }

                    row.Add(cell);
                }
                board.Add(row);
            }
            return board;
        }
    }
}
