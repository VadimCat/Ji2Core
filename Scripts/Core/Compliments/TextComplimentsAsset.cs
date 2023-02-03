using UnityEngine;

namespace Core.Compliments
{
    [CreateAssetMenu(menuName = "Configs/Compliments/TextCompliments")]
    public class TextComplimentsAsset : ScriptableObject
    {
        [SerializeField] private string[] words;
        [SerializeField] private Color[] colors;
        
        public string GetRandomWord()
        {
            return words[Random.Range(0, words.Length)];
        }

        public Color GetRandomColor()
        {
            return colors[Random.Range(0, words.Length)];
        }
    }
}