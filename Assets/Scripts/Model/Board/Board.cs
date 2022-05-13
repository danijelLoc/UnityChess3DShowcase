using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Board
    {
        public static readonly int SquaresInRow = 8;
        public IBoardMarkersObserver boardMarkersObserver;
        public List<Piece> pieces;
        public Piece[,] Layout
        {
            get
            {
                Piece[,] board = new Piece[SquaresInRow, SquaresInRow];
                foreach (Piece piece in pieces)
                {
                    if (piece.IsAlive)
                        board[piece.CurrentSquare.X, piece.CurrentSquare.Y] = piece;
                }
                return board;
            }
        }

        public Board(List<Piece> pieces)
        {
            this.pieces = pieces;
        }

        public static Boolean IsSquareInsideBoard(Vector2Integer square) {
            return 0 <= square.X && square.X < SquaresInRow && 0 <= square.Y && square.Y < SquaresInRow;
        }

        internal void SetAvailableMoves(List<ICommand> availableMoves)
        {
            List<SquareMarker> markers = new List<SquareMarker>();
            foreach (var command in availableMoves)
            {
                markers.Add(new SquareMarker(command.SquareClicked(), Layout[command.SquareClicked().X, command.SquareClicked().Y] != null ?
                    SquareMarkerType.Enemy : SquareMarkerType.Free));
            }
            boardMarkersObserver?.UpdateAvailableMovesMarkers(markers);
        }
    }
}
