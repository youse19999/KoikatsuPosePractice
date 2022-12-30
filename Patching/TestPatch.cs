using CharaStudioModdingLibrary.Test.TestGame.Dev;
using HarmonyLib;
using Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharaStudioModdingLibrary.Patching
{
    [HarmonyPatch(typeof(AnimeList), "OnSelect")]
    public class OnSelectHook
    {
        [HarmonyPostfix]
        public static void Postfix(AnimeList __instance,int _no)
        {
            MPCharCtrl myvalue = Traverse.Create(__instance).Field("mpCharCtrl").GetValue() as MPCharCtrl;
            ModdingLibrary.m.mpCharCtrl = myvalue;
        }
    }
    [HarmonyPatch(typeof(MPTextCtrl), "OnEndEditText")]
    public class Harmony_WindowStack
    {
        [HarmonyPostfix]
        public static void Postfix(MPTextCtrl __instance, int _idx, string _text)
        {
            string[] commands = _text.Split(' ');
            if (_idx == 0)
            {
                if (commands[0] == "c")
                {
                    if (__instance.ociText.textInfo.textInfos.Length == 2)
                    {
                        if (commands[1] == "debug")//テスト。ほとんど意味なし
                        {
                            PrintToText(__instance, "welcome to debug\na\na\na");
                        }
                        if(commands[1] == "poser")
                        {
                            TextComponent.TextInfo textInfo = __instance.ociText.TextComponent.textInfos.SafeGet(1);
                            ModdingLibrary.m.debug_text_cha1 = GameObject.Instantiate(textInfo.text,textInfo.rectTransform);
                            ModdingLibrary.m.debug_text_cha2 = GameObject.Instantiate(textInfo.text, textInfo.rectTransform);
                            ModdingLibrary.m.PlayGame();
                            PrintToText(__instance,"Have nice day");
                        }
                        if (commands[1] == "dumpanim")//アニメ　ダンプする。c dumpanim [path]
                        {
                            string dumptext = "";
                            try
                            {
                                foreach (KeyValuePair<int, Info.GroupInfo> keyValuePair in Singleton<Info>.Instance.dicAGroupCategory)
                                {
                                    dumptext += "[カテゴリー]:" + keyValuePair.Value.name + "\n";
                                    foreach (KeyValuePair<int, string> _keyValuePair in Singleton<Info>.Instance.dicAGroupCategory[keyValuePair.Key].dicCategory)
                                    {
                                        dumptext += "-[グループ]:" + _keyValuePair.Value + "\n";
                                        foreach (KeyValuePair<int, Info.AnimeLoadInfo> __keyValuePair in Singleton<Info>.Instance.dicAnimeLoadInfo[keyValuePair.Key][_keyValuePair.Key])
                                        {
                                            dumptext += "--[アニメ]:" + __keyValuePair.Value.name + "\n";
                                        }
                                    }
                                }
                            }catch(Exception ex)
                            {

                            }
                            System.IO.File.WriteAllText(commands[2],dumptext);
                        }
                        if(commands[1] == "fps")//没
                        {
                            ModdingLibrary.fpsgame.starting = true;
                        }
                    }
                    else
                    {
                        PrintToText(__instance, "ERROR:This text box dont have two input.\nUse window5(ウィンドウ5)",true);
                    }
                }
            }
        }
        
        public static void PrintToText(MPTextCtrl __instance, string _text,bool error = false)
        {

                if (error == true)
                {
                    __instance.ociText.textInfo.textInfos[0].text = _text;
                    TextComponent.TextInfo _textInfo = __instance.ociText.TextComponent.textInfos.SafeGet(0);
                    if (_textInfo != null)
                    {
                        _textInfo.msg = _text;
                    }
                    return;
                }
                __instance.ociText.textInfo.textInfos[1].text = _text;
                TextComponent.TextInfo textInfo = __instance.ociText.TextComponent.textInfos.SafeGet(1);
                if (textInfo != null)
                {
                    //textInfo.text.rectTransform.localScale = new Vector3(15,15,1);
                    textInfo.msg = textInfo.text.text + textInfo.text.transform.localScale.ToString();
                }
        }
     }
}
