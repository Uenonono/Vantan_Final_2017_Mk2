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
    private GameObject firstCommand = null;
    [SerializeField]
    private GameObject secondCommand = null;
    [SerializeField]
    private GameObject thirdCommand = null;
    [SerializeField]
    private GameObject fourthCommand = null;
    [SerializeField]
    private GameObject fifthCommand = null;

    [SerializeField]
    private Text timeText = null;

    private List<UDC.ImageType> images;
    private List<GameObject> commandObjects;
    private List<TFC.InputType> inputList;

    [SerializeField]
    private UDC.Dificulty dificulty = UDC.Dificulty.Easy;

    private UDC.GameMode mode = UDC.GameMode.VSCPU;
    private UDC.PlayerType currentPlayer = UDC.PlayerType.Player1;

    private int previousFrameCommand = -1;
    private int currentCommand = 0;

    private bool resetedToNeutral;

    private int waitTime;
    private float currentWaitTime;
    private float decreaseTime;
    private UDC.GameSettings gameSettings;
    private int[] correctCommands;

    private float CPUTime;//for debug

    void Start() {
      images = new List<UDC.ImageType>();
      commandObjects = new List<GameObject>();
      inputList = new List<TFC.InputType>();
      gameSettings = new UDC.GameSettings();
      correctCommands = new int[2];
      AddCommandsToList();
      SetGameSettings();
    }

    private void AddCommandsToList() {
      commandObjects.Add(firstCommand);
      commandObjects.Add(secondCommand);
      commandObjects.Add(thirdCommand);
      commandObjects.Add(fourthCommand);
      commandObjects.Add(fifthCommand);
    }

    private void SetGameSettings() {
      if (!gameSettings.LoadFromJson()) {
        gameSettings.SerializeDefaultData();
        gameSettings.LoadFromJson();
      }
      waitTime = gameSettings.GetGameTime();
      currentWaitTime = waitTime;
      decreaseTime = gameSettings.GetDecreaseByDificulty(dificulty);
    }

    void Update() {
      UpdateDisplayTime();
      if (currentCommand == 0 && (currentCommand != previousFrameCommand)) {
        GenerateRandomCommands();
      }
      UpdateCommandIcons(currentCommand);
      previousFrameCommand = currentCommand;
      if (currentPlayer != UDC.PlayerType.CPU) {
        if (mode == UDC.GameMode.VSCPU) {
          CheckGamepadInput();
        }
        else if(mode == UDC.GameMode.VSPlayer) {
          CheckGamepadInput(PlayerTypeToPlayerNumber(currentPlayer));
        }
      }
      else {
        CPUAdvanceCommand();
      }
      ResetCurrentCommand();
      currentWaitTime -= Time.deltaTime;
    }

    private int PlayerTypeToPlayerNumber(UDC.PlayerType player) {
      if(player == UDC.PlayerType.Player1) {
        return 1;
      }
      else if (player == UDC.PlayerType.Player2){
        return 2;
      }
      else {
        return 0;
      }
    }

    private void SwitchPlayers() {
      switch (mode) {
        case UDC.GameMode.VSCPU:
          if (currentPlayer == UDC.PlayerType.Player1) {
            currentPlayer = UDC.PlayerType.CPU;
          }
          else {
            currentPlayer = UDC.PlayerType.Player1;
          }
          break;
        case UDC.GameMode.VSPlayer:
          if (currentPlayer == UDC.PlayerType.Player1) {
            currentPlayer = UDC.PlayerType.Player2;
          }
          else {
            currentPlayer = UDC.PlayerType.Player1;
          }
          break;
      }
    }

    private void UpdateDisplayTime() {
      timeText.text = "Time : " + currentWaitTime.ToString("F2");
    }

    private void GenerateRandomCommands() {
      SetSpritesByDificulty();
      images.Clear();
      inputList.Clear();
      for (var i = 0; i < DificultyToQtt(); i++) {
        images.Add((UDC.ImageType)Unity.Random.Range(0, 8));
      }

      for (var i = 0; i < images.Count; i++) {
        commandObjects[i].GetComponent<Image>().sprite = ImageTypeToSprite(images[i]);
      }

      foreach (var img in images) {
        inputList.Add(ImageTypeToInputType(img));
      }
    }

    private void SetSpritesByDificulty() {
      foreach (var obj in commandObjects) {
        obj.SetActive(false);
      }

      for (int i = 0; i < DificultyToQtt(); i++) {
        commandObjects[i].SetActive(true);
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

    private void CPUAdvanceCommand() {
      CPUTime += Time.deltaTime;
      if (CPUTime > 0.5f) {
        currentCommand++;
        CPUTime = 0;
      }
      ResetCurrentCommand();
    }

    private void ResetCurrentCommand() {
      if (currentCommand >= DificultyToQtt()) {
        currentCommand = 0;
        correctCommands[PlayerTypeToScoreIndex(currentPlayer)]++;
        SwitchPlayers();
        currentWaitTime = waitTime - ((float)(decreaseTime / 1000.0f) * correctCommands[PlayerTypeToScoreIndex(currentPlayer)]);
      }
    }

    private int PlayerTypeToScoreIndex(UDC.PlayerType playerType) {
      if(playerType == UDC.PlayerType.Player1) {
        return 0;
      }
      else {
        return 1;
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