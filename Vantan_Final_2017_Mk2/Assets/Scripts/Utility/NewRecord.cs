using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MSMM {

  public class RankingTempData {
    private static uint tempScore;

    public static uint TempScore {
      get { return tempScore; }
      set { tempScore = value; }
    }

    private static string tempName;

    public static string TempName {
      get { return tempName; }
      set { tempName = value; }
    }

    public static void Reset() {
      tempScore = 0;
      tempName = "";
    }
  }

  public enum Symbol {
    A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
    Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9,
  }

  public class NewRecord : MonoBehaviour {

    private Symbol firstSymbol;
    private Symbol secondSymbol;
    private Symbol thirdSymbol;

    private int currentLetter;

    private float timeCounter;
    private bool blinkFlag;

    private bool inputNeutral = true;

    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private string scorePlaceholder = "";
    [SerializeField]
    private bool useScorePlaceholder = false;
    [SerializeField]
    private string scoreUnit = "";

    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private string namePlaceholder = "";
    [SerializeField]
    private bool useNamePlaceholder = false;

    private string resultString;

    [SerializeField]
    private GameObject subCanvas = null;

    [SerializeField]
    string decideSEPath = "";
    [SerializeField]
    string selectSEPath = "";

    void Start() {
      firstSymbol = secondSymbol = thirdSymbol = Symbol.A;
      currentLetter = 0;
      subCanvas.SetActive(false);

      if (useScorePlaceholder) {
        if(scorePlaceholder == "") {
          scorePlaceholder = "Score : ";
        }
      }

      if (useNamePlaceholder) {
        if(namePlaceholder == "") {
          namePlaceholder = "Name : ";
        }
      }

      SoundMgr.SoundLoadSe("RecordDecide", decideSEPath);
      SoundMgr.SoundLoadSe("RecordSelect", selectSEPath);

    }

    void Update() {
      switch (currentLetter) {
        case 0:
          UpdateSymbol(ref firstSymbol);
          break;
        case 1:
          UpdateSymbol(ref secondSymbol);
          break;
        case 2:
          UpdateSymbol(ref thirdSymbol);
          break;
        case 3:
          subCanvas.SetActive(true);
          CheckConfirmation();
          break;
      }
      UpdateText();
      CheckForInputs();
    }

    private void UpdateSymbol(ref Symbol sym) {
      if (inputNeutral) {
        if (Input.GetAxis("Vertical") >= 0.5f) {
          SoundMgr.PlaySe("RecordSelect");
          if (sym != Symbol.A) {
            sym--;
          }
          else {
            sym = Symbol.Num9;
          }
          inputNeutral = false;
        }

        if (Input.GetAxis("Vertical") <= -0.5f) {
          SoundMgr.PlaySe("RecordSelect");
          if (sym != Symbol.Num9) {
            sym++;
          }
          else {
            sym = Symbol.A;
          }
          inputNeutral = false;
        }

        if (Input.GetAxis("BottomGreen") == 1.0f) {
          SoundMgr.PlaySe("RecordDecide");
          currentLetter++;
          inputNeutral = false;
        }

        if (Input.GetAxis("BottomRed") == 1.0f) {
          SoundMgr.PlaySe("RecordDecide");
          if (currentLetter != 0) {
            currentLetter--;
          }
          else {
            firstSymbol = secondSymbol = thirdSymbol = Symbol.A;
          }
          inputNeutral = false;
        }
      }
    }

    private void CheckForInputs() {
      if (ToppingFullCustom.GamepadInputHandler.GetInputs().Count == 0) {
        inputNeutral = true;
      }
    }

    private void UpdateText() {
      timeCounter += Time.deltaTime;
      if (timeCounter > 0.5f && !blinkFlag) {
        timeCounter -= 0.5f;
        SwitchFlag();
      }
      else if(timeCounter > 0.1f && blinkFlag){
        timeCounter -= 0.1f;
        SwitchFlag();
      }

      resultString = "";

      for (int i = 0; i < 3; i++) {
        if (i == currentLetter && blinkFlag) {
          resultString += " ";
        }
        else {
          switch (i) {
            case 0:
              resultString += SymbolToString(firstSymbol);
              break;
            case 1:
              resultString += SymbolToString(secondSymbol);
              break;
            case 2:
              resultString += SymbolToString(thirdSymbol);
              break;
          }
        }
      }
      if (scoreUnit != "") {
        scoreText.text = scorePlaceholder + MSMM.RankingTempData.TempScore.ToString() + " " + scoreUnit;
      }
      else {
        scoreText.text = scorePlaceholder + MSMM.RankingTempData.TempScore.ToString();
      }
      nameText.text = namePlaceholder + resultString;
    }

    private void SwitchFlag() {
      if (blinkFlag) {
        blinkFlag = false;
      }
      else {
        blinkFlag = true;
      }
    }

    private void CheckConfirmation() {
      if (Input.GetAxis("BottomGreen") == 1.0f && inputNeutral) {
        switch (subCanvas.GetComponent<MSMM.MenuSelector>().GetCurrentSelectedIndex()) {
          case 0:
            SoundMgr.PlaySe("RecordDecide");
            resultString = SymbolToString(firstSymbol) + SymbolToString(secondSymbol) + SymbolToString(thirdSymbol);
            MSMM.RankingTempData.TempName = resultString;
            subCanvas.GetComponent<MSMM.MenuSelector>().Reset();
            inputNeutral = false;
            this.gameObject.SetActive(false);
            break;
          case 1:
            SoundMgr.PlaySe("RecordDecide");
            subCanvas.SetActive(false);
            currentLetter--;
            subCanvas.GetComponent<MSMM.MenuSelector>().Reset();
            inputNeutral = false;
            break;
        }
      }
    }

    private string SymbolToString(Symbol sym) {
      if ((int)sym <= 25) {
        return sym.ToString();
      }
      else {
        int numVal = (int)sym - 26;
        return numVal.ToString();
      }
    }

    public void Reset() {
      firstSymbol = secondSymbol = thirdSymbol = Symbol.A;
      currentLetter = 0;
      subCanvas.SetActive(false);
      timeCounter = 0;
    }
  }
}