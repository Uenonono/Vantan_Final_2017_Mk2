using UnityEngine;
using System.Collections;

namespace GGFarAway {
  public class LauncherController : MonoBehaviour {

    private Animator launcherAnimCont;
    private int launchAnimHash;

    void Start() {
      launcherAnimCont = GetComponent<Animator>();
      launchAnimHash = Animator.StringToHash("Base Layer.Launch");
    }

    void Update() {
    }

    public void PlayAnimation() {
      launcherAnimCont.Play(launchAnimHash, 0, 0);
    }

    public bool IsAnimationDone() {
      if(launcherAnimCont.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Launch")) {
        if(launcherAnimCont.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
          return true;
        }
      }

      return false;
    }

  }
}