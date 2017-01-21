using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoftReset : MonoBehaviour {

	void Awake() {
    if (FindObjectsOfType<SoftReset>().Length != 1) {
      Destroy(gameObject);
    }
    else {
      DontDestroyOnLoad(transform.gameObject);
    }
  }

  void Update() {
    if(Input.GetAxis("Option") == 1 && Input.GetAxis("Pause") == 1) {
      var menuObj = GameObject.FindGameObjectsWithTag("Menu Selector");
      if(menuObj != null) {
        foreach(var obj in menuObj) {
        obj.GetComponent<ToppingFullCustom.MenuSelector>().Reset();
        }
      }

      var UDC_GM_Obj = GameObject.FindGameObjectWithTag("UDC Game Manager");
      if(UDC_GM_Obj != null) {
        UDC_GM_Obj.GetComponent<UDCommand.GameManager>().Reset();
      }

      if (SceneManager.GetActiveScene().name.Contains("UDC")) {
        SoundMgr.StopBgm();
      }

      SceneManager.LoadScene("MainTitle");
    }
  }
}
