using Windows.Common;
using Sample.Config;
using UnityEngine;

namespace Sample.Windows
{
    public class PreviewPuzzleModel : BaseWindowModel
    {
        private readonly PuzzleConfig _puzzleConfig;

        public Sprite PuzzleSprite => _puzzleConfig.puzzleSprite;
        public int Reward => _puzzleConfig.reward;

        public PreviewPuzzleModel()
        {
        }

        public PreviewPuzzleModel(PuzzleConfig puzzleConfig)
        {
            _puzzleConfig = puzzleConfig;
        }
    }
}