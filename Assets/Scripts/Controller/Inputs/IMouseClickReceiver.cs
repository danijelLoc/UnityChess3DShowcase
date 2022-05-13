using UnityEngine;

namespace Assets.Scripts.Controller
{
    public interface IMouseClickReceiver
    {
        void OnInputRecieved(Vector3 clickPosition);
    }
}