using UnityEngine;
using System.Collections;
using UDC = UDCommand;

namespace UDCommand {
  public class RescaleByState : MonoBehaviour {

    [SerializeField]
    private Vector2 neutralScale = new Vector2(50, 50);

    [SerializeField]
    private Vector2 highlightedScale = new Vector2(75, 75);

    private UDC.CommandState _currentState;

    public UDC.CommandState currentState {
      get { return _currentState; }
      set { _currentState = value; }
    }

    void Update() {
      if (currentState == UDC.CommandState.Neutral) {
        GetComponent<RectTransform>().sizeDelta = neutralScale;
      }
      else {
        GetComponent<RectTransform>().sizeDelta = highlightedScale;
      }
    }
  }
}
