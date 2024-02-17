using System.Linq;
using UnityEngine;

namespace Ji2.Tools
{
    public static class LayerUtils
    {
        public static bool CheckLayer(this LayerMask mask, int layer) =>
            (mask.value & (1 << layer)) != 0;

        public static int MergeLayerMasks(this LayerMask mask, LayerMask otherMask) =>
            mask.value | otherMask.value;
        
        public static int MergeLayerMasks(this LayerMask mask, params LayerMask[] otherMasks) =>
            otherMasks.Aggregate(mask.value, (result, layer) => result | layer.value);

        public static int SubtractLayersFromMask(this LayerMask layerMask, int layer) =>
            layerMask.value & ~(1 << layer);
        
        public static int SubtractLayersFromMask(this LayerMask layerMask, params int[] layers) =>
            layers.Aggregate(layerMask.value, (result, layer) =>
                result & ~(1 << layer)
            );
    }
}