using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public interface IPieceObserver
    {
        void UpdateSelection(Boolean selected);
        void UpdateIsAlive(Boolean isAlive);
        void UpdatePieceType(PieceType newType);
        void UpdateSquareLocation(Vector2Integer newSquareLocation);
    }
}