using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScreensState
{
    public abstract class ScreenState : MonoBehaviour
    {
        #region Screen Singleton Handler
        private static Dictionary<Type, ScreenState> Screens = new Dictionary<Type, ScreenState>();
        private static void AddScreen<T>(T screen) where T : ScreenState => 
            Screens.Add(typeof(T),screen);
        public static T GetScreen<T>() where T : ScreenState
        {
            if (!Screens.ContainsKey(typeof(T)))
                CreateState<T>();

            return Screens[typeof(T)] as T;
        }
        protected static void ClearScreen<T>() where T : ScreenState =>
            Screens.Remove(typeof(T));
        public static void CreateState<T>() where T : ScreenState
        {
            var instance = new GameObject(typeof(T).Name).AddComponent<T>();
            instance.InitializeState();
            AddScreen(instance);
        }
        #endregion

        #region States
        public bool IsShowing { protected set; get; }
        public abstract void InitializeState();
        public abstract void InitializeView();
        public abstract void Show();
        public abstract void Hide();
        #endregion
    }
}
