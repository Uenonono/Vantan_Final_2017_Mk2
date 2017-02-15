using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class PauseMenu : MonoBehaviour {

    bool isButtonReleased = false;

    MSMM.MenuSelector pauseMenu;

    [SerializeField]
    UDCommand.GameManager manager = null;

    GameObject trans;

    void Start() {
      pauseMenu = GetComponent<MSMM.MenuSelector>();
      trans = GameObject.FindGameObjectWithTag("Transition Handler");
    }

    void Update() {
      if (!isButtonReleased) {
        if (Input.GetAxis("Pause") == 0) {
          isButtonReleased = true;
        }
      }
      if (isButtonReleased) {
        if (Input.GetAxis("Pause") == 1) {
          gameObject.SetActive(false);
          pauseMenu.Reset();
        }
        if (Input.GetAxis("BottomGreen") == 1) {
          var index = GetComponent<MSMM.MenuSelector>().GetCurrentSelectedIndex();
          if (index == 0) {
            gameObject.SetActive(false);
            SoundMgr.PlaySe("UDCDecide");
            pauseMenu.Reset();
            manager.Reset();
            trans.GetComponent<MSMM.Transition>().FadeOutIn();
          }
          if(index == 1) {
            gameObject.SetActive(false);
            SoundMgr.PlaySe("UDCDecide");
            pauseMenu.Reset();
            manager.Reset();
            trans.GetComponent<MSMM.Transition>().LoadScene("UDCTitle");
          }
        }
      }
    }

    public void SetButtonState() {
      isButtonReleased = false;
    }
  }
}
