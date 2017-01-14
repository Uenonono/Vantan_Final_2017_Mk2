
namespace ToppingFullCustom {
  public enum InputType {
    Neutral,
    Left,
    Down,
    Right,
    Up,
    URed,
    UGreen,
    UBlue,
    UYellow,
    BRed,
    BGreen,
    BBlue,
    BYellow
  }

  public enum DirectionalInputs {
    Neutral,
    Left,
    Down,
    Right,
    Up
  }


  public enum ActionInputSubType {
    Upper,
    Bottom
  }

  public enum ActionInputs {
    Neutral,
    Red,
    Green,
    Blue,
    Yellow
  }

  public class InputUtility {
    public static InputType DirectionalToInputType(DirectionalInputs dirInput) {
      var dirInt = (int)dirInput;
      return (InputType)dirInt;
    }

    public static InputType ActionToInputType(ActionInputSubType subType ,ActionInputs actInput) {
      var actInt = (int)actInput;
      if (actInput != ActionInputs.Neutral) {
        if(subType == ActionInputSubType.Upper) {
          return (InputType)actInt + 4;
        }
        else if(subType == ActionInputSubType.Bottom) {
          return (InputType)actInt + 8;
        }
      }
      return InputType.Neutral;
    }
  }
}