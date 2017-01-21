﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ToppingFullCustom {
  public class MainTitleManager : MonoBehaviour {

    [SerializeField]
    GameObject softResetHandler = null;

    void Awake() {
      var srHandler = Instantiate(softResetHandler);
      DontDestroyOnLoad(srHandler);
    }

    private void Update() {
      if (Input.GetAxis("BottomGreen") == 1) {
        var menuSelector = GetComponent<ToppingFullCustom.MenuSelector>();
        var index = menuSelector.GetCurrentSelectedIndex();
        if (index == 0) {
          SceneManager.LoadScene("UDCTitle");
          menuSelector.Reset();
        }
        else if(index == 1) {
          SceneManager.LoadScene("InvaderTitle");
          menuSelector.Reset();
        }
      }
    }
  }
}
