using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class UDCTitleManager : MonoBehaviour {

    void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<ToppingFullCustom.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          UDCommand.SelectedGameMode.SetMode(0);
          SceneManager.LoadScene("UDCMain");
          menuSelector.Reset();
        }
        else if (index == 1) {
          UDCommand.SelectedGameMode.SetMode(1);
          SceneManager.LoadScene("UDCMain");
          menuSelector.Reset();
        }
      }
    }
  }
}
