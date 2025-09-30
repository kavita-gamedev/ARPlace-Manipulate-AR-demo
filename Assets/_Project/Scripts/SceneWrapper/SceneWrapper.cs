using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWrapper : MonoBehaviour
{

    public BaseUIScreen[] screensToActivate;

    void Start()
    {
        for (int i = 0; i < screensToActivate.Length; i++)
        {
            ScreenManager.Instance.ActivateScreen(screensToActivate[i]);
        }
    }
	
}
