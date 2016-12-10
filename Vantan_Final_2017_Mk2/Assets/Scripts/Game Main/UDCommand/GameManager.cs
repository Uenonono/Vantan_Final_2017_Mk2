using UnityEngine;
using Unity = UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UDC = UDCommand;
using TFC = ToppingFullCustom;

namespace UDCommand {
  public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject commandPrefab;

    [SerializeField]
    private Text timeText = null;

    private List<UDC.ImageType> images;
    private List<GameObject> commandObjects;
    private List<TFC.InputType> inputList;

    [SerializeField]
    private UDC.Dificulty dificulty = UDC.Dificulty.Easy;

    private int previousFrameCommand = -1;
    private int currentCommand = 0;

    private bool resetedToNeutral;

    private int waitTime;
    private float currentWaitTime;
    private UDC.GameSettings gameSettings;
    private int correctCommands;
    private bool commandAdded = false;

    void Start() {
      images = new List<UDC.ImageType>();
      commandObjects = new List<GameObject>();
      inputList = new List<TFC.InputType>();
      gameSettings = new UDC.GameSettings();
      InitCommands();
      SetGameSettings();
    }

    private void InitCommands() {
      for (var i = 0; i < DificultyToQtt(); i++) {
        var obj = Instantiate(commandPrefab);
        obj.transform.SetParent(transform, false);
        obj.name = "Command" + (i + 1).ToString();
        obj.GetComponent<PositionByQuantity>().index = i;
        commandObjects.Add(obj);
      }

      foreach (var obj in commandObjects) {
        obj.GetComponent<PositionByQuantity>().SetTotalChildrenFromParent();
      }
    }

    private void SetGameSettings() {
      if (!gameSettings.LoadFromJson()) {
        gameSettings.SerializeDefaultData();
        gameSettings.LoadFromJson();
      }
      waitTime = gameSettings.GetGameTime();
      currentWaitTime = waitTime;
    }

    void Update() {
      UpdateDisplayTime();
      if (currentCommand == 0 && (currentCommand != previousFrameCommand)) {
        GenerateRandomCommands();
      }
      UpdateCommandIcons(currentCommand);
      previousFrameCommand = currentCommand;
      CheckGamepadInput();
      ResetCurrentCommand();
      currentWaitTime -= Time.deltaTime;
    }

    private void UpdateDisplayTime() {
      timeText.text = "Time : " + currentWaitTime.ToString("F2");
    }

    private void GenerateRandomCommands() {
      images.Clear();
      inputList.Clear();
      for (var i = 0; i < commandObjects.Count; i++) {
        images.Add((UDC.ImageType)Unity.Random.Range(0, 8));
      }

      for (var i = 0; i < images.Count; i++) {
        commandObjects[i].GetComponent<Image>().sprite = ImageTypeToSprite(images[i]);
      }

      foreach (var img in images) {
        inputList.Add(ImageTypeToInputType(img));
      }
    }

    private int DificultyToQtt() {
      int res = 3;
      switch (dificulty) {
        case UDC.Dificulty.Easy:
          res = 3;
          break;
        case UDC.Dificulty.Medium:
          res = 4;
          break;
        case UDC.Dificulty.Hard:
          res = 5;
          break;
      }

      return res;
    }

    private void CheckGamepadInput(int player = 0) {
      var inputs = TFC.GamepadInputHandler.GetInputs(player);
      if (resetedToNeutral) {
        if (inputs.Count != 0) {
          if (inputs.Contains(inputList[currentCommand])) {
            resetedToNeutral = false;
            currentCommand++;
          }
          else {
            currentCommand = 0;
            GenerateRandomCommands();
            resetedToNeutral = false;
          }
        }
      }
      else {
        if (inputs.Count == 0) {
          resetedToNeutral = true;
        }
      }
    }

    private void ResetCurrentCommand() {
      if (currentCommand >= commandObjects.Count) {
        currentCommand = 0;
        correctCommands++;
        commandAdded = false;
        currentWaitTime = waitTime;
      }

      if(correctCommands >= 5) {
        if(correctCommands % 5 == 0) {
          if(commandObjects.Count < 10 && !commandAdded) {
            AddCommand();
            commandAdded = true;
          }
        }
      }
    }

    private void AddCommand() {
      var obj = Instantiate(commandPrefab);
      obj.transform.SetParent(transform, false);
      obj.transform.localPosition = new Vector3(0, 1000.0f);
      obj.name = "Command" + (commandObjects.Count + 1).ToString();
      obj.GetComponent<PositionByQuantity>().index = commandObjects.Count;
      commandObjects.Add(obj);
      foreach (var comm in commandObjects) {
        comm.GetComponent<PositionByQuantity>().SetTotalChildrenFromParent();
      }
    }

    private void UpdateCommandIcons(int currentCommand) {
      if (previousFrameCommand == currentCommand) return;
      for (int i = 0; i < commandObjects.Count; i++) {
        if (i == currentCommand) {
          commandObjects[i].GetComponent<UDC.RescaleByState>().currentState = UDC.CommandState.Highlighted;
        }
        else {
          commandObjects[i].GetComponent<UDC.RescaleByState>().currentState = UDC.CommandState.Neutral;
        }
      }
    }

    private Sprite ImageTypeToSprite(UDC.ImageType imageType) {
      string path = "";
      switch (imageType) {
        case UDC.ImageType.Left:
          path = "Sprites/UDCommand/leftArrow";
          break;
        case UDC.ImageType.Down:
          path = "Sprites/UDCommand/downArrow";
          break;
        case UDC.ImageType.Right:
          path = "Sprites/UDCommand/rightArrow";
          break;
        case UDC.ImageType.Up:
          path = "Sprites/UDCommand/upArrow";
          break;

        case UDC.ImageType.Square:
          path = "Sprites/UDCommand/square";
          break;
        case UDC.ImageType.Cross:
          path = "Sprites/UDCommand/cross";
          break;
        case UDC.ImageType.Circle:
          path = "Sprites/UDCommand/circle";
          break;
        case UDC.ImageType.Triangle:
          path = "Sprites/UDCommand/triangle";
          break;
      }

      return Resources.Load<Sprite>(path);
    }

    private TFC.InputType ImageTypeToInputType(UDC.ImageType imageType) {
      return (TFC.InputType)imageType + 1;
    }
  }
}