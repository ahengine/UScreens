using UnityEngine;
using UnityEngine.UI;
using IEnumerator = System.Collections.IEnumerator;

namespace ScreensState
{
    public class PanelState : MonoBehaviour
    {
        public bool IsShowing => gameObject.activeSelf;

        protected const string SHOW_STATE = "Show";
        protected const string HIDE_STATE = "Hide";
        [SerializeField] protected Animator animator;
        [SerializeField] protected float hideDuration = .1f;

        [SerializeField] private Button hideBtn;

        public virtual void Initialize()
        {
            if(hideBtn)
                hideBtn.onClick.AddListener(HideClicked);

            animator = GetComponent<Animator>();
        }

        public virtual void Show()
        {
            animator.Play(SHOW_STATE);
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            if (!IsShowing)
                return;

            animator.Play(HIDE_STATE);
            StartCoroutine(HideAnimation());
        }

        private IEnumerator HideAnimation()
        {
            yield return new WaitForSecondsRealtime(hideDuration);
            HideForce();
        }

        public virtual void HideForce() =>
            gameObject.SetActive(false);

        protected virtual void HideClicked() =>
            Hide();
    }
}