using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts.Model;
using Assets.Scripts.View;

namespace Assets.Scripts.Controller
{
    public class BoardController : MonoBehaviour, IMouseClickHandler
    {
        private BoardView boardView;
        private GameManager gameManager;
        [SerializeField] private PieceViewCreator pieceViewCreator;


        private void Awake()
        {
            boardView = GetComponent<BoardView>();
        }

        private void Start()
        {
            InitialLayout();
        }

        public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
        {
            Vector2Integer squareLocation = boardView.SquareLocationFromPosition(inputPosition);
            gameManager.OnSquareSelected(squareLocation);
        }

        public void ShowLayout(Board boardLayout)
        {
            foreach (Piece piece in boardLayout.pieces)
            {
                PieceView pieceView = pieceViewCreator.CreatePieceView(piece);
                //pieceView.enabled = piece.Destoryed;
            }
        }

        private void InitialLayout()
        {
            // TODO from file
            List<Piece> pieces = new List<Piece>();
            pieces.AddRange(CreateTeam(Team.White));
            pieces.AddRange(CreateTeam(Team.Black));
            Board board = new Board(pieces);
            board.boardMarkersObserver = boardView;
            gameManager = new GameManager(board);
            ShowLayout(board);
        }

        private List<Piece> CreateTeam(Team team)
        {
            List<Piece> pieces = new List<Piece>();
            int firstRowY = team == Team.White ? 0 : 7;
            int secondRowY = team == Team.White ? 1 : 6;

            pieces.Add(new Piece(PieceType.King, team, new Vector2Integer(4, firstRowY)));
            pieces.Add(new Piece(PieceType.Queen, team, new Vector2Integer(3, firstRowY)));
            pieces.Add(new Piece(PieceType.Rook, team, new Vector2Integer(0, firstRowY)));
            pieces.Add(new Piece(PieceType.Rook, team, new Vector2Integer(7, firstRowY)));
            pieces.Add(new Piece(PieceType.Knight, team, new Vector2Integer(1, firstRowY)));
            pieces.Add(new Piece(PieceType.Knight, team, new Vector2Integer(6, firstRowY)));
            pieces.Add(new Piece(PieceType.Bishop, team, new Vector2Integer(2, firstRowY)));
            pieces.Add(new Piece(PieceType.Bishop, team, new Vector2Integer(5, firstRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(0, secondRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(1, secondRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(2, secondRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(3, secondRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(4, secondRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(5, secondRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(6, secondRowY)));
            pieces.Add(new Piece(PieceType.Pawn, team, new Vector2Integer(7, secondRowY)));

            return pieces;
        }
    }
}