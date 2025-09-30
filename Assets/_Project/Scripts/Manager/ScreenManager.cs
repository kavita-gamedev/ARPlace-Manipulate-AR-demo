using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    //Local reference for initiliasing
    private static ScreenManager _instance;
    public AppDataPrefab AppDataPrefab;
     public GamePrefabs GameDataPrefab;
    public List<BaseUIScreen> uiScreens;


    //Property
    public static ScreenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScreenManager>();
            }
            return _instance;
        }
    }

    public List<BaseUIScreen> activatedScreens;
    
    void Awake()
    {
        DeactivateAllScreens();
    }

    void Start()
    {

    }

    /// <summary>
    /// Activates the screen.
    /// </summary>
    /// <param name="iScreen">I screen.</param>
    /// 
   
    public void ActivateScreen(BaseUIScreen iScreen)
    {
        if (!activatedScreens.Contains((BaseUIScreen)iScreen))
        {
            activatedScreens.Add((BaseUIScreen)iScreen);
        }
        iScreen.Activate();
    }

    /// <summary>
    /// Activates the screen.
    /// </summary>
    public void ActivateScreen<T>() where T:IScreen
    {
        IScreen iScreen = uiScreens.Find(t => t.GetType().Name == typeof(T).Name);
        if (!activatedScreens.Contains((BaseUIScreen)iScreen))
        {
            activatedScreens.Add((BaseUIScreen)iScreen);
        }
        iScreen.Activate();
    }

    /// <summary>
    /// Deactivates the screen.
    /// </summary>
    public void DeactivateScreen<T>() where T:IScreen
    {
        IScreen iScreen = uiScreens.Find(t => t.GetType().Name == typeof(T).Name);
        StartCoroutine(DelayToDeactivate(iScreen));
    }

    //Delay for transistion
    private IEnumerator DelayToDeactivate(IScreen s)
    {
        yield return new WaitForSeconds(0);
        s.Deactivate();
    }

    //Gets the T screen's BaseUIScreen componen
    internal T GetScreen<T>() where T : BaseUIScreen
    {
        return (T)uiScreens.Find(t => t.GetType().Name == typeof(T).Name);
    }

    //Gets the T screen's BaseUIScreen componen
    internal void RemoveScreen(BaseUIScreen iScreen)
    {
        if (activatedScreens.Count > 0 && activatedScreens.Contains(iScreen))
        {
            activatedScreens.Remove(iScreen);
        }

        //return uiScreens.Find(t => t.GetType().Name == typeof(T).Name);
    }

    void Update()
    {
        // Debug.Log("Input.GetKeyDown"+Input.GetKeyDown(KeyCode.Escape));
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape___________" + activatedScreens.Count);
            if (activatedScreens.Count > 0)
            {
                Debug.Log("Escape___________" + activatedScreens[activatedScreens.Count - 1].name);
                activatedScreens[activatedScreens.Count - 1].DeviceBackButtonPressed();
            }
        }

        
    }

    public void DeactivateAllScreens()
    {
        for (int i = 0; i < uiScreens.Count; i++)
        {
            uiScreens[i].Deactivate();
        } 
    }

    public void LogoutGameActions()
    {
        DeactivateAllScreens();
    }

    void OnDestroy()
    {
        _instance = null;
    }
}
