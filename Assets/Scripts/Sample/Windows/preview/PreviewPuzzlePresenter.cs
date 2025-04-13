using System.Threading.Tasks;
using Windows.Common;
using Windows.Infrastructure;

namespace Sample.Windows
{
    public class PreviewPuzzlePresenter : BasePresenter<PreviewPuzzleModel, PreviewPuzzleWindow>
    {
        private readonly OpenConfirmWindowEventMediator _openConfirmWindowEventMediator;

        public PreviewPuzzlePresenter(PreviewPuzzleModel model, PreviewPuzzleWindow windowInstance,
            IWindowCloseMediator windowCloseMediator,
            OpenConfirmWindowEventMediator openConfirmWindowEventMediator) : base(model, windowInstance,
            windowCloseMediator)
        {
            _openConfirmWindowEventMediator = openConfirmWindowEventMediator;
        }

        public override Task<IPresenter> ConstructWindow()
        {
            SetWindowData();
            WindowInstance.OnPlayButtonClicked += OnPlayButtonClicked;
            return base.ConstructWindow();
        }

        private void SetWindowData()
        {
            WindowInstance.SetPuzzleSprite(Model.PuzzleSprite);
            WindowInstance.SetReward(Model.Reward);
        }

        private void OnPlayButtonClicked()
        {
            _openConfirmWindowEventMediator.RaiseOpenConfirmWindowEvent();
        }

        public override Task DeconstructWindow()
        {
            WindowInstance.OnPlayButtonClicked -= OnPlayButtonClicked;
            return base.DeconstructWindow();
        }
    }
}