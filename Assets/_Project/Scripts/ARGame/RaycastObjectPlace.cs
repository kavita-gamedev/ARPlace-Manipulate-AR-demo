using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RaycastObjectPlace : MonoBehaviour
{
    public ARRaycastManager raycastManager;   // Assign in inspector
    public ARPlaneManager planeManager;
    public GameObject objectToPlace;          // Assign your prefab here
    public GameObject spawnObj;
    public GameObject capsulePrefab;
    private bool isPlaced = false;            // ✅ Prevent multiple placements
    private static List<ARRaycastHit> rayHits = new List<ARRaycastHit>();

    [Header("Placement Settings")]
    private float minDistanceFromCamera = 5f; // Minimum distance from camera
    private float minPlaneSize = 4f;


    void Update()
    {
        if (isPlaced) return; // ✅ Stop scanning after placement

#if UNITY_EDITOR
        // For testing in editor with mouse
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject(Input.mousePosition);
        }
#else
        // On device: touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject(Input.GetTouch(0).position);
        }
#endif
    }

    
    void PlaceObject(Vector2 touchPosition)
    {
        if (raycastManager.Raycast(touchPosition, rayHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = rayHits[0].pose;

            Camera arCamera = Camera.main;
            Vector3 dirToObject = hitPose.position - arCamera.transform.position;
            if (dirToObject.magnitude < minDistanceFromCamera)
            {
                hitPose.position = arCamera.transform.position + dirToObject.normalized * minDistanceFromCamera;
            }

            // Instantiate the prefab
            spawnObj = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            // ARManager.Instance.Setobject(spawnObj);
            // ✅ Mark as placed, so it won’t place again
            isPlaced = true;
            TogglePlaneDetection(false);
            // PlaceRobot();
            Debug.Log("✅ Object placed at: " + hitPose.position );
        }
    }

    public void TogglePlaneDetection(bool enabled)
    {
        Debug.Log("Hide All plane");
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

    void UpdatePlaneVisibility(ARPlane plane)
    {
        var meshRenderer = plane.GetComponent<MeshRenderer>();
        var visualizer = plane.GetComponent<ARPlaneMeshVisualizer>();

        if (plane.size.x >= minPlaneSize && plane.size.y >= minPlaneSize)
        {
            if (meshRenderer != null) meshRenderer.enabled = true;
            if (visualizer != null) visualizer.enabled = true;
        }
        else
        {
            if (meshRenderer != null) meshRenderer.enabled = false;
            if (visualizer != null) visualizer.enabled = false;
        }
    }

    public void OnPlanesTrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
    {
        foreach (var plane in args.added)
            UpdatePlaneVisibility(plane);

        foreach (var plane in args.updated)
            UpdatePlaneVisibility(plane);
    }

    
    public void ResetAR()
    {
         Debug.Log("ResetAR " );
        // 1️⃣ Destroy spawned object
        if (spawnObj != null)
        {
            Destroy(spawnObj);
            spawnObj = null;
        }
        // 2️⃣ Enable plane detection and visuals
        isPlaced = false;                   // Allow placement again
        TogglePlaneDetection(true);

        Debug.Log("🔄 AR Reset: Planes visible and ready for new placement");
    }

    
}
