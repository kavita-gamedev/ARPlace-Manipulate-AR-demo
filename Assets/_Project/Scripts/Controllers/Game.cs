using System;
using UnityEngine;
public class Game
{
    [Serializable]
    public enum Scenes
    {
        Splash,
        Loading,
        Mainmenu,
        GamePlay
    }

    public enum ToggleState
    {
        On = 0,
        Off = 1
    }


    /// <summary>
    /// Sets the toggle status.
    /// </summary>
    /// <param name="stateName">State name.</param>
    /// <param name="status">Status.</param>
    public static void SetToggleStatus(string stateName, int status)
    {
        Debug.Log("stateName" + stateName + "status" + status);
        PlayerPrefs.SetInt(stateName, status);
    }
    

    /// <summary>
    /// Gets the toggle status.
    /// </summary>
    /// <returns>The toggle status.</returns>
    /// <param name="stateName">State name.</param>
    public static int GetToggleStatus(string stateName)
    {
        return PlayerPrefs.GetInt(stateName);
    }
}

