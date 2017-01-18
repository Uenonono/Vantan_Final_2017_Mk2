using UnityEngine;
using System.Collections.Generic;
using TFC = ToppingFullCustom;
using System;

namespace ToppingFullCustom {
  public class GamepadInputHandler : MonoBehaviour {

    public static List<InputType> GetInputs() {
      List<InputType> inputs = new List<InputType>();
      GetDirectionalInputs(inputs);
      GetActionInputs(inputs);
      return inputs;
    }

    public static List<InputType> GetActionsOnly() {
      List<InputType> inputs = new List<InputType>();
      GetActionInputs(inputs);
      return inputs;
    }

    public static List<ActionInputs> GetColorInputs() {
      List<ActionInputs> resList = new List<ActionInputs>();

      if ((Input.GetAxis("UpperRed") >= 0.5f) || (Input.GetAxis("BottomRed") >= 0.5f)) {
        resList.Add(ActionInputs.Red);
      }

      if ((Input.GetAxis("UpperGreen") >= 0.5f) || (Input.GetAxis("BottomGreen") >= 0.5f)) {
        resList.Add(ActionInputs.Green);
      }

      if ((Input.GetAxis("UpperBlue") >= 0.5f) || (Input.GetAxis("BottomBlue") >= 0.5f)) {
        resList.Add(ActionInputs.Blue);
      }

      if ((Input.GetAxis("UpperYellow") >= 0.5f) || (Input.GetAxis("BottomYellow") >= 0.5f)) {
        resList.Add(ActionInputs.Yellow);
      }

      return resList;
    }
    
    private static void GetActionInputs(List<InputType> inputs) {
      ActionInputs actInput = ActionInputs.Neutral;

      if (Input.GetAxis("UpperRed") >= 0.5f) {
        actInput = ActionInputs.Red;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Upper, actInput));
      }

      if(Input.GetAxis("BottomRed") >= 0.5f) {
        actInput = ActionInputs.Red;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Bottom, actInput));
      }

      if (Input.GetAxis("UpperGreen") >= 0.5f) {
        actInput = ActionInputs.Green;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Upper, actInput));
      }

      if (Input.GetAxis("BottomGreen") >= 0.5f) {
        actInput = ActionInputs.Green;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Bottom, actInput));
      }

      if (Input.GetAxis("UpperBlue") >= 0.5f) {
        actInput = ActionInputs.Blue;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Upper, actInput));
      }

      if (Input.GetAxis("BottomBlue") >= 0.5f) {
        actInput = ActionInputs.Blue;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Bottom, actInput));
      }

      if (Input.GetAxis("UpperYellow") >= 0.5f) {
        actInput = ActionInputs.Yellow;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Upper, actInput));
      }

      if (Input.GetAxis("BottomYellow") >= 0.5f) {
        actInput = ActionInputs.Yellow;
        inputs.Add(TFC.InputUtility.ActionToInputType(ActionInputSubType.Bottom, actInput));
      }
    }

    private static void GetDirectionalInputs(List<InputType> inputs) {
      DirectionalInputs horInput;
      DirectionalInputs vertInput;

      if (Input.GetAxis("Horizontal") >= 0.5f) {
        horInput = DirectionalInputs.Right;
      }
      else if (Input.GetAxis("Horizontal") <= -0.5f) {
        horInput = DirectionalInputs.Left;
      }
      else {
        horInput = DirectionalInputs.Neutral;
      }

      if (Input.GetAxis("Vertical") >= 0.5f) {
        vertInput = DirectionalInputs.Up;
      }
      else if (Input.GetAxis("Vertical") <= -0.5f) {
        vertInput = DirectionalInputs.Down;
      }
      else {
        vertInput = DirectionalInputs.Neutral;
      }

      if (horInput != DirectionalInputs.Neutral) {
        inputs.Add(TFC.InputUtility.DirectionalToInputType(horInput));
      }

      if (vertInput != DirectionalInputs.Neutral) {
        inputs.Add(TFC.InputUtility.DirectionalToInputType(vertInput));
      }
    }
  }
}