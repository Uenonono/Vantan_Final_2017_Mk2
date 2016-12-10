using UnityEngine;
using System.Collections;
using System;

namespace UDCommand {
  public class PositionByQuantity : MonoBehaviour {

    private int _index;

    public int index {
      get { return _index; }
      set { _index = value; }
    }

    private int totalChildren;

    void Update() {
      UpdatePosition();
    }

    private void UpdatePosition() {
      int xPos = 0;
      int yPos = 0;

      bool isEven = CheckEvenTotal();

      if (index < 5) {
        yPos = 200;
      }
      else {
        yPos = -200;
      }

      if (totalChildren < 6) {
        if (isEven) {
          var halfTotal = totalChildren / 2;
          var firstX = -250 * halfTotal + 125;
          xPos = firstX + 250 * index;
        }
        else {
          var halfTotalRounded = (totalChildren - 1) / 2;
          var firstX = -250 * halfTotalRounded;
          xPos = firstX + 250 * index;
        }
      }
      else {
        if (index < 5) {
          var firstX = -250 * 2;
          xPos = firstX + 250 * index;
        }
        else {
          if (!isEven) {
            var halfTotal = (totalChildren - 5) / 2;
            var firstX = -250 * halfTotal + 125;
            xPos = firstX + 250 * (index - 5);
          }
          else {
            var halfTotalRounded = ((totalChildren - 5) - 1) / 2;
            var firstX = -250 * halfTotalRounded;
            xPos = firstX + 250 * (index - 5);
          }
        }
      }

      transform.localPosition = new Vector3(xPos, yPos);
    }

    private bool CheckEvenTotal() {
      if (totalChildren % 2 == 0) {
        return true;
      }
      else return false;
    }

    public void SetTotalChildrenFromParent() {
      totalChildren = transform.parent.childCount;
    }
  }
}
