using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public enum SquareMarkerType
    {
        Free, Enemy
    }

    public class SquareMarker
    {
        public Vector2Integer Square { get; private set; }
        public SquareMarkerType Type { get; private set; }

        public SquareMarker(Vector2Integer square, SquareMarkerType type)
        {
            Square = square;
            Type = type;
        }
    }
}
