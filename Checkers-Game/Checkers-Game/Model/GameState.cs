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
        [JsonIgnore]
        public string CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        [JsonIgnore]
        public List<Cell> Cells { get => _cells; set => _cells = value; }
        [JsonIgnore]
        public int Size { get => size; set => size = value; }

        public ObservableCollection<ObservableCollection<Cell>> GetBoard()
        {
            var board = Helper.GetNewEmptyBoard(Size, Size);
            foreach(var cell in Cells)
            {
                board[cell.Row][cell.Column] = cell;
            }
            return board;
        }


    }
}
