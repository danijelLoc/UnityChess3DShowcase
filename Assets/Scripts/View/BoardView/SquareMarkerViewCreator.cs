using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
	public class SquareMarkerViewCreator : MonoBehaviour
	{
		[SerializeField] private Color freeSquareColor;
		[SerializeField] private Color enemySquareColor;
		[SerializeField] private Material squareMarkerMaterial;
		[SerializeField] private SquareMarkerView squareMarkerPrefab;
		private BoardView boardView;

		private void Awake()
		{
			boardView = GetComponent<BoardView>();
		}

		public SquareMarkerView CreateMarker(SquareMarker squareMarker)
		{
			SquareMarkerView newMarker = Instantiate(squareMarkerPrefab);
			newMarker.SetMaterial(squareMarkerMaterial);
			newMarker.SetColor(squareMarker.Type == SquareMarkerType.Free ? freeSquareColor : enemySquareColor);
			newMarker.transform.localPosition = boardView.PositionFromSquareLocation(squareMarker.Square);
			return newMarker;
		}
	}
}
