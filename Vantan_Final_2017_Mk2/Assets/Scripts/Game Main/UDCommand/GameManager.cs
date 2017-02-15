using UnityEngine;
using Unity = UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
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
    private GameObject UICanvas = null;

    [SerializeField]
    private GameObject SpecialModeCanvas = null;

    private UDC.GameMode gameMode;

    private List<UDC.ImageType> images;
    private List<GameObject> commandObjects;
    private List<TFC.InputType> inputList;
    private List<TFC.ActionInputs> trialInputList;

    private int previousFrameCommand = -1;
    private int currentCommand = 0;

    private bool resetedToNeutral;

    public float waitTime = 60.0f;

    private float currentWaitTime;

    private int correctCommands;
    private bool commandAdded = false;
    private bool waitingAnimation = false;
    private float waitingTime = 0.0f;
    private bool missSEPlayed = false;

    private bool init = false;
    private bool onStandby = true;
    private bool gameEnded = false;
    private bool endSEPlayed = false;

    [SerializeField]
    GameObject pauseCanvas = null;
    private bool isPaused = false;
    private bool isPauseReleased = true;

    GameObject trans;

    void Start() {
      images = new List<UDC.ImageType>();
      commandObjects = new List<GameObject>();
      inputList = new List<TFC.InputType>();
      trialInputList = new List<TFC.ActionInputs>();
      SoundMgr.SoundLoadSe("UDCCorrectCommand", "UDCommand/CorrectCommand");
      SoundMgr.SoundLoadSe("UDCCorrectList", "UDCommand/CorrectList");
      SoundMgr.SoundLoadSe("UDCMissCommand", "UDCommand/MissCommand");
      SoundMgr.SoundLoadSe("UDCGameEnd", "UDCommand/Finish");
      trans = GameObject.Find("Transition Handler");
    }

    private void Init() {
      transform.DetachChildren();
      foreach (var command in commandObjects) {
        Destroy(command);
      }
      commandObjects.Clear();
      previousFrameCommand = -1;
      currentCommand = 0;
      score = 0;
      currentWaitTime = waitTime;
      correctCommands = 0;
      onStandby = true;
      gameEnded = false;
      endSEPlayed = false;
      standbyCanvas.SetActive(true);
      pauseCanvas.SetActive(false);
      UICanvas.SetActive(true);
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

    IEnumerator LateReset(float seconds) {
      yield return new WaitForSeconds(seconds);
      Reset();
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
        if (onStandby) {
          standbyCanvas.SetActive(true);
        }
        UpdateDisplayTime();
        UpdateScoreText();
        if (currentCommand == 0 && (currentCommand != previousFrameCommand)) {
          GenerateRandomCommands();
        }
        UpdateCommandIcons(currentCommand);
        previousFrameCommand = currentCommand;
        if (Input.GetAxis("Pause") == 0) {
          isPauseReleased = true;
        }
        if ((Input.GetAxis("Pause") == 1) && isPauseReleased) {
          isPaused = true;
          standbyCanvasLastState = standbyCanvas.activeSelf;
          standbyCanvas.GetComponent<MSMM.MenuSelector>().Reset();
          standbyCanvas.SetActive(false);
          pauseCanvas.SetActive(true);
          pauseCanvas.GetComponent<PauseMenu>().SetButtonState();
        }
        if (!onStandby && !gameEnded) {
          if (waitingAnimation) {
            var radish = commandObjects[currentCommand].GetComponent<CommandManager>().GetRadish();
            if(waitingTime >= 0.167f && !missSEPlayed) {
              SoundMgr.PlaySe("UDCMissCommand");
              missSEPlayed = true;
            }
            if (radish.GetComponent<RectTransform>().anchoredPosition.y >= 150) {
              GenerateRandomCommands();
              currentCommand = 0;
              waitingAnimation = false;
            }
            waitingTime += Time.deltaTime;
          }
          else {
            CheckGamepadInput();
            ResetCurrentCommand();
          }
          currentWaitTime -= Time.deltaTime;
        }
      }
      if (isPaused) {
        if (!pauseCanvas.activeSelf) {
          isPaused = false;
          isPauseReleased = false;
          standbyCanvas.SetActive(standbyCanvasLastState);
        }
      }

      if (currentWaitTime <= 0) {
        gameEnded = true;
        currentWaitTime = 0;
        UICanvas.SetActive(false);
      }

      if (gameEnded) {
        if (!endSEPlayed) {
          SoundMgr.PlaySe("UDCGameEnd");
          endSEPlayed = true;
        }
        SpecialModeCanvas.SetActive(true);
        SpecialModeCanvas.GetComponent<SpecialModeTransition>().Execute();
        MSMM.RankingTempData.TempScore = (uint)score;
        UDC.SpecialModeData.Time = correctCommands;
        StartCoroutine(LateReset(3));
        SoundMgr.StopBgm();
      }
    }

    private void UpdateDisplayTime() {
      timeText.text = "残り時間　： " + currentWaitTime.ToString("F2");
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
        else if (gameMode == UDC.GameMode.Challenge) {
          images.Add((UDC.ImageType)Unity.Random.Range(4, 12));
        }
      }

      for (var i = 0; i < images.Count; i++) {
        commandObjects[i].GetComponent<CommandManager>().SetCommand(images[i]);
        commandObjects[i].GetComponentInChildren<Animator>().Play("Idle");
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
              commandObjects[currentCommand].GetComponentInChildren<Animator>().Play("Popup");
              currentCommand++;
            }
            else {
              MissCommand();
              resetedToNeutral = false;
            }
          }
          else if (gameMode == UDC.GameMode.Challenge) {
            if (inputs.Contains(inputList[currentCommand])) {
              resetedToNeutral = false;
              AddScoreByCommandIndex(currentCommand);
              SoundMgr.PlaySe("UDCCorrectCommand", 2);
              commandObjects[currentCommand].GetComponentInChildren<Animator>().Play("Popup");
              currentCommand++;
            }
            else {
              MissCommand();
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

    private void MissCommand() {
      commandObjects[currentCommand].GetComponentInChildren<Animator>().Play("MPopup");
      commandObjects[currentCommand].GetComponent<CommandManager>().SwapMandragora();
      waitingAnimation = true;
      waitingTime = 0.0f;
      missSEPlayed = false;
    }

    private void AddScoreByCommandIndex(int currentCommand) {
      score += (int)(10 * Mathf.Pow(2, currentCommand));
    }

    private void ResetCurrentCommand() {
      if (currentCommand >= commandObjects.Count) {
        var radish = commandObjects[commandObjects.Count - 1].GetComponent<CommandManager>().GetRadish();
        if (radish.GetComponent<RectTransform>().anchoredPosition.y >= 150) {
          currentCommand = 0;
          correctCommands++;
          SoundMgr.PlaySe("UDCCorrectList", 4);
          commandAdded = false;
        }
      }

      if (correctCommands >= 10) {
        if (correctCommands % 10 == 0) {
          if (commandObjects.Count < 5 && !commandAdded) {
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

    public float GetCurrentTime() {
      return currentWaitTime;
    }

    private TFC.InputType ImageTypeToInputType(UDC.ImageType imageType) {
      return (TFC.InputType)imageType + 1;
    }

    private TFC.ActionInputs ImageTypeToActionInput(UDC.ImageType imageType) {
      return (TFC.ActionInputs)((int)imageType % 4) + 1;
    }
  }
}