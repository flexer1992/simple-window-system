using System.Threading.Tasks;
using Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.Windows.confirmation
{
    public class ConfirmationWindowView : BaseWindow
    {
        [SerializeField] private Text rewardLabel;
        [SerializeField] private Button confirmButton;

        public void SetReward(int reward)
        {
            rewardLabel.text = reward.ToString();
        }

        public override Task Construct()
        {
            confirmButton.onClick.AddListener(RaiseCloseWindowEvent);
            return base.Construct();
        }

        public override Task Deconstruct()
        {
            confirmButton.onClick.RemoveListener(RaiseCloseWindowEvent);
            return base.Deconstruct();
        }
    }
}