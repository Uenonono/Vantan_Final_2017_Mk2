using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UDCommand {
  public class UDCTitleManager : MonoBehaviour {

    void Start() {
      SoundMgr.SoundLoadBgm("UDCBGM", "UDCommand/BGM");
      ushort cnt = 0;
      while (SoundMgr.isBgmPlaying("UDCBGM") == -1 && cnt < 10) {
        SoundMgr.SoundLoadBgm("UDCBGM", "UDCommand/BGM");
        cnt++;
      }

      if(cnt == 9) {
        Debug.Log("Error on loading the sound file");
      }

      if(SoundMgr.isBgmPlaying("UDCBGM") != 1) {
        SoundMgr.PlayBgm("UDCBGM");
      }
    }

    void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<MSMM.MenuSelector>();
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
        else if(index == 2) {
          SceneManager.LoadScene("UDCRanking");
          menuSelector.Reset();
        }
      }
    }
  }
}
