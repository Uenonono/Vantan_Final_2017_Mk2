using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace MSMM
{
    public class SoftReset : MonoBehaviour
    {

        void Awake()
        {
            if (FindObjectsOfType<SoftReset>().Length != 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(transform.gameObject);
            }
        }

        void Update()
        {
            if (Input.GetAxis("Option") == 1 && Input.GetAxis("Pause") == 1)
            {
                var menuObj = GameObject.FindGameObjectsWithTag("Menu Selector");
                if (menuObj != null)
                {
                    foreach (var obj in menuObj)
                    {
                        obj.GetComponent<MSMM.MenuSelector>().Reset();
                    }
                }

                var UDC_GM_Obj = GameObject.FindGameObjectWithTag("UDC Game Manager");
                if (UDC_GM_Obj != null)
                {
                    UDC_GM_Obj.GetComponent<UDCommand.GameManager>().Reset();
                }

                if (SceneManager.GetActiveScene().name.Contains("UDC"))
                {
                    SoundMgr.StopBgm();
                }

                var STG_GM_Obj = GameObject.FindGameObjectWithTag("STG Scene Switcher");
                if (STG_GM_Obj != null)
                {
                    STG_GM_Obj.GetComponent<GameMgr>().Disable();
                }

                if (SceneManager.GetActiveScene().name.Contains("STG Scene Switcher"))
                {
                    SoundMgr.StopBgm();
                }

                SceneManager.LoadScene("MainTitle");
            }
        }
    }
}
