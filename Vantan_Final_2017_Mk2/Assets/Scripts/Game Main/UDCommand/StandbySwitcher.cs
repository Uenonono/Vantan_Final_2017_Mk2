using UnityEngine;
using System.Collections;

namespace UDCommand {
  public class StandbySwitcher : MonoBehaviour {
    [SerializeField]
    UDCommand.GameManager manager = null;

    private void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = gameObject.GetComponent<MSMM.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          SoundMgr.PlaySe("UDCDecide");
          menuSelector.Reset();
          manager.StartGame();
          gameObject.SetActive(false);
        }
      }
    }
  }
}
