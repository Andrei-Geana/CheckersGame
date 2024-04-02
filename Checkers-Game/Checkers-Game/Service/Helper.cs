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

        public static ObservableCollection<ObservableCollection<Cell>> GetNewBoard(int nrRows=8, int nrColumns=8)
        {
            ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
            for(int i=0;i<nrRows;++i)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell> ();
                for(int j=0;j<nrColumns;++j)
                {
                    Cell cell = new Cell(i, j, PieceColorEnum.WHITE);

                    if ((i + j) % 2 != 0)
                    {
                        cell.BackgroundColor = PieceColorEnum.BLACK;
                    }

                    if (i < 3)
                    {
                        if ((i + j) % 2 != 0)
                        {
                            cell.Piece = new Piece(PieceTypeEnum.PAWN, PieceColorEnum.WHITE);
                        }
                    }
                    else
                    {
                        if(i>4)
                        {
                            if ((i + j) % 2 != 0)
                            {
                                cell.Piece = new Piece(PieceTypeEnum.PAWN, PieceColorEnum.BLACK);
                            }
                        }
                    }
                    row.Add(cell);
                }
                board.Add(row);
            }
            return board;
        }
    }
}
