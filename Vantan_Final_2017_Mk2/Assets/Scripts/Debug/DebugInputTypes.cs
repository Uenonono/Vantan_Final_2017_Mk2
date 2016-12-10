using System.Collections.Generic;

namespace TFCDebug {
  public struct DebugGamepadInput {
    private DebugDirectionalInputs _dirInput;

    public DebugDirectionalInputs dirInput {
      get { return _dirInput; }
      set { _dirInput = value; }
    }

    private List<DebugActionInputs> _actInputs;

    public List<DebugActionInputs> actInputs {
      get { return _actInputs; }
      set { _actInputs = value; }
    }
  }

  public enum DebugDirectionalInputs {
    Neutral,
    Up,
    UpLeft,
    Left,
    DownLeft,
    Down,
    DownRight,
    Right,
    UpRight
  }

  public enum DebugActionInputs {
    ActUp,
    ActLeft,
    ActDown,
    ActRight
  }
}