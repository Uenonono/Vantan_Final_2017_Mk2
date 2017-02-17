namespace STG
{
    public enum GameModeSTG
    {
        Trial,
    }

    public static class SelectedGameModeSTG
    {
        static int mode = 0;

        public static void SetMode(int val)
        {
            mode = val;
        }

        public static int GetMode()
        {
            return mode;
        }
    }
}