using Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace KoikatsuGameEngine.Test.TestGame.Dev
{
    public class FPSGAME
    {
        public bool starting = false;
        public bool loading = false;
        public bool update = false;
        public static OCICamera charactorcamera;
        public ChaControl cha1 = null;
        public ChaControl cha2 = null;
        public void Update()
        {
            if(starting == true)
            {
                starting = false;
                Singleton<Studio.Studio>.Instance.AddFemale(@"F:\koikatsu\koikatsu\UserData\chara\female\KoikatsuSun_F_20221225151658454_齊藤 美奈.png");
                int foundchara = 0;
                foreach (ChaControl cha in Resources.FindObjectsOfTypeAll(typeof(ChaControl)))
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
                Singleton<Studio.Studio>.Instance.AddCamera();
                loading = true;
            }
            if(loading == true)
            {
                loading = false;
                update = true;
            }
            if(update == true)
            {
                //Camera.main.transform.position = cha1.objHead.transform.position;
                //Camera.main.transform.rotation = cha1.objHead.transform.rotation;
                if (charactorcamera != null)
                {
                    Singleton<Studio.Studio>.Instance.ChangeCamera(charactorcamera, true, false);
                }
            }
        }
    }
}
