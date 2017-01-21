using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class UDCResultManager : MonoBehaviour {

    void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<ToppingFullCustom.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          SceneManager.LoadScene("UDCTitle");
          menuSelector.Reset();
        }
        else if (index == 1) {
          SceneManager.LoadScene("MainTitle");
          SoundMgr.StopBgm();
          menuSelector.Reset();
        }
      }
    }
  }
}