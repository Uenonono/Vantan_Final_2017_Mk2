using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class UDCRankingManager : MonoBehaviour {

    GameObject trans;

    void Start() {
      trans = GameObject.FindGameObjectWithTag("Transition Handler");
    }

    void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<MSMM.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          trans.GetComponent<MSMM.Transition>().LoadScene("UDCTitle");
          SoundMgr.PlaySe("UDCDecide");
          menuSelector.Reset();
        }
      }
    }
  }
}