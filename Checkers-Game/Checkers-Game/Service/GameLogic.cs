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
        List<CellViewModel> possibleMoves;

        public GameLogic(GameViewModel gameViewModel) { game = gameViewModel; }


        //scenarii:
        //  -firstCell nu a fost selectat inca -> se seteaza firstCell, se modifica culoarea si se modifica toate mutarile posibile pentru ilustrare
        //  -firstCell a fost deja selectat, dar obj este chiar firstCell => nu se intampla nimic
        //  -firstCell a fost deja selectat, dar obj este o piesa de aceeasi culoare => firstCell devine obj si se reseteaza/seteaza culorile
        //  -firstCell a fost deja selectat, iar obj este un spatiu gol => se va muta piesa de la firstCell la obj
        public void ClickAction(CellViewModel obj)
        {
            if (firstSelectedCell == null)
            {
                //this should be replaced with changing the color of BackgroundColor
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
                    //here firstCell should reset color and obj should get the selectedCell color
                    RevertToOriginalBackgroundColor(firstSelectedCell);
                    RevertPossibleMoves();
                    firstSelectedCell = obj;
                    ShowPossibleMoves();
                    ChangeBackgroundColor(firstSelectedCell, PieceColorEnum.BLUE);
                    return;
                }

                //  trebuie sa se verifice daca sare peste o piesa sau mutarea este valabila
                RevertToOriginalBackgroundColor(firstSelectedCell);
                RevertPossibleMoves();
                SwitchPiece(obj, firstSelectedCell);
                CheckForPromotion(obj);
                SwitchPlayerTurn();
                
            }
        }


        //  daca nu a fost selectata inca o piesa, se pot selecta doar piesele care au aceeasi culoare ca currentPlayer din GameViewModel
        //  daca a fost selectata o prima piesa, atunci se poate selecta o alta piesa de aceeasi culoare, sau se va selecta o celula goala si se va seta secondNode
        
        //  trebuie sa adaug ca doar mutarile posibile sa fie selectabile
        public bool IsClickable(CellViewModel currentCell)
        {
            if (currentCell == null) return false;
            if (firstSelectedCell == null)
                return currentCell.SimpleCell.Piece != null && currentCell.SimpleCell.Piece.Color == game.CurrentPlayer.Color;
            if (currentCell == firstSelectedCell)
                return true;
            if (currentCell.SimpleCell.Piece != null && currentCell.SimpleCell.Piece.Color == game.CurrentPlayer.Color)
                return true;
            return possibleMoves.Contains(currentCell);
            //return (currentCell.SimpleCell.Piece == null) || (currentCell.SimpleCell.Piece.Color == game.CurrentPlayer.Color);
        }

        private void SwitchPiece(CellViewModel toCell, CellViewModel fromCell)
        {
            toCell.SimpleCell.Piece = fromCell.SimpleCell.Piece;
            fromCell.SimpleCell.Piece = null;
            toCell.NotifyThatPieceChanged();
            fromCell.NotifyThatPieceChanged();
        }

        private void SwitchPlayerTurn()
        {
            if (game.CurrentPlayer == game.Player1)
                game.CurrentPlayer = game.Player2;
            else
                game.CurrentPlayer = game.Player1;
            firstSelectedCell = null;
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
            foreach(var item in possibleMoves)
            {
                ChangeBackgroundColor(item, PieceColorEnum.GREEN);
            }
        }

        private void RevertPossibleMoves()
        {
            foreach (var item in possibleMoves)
            {
                RevertToOriginalBackgroundColor(item);
            }
        }
    }
}
