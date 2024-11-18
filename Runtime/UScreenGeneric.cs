using UnityEngine;

namespace UScreens
{
    public abstract class UScreenGeneric<TState, TView> : UScreen 
    where TState : UScreen 
    where TView : MonoBehaviour
    {
        protected const string VIEWS_ADDRESS_IN_RESOURCE = "";
        private TView view;
        protected TView View => view ??= InitView();

        protected virtual string ViewAddress => VIEWS_ADDRESS_IN_RESOURCE + typeof(TView).Name;

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

        private void OnDestroy() =>
            DestoryState();

        public override void ChangeScreen() =>
            RouterBase.ChangeState(this);

        protected override void DestoryState() =>
            UScreenRepo.Remove<TState>();

        private bool isInitialized;
        internal override void Initialize() 
        {
            #if UNITY_EDITOR
            if(isInitialized)
                Debug.LogError("Don't Call it Initialize Manually, This Function Auto Called When Created This GameObject");
            #endif
            isInitialized = true;
            InitializeState();
        }
    }
}