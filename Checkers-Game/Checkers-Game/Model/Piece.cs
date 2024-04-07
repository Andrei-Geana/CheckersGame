using Checkers_Game.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    public class Piece
    {
        [JsonIgnore]
        private PieceTypeEnum _type;
        [JsonIgnore]
        private PieceColorEnum _color;
        [JsonProperty("type")]
        public PieceTypeEnum Type { get => _type; set => _type = value; }
        [JsonProperty("color")]
        public PieceColorEnum Color { get => _color; set => _color = value; }

        public Piece() {}
        public Piece(PieceTypeEnum pieceTypeEnum, PieceColorEnum pieceColorEnum) { Type = pieceTypeEnum; Color = pieceColorEnum; }
        public Piece(PieceColorEnum pieceColorEnum) {  Color = pieceColorEnum; }

        [JsonIgnore]
        public string PathToIcon
        {
            get 
            {
                if (Type == PieceTypeEnum.PAWN && Color == PieceColorEnum.BLACK)
                    return Helper.BlackPawnPath;

                if (Type == PieceTypeEnum.PAWN && Color == PieceColorEnum.WHITE)
                    return Helper.WhitePawnPath;

                if (Type == PieceTypeEnum.KING && Color == PieceColorEnum.BLACK)
                    return Helper.BlackKingPath;

                if (Type == PieceTypeEnum.KING && Color == PieceColorEnum.WHITE)
                    return Helper.WhiteKingPath;

                //error check
                return "";
            }
        }

        public void ChangeTypeTo(PieceTypeEnum pieceTypeEnum)
        {
            Type = pieceTypeEnum;
        }

        public void RankUp()
        {
            Type = PieceTypeEnum.KING;
        }

    }
}
