using UnityEngine;
using System.Collections;

namespace GGFarAway {
  public class BatterController : MonoBehaviour {

    private Animator batterAnimCont;
    private int swingAnimHash;

    void Start() {
      batterAnimCont = GetComponent<Animator>();
      swingAnimHash = Animator.StringToHash("Base Layer.Swing");
    }

    void Update() {
    }

    public void PlayAnimation() {
      batterAnimCont.Play(swingAnimHash, 0, 0);
    }

  }
}
