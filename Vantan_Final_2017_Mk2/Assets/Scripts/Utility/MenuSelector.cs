using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace MSMM
{
    public class MenuSelector : MonoBehaviour
    {

        [SerializeField]
        private Image[] buttons = null;

        [SerializeField]
        private string selectSEPath = "";

        [SerializeField]
        private Color neutralColor = Color.white;
        [SerializeField]
        private Color selectedColor = Color.red;

        private int selectedIndex = -1;

        private bool initialInput = false;

        private bool stickNeutral = true;

        private bool active = true;

        void Start()
        {
            if (selectSEPath != "")
            {
                SoundMgr.SoundLoadSe("SelectSE", selectSEPath);
            }
        }

        void Update()
        {
            if (active)
            {
                if (initialInput)
                {
                    if (stickNeutral)
                    {
                        if (Input.GetAxis("Horizontal") > 0.5f)
                        {
                            if (selectSEPath != "")
                            {
                                SoundMgr.PlaySe("SelectSE");
                            }
                            selectedIndex++;
                            if (selectedIndex > (buttons.Length - 1))
                            {
                                selectedIndex = 0;
                            }
                        }

                        if (Input.GetAxis("Horizontal") < -0.5f)
                        {
                            if (selectSEPath != "")
                            {
                                SoundMgr.PlaySe("SelectSE");
                            }
                            selectedIndex--;
                            if (selectedIndex < 0)
                            {
                                selectedIndex = buttons.Length - 1;
                            }
                        }
                        stickNeutral = false;
                    }
                }

                if (!initialInput)
                {
                    if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
                    {
                        if (selectSEPath != "")
                        {
                            SoundMgr.PlaySe("SelectSE");
                        }
                        selectedIndex = 0;
                        initialInput = true;
                        stickNeutral = false;
                    }
                }


                if (Input.GetAxis("Horizontal") == 0)
                {
                    stickNeutral = true;
                }

                ChangeColorBySelection(selectedIndex);
            }
        }

        private void ChangeColorBySelection(int index)
        {
            if (index >= 0)
            {
                if (buttons.Length > 0)
                {
                    foreach (Image but in buttons)
                    {
                        but.color = neutralColor;
                    }
                    buttons[index].color = selectedColor;
                }
            }
        }

        public void SetComponentActive(bool val)
        {
            active = val;
        }

        public int GetCurrentSelectedIndex()
        {
            return selectedIndex;
        }

        public void Reset()
        {
            selectedIndex = -1;
            initialInput = false;
            stickNeutral = true;
            foreach (Image but in buttons)
            {
                but.color = Color.white;
            }
            active = true;

            Score.score = 0;
            ResultScore.SScore = 0;
            ResultScore.MainScore = 0;
            STGBoss.isDead = false;
            STGPlayer.isDead = false;
        }
    }
}