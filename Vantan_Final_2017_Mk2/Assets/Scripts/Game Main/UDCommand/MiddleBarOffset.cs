using UnityEngine;
using System.Collections;

namespace UDCommand {
  public class MiddleBarOffset : MonoBehaviour {

    [SerializeField]
    private GameObject rope = null;

    void Start() {
      if (rope == null) {
        Debug.Log("Rope Not Attached!!!");
      }
    }

    void Update() {
      transform.position = rope.transform.position;
    }
  }
}