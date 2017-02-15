using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UDCommand {
  public class TextAnimation : MonoBehaviour {

    [SerializeField]
    GameObject[] texts = null;
    bool[] textActive = new bool[4];

    float timeCount;

    private void Start() {
      Reset();
    }

    private void Update() {
      if (!textActive[0]) {
        Activate(0);
      }

      if (!textActive[1] && timeCount >= 0.5f) {
        Activate(1);
      }

      if (!textActive[2] && timeCount >= 1.0f) {
        Activate(2);
      }

      if (!textActive[3] && timeCount >= 1.5f) {
        Activate(3);
      }

      if (!textActive[3]) {
        timeCount += Time.deltaTime;
      }
    }

    public void Reset() {
      timeCount = 0;
      for (int i = 0; i < 4; i++) {
        textActive[i] = false;
      }
    }

    private void Activate(int index) {
      texts[index].GetComponent<Animator>().Play("TextMove");
      textActive[index] = true;
      StartCoroutine(Randomize(index));
    }

    IEnumerator Randomize(int index) {
      while (true) {
        texts[index].transform.localPosition = new Vector3(1600, Random.Range(-400.0f, 400.0f));
        yield return new WaitForSeconds(2);
      }
    }

    private void Deactivate() {
      for (int i = 0; i < 4; i++) {
        texts[i].GetComponent<Animator>().Play("TextIdle");
      }
    }
  }
}
