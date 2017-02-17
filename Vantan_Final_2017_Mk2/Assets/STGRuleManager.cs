using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace STG
{
    public class STGRuleManager : MonoBehaviour
    {
        GameObject trans;


        void Start()
        {
            SoundMgr.SoundLoadSe("Start", "Invader/Start");

            trans = GameObject.FindGameObjectWithTag("Transition Handler");
        }


        void Update()
        {
            if (Input.GetAxis("BottomRed") == 1 || Input.GetAxis("BottomGreen") == 1 || Input.GetAxis("BottomBlue") == 1 || Input.GetAxis("BottomYellow") == 1)
            {
                SoundMgr.StopBgm();
                var menuSelector = GetComponent<MSMM.MenuSelector>();
                STG.SelectedGameModeSTG.SetMode(0);
                trans.GetComponent<MSMM.Transition>().LoadScene("InvaderMain");
                SoundMgr.PlaySe("Start", 6);
                menuSelector.Reset();
            }

            if (Input.GetAxis("Option") == 1 && Input.GetAxis("Pause") == 1)
            {
                SoundMgr.StopBgm();
            }
        }
    }
}

