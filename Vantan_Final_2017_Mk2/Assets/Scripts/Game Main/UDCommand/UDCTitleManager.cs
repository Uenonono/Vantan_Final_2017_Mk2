using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class UDCTitleManager : MonoBehaviour {

    GameObject trans;

    void Start() {
      SoundMgr.SoundLoadBgm("UDCBGM", "UDCommand/BGM");
      SoundMgr.SoundLoadSe("UDCDecide", "UDCommand/Decide");

      if(SoundMgr.isBgmPlaying("UDCBGM") != 1) {
        SoundMgr.PlayBgm("UDCBGM",0.3f);
      }

      trans = GameObject.FindGameObjectWithTag("Transition Handler");
    }

    void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<MSMM.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          UDCommand.SelectedGameMode.SetMode(0);
          trans.GetComponent<MSMM.Transition>().LoadScene("UDCMain");
          SoundMgr.PlaySe("UDCDecide");
          menuSelector.Reset();
        }
        else if (index == 1) {
          UDCommand.SelectedGameMode.SetMode(1);
          trans.GetComponent<MSMM.Transition>().LoadScene("UDCMain");
          SoundMgr.PlaySe("UDCDecide");
          menuSelector.Reset();
        }
        else if(index == 2) {
          trans.GetComponent<MSMM.Transition>().LoadScene("UDCRanking");
          SoundMgr.PlaySe("UDCDecide");
          menuSelector.Reset();
        }
      }
    }
  }
}
