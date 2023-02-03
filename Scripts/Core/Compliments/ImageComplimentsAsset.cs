using UnityEngine;
using UnityEngine.UI;

namespace Core.Compliments
{
    [CreateAssetMenu(menuName = "Configs/Compliments/ImageCompliments")]
    public class ImageComplimentsAsset : ScriptableObject
    {
        [SerializeField] private Sprite[] words;

        public Sprite GetRandomWord()
        {
            return words[Random.Range(0, words.Length)];
        }
    }
}