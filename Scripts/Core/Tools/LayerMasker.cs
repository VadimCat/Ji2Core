using System.Linq;
using UnityEngine;

namespace Ji2Core.Core.Tools
{
    public static class LayerMasker
    {
        public static bool CheckLayer(LayerMask mask, int layer) =>
            (mask.value & (1 << layer)) != 0;

        public static int MergeLayerMasks(params LayerMask[] masks) =>
            masks.Aggregate((mask1, mask2) => mask1.value | mask2.value);

        public static int SubtractLayersFromMask(LayerMask layerMask, params int[] layers) =>
            layers.Aggregate(layerMask.value, (result, layer) =>
                result & ~(1 << layer)
            );
    }
}