using System;

namespace Sample
{
    public class OpenConfirmWindowEventMediator
    {
        public event Action OnOpenConfirmWindow = delegate { };

        public void RaiseOpenConfirmWindowEvent()
        {
            OnOpenConfirmWindow();
        }
    }
}