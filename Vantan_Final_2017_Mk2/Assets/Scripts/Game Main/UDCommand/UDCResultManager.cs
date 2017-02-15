using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class UDCResultManager : MonoBehaviour {

    GameObject trans;

    void Start() {
      trans = GameObject.FindGameObjectWithTag("Transition Handler");
      SoundMgr.SoundLoadBgm("UDCBGM", "UDCommand/BGM");
      if(SoundMgr.isBgmPlaying("UDCBGM") != 1) {
        SoundMgr.PlayBgm("UDCBGM");
      }
    }

    void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<MSMM.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          SoundMgr.PlaySe("UDCDecide");
          trans.GetComponent<MSMM.Transition>().LoadScene("UDCTitle");
          menuSelector.Reset();
        }
        else if (index == 1) {
          SoundMgr.PlaySe("UDCDecide");
          trans.GetComponent<MSMM.Transition>().LoadScene("MainTitle");
          SoundMgr.StopBgm();
          menuSelector.Reset();
        }
      }
    }
  }
}