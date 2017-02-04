using UnityEngine;
using Unity = UnityEngine;
using UnityEngine.SceneManagement;
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

    private int score;
    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private GameObject standbyCanvas = null;
    private bool standbyCanvasLastState;

    [SerializeField]
    private GameObject resultCanvas = null;
    private bool resultCanvasLastState;

    private UDC.GameMode gameMode;

    private List<UDC.ImageType> images;
    private List<GameObject> commandObjects;
    private List<TFC.InputType> inputList;
    private List<TFC.ActionInputs> trialInputList;

    private int previousFrameCommand = -1;
    private int currentCommand = 0;

    private bool resetedToNeutral;

    public float waitTimeDebug = 60.0f;

    private float waitTime;

    private int correctCommands;
    private bool commandAdded = false;

    private bool init = false;
    private bool onStandby = true;
    private bool gameEnded = false;

    [SerializeField]
    GameObject pauseCanvas = null;
    private bool isPaused = false;
    private bool isPauseReleased = true;

    void Start() {
      images = new List<UDC.ImageType>();
      commandObjects = new List<GameObject>();
      inputList = new List<TFC.InputType>();
      trialInputList = new List<TFC.ActionInputs>();
      SoundMgr.SoundLoadSe("UDCCorrectCommand", "UDCommand/CorrectCommand");
      SoundMgr.SoundLoadSe("UDCCorrectList", "UDCommand/CorrectList");
      SoundMgr.SoundLoadSe("UDCMissCommand", "UDCommand/MissCommand");
    }

    private void Init() {
      transform.DetachChildren();
      foreach(var command in commandObjects) {
        Destroy(command);
      }
      commandObjects.Clear();
      previousFrameCommand = -1;
      currentCommand = 0;
      score = 0;
      waitTime = waitTimeDebug;
      correctCommands = 0;
      onStandby = true;
      gameEnded = false;
      standbyCanvas.SetActive(true);
      resultCanvas.SetActive(false);
      UpdateDisplayTime();
      UpdateScoreText();
      gameMode = (UDC.GameMode)UDC.SelectedGameMode.GetMode();
      InitCommands();
      init = true;
    }

    private void InitCommands() {
      for (var i = 0; i < 3; i++) {
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

    public void Reset() {
      init = false;
    }

    public void StartGame() {
      onStandby = false;
      resetedToNeutral = false;
    }

    void Update() {
      if (!init) {
        Init();
      }
      if (!isPaused) {
        UpdateDisplayTime();
        UpdateScoreText();
        if (currentCommand == 0 && (currentCommand != previousFrameCommand)) {
          GenerateRandomCommands();
        }
        UpdateCommandIcons(currentCommand);
        previousFrameCommand = currentCommand;
        if(Input.GetAxis("Pause") == 0) {
          isPauseReleased = true;
        }
        if ((Input.GetAxis("Pause") == 1) && isPauseReleased) {
          isPaused = true;
          standbyCanvasLastState = standbyCanvas.activeSelf;
          resultCanvasLastState = resultCanvas.activeSelf;
          standbyCanvas.GetComponent<MSMM.MenuSelector>().Reset();
          resultCanvas.GetComponent<MSMM.MenuSelector>().Reset();
          standbyCanvas.SetActive(false);
          resultCanvas.SetActive(false);
          pauseCanvas.SetActive(true);
          pauseCanvas.GetComponent<PauseMenu>().SetButtonState();
        }
        if (!onStandby && !gameEnded) {
        CheckGamepadInput();
        ResetCurrentCommand();
          waitTime -= Time.deltaTime;
        }
      }
      if (isPaused) {
        if(!pauseCanvas.activeSelf) {
          isPaused = false;
          isPauseReleased = false;
          standbyCanvas.SetActive(standbyCanvasLastState);
          resultCanvas.SetActive(resultCanvasLastState);
        }
      }

      if(waitTime <= 0) {
        gameEnded = true;
        waitTime = 0;
        if (!isPaused) {
          resultCanvas.SetActive(true);
        }
      }
    }

    private void UpdateDisplayTime() {
      timeText.text = "残り時間　： " + waitTime.ToString("F2");
    }

    private void UpdateScoreText() {
      scoreText.text = score.ToString() + "　どっこい";
    }

    private void GenerateRandomCommands() {
      images.Clear();
      inputList.Clear();
      trialInputList.Clear();
      for (var i = 0; i < commandObjects.Count; i++) {
        if (gameMode == UDC.GameMode.Trial) {
          images.Add((UDC.ImageType)Unity.Random.Range(0, 4));
        }
        else if(gameMode == UDC.GameMode.Challenge) {
          images.Add((UDC.ImageType)Unity.Random.Range(4, 12));
        }
      }

      for (var i = 0; i < images.Count; i++) {
        commandObjects[i].GetComponent<Image>().sprite = ImageTypeToSprite(images[i]);
      }

      if (gameMode == UDC.GameMode.Trial) {
        foreach (var img in images) {
          trialInputList.Add(ImageTypeToActionInput(img));
        }   
      }
      else if (gameMode == UDC.GameMode.Challenge) {
        foreach (var img in images) {
          inputList.Add(ImageTypeToInputType(img));
        }
      }
    }

    private void CheckGamepadInput() {
      var inputs = TFC.GamepadInputHandler.GetActionsOnly();
      var trialInputs = TFC.GamepadInputHandler.GetColorInputs();
      if (resetedToNeutral) {
        if (inputs.Count != 0) {
          if (gameMode == UDC.GameMode.Trial) {
            if (trialInputs.Contains(trialInputList[currentCommand])) {
              resetedToNeutral = false;
              AddScoreByCommandIndex(currentCommand);
              SoundMgr.PlaySe("UDCCorrectCommand", 2);
              currentCommand++;
            }
            else {
              currentCommand = 0;
              GenerateRandomCommands();
              SoundMgr.PlaySe("UDCMissCommand", 3);
              resetedToNeutral = false;
            }
          }
          else if (gameMode == UDC.GameMode.Challenge) {
            if (inputs.Contains(inputList[currentCommand])) {
              resetedToNeutral = false;
              AddScoreByCommandIndex(currentCommand);
              SoundMgr.PlaySe("UDCCorrectCommand", 2);
              currentCommand++;
            }
            else {
              currentCommand = 0;
              GenerateRandomCommands();
              SoundMgr.PlaySe("UDCMissCommand", 3);
              resetedToNeutral = false;
            }
          }
        }
      }
      else {
        if (inputs.Count == 0) {
          resetedToNeutral = true;
        }
      }
    }

    private void AddScoreByCommandIndex(int currentCommand) {
      score += (int)(10 * Mathf.Pow(2, currentCommand));
    }

    private void ResetCurrentCommand() {
      if (currentCommand >= commandObjects.Count) {
        currentCommand = 0;
        correctCommands++;
        SoundMgr.PlaySe("UDCCorrectList", 4);
        commandAdded = false;
      }

      if(correctCommands >= 10) {
        if(correctCommands % 10 == 0) {
          if(commandObjects.Count < 5 && !commandAdded) {
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

    public void EndGame() {
      MSMM.RankingTempData.TempScore = (uint)score;
      Reset();
      switch (gameMode) {
        case GameMode.Trial:
          SceneManager.LoadScene("UDCTrialResult");
          break;
        case GameMode.Challenge:
          SceneManager.LoadScene("UDCChallengeResult");
          break;
      }    
    }

    private Sprite ImageTypeToSprite(UDC.ImageType imageType) {
      string path = "";
      switch (imageType) {
        case UDC.ImageType.Red:
          path = "Sprites/UDCommand/red";
          break;
        case UDC.ImageType.Green:
          path = "Sprites/UDCommand/green";
          break;
        case UDC.ImageType.Blue:
          path = "Sprites/UDCommand/blue";
          break;
        case UDC.ImageType.Yellow:
          path = "Sprites/UDCommand/yellow";
          break;

        case UDC.ImageType.URed:
          path = "Sprites/UDCommand/Ured";
          break;
        case UDC.ImageType.UGreen:
          path = "Sprites/UDCommand/Ugreen";
          break;
        case UDC.ImageType.UBlue:
          path = "Sprites/UDCommand/Ublue";
          break;
        case UDC.ImageType.UYellow:
          path = "Sprites/UDCommand/Uyellow";
          break;

        case UDC.ImageType.BRed:
          path = "Sprites/UDCommand/Bred";
          break;
        case UDC.ImageType.BGreen:
          path = "Sprites/UDCommand/Bgreen";
          break;
        case UDC.ImageType.BBlue:
          path = "Sprites/UDCommand/Bblue";
          break;
        case UDC.ImageType.BYellow:
          path = "Sprites/UDCommand/Byellow";
          break;
      }

      return Resources.Load<Sprite>(path);
    }

    private TFC.InputType ImageTypeToInputType(UDC.ImageType imageType) {
      return (TFC.InputType)imageType + 1;
    }

    private TFC.ActionInputs ImageTypeToActionInput(UDC.ImageType imageType) {
      return (TFC.ActionInputs)((int)imageType % 4) + 1;
    }
  }
}