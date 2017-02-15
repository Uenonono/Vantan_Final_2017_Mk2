using UnityEngine;
using System.Collections;

namespace UDCommand {
  public static class SpecialModeData {
    private static int time;

    public static int Time {
      get { return time; }
      set { time = value; }
    }
  }

  public class SpecialMode : MonoBehaviour {

    float currentTime;
    int buttonCount;

    bool[] buttonDown = new bool[8];
    bool animInit = false;
    bool endSEPlayed = false;

    [SerializeField]
    private GameObject resultCanvas = null;

    [SerializeField]
    private GameObject instructText = null;

    [SerializeField]
    private GameObject textAnim = null;

    private GameObject trans;

    void Start() {
      if (SpecialModeData.Time < 5) {
        currentTime = 5;
      }
      else if (SpecialModeData.Time > 20) {
        currentTime = 20;
      }
      else {
        currentTime = SpecialModeData.Time;
      }
      SoundMgr.SoundLoadBgm("SpecialBGM", "UDCommand/SpecialBGM");
      SoundMgr.SoundLoadSe("UDCGameEnd", "UDCommand/Finish");
      SoundMgr.SoundLoadSe("UDCDecide", "UDCommand/Decide");

      if (SoundMgr.isBgmPlaying("SpecialBGM") == 0) {
        SoundMgr.PlayBgm("SpecialBGM");
      }

      trans = GameObject.FindGameObjectWithTag("Transition Handler");

      instructText.SetActive(true);
      resultCanvas.SetActive(false);
      textAnim.SetActive(false);
    }

    void Update() {
      if (currentTime > 0) {
        GetInputs();
      }

      if (buttonCount > 0 && !animInit) {
        gameObject.GetComponentInChildren<Animator>().Play("Wiggle");
        animInit = true;
        textAnim.SetActive(true);
      }

      if (currentTime > 0 && buttonCount > 0) {
        currentTime -= Time.deltaTime;
      }

      if (currentTime <= 0) {
        gameObject.GetComponentInChildren<Animator>().Play("PopupSpecial");
        instructText.SetActive(false);
        resultCanvas.SetActive(true);
        if (!endSEPlayed) {
          SoundMgr.PlaySe("UDCGameEnd");
          endSEPlayed = true;
          textAnim.GetComponent<TextAnimation>().Reset();
          textAnim.SetActive(false);
        }
        SoundMgr.StopBgm();
      }

      if (resultCanvas.activeSelf == true) {
        var selector = resultCanvas.GetComponent<MSMM.MenuSelector>();
        var index = selector.GetCurrentSelectedIndex();
        if(index == 0) {
          if (Input.GetAxis("BottomGreen") == 1) {
            if (UDCommand.SelectedGameMode.GetMode() == (int)UDCommand.GameMode.Trial) {
              MSMM.RankingTempData.TempScore += (uint)buttonCount * 5;
              SoundMgr.PlaySe("UDCDecide");
              selector.Reset();
              trans.GetComponent<MSMM.Transition>().LoadScene("UDCTrialResult");
              MSMM.PlayCounter.AddCount(MSMM.CountType.UDCEnd);
            }
            else if (UDCommand.SelectedGameMode.GetMode() == (int)UDCommand.GameMode.Challenge) {
              MSMM.RankingTempData.TempScore += (uint)buttonCount * 5;
              SoundMgr.PlaySe("UDCDecide");
              selector.Reset();
              trans.GetComponent<MSMM.Transition>().LoadScene("UDCChallengeResult");
              MSMM.PlayCounter.AddCount(MSMM.CountType.UDCEnd);
            }
          }
        }
      }
    }

    private void GetInputs() {
      if (Input.GetAxis("BottomRed") == 1 && !buttonDown[0]) {
        buttonCount++;
        buttonDown[0] = true;
      }
      else {
        buttonDown[0] = false;
      }

      if (Input.GetAxis("BottomGreen") == 1 && !buttonDown[1]) {
        buttonCount++;
        buttonDown[1] = true;
      }
      else {
        buttonDown[1] = false;
      }

      if (Input.GetAxis("BottomBlue") == 1 && !buttonDown[2]) {
        buttonCount++;
        buttonDown[2] = true;
      }
      else {
        buttonDown[2] = false;
      }

      if (Input.GetAxis("BottomYellow") == 1 && !buttonDown[3]) {
        buttonCount++;
        buttonDown[3] = true;
      }
      else {
        buttonDown[3] = false;
      }

      if (Input.GetAxis("UpperRed") == 1 && !buttonDown[4]) {
        buttonCount++;
        buttonDown[4] = true;
      }
      else {
        buttonDown[4] = false;
      }

      if (Input.GetAxis("UpperGreen") == 1 && !buttonDown[5]) {
        buttonCount++;
        buttonDown[5] = true;
      }
      else {
        buttonDown[5] = false;
      }

      if (Input.GetAxis("UpperBlue") == 1 && !buttonDown[6]) {
        buttonCount++;
        buttonDown[6] = true;
      }
      else {
        buttonDown[6] = false;
      }

      if (Input.GetAxis("UpperYellow") == 1 && !buttonDown[7]) {
        buttonCount++;
        buttonDown[7] = true;
      }
      else {
        buttonDown[7] = false;
      }
    }
  }
}
