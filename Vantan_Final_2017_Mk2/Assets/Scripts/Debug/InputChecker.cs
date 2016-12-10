using UnityEngine;
using System.Collections;

namespace TFCDebug {
  public class InputChecker : MonoBehaviour {

    [SerializeField]
    private GameObject DirUp = null;
    [SerializeField]
    private GameObject DirLeft = null;
    [SerializeField]
    private GameObject DirDown = null;
    [SerializeField]
    private GameObject DirRight = null;

    [SerializeField]
    private GameObject ActUp = null;
    [SerializeField]
    private GameObject ActLeft = null;
    [SerializeField]
    private GameObject ActDown = null;
    [SerializeField]
    private GameObject ActRight = null;

    private void Update() {
      UpdateActionInputs();
      UpdateDirectionInputs();
    }

    private void UpdateActionInputs() {
      if (Input.GetAxis("ActPink") == 1.0f) {
        ActLeft.GetComponent<Renderer>().material.color = Color.red;
      }
      else {
        ActLeft.GetComponent<Renderer>().material.color = Color.white;
      }

      if (Input.GetAxis("ActBlue") == 1.0f) {
        ActDown.GetComponent<Renderer>().material.color = Color.red;
      }
      else {
        ActDown.GetComponent<Renderer>().material.color = Color.white;
      }

      if (Input.GetAxis("ActRed") == 1.0f) {
        ActRight.GetComponent<Renderer>().material.color = Color.red;
      }
      else {
        ActRight.GetComponent<Renderer>().material.color = Color.white;
      }
      if (Input.GetAxis("ActGreen") == 1.0f) {
        ActUp.GetComponent<Renderer>().material.color = Color.red;
      }
      else {
        ActUp.GetComponent<Renderer>().material.color = Color.white;
      }
    }

    private void UpdateDirectionInputs() {
      if (Input.GetAxis("Horizontal") >= 1.0f || Input.GetAxis("DirHorizontal") >= 1.0f) {
        DirRight.GetComponent<Renderer>().material.color = Color.red;
        DirLeft.GetComponent<Renderer>().material.color = Color.white;
      }
      else if (Input.GetAxis("Horizontal") <= -1.0f || Input.GetAxis("DirHorizontal") <= -1.0f) {
        DirRight.GetComponent<Renderer>().material.color = Color.white;
        DirLeft.GetComponent<Renderer>().material.color = Color.red;
      }
      else {
        DirRight.GetComponent<Renderer>().material.color = Color.white;
        DirLeft.GetComponent<Renderer>().material.color = Color.white;
      }

      if (Input.GetAxis("Vertical") >= 1.0f || Input.GetAxis("DirVertical") >= 1.0f) {
        DirUp.GetComponent<Renderer>().material.color = Color.red;
        DirDown.GetComponent<Renderer>().material.color = Color.white;
      }
      else if (Input.GetAxis("Vertical") <= -1.0f || Input.GetAxis("DirVertical") <= -1.0f) {
        DirUp.GetComponent<Renderer>().material.color = Color.white;
        DirDown.GetComponent<Renderer>().material.color = Color.red;
      }
      else {
        DirUp.GetComponent<Renderer>().material.color = Color.white;
        DirDown.GetComponent<Renderer>().material.color = Color.white;
      }
    }
  }
}