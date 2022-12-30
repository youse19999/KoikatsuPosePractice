using BepInEx;
using CharaStudioModdingLibrary.Test.TestGame.Dev;
using HarmonyLib;
using Illusion.Extensions;
using KoikatsuGameEngine.Test.TestGame.Dev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static CharaStudioModdingLibrary.Library.System.Chara.CharaEnum;

namespace CharaStudioModdingLibrary
{
    [BepInPlugin("monaca.moddinglibrary", "moddinglibrary", "1.0.0")]

    public class ModdingLibrary:BaseUnityPlugin
    {
        public static MainGame m = new MainGame();
        public static FPSGAME fpsgame = new FPSGAME();
        public ModdingLibrary()
        {
            Harmony harmony = new Harmony("com.harmony.rimworld.example");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        public void Update()
        {
            //TutorialDialog.Create("ログ", "", "a");
            m.Update();
            fpsgame.Update();
        }
    }
}
