using Cysharp.Threading.Tasks;
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
        }

        public sealed override async UniTask TryCreateViewAsync()
        {
            if (view == null)
                view = await InitViewAsync();
        }

        private TView InitView()
        {
            view = Instantiate(Resources.Load<GameObject>(ViewAddress), transform).GetComponent<TView>();
            InitializeView();

            return view;
        }

        private async UniTask<TView> InitViewAsync()
        {
            view = await UniLoad.LoadAndInstantiate<TView>(ViewAddress);

            view.transform.SetParent(transform);
            await UniTask.Yield();

            // Activate gameObject after reparenting
            if (!view.gameObject.activeInHierarchy)
            {
                view.gameObject.SetActive(true);
                await UniTask.Yield();
            }

            InitializeView();
            await UniTask.Yield();

            return view;
        }

        protected virtual void Awake()
        {
            EventSystemChecker.TryToFind();
        }

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

        private void OnDestroy() =>
            UScreenRepo.Remove<TState>();
    }
}