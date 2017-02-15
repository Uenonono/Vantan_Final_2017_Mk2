using UnityEngine;
using System.Collections;

namespace UDCommand {
  public class GameParticles : MonoBehaviour {

    public void PlayParticles() {
      var objects = gameObject.GetComponentsInChildren<Animator>();

      foreach (var obj in objects) {
        if (obj.tag == "Soil Particle") {
          obj.Play("Blink");
        }

        if (obj.tag == "Popup Effect") {
          obj.Play("Flicker");
        }
      }
    }
  }
}
