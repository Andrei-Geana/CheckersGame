using Checkers_Game.Model;
using Checkers_Game.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace Checkers_Game.ViewModel
{
    public class GameViewModel : BaseViewModel
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
            Player2 = new Player("player2", PieceColorEnum.WHITE);
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
        public Player CurrentPlayer 
        { 
            get => _currentPlayer;
            set
            {
                if(_currentPlayer == value) return;
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        private List<CellViewModel> GetReachableCellsForIndices(CellViewModel currentCell, List<Tuple<int,int>> moveIndices)
        {
            List<CellViewModel> reachableCells = new List<CellViewModel>();
            foreach (var move in moveIndices)
            {
                int newRow = currentCell.SimpleCell.Row + move.Item1;
                int newColumn = currentCell.SimpleCell.Column + move.Item2;
                if (!Helper.IsInBoard(newRow, newColumn))
                    continue;
                if (GameBoard[newRow][newColumn].SimpleCell.Piece != null)
                    continue;
                reachableCells.Add(GameBoard[newRow][newColumn]);
            }
            return reachableCells;
        }

        public List<CellViewModel> GetReachableCells(CellViewModel currentCell)
        {
            List<CellViewModel> reachableCells = null;

            switch(currentCell.SimpleCell.Piece.Color)
            {
                case PieceColorEnum.BLACK:
                    if (currentCell.SimpleCell.Icon == Helper.BlackPawnPath)
                        reachableCells = GetReachableCellsForIndices(currentCell, Helper.moveIndicesForBlack);
                    else
                        reachableCells = GetReachableCellsForIndices(currentCell, Helper.moveIndicesForKings);
                    break;
                case PieceColorEnum.WHITE:
                    if (currentCell.SimpleCell.Icon == Helper.WhitePawnPath)
                        reachableCells = GetReachableCellsForIndices(currentCell, Helper.moveIndicesForWhite);
                    else
                        reachableCells = GetReachableCellsForIndices(currentCell, Helper.moveIndicesForKings);
                    break;
            }
            return reachableCells;
        }
    }
}
