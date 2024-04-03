using Checkers_Game.Model;
using Checkers_Game.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Checkers_Game.Service
{
    public class GameLogic
    {
        private GameViewModel game;
        public GameLogic(GameViewModel gameViewModel) { game = gameViewModel; }

        public void ClickAction(Cell obj)
        {
            MessageBox.Show("Ai apasat un buton de la " + obj.Row.ToString() +"," + obj.Column.ToString());

        }

        public bool IsClickable(Cell currentCell)
        {
            if (currentCell == null) return false;
            return currentCell.Piece != null;
        }
    }
}
