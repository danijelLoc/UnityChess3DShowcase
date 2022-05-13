using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
    class PieceViewCreator : MonoBehaviour
    {
        [SerializeField] private PieceView[] piecesPrefabs;
        [SerializeField] private Material whitePieceMaterial;
        [SerializeField] private Material blackPieceMaterial;
        public MeshFilter queenMeshFilter;
        public MeshFilter pawnMeshFilter;
        private BoardView boardView;
        private Dictionary<Team, Material> teamToMaterial = new Dictionary<Team, Material>();
        private Dictionary<PieceType, PieceView> pieceTypeToPrefab = new Dictionary<PieceType, PieceView>();

        private void Awake()
        {
            boardView = GetComponent<BoardView>();
            teamToMaterial.Add(Team.Black, blackPieceMaterial);
            teamToMaterial.Add(Team.White, whitePieceMaterial);
            foreach (var piecePrefab in piecesPrefabs)
            {
                pieceTypeToPrefab.Add(piecePrefab.PieceType, piecePrefab);
            }
        }

        public PieceView CreatePieceView(Piece piece)
        {
            PieceView piecePrefab = pieceTypeToPrefab[piece.Type];
            PieceView newPiece = Instantiate(piecePrefab);
            newPiece.SetMaterial(teamToMaterial[piece.Team]);
            newPiece.SetBoard(boardView);
            newPiece.SetPieceViewCreator(this);
            newPiece.transform.localPosition = boardView.PositionFromSquareLocation(piece.CurrentSquare);
            piece.observer = newPiece;
            return newPiece;
        }
    }
}