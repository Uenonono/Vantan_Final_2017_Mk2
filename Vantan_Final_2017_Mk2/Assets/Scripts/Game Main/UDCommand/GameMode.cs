namespace UDCommand {
  public enum GameMode {
    Trial,
    Challenge
  }

  public static class SelectedGameMode {
    static int mode = 0;

    public static void SetMode(int val) {
      mode = val;
    }

    public static int GetMode() {
      return mode;
    }
  }
}