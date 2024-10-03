using UnityEngine;

namespace UScreens
{
    public class ScreenChanger : MonoBehaviour
    {
        [SerializeField] private UScreen nextScreen;

        public void ChangeScreen() =>
            nextScreen.ChangeScreen();
    }
}