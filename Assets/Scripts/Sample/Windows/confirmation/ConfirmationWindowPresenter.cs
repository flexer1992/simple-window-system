using System.Threading.Tasks;
using Windows.Common;
using Windows.Infrastructure;

namespace Sample.Windows.confirmation
{
    public class ConfirmationWindowPresenter : BasePresenter<ConfirmationWindowModel, ConfirmationWindowView>
    {
        public ConfirmationWindowPresenter(ConfirmationWindowModel model, ConfirmationWindowView windowInstance,
            IWindowCloseMediator windowCloseMediator) : base(model, windowInstance, windowCloseMediator)
        {
        }

        public override Task<IPresenter> ConstructWindow()
        {
            WindowInstance.CloseButtonClick += OnCloseButtonListener;
            WindowInstance.SetReward(Model.Reward);
            return base.ConstructWindow();
        }

        private void OnCloseButtonListener()
        {
            WindowCloseMediator.RequestCloseTopWindow();
        }

        public override Task DeconstructWindow()
        {
            WindowInstance.CloseButtonClick -= OnCloseButtonListener;
            return base.DeconstructWindow();
        }
    }
}