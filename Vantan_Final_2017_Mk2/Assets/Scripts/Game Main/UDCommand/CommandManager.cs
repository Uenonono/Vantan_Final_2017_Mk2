using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UDCommand {
  public class CommandManager : MonoBehaviour {

    [SerializeField]
    Image radish = null;
    [SerializeField]
    Image colorCircle = null;
    [SerializeField]
    Image assistNumber = null;

    [SerializeField]
    GameObject radishObject = null;

    [SerializeField]
    GameObject particleObject = null;

    List<Sprite> colorCircleSprites;
    List<Sprite> numberSprites;

    void Awake() {
      if(radish == null || colorCircle == null || assistNumber == null || radishObject == null) {
        Debug.LogError("Attach the Objects!");
      }

      colorCircleSprites = new List<Sprite>();
      numberSprites = new List<Sprite>();

      colorCircleSprites.AddRange(Resources.LoadAll<Sprite>("Sprites/UDCommand/ColorCircles"));
    }

    public void SetCommand(ImageType type) {

      radish.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Radish");

      if ((int)type % 4 == 0) {
        colorCircle.sprite = colorCircleSprites[2];
      }
      else if ((int)type % 4 == 1) {
        colorCircle.sprite = colorCircleSprites[1];
      }
      else if ((int)type % 4 == 2) {
        colorCircle.sprite = colorCircleSprites[0];
      }
      else if ((int)type % 4 == 3) {
        colorCircle.sprite = colorCircleSprites[3];
      }

      switch (type) {
        case ImageType.BRed:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num1");
          break;
        case ImageType.BGreen:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num2");
          break;
        case ImageType.BBlue:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num3");
          break;
        case ImageType.BYellow:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num4");
          break;
        case ImageType.URed:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num5");
          break;
        case ImageType.UGreen:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num6");
          break;
        case ImageType.UBlue:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num7");
          break;
        case ImageType.UYellow:
          assistNumber.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Num8");
          break;
        default:
          assistNumber.gameObject.SetActive(false);
          break;
      }
    }

    public void SwapMandragora() {
      radish.sprite = Resources.Load<Sprite>("Sprites/UDCommand/Mandragora");
    }

    public GameObject GetRadish() {
      return radishObject;
    }

    public GameObject GetParticles() {
      return particleObject;
    }
  }
}
