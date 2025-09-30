using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class UtilityManager : MonoBehaviour
{
    public static UtilityManager Instance;
    public GamePrefabs gamePrefabs;
    public string SelectedUtility;
    public List<int> unusedIndices = new List<int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


        LoadUnusedIndices();
    }

    [ContextMenu("OnClick_SelectUtility")]
    public void OnClick_SelectUtility()
    {
        GetRandomUtilityTitle();
    }
    public string GetRandomUtilityTitle()
    {
        if (gamePrefabs.UtilityDataInfo == null || gamePrefabs.UtilityDataInfo.Count == 0)
            return null;

        // If all used, reset
        if (unusedIndices.Count == 0)
        {
            for (int i = 0; i < gamePrefabs.UtilityDataInfo.Count; i++)
                unusedIndices.Add(i);
        }

        // Pick a random index from unused
        int randIdx = UnityEngine.Random.Range(0, unusedIndices.Count);
        int utilityIdx = unusedIndices[randIdx];
        unusedIndices.RemoveAt(randIdx);

        // Save unused list back to PlayerPrefs
        SaveUnusedIndices();

       SelectedUtility= gamePrefabs.UtilityDataInfo[utilityIdx].name;
        return SelectedUtility;
    }
    private void SaveUnusedIndices()
    {
        // Convert list to string (comma-separated)
        string data = string.Join(",", unusedIndices);
        PlayerPrefs.SetString(EventVariables.PlayerPrefsKey, data);
        PlayerPrefs.Save();
    }

    public void LoadUnusedIndices()
    {
        if (PlayerPrefs.HasKey(EventVariables.PlayerPrefsKey))
        {
            string data = PlayerPrefs.GetString(EventVariables.PlayerPrefsKey);
            if (!string.IsNullOrEmpty(data))
            {
                string[] split = data.Split(',');
                unusedIndices.Clear();
                foreach (string s in split)
                {
                    if (int.TryParse(s, out int idx))
                        unusedIndices.Add(idx);
                }
            }
        }

        // If still empty (first run or reset), fill fresh
        if (unusedIndices.Count == 0)
        {
            for (int i = 0; i < gamePrefabs.UtilityDataInfo.Count; i++)
                unusedIndices.Add(i);
        }
    }

}
