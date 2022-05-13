using System;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public interface IMouseClickHandler
    {
        void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick);
    }
}
