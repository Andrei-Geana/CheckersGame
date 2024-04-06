using Checkers_Game.Model;
using Checkers_Game.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Checkers_Game.Service
{
    public class GameLogic
    {
        private GameViewModel game;
        private CellViewModel firstSelectedCell = null;

        //toCell, bool if it eliminates enemy piece
        Dictionary<CellViewModel, bool> possibleMoves;

        bool eliminatedAPiece = false;
        bool canMultipleJump = false;

        public GameLogic(GameViewModel gameViewModel) { game = gameViewModel; }

        public void ClickAction(CellViewModel obj)
        {
            if (firstSelectedCell == null)
            {
                //MessageBox.Show("Ai apasat un buton de la " + obj.SimpleCell.Row.ToString() + "," + obj.SimpleCell.Column.ToString());
                firstSelectedCell = obj;
                ChangeBackgroundColor(firstSelectedCell, PieceColorEnum.BLUE);
                ShowPossibleMoves();
            }
            else
            {
                if (obj == firstSelectedCell)
                {
                    RevertToOriginalBackgroundColor(firstSelectedCell);
                    RevertPossibleMoves();
                    firstSelectedCell = null;
                    return;
                }
                if(obj.SimpleCell.Piece!=null && obj.SimpleCell.Piece.Color == game.CurrentPlayer.Color)
                {
                    RevertToOriginalBackgroundColor(firstSelectedCell);
                    RevertPossibleMoves();
                    firstSelectedCell = obj;
                    ChangeBackgroundColor(firstSelectedCell, PieceColorEnum.BLUE);
                    ShowPossibleMoves();
                    return;
                }

                RevertToOriginalBackgroundColor(firstSelectedCell);
                RevertPossibleMoves();
                SwitchPiece(obj, firstSelectedCell);
                CheckForPromotion(obj);

                if (eliminatedAPiece is true && canMultipleJump is true)
                {
                    ShowPossibleMoves();
                    KeepOnlyMovesThatEliminateEnemyPiece();
                    if (possibleMoves.Count < 1)
                    {
                        SwitchPlayerTurn();
                    }
                    else
                    {
                        ChangeBackgroundColor(firstSelectedCell, PieceColorEnum.BLUE);
                    }
                }
                else 
                    SwitchPlayerTurn();
                
            }
        }

        public bool IsClickable(CellViewModel currentCell)
        {
            if (currentCell == null) return false;

            if (firstSelectedCell == null)
                return currentCell.SimpleCell.Piece != null && currentCell.SimpleCell.Piece.Color == game.CurrentPlayer.Color;

            if (canMultipleJump is true && eliminatedAPiece is true)
            {
                return possibleMoves.ContainsKey(currentCell);
            }

            if (currentCell == firstSelectedCell)
                return true;

            if (currentCell.SimpleCell.Piece != null && currentCell.SimpleCell.Piece.Color == game.CurrentPlayer.Color)
                return true;

            return possibleMoves.ContainsKey(currentCell);
        }

        private void SwitchPiece(CellViewModel toCell, CellViewModel fromCell)
        {
            if (possibleMoves[toCell] is true)
            {
                int row = (toCell.SimpleCell.Row + fromCell.SimpleCell.Row) / 2;
                int column = (toCell.SimpleCell.Column + fromCell.SimpleCell.Column) / 2;
                game.EliminatePieceAt(row, column);
                eliminatedAPiece = true;
            }
            toCell.SimpleCell.Piece = fromCell.SimpleCell.Piece;
            fromCell.SimpleCell.Piece = null;
            toCell.NotifyThatPieceChanged();
            fromCell.NotifyThatPieceChanged();


            if (canMultipleJump is true && eliminatedAPiece is true)
            {
                firstSelectedCell = toCell;
            }
        }

        private void SwitchPlayerTurn()
        {
            if (game.CurrentPlayer == game.Player1)
                game.CurrentPlayer = game.Player2;
            else
                game.CurrentPlayer = game.Player1;
            firstSelectedCell = null;
            eliminatedAPiece = false;
        }
        private void CheckForPromotion(CellViewModel obj)
        {
            if (obj.SimpleCell.Row == 0 && obj.SimpleCell.Piece.Color == PieceColorEnum.BLACK)
                obj.SimpleCell.Piece.RankUp();
            else if (obj.SimpleCell.Row == (Helper.numberOfRows-1) && obj.SimpleCell.Piece.Color == PieceColorEnum.WHITE)
                obj.SimpleCell.Piece.RankUp();
            obj.NotifyThatPieceChanged();
        }

        private void RevertToOriginalBackgroundColor(CellViewModel obj)
        {
            Helper.ResetColor(obj);
            obj.NotifyThatPieceChanged();
        }
        private void ChangeBackgroundColor(CellViewModel obj, PieceColorEnum color)
        {
            switch(color)
            {
                case PieceColorEnum.WHITE:
                    obj.SimpleCell.BackgroundColor = nameof(PieceColorEnum.WHITE);
                    break;
                case PieceColorEnum.BLACK:
                    obj.SimpleCell.BackgroundColor = nameof(PieceColorEnum.BLACK);
                    break;
                case PieceColorEnum.BLUE:
                    obj.SimpleCell.BackgroundColor = nameof(PieceColorEnum.BLUE);
                    break;
                case PieceColorEnum.GREEN:
                    obj.SimpleCell.BackgroundColor = nameof(PieceColorEnum.GREEN);
                    break;
            }
            obj.NotifyThatPieceChanged();
        }

        private void ShowPossibleMoves() 
        {
            possibleMoves = game.GetReachableCells(firstSelectedCell);
            foreach (var item in possibleMoves)
            {
                ChangeBackgroundColor(item.Key, PieceColorEnum.GREEN);
            }
        }

        private void RevertPossibleMoves()
        {
            foreach (var item in possibleMoves)
            {
                RevertToOriginalBackgroundColor(item.Key);
            }
        }

        private void KeepOnlyMovesThatEliminateEnemyPiece()
        {
            var filteredMoves = possibleMoves.Where(item => item.Value == true)
                                              .ToDictionary(item => item.Key, item => item.Value);
            foreach(var item in possibleMoves)
            {
                if (!filteredMoves.ContainsKey(item.Key))
                    RevertToOriginalBackgroundColor(item.Key);
            }

            possibleMoves = filteredMoves;

        }
    }
}
