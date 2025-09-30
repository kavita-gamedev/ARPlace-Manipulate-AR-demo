using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum LookMode
{
    None,
    Camera
}

[RequireComponent(typeof(ARPlaneManager))]
public class ObjectPlacement : MonoBehaviour
{
    private ARPlaneManager planeManager;

    [Header("Placement Settings")]
    [SerializeField] private GameObject objectPlacer;
    //[SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private Vector3 offset = new Vector3(0, 0.01f, 0); // tiny lift to avoid clipping
    [SerializeField] private Vector3 scale = Vector3.one;
    // [SerializeField] private LookMode lookMode = LookMode.Camera;

    private bool isPlaced;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();

        if (objectPlacer != null)
        {
            objectPlacer.SetActive(false);
            objectPlacer.transform.localScale = scale;
        }
        else
        {
            Debug.LogWarning($"{nameof(ObjectPlacement)}: Object Placer is not assigned.");
        }
        // MoveEnviroment();

        Debug.Log("ARSession state = " + ARSession.state);

    }


    /// <summary>
    /// Called when AR planes are updated.
    /// </summary>
    public void OnARTrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> eventArgs)
    {
        if (isPlaced) return; // avoid multiple placements
        if (eventArgs.updated == null || eventArgs.updated.Count == 0) return;

        var plane = eventArgs.updated[0];
        Debug.Log("x = "+plane.size.x +"y = "+plane.size.y);
        if (plane.size.x > 4f && plane.size.y > 4f)
        {
             PlaceObject(plane.center);
             TogglePlaneDetection(false);
            isPlaced = true;
        }

       

        if (environmentRoot != null)
        {
            //MoveEnviroment();
        }
    }

    private void PlaceObject(Vector3 position)
    {
        Debug.Log("Placing object at: " + position);
        objectPlacer.transform.position = position + offset;
        objectPlacer.transform.LookAt(Camera.main.transform.position);
        objectPlacer.SetActive(true);
    }

    // public void TogglePlaneDetection(bool enabled)
    // {
    //     planeManager.enabled = enabled;
    //     SetAllPlanesActive(enabled);
    // }

    // private void SetAllPlanesActive(bool active)
    // {
    //     foreach (var plane in planeManager.trackables)
    //     {
    //         plane.gameObject.SetActive(active);
    //     }
    // }


    public void TogglePlaneDetection(bool enabled)
    {
        if (planeManager == null) return;

        planeManager.enabled = enabled;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(enabled);

            // Hide plane visuals when detection is off
            var meshRenderer = plane.GetComponent<MeshRenderer>();
            if (meshRenderer != null && !enabled)
            {
                meshRenderer.enabled = false;
            }

            // Optional: disable ARPlaneMeshVisualizer
            var visualizer = plane.GetComponent<ARPlaneMeshVisualizer>();
            if (visualizer != null)
            {
                visualizer.enabled = enabled;
            }
        }
    }

    [Header("Tutorial Camera Settings")]
   
    [Header("Reference Transforms")]
    public Transform arCamera;             // XR Origin → Camera Offset → Main Camera
    public Transform tutorialCameraPos;    // Your reference view
    public Transform environmentRoot;      // Parent of all environment objects (capsule, landscape, etc.)
      public float distanceInFront = -2f;   // How far in front of the user
    public float heightOffset = 0f;    
    //  void MoveEnviroment()
    // {
    //     if (arCamera == null || environmentRoot == null)
    //     {
    //         Debug.LogError("ARTutorialPlacer: Missing references!");
    //         return;
    //     }

    //     // Place environment in front of the camera
    //     Vector3 forward = arCamera.forward;
    //     forward.y = 0; // Keep it level (ignore tilt up/down)
    //     forward.Normalize();

    //     Vector3 targetPos = arCamera.position + forward * distanceInFront;
    //     targetPos.y += heightOffset;

    //     environmentRoot.position = targetPos;
    //     environmentRoot.rotation = Quaternion.LookRotation(forward, Vector3.up);

    //     Debug.Log("✅ Environment placed in front of user for tutorial.");
    
    // }
}