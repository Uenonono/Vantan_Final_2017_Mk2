using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoftReset : MonoBehaviour {

	void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }

  void Update() {
    if(Input.GetAxis("Option") == 1 && Input.GetAxis("Pause") == 1) {
      var menuObj = GameObject.FindGameObjectWithTag("Menu Selector");
      if(menuObj != null) {
        menuObj.GetComponent<ToppingFullCustom.MenuSelector>().Reset();
      }

      var UDC_GM_Obj = GameObject.FindGameObjectWithTag("UDC Game Manager");
      if(UDC_GM_Obj != null) {
        UDC_GM_Obj.GetComponent<UDCommand.GameManager>().Reset();
      }

      SceneManager.LoadScene("MainTitle");
    }
  }
}
