using UnityEngine;
using System.Collections.Generic;
using TFC = ToppingFullCustom;

namespace ToppingFullCustom {
  public class GamepadInputHandler : MonoBehaviour {

    public static List<InputType> GetInputs(int player = 0) {
      List<InputType> inputs = new List<InputType>();
      GetDirectionalInputs(inputs,player);
      GetActionInputs(inputs,player);

      return inputs;
    }

    private static void GetActionInputs(List<InputType> inputs, int player = 0) {
      ActionInputs actInput = ActionInputs.Neutral;

      string stringMod = "";

      if(player == 1) {
        stringMod = "P1";
      }
      else if(player == 2) {
        stringMod = "P2";
      }

      if (Input.GetAxis(stringMod + "ActPink") >= 0.5f) {
        actInput = ActionInputs.Square;
        inputs.Add(TFC.InputUtility.ActionToInputType(actInput));
      }

      if (Input.GetAxis(stringMod + "ActBlue") >= 0.5f) {
        actInput = ActionInputs.Cross;
        inputs.Add(TFC.InputUtility.ActionToInputType(actInput));
      }

      if (Input.GetAxis(stringMod + "ActRed") >= 0.5f) {
        actInput = ActionInputs.Circle;
        inputs.Add(TFC.InputUtility.ActionToInputType(actInput));
      }

      if (Input.GetAxis(stringMod + "ActGreen") >= 0.5f) {
        actInput = ActionInputs.Triangle;
        inputs.Add(TFC.InputUtility.ActionToInputType(actInput));
      }
    }

    private static void GetDirectionalInputs(List<InputType> inputs, int player = 0) {
      DirectionalInputs horInput;
      DirectionalInputs vertInput;

      string stringMod = "";

      if (player == 1) {
        stringMod = "P1";
      }
      else if (player == 2) {
        stringMod = "P2";
      }

      if ((Input.GetAxis(stringMod + "Horizontal") >= 0.5f) || (Input.GetAxis(stringMod + "DirHorizontal") >= 0.5f)) {
        horInput = DirectionalInputs.Right;
      }
      else if ((Input.GetAxis(stringMod + "Horizontal") <= -0.5f) || (Input.GetAxis(stringMod + "DirHorizontal") <= -0.5f)) {
        horInput = DirectionalInputs.Left;
      }
      else {
        horInput = DirectionalInputs.Neutral;
      }

      if ((Input.GetAxis(stringMod + "Vertical") >= 0.5f) || (Input.GetAxis(stringMod + "DirVertical") >= 0.5f)) {
        vertInput = DirectionalInputs.Up;
      }
      else if ((Input.GetAxis(stringMod + "Vertical") <= -0.5f) || (Input.GetAxis(stringMod + "DirVertical") <= -0.5f)) {
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