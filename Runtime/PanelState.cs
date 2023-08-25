using System;
using UnityEngine;
using UnityEngine.UI;
using IEnumerator = System.Collections.IEnumerator;

namespace ScreensState
{
    public class PanelState : MonoBehaviour
    {
        public bool IsShowing => gameObject.activeSelf;


        [SerializeField] private Button hideBtn;

        [NonSerialized] protected IPanelAnim panelAnim;

        [NonSerialized] protected float currentHideDuration = .1f;


        public virtual IPanelAnim GetPanelAnim() => new AnimatorPanelAnim();

        protected virtual void OnValidate() 
        {
#if UNITY_EDITOR
            (panelAnim ??= GetPanelAnim()).Validate(this);
#endif        
        }

        public virtual void Initialize()
        {
            if(hideBtn)
                hideBtn.onClick.AddListener(HideClicked);
            (panelAnim = GetPanelAnim()).Initialize(this);
        }

        public virtual void Show()
        {
            panelAnim.Show();
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            if (!IsShowing)
                return;

            currentHideDuration = panelAnim.Hide();
            StartCoroutine(HideAnimation());
        }

        private IEnumerator HideAnimation()
        {
            yield return new WaitForSecondsRealtime(currentHideDuration);
            HideForce();
        }

        public virtual void HideForce() =>
            gameObject.SetActive(false);

        protected virtual void HideClicked() =>
            Hide();
    }
}