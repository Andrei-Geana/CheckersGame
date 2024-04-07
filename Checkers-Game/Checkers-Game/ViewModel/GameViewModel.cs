using Checkers_Game.Command;
using Checkers_Game.Model;
using Checkers_Game.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace Checkers_Game.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        private GameLogic _gameLogic;
        private ObservableCollection<ObservableCollection<CellViewModel>> _gameBoard;
        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;
        public GameViewModel()
        {
            _gameLogic = new GameLogic(this);
            _gameBoard = CellBoardToCellVMBoard(Helper.GetNewStandardBoard());
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
        public ObservableCollection<ObservableCollection<CellViewModel>> GameBoard
        {
            get
            {
                return _gameBoard;
            }
            set
            {
                _gameBoard = value;
                OnPropertyChanged(nameof(GameBoard));
            }
        }
        public Player Player1 { get => _player1; set => _player1 = value; }
        public Player Player2 { get => _player2; set => _player2 = value; }
        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                if (_currentPlayer == value) return;
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        private Dictionary<CellViewModel, bool> GetReachableCellsForIndices(CellViewModel currentCell, List<Tuple<int, int>> moveIndices)
        {
            Dictionary<CellViewModel, bool> reachableCells = new Dictionary<CellViewModel, bool>();
            foreach (var move in moveIndices)
            {
                int newRow = currentCell.SimpleCell.Row + move.Item1;
                int newColumn = currentCell.SimpleCell.Column + move.Item2;
                if (!Helper.IsInBoard(newRow, newColumn))
                    continue;
                if (GameBoard[newRow][newColumn].SimpleCell.Piece != null)
                {
                    if (GameBoard[newRow][newColumn].SimpleCell.Piece.Color != currentCell.SimpleCell.Piece.Color)
                    {
                        int newOverRow = newRow + move.Item1;
                        int newOverColumn = newColumn + move.Item2;
                        if (!Helper.IsInBoard(newOverRow, newOverColumn))
                            continue;
                        if (GameBoard[newOverRow][newOverColumn].SimpleCell.Piece != null)
                            continue;
                        reachableCells.Add(GameBoard[newOverRow][newOverColumn], true);
                    }
                    continue;
                }
                reachableCells.Add(GameBoard[newRow][newColumn], false);
            }
            return reachableCells;
        }

        public Dictionary<CellViewModel, bool> GetReachableCells(CellViewModel currentCell)
        {
            Dictionary<CellViewModel, bool> reachableCells = null;

            switch (currentCell.SimpleCell.Piece.Color)
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

        public void EliminatePieceAt(int row, int column)
        {
            GameBoard[row][column].SimpleCell.Piece = null;
            GameBoard[row][column].NotifyThatPieceChanged();
        }

        public int GetNumberOfPiecesOfAColor(PieceColorEnum pieceColorEnum)
        {
            int nr = 0;
            for (int i = 0; i < GameBoard.Count; ++i)
            {
                for (int j = 0; j < GameBoard[i].Count; ++j)
                    if (GameBoard[i][j].SimpleCell.Piece != null && GameBoard[i][j].SimpleCell.Piece.Color == pieceColorEnum)
                        nr++;
            }
            return nr;
        }

        private ICommand _newGameButton;
        public ICommand NewGameButton
        {
            get
            {
                if (_newGameButton == null)
                {
                    _newGameButton = new RelayCommand<object>(param => ResetGame());
                }
                return _newGameButton;
            }
        }

        private ICommand _openGameButton;
        public ICommand OpenGameButton
        {
            get
            {
                if (_openGameButton == null)
                {
                    _openGameButton = new RelayCommand<object>(param => LoadGameState());
                }
                return _openGameButton;
            }
        }
        public void ResetGame()
        {
            GameBoard = CellBoardToCellVMBoard(Helper.GetNewStandardBoard());
            CurrentPlayer = Player1;
        }


        public void LoadGameState()
        {
            //might add messagebox that requests to save a model input file

            //open input messagebox
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt;*.|All files|*.*";
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() is true)
            {
                string sourceFilePath = openFileDialog.FileName;
                Console.Write(sourceFilePath);

                GameState gamestate = Helper.LoadSavedGame(sourceFilePath);
                ApplyGameState(gamestate);
            }
            else
            {
                //testing
                GameState gamestate = Helper.LoadSavedGame("D:\\facultate\\an2\\sem2\\MAP\\Tema2MAP\\Checkers-Game\\Checkers-Game\\Resource\\savedGame.txt");
                ApplyGameState(gamestate);
            }
        }

        private void ApplyGameState(GameState state)
        {
            if (state is null) return;
            GameBoard = CellBoardToCellVMBoard(state.GetBoard());

            //should be changed
            if (state.GetCurrentPlayer() == "WHITE")
            {
                CurrentPlayer = Player2;
            }
            else
                CurrentPlayer = Player1;

        }

        public bool Multiple_Jump_Setting
        {
            get
            {
                return _gameLogic.CanMultipleJump;
            }
            set
            {
                _gameLogic.CanMultipleJump = value;
                OnPropertyChanged(nameof(Multiple_Jump_Setting));
            }
        }

        public bool Show_Possible_Moves_Setting
        {
            get
            {
                return _gameLogic.CanShowPossibleMoves;
            }
            set
            {
                _gameLogic.CanShowPossibleMoves = value;
                OnPropertyChanged(nameof(Show_Possible_Moves_Setting));
            }
        }
    }
}
