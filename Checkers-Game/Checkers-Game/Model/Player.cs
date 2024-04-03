using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    public class Player
    {
        private string _username;
        private PieceColorEnum _color;
        private int _score;
        public Player(string username, PieceColorEnum color, int score=0)
        {
            Username = username;
            Color = color;
            Score = score;
        }

        public string Username { get => _username; set => _username = value; }
        public PieceColorEnum Color { get => _color; set => _color = value; }
        public int Score { get => _score; set => _score = value; }
    }
}
