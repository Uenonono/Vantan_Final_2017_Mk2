using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace STG
{
    public class STGTitleManager : MonoBehaviour
    {
        GameObject trans;


        void Start()
        {
            SoundMgr.SoundLoadSe("Start", "Invader/Start");
            SoundMgr.SoundLoadBgm("Window_Rule", "Invader/Window_Rule");
            SoundMgr.PlayBgm("Window_Rule", 0.3f);

            trans = GameObject.FindGameObjectWithTag("Transition Handler");
        }


        void Update()
        {
            if (Input.GetAxis("BottomRed") == 1 || Input.GetAxis("BottomGreen") == 1 || Input.GetAxis("BottomBlue") == 1 || Input.GetAxis("BottomYellow") == 1)
            {
                var menuSelector = GetComponent<MSMM.MenuSelector>();
                STG.SelectedGameModeSTG.SetMode(0);
                trans.GetComponent<MSMM.Transition>().LoadScene("InvaderRule");
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
