using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace STG
{
    public class STGResultManager : MonoBehaviour
    {
        GameObject trans;

        void Start()
        {
            SoundMgr.SoundLoadBgm("Result_A", "Invader/Result_A");
            SoundMgr.PlayBgm("Result_A");

            trans = GameObject.FindGameObjectWithTag("Transition Handler");
        }


        void Update()
        {
            if (Input.GetAxis("BottomBlue") == 1)
            {
                SoundMgr.StopBgm();
                var menuSelector = GetComponent<MSMM.MenuSelector>();
                STG.SelectedGameModeSTG.SetMode(0);
                trans.GetComponent<MSMM.Transition>().LoadScene("InvaderMain");
                menuSelector.Reset();
            }

            if (Input.GetAxis("BottomYellow") == 1)
            {
                SoundMgr.StopBgm();
                var menuSelector = GetComponent<MSMM.MenuSelector>();
                STG.SelectedGameModeSTG.SetMode(0);
                trans.GetComponent<MSMM.Transition>().LoadScene("MainTitle");
                menuSelector.Reset();
            }


            if (Input.GetAxis("Option") == 1 && Input.GetAxis("Pause") == 1)
            {
                SoundMgr.StopBgm();
            }
        }
    }
}