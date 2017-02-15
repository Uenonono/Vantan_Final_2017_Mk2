using UnityEngine;
using System.Collections;

namespace UDCommand {
  public class SpecialModeTransition : MonoBehaviour {

    public void Execute() {
      StartCoroutine(_Execute());
    }

    IEnumerator _Execute() {
      gameObject.GetComponentInChildren<Animator>().Play("SpecialModeTrans");
      yield return new WaitForSeconds(2.0f);
      GameObject.FindGameObjectWithTag("Transition Handler").GetComponent<MSMM.Transition>().LoadScene("UDCSpecialMode");
    }
  }
}
