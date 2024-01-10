using System;
using UnityEngine;
using UnityEngine.UI;

namespace UScreens
{
    public class UPanel : MonoBehaviour
    {
        public bool IsShowing => gameObject.activeSelf;

        [SerializeField] private Button hideBtn;

        public event Action OnShow;
        public event Action OnHide;

        public virtual void Initialize()
        {
            if (hideBtn)
                hideBtn.onClick.AddListener(Hide);
        }

        public virtual void Show()
        {
            if (IsShowing)
                return;

            OnShow?.Invoke();
        }

        public virtual void Hide()
        {
            if (!IsShowing)
                return;

            OnHide?.Invoke();
        }
    }
}