using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace MSMM{
  public class MenuSelector : MonoBehaviour {

    [SerializeField]
    private Image[] buttons = null;

    private int selectedIndex = -1;

    private bool initialInput = false;

    private bool stickNeutral = true;

    void Start() {
    }

    void Update() {
      if (initialInput) {
        if (stickNeutral) {
          if (Input.GetAxis("Horizontal") > 0.5f) {
            selectedIndex++;
            if (selectedIndex > (buttons.Length - 1)) {
              selectedIndex = 0;
            }
          }

          if (Input.GetAxis("Horizontal") < -0.5f) {
            selectedIndex--;
            if (selectedIndex < 0) {
              selectedIndex = buttons.Length - 1;
            }
          }
          stickNeutral = false;
        }
      }

      if (!initialInput) {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)) {
          selectedIndex = 0;
          initialInput = true;
          stickNeutral = false;
        }
      }


      if (Input.GetAxis("Horizontal") == 0) {
        stickNeutral = true;
      }

      ChangeColorBySelection(selectedIndex);
    }

    private void ChangeColorBySelection(int index) {
      if (index >= 0) {
        if (buttons.Length > 0) {
          foreach (Image but in buttons) {
            but.color = Color.white;
          }
          buttons[index].color = Color.red;
        }
      }
    }

    public int GetCurrentSelectedIndex() {
      return selectedIndex;
    }

    public void Reset() {
      selectedIndex = -1;
      initialInput = false;
      stickNeutral = true;
      foreach(Image but in buttons) {
        but.color = Color.white;
      }
    }
  }
}