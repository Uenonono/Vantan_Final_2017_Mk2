using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ToppingFullCustom
{
    public class MainTitleManager : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetAxis("BottomGreen") == 1)
            {
                var menuSelector = GetComponent<ToppingFullCustom.MenuSelector>();
                var index = menuSelector.GetCurrentSelectedIndex();
                if (index == 0)
                {
                    SceneManager.LoadScene("UDCTitle");
                    menuSelector.Reset();
                }

                if (index == 1)
                {
                    SceneManager.LoadScene("InvaderTitle");
                    menuSelector.Reset();
                    STGGameState.SetState(0);
                }
            }
        }
    }
}
