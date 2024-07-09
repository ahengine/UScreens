using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UScreens
{
    public abstract class UScreen : MonoBehaviour
    {
        public bool IsShowing { protected set; get; }
        public abstract void TryCreateView();
        public abstract UniTask TryCreateViewAsync();
        public abstract void InitializeState();
        public abstract void InitializeView();
        public abstract void Show();
        public abstract void Hide();
    }
}
