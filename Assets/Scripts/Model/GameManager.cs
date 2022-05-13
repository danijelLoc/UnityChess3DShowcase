using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class GameManager
    {
        private Piece selectedPiece;
        private List<ICommand> selectedPieceAvailableMoves;
        private Board board;
        private Team currentTeam;

        public GameManager(Board startingBoardLayout, Team startingTeam = Team.White)
        {
            this.board = startingBoardLayout;
            currentTeam = startingTeam;
        }

        public void OnSquareSelected(Vector2Integer squareLocation)
        {
            Piece piece = board.Layout[squareLocation.X, squareLocation.Y];
            if (selectedPiece != null)
            {
                if (piece != null && selectedPiece == piece)
                    Deselect();
                else if (piece != null && selectedPiece != piece && piece.Team == currentTeam)
                    Select(piece);
                else
                    TryToMoveSelectedPiece(squareLocation);
            }
            else
            {
                if (piece != null && piece.Team == currentTeam)
                    Select(piece);
            }
        }

        private void TryToMoveSelectedPiece(Vector2Integer destinationSquare)
        {
            ICommand availableCommand = AvailableMoveForDestinationSquare(destinationSquare);
            if (availableCommand != null)
            {
                availableCommand.Do();
                Deselect();
                SwitchCurrentTeam();
            }
        }

        private ICommand AvailableMoveForDestinationSquare(Vector2Integer destinationSquare)
        {
            foreach (var move in selectedPieceAvailableMoves)
            {
                if (move.SquareClicked().Equals(destinationSquare))
                    return move;
            }
            return null;
        }

        private void Select(Piece piece)
        {
            Deselect();
            piece.SetIsSelected(true);
            selectedPiece = piece;
            selectedPieceAvailableMoves = MovingService.AvailableMoves(piece, board);
            board.SetAvailableMoves(selectedPieceAvailableMoves);
        }

        private void Deselect()
        {
            if (selectedPiece != null)
            {
                selectedPiece.SetIsSelected(false);
                selectedPiece = null;
                board.SetAvailableMoves(new List<ICommand>());
            }
        }

        private void SwitchCurrentTeam()
        {
            switch (currentTeam)
            {
                case Team.White:
                    currentTeam = Team.Black; break;

                case Team.Black:
                    currentTeam = Team.White; break;
            }
        }
    }
}

