using Illusion.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CharaStudioModdingLibrary.Library.System.Chara.CharaEnum;

namespace CharaStudioModdingLibrary.Library.System.Chara
{
    class CharaManager
    {
        public static ChaFileControl[] GetAllCharactor(Gender gender)
        {
            FolderAssist folderAssist = new FolderAssist();
            string[] searchPattern = new string[]
            {
            "*.png"
            };
            string folder = "chara/female/";
            if (gender == Gender.FEMALE)
            {
                folder = UserData.Path + "chara/female/";
            }
            else
            {
                if (gender == Gender.MALE)
                {
                    folder = UserData.Path + "chara/male/";
                }
            }
            folderAssist.CreateFolderInfoEx(folder, searchPattern, true);
            List<string> list = (from n in folderAssist.lstFile.Shuffle<FolderAssist.FileInfo>() select n.FullPath).ToList<string>();
            List<ChaFileControl> list2 = new List<ChaFileControl>();
            for (int i = 0; i < list.Count; i++)
            {
                ChaFileControl chaFileControl = new ChaFileControl();
                if (chaFileControl.LoadCharaFile(list[i], 1, true, true) && chaFileControl.parameter.sex != 0)
                {
                    list2.Add(chaFileControl);
                }
            }
            return list2.ToArray(); ;
        }
    }
}
