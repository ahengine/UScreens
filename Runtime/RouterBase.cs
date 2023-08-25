namespace ScreensState
{
    public static class RouterBase
    {
        public static ScreenState Current { private set; get; } = null;

        public static void ChangeState(ScreenState newState)
        {
            if(Current == newState)
                return;

            if(Current != null) Current.Hide();
            Current = newState;
            if (Current != null) Current.Show();
        }
    }
}
