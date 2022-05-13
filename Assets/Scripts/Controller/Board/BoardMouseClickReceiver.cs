using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoardMouseClickReceiver : MonoBehaviour, IMouseClickReceiver
    {
        protected IMouseClickHandler[] clickHandlers;
        protected BoxCollider targetBoxCollider;

        private void Awake()
        {
            clickHandlers = GetComponents<IMouseClickHandler>();
            targetBoxCollider = GetComponent<BoxCollider>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    // if player click on upper side of board
                    if (hit.collider == targetBoxCollider &&
                        hit.point.y >= targetBoxCollider.bounds.max.y - 1e-4)
                        OnInputRecieved(hit.point);
                }
            }
        }

        public void OnInputRecieved(Vector3 clickPosition)
        {
            foreach (var clickHandler in clickHandlers)
            {
                clickHandler.ProcessInput(clickPosition, null, null);
            }
        }
    }
}