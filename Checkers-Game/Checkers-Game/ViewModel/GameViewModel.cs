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
        private GameLogic _gameLogic;
        private ObservableCollection<ObservableCollection<Cell>> _gameBoard;
        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;

        public GameViewModel()
        {
            _gameLogic = new GameLogic(this);
            _gameBoard = Helper.GetNewBoard();
            GameBoard = CellBoardToCellVMBoard(_gameBoard);
            Player1 = new Player("player1", PieceColorEnum.BLACK);
            Player2 = new Player("player2", PieceColorEnum.BLACK);
            CurrentPlayer = Player1;
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
        public Player Player1 { get => _player1; set => _player1 = value; }
        public Player Player2 { get => _player2; set => _player2 = value; }
        public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
    }
}
