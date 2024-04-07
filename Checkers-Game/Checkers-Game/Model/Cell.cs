using Checkers_Game.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    public class Cell
    {
        [JsonIgnore]
        private int _row;
        [JsonIgnore]
        private int _column;
        [JsonIgnore]
        private Piece _piece;
        [JsonIgnore]
        private PieceColorEnum _backgroundColor;
        public Cell() { }
        public Cell(int row, int column, PieceColorEnum _backgroundColor, Piece piece=null)
        {
            Row = row;
            Column = column;
            Piece = piece;
            this._backgroundColor = _backgroundColor;
        }
        [JsonProperty("row")]
        public int Row { get => _row; set => _row = value; }
        [JsonProperty("column")]
        public int Column { get => _column; set => _column = value; }
        [JsonProperty("piece")]
        public Piece Piece { get => _piece; set => _piece = value; }
        [JsonIgnore]
        public string BackgroundColor 
        {
            get
            {
                switch(_backgroundColor)
                {
                    case PieceColorEnum.WHITE:
                        return Helper.WhiteColor;
                    case PieceColorEnum.BLACK:
                        return Helper.BlackColor;
                    case PieceColorEnum.BLUE:
                        return Helper.BlueColor;
                    case PieceColorEnum.GREEN:
                        return Helper.GreenColor;
                }
                if (_backgroundColor == PieceColorEnum.BLACK)
                    return Helper.BlackColor;
                if (_backgroundColor == PieceColorEnum.WHITE)
                    return Helper.WhiteColor;

                //error check
                throw new ArgumentOutOfRangeException(nameof(BackgroundColor));
            }
            set
            {
                switch(value)
                {
                    case "WHITE":
                        _backgroundColor = PieceColorEnum.WHITE;
                        break;
                    case "BLACK":
                        _backgroundColor = PieceColorEnum.BLACK;
                        break;
                    case "BLUE":
                        _backgroundColor = PieceColorEnum.BLUE;
                        break;
                    case "GREEN":
                        _backgroundColor = PieceColorEnum.GREEN;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(BackgroundColor));
                }
            }
        }

        [JsonIgnore]
        public string Icon
        {
            get 
            {
                if (_piece is null)
                    return Helper.TransparentPath;
                return Piece.PathToIcon;
            }

            set
            {

            }
        }
    }
}
