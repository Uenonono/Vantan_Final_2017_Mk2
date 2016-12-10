using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TFCD = TFCDebug;

namespace TFCDebug {
  public class InputTypeDisplay : MonoBehaviour {

    [SerializeField]
    private Text inputDisplay = null;
    [SerializeField]
    private Text inputLog = null;

    private TFCD.DebugGamepadInputHandler gamePadInput;

    string currentInput;
    string previousInput;
    List<string> inputLogList;

    private void Start() {
      inputDisplay.text = "";
      inputLog.text = "";
      gamePadInput = GetComponent<TFCD.DebugGamepadInputHandler>();
      inputLogList = new List<string>();
    }

    private void Update() {
      UpdateDisplayText();
      UpdateInputLog();
    }

    private void UpdateDisplayText() {
      currentInput = "";
      currentInput += gamePadInput.GetInputs().dirInput.ToString();
      if (gamePadInput.GetInputs().actInputs.Count != 0) {
        foreach (var input in gamePadInput.GetInputs().actInputs) {
          currentInput += input.ToString();
        }
      }
      inputDisplay.text = currentInput;
    }

    private void UpdateInputLog() {
      if (previousInput != currentInput) {
        inputLogList.Add(currentInput);
        previousInput = currentInput;
        while (inputLogList.Count >= 20) {
          inputLogList.RemoveAt(0);
        }
      }

      string outString = "";

      foreach (string str in inputLogList) {
        outString += str + ",";
      }

      inputLog.text = outString;
    }

  }
}