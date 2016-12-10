using UnityEngine;
using System.Collections;

namespace GGFarAway {
  public class BallCollisionDetection : MonoBehaviour {

    private bool _isHit = false;

    public bool isHit {
      get { return _isHit; }
      set { _isHit = value; }
    }

    private void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.CompareTag("Bat")) {
        isHit = true;
      }
    }
  }
}
