using System;
namespace Assets.Scripts.Model
{
    public class PromotionCommand : MoveCommand
    {

        public PromotionCommand(Piece selectedPiece, Vector2Integer startSquareLocation, Piece pieceToBeCaptured, Vector2Integer endSquareLocation) :
            base(selectedPiece, startSquareLocation, pieceToBeCaptured, endSquareLocation)
        { }

        public override void Do()
        {
            base.Do();
            SelectedPiece.SetType(PieceType.Queen);
        }

        public override void Undo()
        {
            base.Undo();
            SelectedPiece.SetType(PieceType.Pawn);
        }
    }
}