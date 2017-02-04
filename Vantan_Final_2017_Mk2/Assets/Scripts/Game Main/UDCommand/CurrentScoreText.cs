using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UDCommand {
  public class CurrentScoreText : MonoBehaviour {

    private Text scoreText;

    void Start() {
      scoreText = gameObject.GetComponent<Text>();
    }

    void Update() {
      scoreText.text = "どっこい数：" + MSMM.RankingTempData.TempScore.ToString() + " どっこい";
    }
  }
}
