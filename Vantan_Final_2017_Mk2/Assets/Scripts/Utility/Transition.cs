using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace MSMM {
  public class Transition : MonoBehaviour {
    public void LoadScene(string nextScene) {
      gameObject.GetComponent<Animator>().Play("FadeOut");
      StartCoroutine(Load(nextScene));
    }

    IEnumerator Load(string nextScene) {
      yield return new WaitForSeconds(0.5f);
      SceneManager.LoadScene(nextScene);
    }

    public void FadeOutIn() {
      StartCoroutine(_FadeOutIn());
    }

    IEnumerator _FadeOutIn() {
      gameObject.GetComponent<Animator>().Play("FadeOut");
      yield return new WaitForSeconds(0.5f);
      gameObject.GetComponent<Animator>().Play("FadeIn");
    }
  }
}
