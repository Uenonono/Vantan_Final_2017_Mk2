using UnityEngine;
using System.Collections.Generic;
using TFCD = TFCDebug;

namespace TFCDebug {
  public class DebugGamepadInputHandler : MonoBehaviour {

    TFCD.DebugGamepadInput gamepadInput;

    private void Start() {
      gamepadInput.actInputs = new List<TFCD.DebugActionInputs>();
      gamepadInput.dirInput = TFCD.DebugDirectionalInputs.Neutral;
    }

    private void GetActionInputs() {
      gamepadInput.actInputs.Clear();
      if (Input.GetAxis("ActPink") == 1.0f) {
        gamepadInput.actInputs.Add(TFCD.DebugActionInputs.ActLeft);
      }
      if (Input.GetAxis("ActBlue") == 1.0f) {
        gamepadInput.actInputs.Add(TFCD.DebugActionInputs.ActDown);
      }
      if (Input.GetAxis("ActRed") == 1.0f) {
        gamepadInput.actInputs.Add(TFCD.DebugActionInputs.ActRight);
      }
      if (Input.GetAxis("ActGreen") == 1.0f) {
        gamepadInput.actInputs.Add(TFCD.DebugActionInputs.ActUp);
      }
    }

    private void GetDirectionInputs() {
      TFCD.DebugDirectionalInputs horizontalInput = TFCD.DebugDirectionalInputs.Neutral;
      TFCD.DebugDirectionalInputs verticalInput = TFCD.DebugDirectionalInputs.Neutral;

      Debug.Log("Horizontal = " + Input.GetAxis("Horizontal").ToString());
      Debug.Log("DirHorizontal = " + Input.GetAxis("DirHorizontal").ToString());

      if (Input.GetAxis("Horizontal") >= 1.0f || Input.GetAxis("DirHorizontal") >= 1.0f) {
        horizontalInput = TFCD.DebugDirectionalInputs.Right;
      }
      else if (Input.GetAxis("Horizontal") <= -1.0f || Input.GetAxis("DirHorizontal") <= -1.0f) {
        horizontalInput = TFCD.DebugDirectionalInputs.Left;
      }
      else {
        horizontalInput = TFCD.DebugDirectionalInputs.Neutral;
      }

      Debug.Log("Vertical = " + Input.GetAxis("Vertical").ToString());
      Debug.Log("DirVertical = " + Input.GetAxis("DirVertical").ToString());

      if (Input.GetAxis("Vertical") >= 1.0f || Input.GetAxis("DirVertical") >= 1.0f) {
        verticalInput = TFCD.DebugDirectionalInputs.Up;
      }
      else if (Input.GetAxis("Vertical") <= -1.0f || Input.GetAxis("DirVertical") <= -1.0f) {
        verticalInput = TFCD.DebugDirectionalInputs.Down;
      }
      else {
        verticalInput = TFCD.DebugDirectionalInputs.Neutral;
      }

      MixDirectionalInputs(horizontalInput, verticalInput);
    }

    private void MixDirectionalInputs(TFCD.DebugDirectionalInputs horizontalInput, TFCD.DebugDirectionalInputs verticalInput) {
      if (horizontalInput == TFCD.DebugDirectionalInputs.Neutral) {
        gamepadInput.dirInput = verticalInput;
      }
      else if (horizontalInput == TFCD.DebugDirectionalInputs.Left) {
        if (verticalInput == TFCD.DebugDirectionalInputs.Neutral) {
          gamepadInput.dirInput = horizontalInput;
        }
        else if (verticalInput == TFCD.DebugDirectionalInputs.Up) {
          gamepadInput.dirInput = TFCD.DebugDirectionalInputs.UpLeft;
        }
        else {
          gamepadInput.dirInput = TFCD.DebugDirectionalInputs.DownLeft;
        }
      }
      else {
        if (verticalInput == TFCD.DebugDirectionalInputs.Neutral) {
          gamepadInput.dirInput = horizontalInput;
        }
        else if (verticalInput == TFCD.DebugDirectionalInputs.Up) {
          gamepadInput.dirInput = TFCD.DebugDirectionalInputs.UpRight;
        }
        else {
          gamepadInput.dirInput = TFCD.DebugDirectionalInputs.DownRight;
        }
      }
    }

    public TFCD.DebugGamepadInput GetInputs() {
      GetActionInputs();
      GetDirectionInputs();
      return gamepadInput;
    }
  }
}
