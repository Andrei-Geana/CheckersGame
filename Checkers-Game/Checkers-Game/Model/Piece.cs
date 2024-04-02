using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    public class Piece
    {
        private PieceTypeEnum _type;
        private PieceColorEnum _color;
        public PieceTypeEnum Type { get => _type; set => _type = value; }
        public PieceColorEnum Color { get => _color; set => _color = value; }

        public Piece() { Type = PieceTypeEnum.PAWN; Color = PieceColorEnum.BLACK; }
        public Piece(PieceTypeEnum pieceTypeEnum, PieceColorEnum pieceColorEnum) { Type = pieceTypeEnum; Color = pieceColorEnum; }
        public Piece(PieceColorEnum pieceColorEnum) {  Color = pieceColorEnum; }


        public string PathToIcon
        {
            get 
            {
                if (Type == PieceTypeEnum.PAWN && Color == PieceColorEnum.BLACK)
                    return "/Checkers-Game;component/Resource/blackpiece.png";

                if (Type == PieceTypeEnum.PAWN && Color == PieceColorEnum.WHITE)
                    return "/Checkers-Game;component/Resource/whitepiece.png";

                if (Type == PieceTypeEnum.KING && Color == PieceColorEnum.BLACK)
                    return "/Checkers-Game;component/Resource/blackking.png";

                if (Type == PieceTypeEnum.KING && Color == PieceColorEnum.WHITE)
                    return "/Checkers-Game;component/Resource/whiteking.png";

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
