using UnityEngine;
using System.Collections;
using GGFA = GGFarAway;

namespace GGFarAway {
  public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject ball = null;
    private Rigidbody ballRb;
    private GGFA.LaunchPadOffset padOffset = null;

    [SerializeField]
    private GGFA.LauncherController launcher = null;

    [SerializeField]
    private GGFA.BatterController batter = null;

    private GGFA.BallState ballState = GGFA.BallState.Standby;

    void Start() {
      ballRb = ball.GetComponent<Rigidbody>();
      padOffset = ball.GetComponent<GGFA.LaunchPadOffset>();
    }

    void Update() {
      if(ballState == GGFA.BallState.Standby) {
        if (Input.GetAxis("ActRed") >= 1.0f) {
          launcher.PlayAnimation();
          ballState = GGFA.BallState.Launching;
        }
      }

      if (ballState == GGFA.BallState.Launching) {
        if (launcher.IsAnimationDone()) {
          padOffset.isActive = false;
          ballState = GGFA.BallState.Launched;
        }
      }

      if (ballState == GGFA.BallState.Launched && (ball.transform.position.x <= 199.0f)) {
        ball.transform.Translate(Vector3.right * 0.5f);
      }
      else if(ballState == GGFA.BallState.Launched) {
        ballState = GGFA.BallState.Reflecting;
      }

      if (ballState == GGFA.BallState.Reflecting) {
        if (Input.GetAxis("ActRed") >= 1.0f) {
          batter.PlayAnimation();
        }
      }

      if (ball.GetComponent<GGFA.BallCollisionDetection>().isHit && ballState == GGFA.BallState.Reflecting) {
        Debug.Log("Hit!");
        ballRb.useGravity = true;
        ballRb.AddForce((Vector3.up + Vector3.left) * 50.0f,ForceMode.Impulse);
        ballState = GGFA.BallState.Reflected;
      }
    }
  }
}