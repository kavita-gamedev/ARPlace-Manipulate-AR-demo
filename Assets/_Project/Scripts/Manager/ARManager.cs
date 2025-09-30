using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
public class ARManager : MonoBehaviour
{
    public static ARManager Instance;
    public bool canrotate = false;
    public GameObject PlacedObj;
    ARObjectManipulator objectManipulator;
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }

        canrotate = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(checkSupport());
        
    }
    // public void Setobject(GameObject obj)
    // {
    //     objectManipulator = obj.GetComponent<ARObjectManipulator>();
    // }
     private int currentIndex = 0;
    // public void NextObject()
    // {
    //     if (objectManipulator.listSpawnObj.Count == 0) return;

    //     currentIndex++;
    //     if (currentIndex >= objectManipulator.listSpawnObj.Count)
    //         currentIndex = 0;


    //         Debug.Log("NextObject"+currentIndex);

    //     objectManipulator.RotateParentToChild(currentIndex);
    // }

    // public void PreviousObject()
    // {
    //     if (objectManipulator.listSpawnObj.Count == 0) return;

    //     currentIndex--;
    //     if (currentIndex < 0)
    //         currentIndex = objectManipulator.listSpawnObj.Count - 1;

    //     Debug.Log("PreviousObject"+currentIndex);
    //     objectManipulator.RotateParentToChild(currentIndex);
    // }

    public void Reset()
    {
        currentIndex = 0;
     }
    IEnumerator checkSupport()
    {
        Debug.Log("Checking AR support...");

        // Start async check
        yield return ARSession.CheckAvailability();

        switch (ARSession.state)
        {
            case ARSessionState.None:
                Debug.Log("AR Session state: None");
                break;

            case ARSessionState.Unsupported:
                Debug.LogError("❌ AR is NOT supported on this device.");
                break;

            case ARSessionState.CheckingAvailability:
                Debug.Log("Checking AR availability...");
                break;

            case ARSessionState.NeedsInstall:
                Debug.Log("⚠️ AR software needs to be installed (Google Play Services for AR).");
                break;

            case ARSessionState.Installing:
                Debug.Log("Installing AR software...");
                break;

            case ARSessionState.Ready:
                Debug.Log("✅ AR is supported and ready.");
                break;

            case ARSessionState.SessionInitializing:
                Debug.Log("AR session is initializing...");
                break;

            case ARSessionState.SessionTracking:
                Debug.Log("✅ AR session is running and tracking!");
                break;

            default:
                Debug.Log("Unknown AR state: " + ARSession.state);
                break;
        }
    }
    
    public void lOadGamePlay()
    {

        Debug.Log("@@@@c lOadGamePlay " );
        SceneManager.LoadScene((int)Game.Scenes.GamePlay);
    }
    
  
}
