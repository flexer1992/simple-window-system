using Windows.Implementation;
using Windows.Infrastructure;
using Sample;
using Sample.Config;
using Sample.Implementation;
using Sample.Windows;
using Sample.Windows.confirmation;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private WindowsContainer windowsContainer;
    [SerializeField] private PuzzleConfig puzzleConfig;

    private IWindowService _windowService;
    private IPresentersFactory _presentersFactory;
    private IWindowFactory _windowFactory;
    private IModelsFactory _modelsFactory;
    private readonly OpenConfirmWindowEventMediator _openConfirmWindowEventMediator = new();


    private void Awake()
    {
        var windowCloseMediator = new WindowCloseMediator();
        _modelsFactory = new ModelsFactory(puzzleConfig);
        _windowFactory = new WindowsFactory(new WindowPathProvider());
        _presentersFactory = new PresentersFactory(windowCloseMediator, _windowFactory, _modelsFactory,
            _openConfirmWindowEventMediator);
        _windowService = new WindowService(windowCloseMediator, _presentersFactory, windowsContainer);

        _openConfirmWindowEventMediator.OnOpenConfirmWindow += OnOpenConfirmWindowEventHandler;
    }

    void Start()
    {
        _windowService.Show<PreviewPuzzleWindow, PreviewPuzzleModel, PreviewPuzzlePresenter>();
    }

    private void OnOpenConfirmWindowEventHandler()
    {
        _windowService.Show<ConfirmationWindowView, ConfirmationWindowModel, ConfirmationWindowPresenter>(
            new ConfirmationWindowModel(puzzleConfig.reward));
    }

    private void OnDestroy()
    {
        _openConfirmWindowEventMediator.OnOpenConfirmWindow -= OnOpenConfirmWindowEventHandler;
        _windowService.Dispose();
    }
}