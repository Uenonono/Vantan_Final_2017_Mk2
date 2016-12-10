using UnityEngine;
using System.Collections;

namespace GGFarAway {
  public class LaunchPadOffset : MonoBehaviour {

    [SerializeField]
    private GameObject launchPadBall = null;

    private bool _isActive = true;

    /// <summary>
    /// Not Unity's property (Caution!)
    /// </summary>
    public bool isActive {
      get { return _isActive; }
      set { _isActive = value; }
    }

    void Update() {
      if (isActive) {
        transform.position = launchPadBall.transform.position;
      }
    }

  }
}