using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UDCommand {
  public class TimeBar : MonoBehaviour {

    [SerializeField]
    private Color start = Color.green;

    [SerializeField]
    private Color end = Color.red;

    [SerializeField]
    private UDCommand.GameManager manager = null;


    private RectTransform rt;
    private Vector2 initialOffsetMin;
    private Vector2 initialOffsetMax;

    private Image barImage;

    void Start() {
      if (manager == null) {
        Debug.LogError("Attach Game Manager!");
      }

      rt = gameObject.GetComponent<RectTransform>();
      initialOffsetMax = rt.offsetMax;
      initialOffsetMin = rt.offsetMin;

      barImage = gameObject.GetComponent<Image>();
    }

    void Update() {
      barImage.color = Color.Lerp(end, start, manager.GetCurrentTime() / 60.0f);
      
      rt.offsetMax = Vector2.Lerp(new Vector2(-500, initialOffsetMax.y),initialOffsetMax, manager.GetCurrentTime() / 60.0f);
    }

  }
}
