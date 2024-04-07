using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers_Game.Service;
using Newtonsoft.Json;

namespace Checkers_Game.Model
{
    public class GameState
    {
        [JsonProperty("current-player")]
        private string _currentPlayer;
        [JsonProperty("cells")]
        private List<Cell> _cells;
        [JsonProperty("size")]
        private int size;
        public GameState() 
        { }

        public ObservableCollection<ObservableCollection<Cell>> GetBoard()
        {
            var board = Helper.GetNewEmptyBoard(size, size);
            foreach(var cell in _cells)
            {
                board[cell.Row][cell.Column] = cell;
            }
            return board;
        }

        public string GetCurrentPlayer()
        {
            return _currentPlayer;
        }

    }
}
