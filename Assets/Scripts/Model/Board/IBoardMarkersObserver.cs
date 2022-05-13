using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public interface IBoardMarkersObserver
    {
        void UpdateAvailableMovesMarkers(List<SquareMarker> availableMovesSquares);
    }
}