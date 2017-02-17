using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace STG
{
    public class STGMainManager : MonoBehaviour
    {
        GameObject trans;

        float activeTime;
        float active;

        void Start()
        {
            activeTime = 5;
            active = 0;

            //trans = GameObject.FindGameObjectWithTag("Transition Handler");
        }


        void Update()
        {
            if (STGPlayer.isDead || GameTime.isTimeUp || STGBoss.isDead)
            {
                SoundMgr.StopBgm();
                active += Time.deltaTime;
                if (active >= activeTime)
                {
                    active = 0;
                    var menuSelector = GetComponent<MSMM.MenuSelector>();
                    STG.SelectedGameModeSTG.SetMode(0);
                    SceneManager.LoadScene("InvaderResult");
                    SoundMgr.StopBgm();
                }
            }

            if (Input.GetAxis("Option") == 1 && Input.GetAxis("Pause") == 1)
            {
                SoundMgr.StopBgm();
            }
        }
    }
}

