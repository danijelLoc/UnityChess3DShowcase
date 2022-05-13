using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;
using System.Collections.Generic;

namespace Assets.Scripts.View
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoardView : MonoBehaviour, IBoardMarkersObserver
    {
        public Transform bottomLeftSquare;
        private BoxCollider boxCollider; 
        public float Width { get { return boxCollider.size.x; } }
        public float SquareWidth { get { return bottomLeftSquare.localScale.x; } }
        private SquareMarkerViewCreator squareMarkerCreator;
        private List<SquareMarkerView> squareMarkerViews;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            squareMarkerCreator = GetComponent<SquareMarkerViewCreator>();
            squareMarkerViews = new List<SquareMarkerView>();
        }

        public Vector2Integer SquareLocationFromPosition(Vector3 position)
        {
            int x = Mathf.FloorToInt((transform.InverseTransformPoint(position).x + Width / 2) / SquareWidth);
            int y = Mathf.FloorToInt((transform.InverseTransformPoint(position).z + Width / 2) / SquareWidth);
            return new Vector2Integer(x, y);
        }

        public Vector3 PositionFromSquareLocation(Vector2Integer squareLocation)
        {
            return bottomLeftSquare.position +
                new Vector3(squareLocation.X * SquareWidth,
                            0f, squareLocation.Y * SquareWidth);
        }

        public void UpdateAvailableMovesMarkers(List<SquareMarker> availableMovesSquares)
        {
            ClearMarkers();
            foreach (SquareMarker squareMarker in availableMovesSquares)
            {
                SquareMarkerView markerView = squareMarkerCreator.CreateMarker(squareMarker);
                squareMarkerViews.Add(markerView);
            }
        }

        public void ClearMarkers()
        {
            for (int i = 0; i < squareMarkerViews.Count; i++)
            {
                Destroy(squareMarkerViews[i].gameObject);
            }
            squareMarkerViews = new List<SquareMarkerView>();
        }
    }
}
