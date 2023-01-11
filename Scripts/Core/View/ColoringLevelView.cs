using PaintIn3D;
using UnityEngine;

namespace SceneView
{
    public class ColoringLevelView : MonoBehaviour
    {
        [SerializeField] private P3dChangeCounter counter;
        [SerializeField] private ColoringConfig coloringConfig;

        public float ProgressRatio => counter.Ratio;
    }
}