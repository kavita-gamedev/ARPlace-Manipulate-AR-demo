using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Globalization;


public static class MyExtensions
{
 

    public static float RoundOff(this float mValue)
    {
        return Mathf.Round(mValue * 10f) / 10f;
    }

    public static Vector3 RoundOff(this Vector3 thisVec)
    {
        float newX = Mathf.Round(thisVec.x * 10f) / 10f;
        float newY = Mathf.Round(thisVec.y * 10f) / 10f;
        float newZ = Mathf.Round(thisVec.z * 10f) / 10f;

        return new Vector3(newX, newY, newZ);
    }




    public static string ToShortString(this string inputString)
    {
        int validCharacters = 10;
        //int maxDots = 2;

        if (inputString.Length > validCharacters)
        {
            inputString = inputString.Remove(10) + "..";
        }

        return inputString;
    }

       public static int ToInt(this float num)
    {
        return (int)num;
    }

    public static Coroutine Execute(this MonoBehaviour monoBehaviour, Action action, float time)
    {
        return monoBehaviour.StartCoroutine(DelayedAction(action, time));
    }
    static IEnumerator DelayedAction(Action action, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        action();
    }
}
