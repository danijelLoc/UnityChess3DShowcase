using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class MovingService
    {

        public static List<ICommand> AvailableMoves(Piece piece, Board biard)
        {
            switch (piece.Type)
            {
                case PieceType.Pawn:
                    return PawnAvailableMoves(piece, biard);
                case PieceType.Knight:
                    return KnightAvailableMoves(piece, biard);
                case PieceType.Bishop:
                    return BishopAvailableMoves(piece, biard);
                case PieceType.Rook:
                    return RookAvailableMoves(piece, biard);
                case PieceType.Queen:
                    return QueenAvailableMoves(piece, biard);
                case PieceType.King:
                    return KingAvailableMoves(piece, biard);
                default:
                    throw new NotImplementedException("Moving strategy for piece type is not implemented");
            }
        }

        private static List<ICommand> RookAvailableMoves(Piece selectedPiece, Board board)
        {
            return MovingUtils.ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<ICommand> BishopAvailableMoves(Piece selectedPiece, Board board)
        {
            return MovingUtils.ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, Board.SquaresInRow);
        }

        private static List<ICommand> QueenAvailableMoves(Piece selectedPiece, Board board)
        {
            List<ICommand> diagonalMoves = MovingUtils.ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, Board.SquaresInRow);
            List<ICommand> lineMoves = MovingUtils.ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, Board.SquaresInRow);
            diagonalMoves.AddRange(lineMoves);
            return diagonalMoves;
        }

        private static List<ICommand> KingAvailableMoves(Piece selectedPiece, Board board)
        {
            List<ICommand> diagonalMoves = MovingUtils.ContinuousMoves(MovingUtils.diagonalDirections, selectedPiece, board, 1);
            List<ICommand> lineMoves = MovingUtils.ContinuousMoves(MovingUtils.lineDirections, selectedPiece, board, 1);

            List<ICommand> lineMovesCheckedForCastling = new List<ICommand>();
            foreach (MoveCommand move in lineMoves) {
                var castlingCommand = GetCastlingCommand(move, board);
                if (castlingCommand != null)
                    lineMovesCheckedForCastling.Add(castlingCommand);
                else
                    lineMovesCheckedForCastling.Add(move);
            }

            diagonalMoves.AddRange(lineMovesCheckedForCastling);
            return diagonalMoves;
        }

        private static CastlingCommand GetCastlingCommand(MoveCommand move, Board board)
        {
            Vector2Integer leftDirection = new Vector2Integer(-1, 0);
            Vector2Integer rightDirection = new Vector2Integer(1, 0);
            int firstRow = move.SelectedPiece.Team == Team.White ? 0 : 7;

            var LeftCastlingKingLocation = new Vector2Integer(MovingUtils.LeftCastlingKingXLocation, firstRow);
            var LeftCastlingRookLocation = new Vector2Integer(MovingUtils.LeftCastlingRookXLocation, firstRow);
            var RightCastlingKingLocation = new Vector2Integer(MovingUtils.RightCastlingKingXLocation, firstRow);
            var RightCastlingRookLocation = new Vector2Integer(MovingUtils.RightCastlingRookXLocation, firstRow);

            // TODO cant castle under attack, also if new position is under attack, or if path between is under attack
            var direction = move.EndSquareLocation - move.StartSquareLocation;
            var firstPiece = MovingUtils.FirstPieceInDirection(direction, move.SelectedPiece, board);
            if (move.SelectedPiece.MoveCounter == 0 && firstPiece.Type == PieceType.Rook &&
                firstPiece.MoveCounter == 0 && move.StartSquareLocation.Y == firstRow && firstPiece.Team == move.SelectedPiece.Team)
            {
                // directly checking x cordinate because of possible quizz mode where rook is on different position but has 0 in move counter
                if (direction.Equals(leftDirection) && firstPiece.CurrentSquare.X == 0)
                {
                    return new CastlingCommand(move.SelectedPiece, move.StartSquareLocation, LeftCastlingKingLocation,
                        move.EndSquareLocation, firstPiece, firstPiece.CurrentSquare, LeftCastlingRookLocation);
                }
                else if (direction.Equals(rightDirection) && firstPiece.CurrentSquare.X == 7) {
                    return new CastlingCommand(move.SelectedPiece, move.StartSquareLocation, RightCastlingKingLocation,
                        move.EndSquareLocation, firstPiece, firstPiece.CurrentSquare, RightCastlingRookLocation);
                }
                    
            }
            return null;
        }


        private static List<ICommand> KnightAvailableMoves(Piece selectedPiece, Board board)
        {
            List<ICommand> moves = new List<ICommand>();
            foreach (var direction in MovingUtils.knightDirections)
            {
                Vector2Integer potentialSquare = selectedPiece.CurrentSquare + direction;
                if (!Board.IsSquareInsideBoard(potentialSquare))
                    continue;
                Piece pieceOnPotentialSquare = board.Layout[potentialSquare.X, potentialSquare.Y];
                if (pieceOnPotentialSquare == null)
                {
                    MoveCommand move = new MoveCommand(selectedPiece, selectedPiece.CurrentSquare, null, potentialSquare);
                    moves.Add(move);
                }
                else if (pieceOnPotentialSquare.Team != selectedPiece.Team)
                {
                    // TODO check king shielding
                    MoveCommand move = new MoveCommand(selectedPiece, selectedPiece.CurrentSquare, pieceOnPotentialSquare, potentialSquare);
                    moves.Add(move);
                }
                else if (pieceOnPotentialSquare.Team == selectedPiece.Team)
                    continue;
            }
            return moves;
        }


        private static List<ICommand> PawnAvailableMoves(Piece selectedPawn, Board board)
        {
            List<ICommand> moves = new List<ICommand>();

            // advance
            Vector2Integer direction = MovingUtils.PawnLineDirection(selectedPawn.Team);
            var lineAdvanceMoves = MovingUtils.ContinuousMoves(new List<Vector2Integer> { direction }, selectedPawn,
                board, selectedPawn.MoveCounter == 0 ? 2 : 1, false);
            moves.AddRange(lineAdvanceMoves);

            // atack
            List<Vector2Integer> attackDirections = MovingUtils.PawnAttackDirections(selectedPawn.Team);
            foreach (var attackDirection in attackDirections) {
                Vector2Integer destination = selectedPawn.CurrentSquare + attackDirection;
                if (!Board.IsSquareInsideBoard(destination))
                    continue;
                Piece pieceToCapture = board.Layout[destination.X, destination.Y];
                Piece pieceNextTo = board.Layout[destination.X, selectedPawn.CurrentSquare.Y];
                // pieceToCaptureEnPassant
                if (pieceNextTo != null && pieceNextTo.Type == PieceType.Pawn && pieceNextTo.MoveCounter == 1 &&
                    pieceNextTo.CurrentSquare.Y == MovingUtils.PawnTwoSquareAdvanceYLocation(pieceNextTo.Team))
                {
                    // TODO turn right after two square advance only
                    var enPassant = new MoveCommand(selectedPawn, selectedPawn.CurrentSquare, pieceNextTo, destination);
                    moves.Add(enPassant);
                }
                if (pieceToCapture != null && pieceToCapture.Team != selectedPawn.Team)
                {
                    moves.Add(new MoveCommand(selectedPawn, selectedPawn.CurrentSquare, pieceToCapture, destination));
                }
            }

            List<ICommand> movesCheckedForPromotion = new List<ICommand>();
            foreach (MoveCommand move in moves)
            {
                if (move.EndSquareLocation.Y == MovingUtils.PawnPromotionYLocation(move.SelectedPiece.Team))
                    movesCheckedForPromotion.Add(new PromotionCommand(move.SelectedPiece, move.StartSquareLocation, move.PieceToBeCaptured, move.EndSquareLocation));
                else
                    movesCheckedForPromotion.Add(move);
            }

            return (List<ICommand>)movesCheckedForPromotion;
        }
    }
}
