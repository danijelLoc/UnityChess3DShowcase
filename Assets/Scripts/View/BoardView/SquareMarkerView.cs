using System;
using UnityEngine;

namespace Assets.Scripts.View
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SquareMarkerView : MonoBehaviour
    {
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }
    }
}
