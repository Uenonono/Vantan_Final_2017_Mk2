using UnityEngine;
using System.Collections;

namespace UDCommand {
  public class StandbySwitcher : MonoBehaviour {
    [SerializeField]
    UDCommand.GameManager manager = null;

    private void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<ToppingFullCustom.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          manager.StartGame();
          menuSelector.Reset();
          gameObject.SetActive(false);
        }
      }
    }
  }
}
