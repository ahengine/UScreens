using UnityEngine;

namespace UScreens
{
    public abstract class UScreenDIGeneric<TState, TView, TConfig, TInfo> : UScreenGeneric<TState,TView>
    where TState : UScreen 
    where TView : MonoBehaviour
    where TConfig : class
    where TInfo : ScriptableObject
    {
        protected TConfig Config {private set; get;}
        public void SetConfig(TConfig config) =>
            Config = config;

        private TInfo info;
        protected TInfo Info => 
            info ??= Resources.Load<TInfo>(typeof(TInfo).Name);
    }
}