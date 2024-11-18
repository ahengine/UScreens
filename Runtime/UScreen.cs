using UnityEngine;

namespace UScreens
{
    public abstract class UScreen : MonoBehaviour
    {
        public bool IsShowing { protected set; get; }
        protected abstract void InitializeState();
        protected abstract void InitializeView();
        protected abstract void DestoryState();
        /// <summary>
        /// Don't Call it, This is Constructor of Screen
        /// </summary>
        internal abstract void Initialize();
        public abstract void Show();
        public abstract void Hide();

        public abstract void ChangeScreen();
    }
}
