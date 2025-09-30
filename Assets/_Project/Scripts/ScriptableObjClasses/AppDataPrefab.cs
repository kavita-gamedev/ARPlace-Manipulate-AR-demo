using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Custom Objects/Prefabs Container")]
public class AppDataPrefab : ScriptableObject
{

    public int selectedCompanion;

    #region Languade Data Info
    [System.Serializable]
    public class LanguageData
    {
        public int id;
        public string language;
        public string code;

    }
    public void GetLangCode(int id, Action<string> callback)
    {
        LanguageData data = LanguageDataInfo.Find(x => x.id == id);
        callback?.Invoke(data.code);
    }

    public string GetLangName(int id)
    {
        LanguageData data = LanguageDataInfo.Find(x => x.id == id);
        return data.language;
    }

    [Header("Language Data Info")]
    public List<LanguageData> LanguageDataInfo;

    #endregion
    #region Level Data Info
    [System.Serializable]
    public class LevelData
    {
        public int id;
        public string title;
        public string Body;
        public List<CompanionData> CompanionDataInfo;
         public List<TutorialData> GameTutorialDataInfo;

    }

    [System.Serializable]
    public class CompanionData
    {

        public int id;
        public Sprite img;
        public string name;

        public GameObject obj;
    }


    [Header("Level Data Info")]
    public List<LevelData> LevelDataInfo;
    public void GetLevelDataByID(int id, Action<LevelData> callback)
    {
        LevelData data = LevelDataInfo.Find(x => x.id == id);
        callback?.Invoke(data);
    }

    #endregion
    #region Tutorial Data Info
    [System.Serializable]
    public class TutorialData
    {
        public int id;
        public string Title;
        public string Body;
    }

    [Header("TutorialData Info")]
    public List<TutorialData> TutorialDataInfo;
    #endregion

   

}
