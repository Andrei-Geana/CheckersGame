using Checkers_Game.Model;
using Checkers_Game.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.ViewModel
{
    public class GameViewModel
    {
        private Helper _helper;
        private GameLogic _gameLogic;
        private ObservableCollection<ObservableCollection<Cell>> _gameBoard;

        public GameViewModel()
        {
            _gameBoard = Helper.GetNewBoard();
            GameBoard = CellBoardToCellVMBoard(_gameBoard);
        }

        private ObservableCollection<ObservableCollection<CellViewModel>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellViewModel>> result = new ObservableCollection<ObservableCollection<CellViewModel>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellViewModel> line = new ObservableCollection<CellViewModel>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellViewModel cellVM = new CellViewModel(c, _gameLogic);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
        public ObservableCollection<ObservableCollection<CellViewModel>> GameBoard { get; set; }
    }
}
