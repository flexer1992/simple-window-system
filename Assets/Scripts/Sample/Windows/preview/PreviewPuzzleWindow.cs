using System;
using System.Threading.Tasks;
using Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.Windows
{
    public class PreviewPuzzleWindow : BaseWindow
    {
        [SerializeField] private Image previewImage;
        [SerializeField] private Button playButton;
        [SerializeField] private Text rewardLabel;

        public event Action OnPlayButtonClicked = delegate { };

        public override Task Construct()
        {
            playButton.onClick.AddListener(PlayButtonClickHandler);
            return base.Construct();
        }

        public override Task Deconstruct()
        {
            playButton.onClick.RemoveListener(PlayButtonClickHandler);
            return base.Deconstruct();
        }

        public void SetPuzzleSprite(Sprite puzzleSprite)
        {
            previewImage.sprite = puzzleSprite;
        }

        public void SetReward(int reward)
        {
            rewardLabel.text = reward.ToString();
        }

        private void PlayButtonClickHandler()
        {
            OnPlayButtonClicked();
        }
    }
}