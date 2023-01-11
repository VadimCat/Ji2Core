using UnityEngine;

public static class FloatExtensions
{
    public static float ToAudioLevel(this float value)
    {
        return Mathf.Log10(value) * 20;
    }
}
