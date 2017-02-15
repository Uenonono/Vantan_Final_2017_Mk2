using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace MSMM {
  public class MainTitleManager : MonoBehaviour {
    [SerializeField]
    GameObject softResetHandler = null;


    void Awake() {
      var srHandler = Instantiate(softResetHandler);
      DontDestroyOnLoad(srHandler);
    }

    private void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<MSMM.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          var trans = GameObject.FindGameObjectWithTag("Transition Handler");
          trans.GetComponent<Transition>().LoadScene("UDCTitle");
          menuSelector.Reset();
        }

        if (index == 1) {
          SceneManager.LoadScene("InvaderTitle");
          menuSelector.Reset();
          STGGameState.SetState(0);
        }
      }
    }
  }
}
