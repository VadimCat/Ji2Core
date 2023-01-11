using UnityEngine;

namespace SceneView
{
    [CreateAssetMenu]
    public class ColoringConfig : ScriptableObject
    {
        [SerializeField] private float brushRadius = .3f;

        public float BrushRadius => brushRadius;
    }
}