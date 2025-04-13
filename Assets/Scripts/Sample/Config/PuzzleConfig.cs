using UnityEngine;

namespace Sample.Config
{
    [CreateAssetMenu(fileName = "PuzzleConfig", menuName = "PuzzleConfig", order = 0)]
    public class PuzzleConfig : ScriptableObject
    {
        public Sprite puzzleSprite;
        public int reward;
    }
}