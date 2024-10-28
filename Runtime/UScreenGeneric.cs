using UnityEngine;

namespace UScreens
{
    public abstract class UScreenGeneric<TState, TView> : UScreen where TState : UScreen where TView : MonoBehaviour
    {
        protected const string VIEWS_ADDRESS_IN_RESOURCE = "";

        private TView view;

        protected TView View
        {
            get
            {
                TryCreateView();
                return view;
            }
        }

        protected virtual string ViewAddress => VIEWS_ADDRESS_IN_RESOURCE + typeof(TView).Name;

        public sealed override void TryCreateView()
        {
            if (view == null)
                view = InitView();
            Hide();
        }

        private TView InitView()
        {
            view = Instantiate(Resources.Load<GameObject>(ViewAddress), transform).GetComponent<TView>();
            InitializeView();
            EventSystemChecker.TryToFind();
            return view;
        }

        public override void Show()
        {
            IsShowing = true;
            if(View) View.gameObject.SetActive(true);
        }
        public override void Hide()
        {
            IsShowing = false;
            if(View) View.gameObject.SetActive(false);
        }

        public override void ChangeScreen() =>
            RouterBase.ChangeState(this);

        private void OnDestroy() =>
            UScreenRepo.Remove<TState>();
    }
}