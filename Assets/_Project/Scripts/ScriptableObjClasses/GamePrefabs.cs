using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.a ;

[CreateAssetMenu(menuName = "Custom Objects/Prefabs Container")]
public class GamePrefabs : ScriptableObject
{

    public int SelectedUtility;
    #region Languade Data Info
    [System.Serializable]
    public class UtilityData
    {
        public int id;
        public string name;
        public GameObject obj;
        public Sprite img;
        public List<ScenarioData> scenarioDatas;
    }

    [Header("UtilityData Info")]
    public List<UtilityData> UtilityDataInfo;

    [System.Serializable]
    public class ScenarioData
    {
        public int id;                // Scenario ID
        public bool correctAnswer;    // True or False
    }


    #endregion
}
