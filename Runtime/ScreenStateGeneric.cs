using UnityEngine;

namespace ScreensState
{
    public abstract class ScreenState<TState,TView> : ScreenState where TState : ScreenState where TView : MonoBehaviour
    {
        protected const string VIEWS_ADDRESS_IN_RESOURCE = "";

        private void OnDestroy() => ClearScreen<TState>();

        private TView view;
        protected TView View => view ?? InitView();

        private TView InitView()
        {
            view = Instantiate(Resources.Load<GameObject>(ViewAddress), transform).GetComponent<TView>();
            InitializeView();
            return view;
        }

        protected virtual string ViewAddress => VIEWS_ADDRESS_IN_RESOURCE + typeof(TView).Name;

        public override void Show()
        {
            IsShowing = true;
            View.gameObject.SetActive(true);
        }
        public override void Hide()
        {
            IsShowing = false;
            View.gameObject.SetActive(false);
        }

        public void ChangePage() =>
            RouterBase.ChangeState(this);
    }
}