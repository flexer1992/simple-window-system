using Windows.Common;

namespace Sample.Windows.confirmation
{
    public class ConfirmationWindowModel : BaseWindowModel
    {
        public int Reward { get; }

        public ConfirmationWindowModel(int reward)
        {
            Reward = reward;
        }
    }
}