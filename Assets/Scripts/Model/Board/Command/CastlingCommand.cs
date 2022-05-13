using System;
namespace Assets.Scripts.Model
{
    public class CastlingCommand: ICommand
    {
        public Piece King { get; private set; }
        public Vector2Integer KingStartSquareLocation { get; private set; }
        public Vector2Integer KingEndSquareLocation { get; private set; }
        public Vector2Integer KingSelectedSquareLocation { get; private set; }

        public Piece Rook { get; private set; }
        public Vector2Integer RookStartSquareLocation { get; private set; }
        public Vector2Integer RookEndSquareLocation { get; private set; }

        public CastlingCommand(Piece king, Vector2Integer kingStartSquareLocation, Vector2Integer kingEndSquareLocation,
            Vector2Integer kingSelectedSquareLocation, Piece rook, Vector2Integer rookStartSquareLocation, Vector2Integer rookEndSquareLocation)
        {
            King = king;
            KingStartSquareLocation = kingStartSquareLocation;
            KingEndSquareLocation = kingEndSquareLocation;
            KingSelectedSquareLocation = kingSelectedSquareLocation;
            Rook = rook;
            RookStartSquareLocation = rookStartSquareLocation;
            RookEndSquareLocation = rookEndSquareLocation;
        }

        public virtual void Do()
        {
            King.MoveTo(KingEndSquareLocation);
            Rook.MoveTo(RookEndSquareLocation);
        }

        public virtual void Undo()
        {
            King.MoveTo(KingStartSquareLocation, true);
            Rook.MoveTo(RookStartSquareLocation, true);
        }

        public Vector2Integer SquareClicked()
        {
            return KingSelectedSquareLocation;
        }
    }
}