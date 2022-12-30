using BepInEx.Logging;
using CharaStudioModdingLibrary.Library.System.Chara;
using HarmonyLib;
using Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using static CharaStudioModdingLibrary.Library.System.Chara.CharaEnum;

namespace CharaStudioModdingLibrary.Test.TestGame.Dev
{
    public class MainGame : MonoBehaviour
    {
        public bool playing = false;
        public bool loading = false;
        public bool update = false;
        public bool TimerReset = true;
        public MPCharCtrl mpCharCtrl;
        public int WaitSecValue = 0;
        public int SketchTime = 30;
        public int NowSketchTime = 0;
        public ChaControl cha1 = null;
        public ChaControl cha2 = null;
        public Text debug_text_cha1;
        public Text debug_text_cha2;
        public void PlayGame()
        {
            ChaFileControl[] AllFemaleChara = CharaManager.GetAllCharactor(Gender.FEMALE);
            string chaalltext = "";
            foreach(ChaFileControl chaFile in AllFemaleChara)
            {
                chaalltext += chaFile.charaFileName + "\n";
            }
            //StartCoroutine(Minus1sec(NowSketchTime));
            playing = true;
            //TutorialDialog.Create("ログ", "", chaalltext);
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftControl))
            {
                SketchTime -= 10;
                TimerReset = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftControl))
            {
                SketchTime += 10;
                TimerReset = true;
            }
            if (TimerReset == true)
            {
                SelectPose();
                NowSketchTime = SketchTime;
                TimerReset = false;
            }
            if(playing == true)
            {
                Singleton<Studio.Studio>.Instance.AddFemale(@"F:\koikatsu\koikatsu\UserData\chara\female\KoikatsuSun_F_20221225151658454_齊藤 美奈.png");
                playing = false;
                int foundchara = 0;
                foreach(ChaControl cha in Resources.FindObjectsOfTypeAll(typeof(ChaControl)))
                {
                    foundchara += 1;
                    if (foundchara == 1)
                    {
                        cha1 = cha;
                    }
                }
                Singleton<Studio.Studio>.Instance.AddFemale(@"F:\koikatsu\koikatsu\UserData\chara\female\KoikatsuSun_F_20221225151658454_齊藤 美奈.png");
                foreach (ChaControl cha in Resources.FindObjectsOfTypeAll(typeof(ChaControl)))
                {
                    foundchara += 1;
                    if (foundchara == 2)
                    {
                        cha2 = cha;
                    }
                }
                loading = true;
            }
            if(loading == true)
            {
                //cha1.SetSiruFlags(ChaFileDefine.SiruParts.SiruKao, 1);
                loading = false;
                update = true;
                cha1.transform.Translate(3, 0, 0);
                //cha2.SetSiruFlags(ChaFileDefine.SiruParts.SiruBackDown, 1);
                //cha2.AnimPlay("");
            }
            if(update == true)
            {
                debug_text_cha1.transform.position = cha1.transform.position;
                debug_text_cha1.transform.Translate(0, cha1.objHead.transform.position.y + 0.4f, 0);
                debug_text_cha2.transform.position = cha2.transform.position;
                debug_text_cha2.transform.Translate(0, cha2.objHead.transform.position.y + 0.4f, 0);
                debug_text_cha1.text = cha1.animBody.GetCurrentAnimatorClipInfo(0)[0].clip.name +"|" + NowSketchTime.ToString() + "秒";
                debug_text_cha2.text = cha2.animBody.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                if(WaitSecValue <= 50)
                {
                    WaitSecValue += 1;
                }
                else
                {
                    WaitSecValue = 0;
                    if (NowSketchTime > 0)
                    {
                        NowSketchTime -= 1;
                    }
                    else
                    {
                        TimerReset = true;
                    }
                }
            }
        }
        public void SelectPose()
        {
            bool load = false;
            foreach (KeyValuePair<int, Studio.Info.GroupInfo> keyValuePair in Singleton<Studio.Info>.Instance.dicAGroupCategory)
            {
                foreach (KeyValuePair<int, string> _keyValuePair in Singleton<Studio.Info>.Instance.dicAGroupCategory[keyValuePair.Key].dicCategory)
                {
                    foreach (KeyValuePair<int, Studio.Info.AnimeLoadInfo> __keyValuePair in Singleton<Studio.Info>.Instance.dicAnimeLoadInfo[keyValuePair.Key][_keyValuePair.Key])
                    {
                        if (UnityEngine.Random.Range(0, 100) == 1)
                        {
                            if (load == false)
                            {
                                mpCharCtrl.LoadAnime(AnimeGroupList.SEX.Unknown, keyValuePair.Key, _keyValuePair.Key, __keyValuePair.Key);
                                load = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }
        /*
        public void Minus1sec(int sec)
        {
            yield return new WaitForSeconds(1);
            sec -= 1;
            if (sec >= 0) { 
                StartCoroutine(Minus1sec(sec));
            }
        }
        */
    }
}