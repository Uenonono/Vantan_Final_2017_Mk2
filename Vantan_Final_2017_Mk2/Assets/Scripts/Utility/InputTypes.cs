
namespace ToppingFullCustom {
  public enum InputType {
    Neutral,
    Left,
    Down,
    Right,
    Up,
    Pink,
    Blue,
    Red,
    Green
  }

  public enum DirectionalInputs {
    Neutral,
    Left,
    Down,
    Right,
    Up
  }

  public enum ActionInputs {
    Neutral,
    Square,
    Cross,
    Circle,
    Triangle
  }

  public class InputUtility {
    public static InputType DirectionalToInputType(DirectionalInputs dirInput) {
      var dirInt = (int)dirInput;
      return (InputType)dirInt;
    }

    public static InputType ActionToInputType(ActionInputs actInput) {
      var actInt = (int)actInput;
      if (actInput != ActionInputs.Neutral) {
        return (InputType)actInt + 4;
      }
      return InputType.Neutral;
    }
  }
}