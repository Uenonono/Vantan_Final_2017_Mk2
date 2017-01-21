using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class UDCRankingManager : MonoBehaviour {

    void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<MSMM.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          SceneManager.LoadScene("UDCTitle");
          menuSelector.Reset();
        }
      }
    }
  }
}