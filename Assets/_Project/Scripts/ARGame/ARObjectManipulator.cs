using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ARObjectManipulator : MonoBehaviour
{
    private Touch touchZero, touchOne;
    public Transform parentTransform;
    // public List<Transform> SpawnPositions;
    public ScenrioObj spawnPrefab;
    // public List<ScenrioObj> listSpawnObj;
    private Vector2 touchZeroPrevPos, touchOnePrevPos;
    private float prevTouchDeltaMag, touchDeltaMag, deltaMagnitudeDiff;

    [Header("Sensitivity Settings")]
    public float dragSpeed = 0.001f;        // Movement speed
    public float scaleSpeed = 0.01f;        // Pinch zoom speed
    public float rotationSpeed = 0.2f;      // Rotation speed

    void Update()
    {
        // Skip if touching UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(0) && !ARManager.Instance.canrotate)
            return;

#if UNITY_EDITOR   // 🖥️ Editor (Mouse Input)
        HandleMouseInput();
#else              // 📱 Mobile (Touch Input)
        HandleTouchInput();
#endif
    }

    public void Awake()
    {
        // SpawnScenarios();
        // RotateParentToChild(1);
    }


   
    //  public void SpawnScenarios()
    // {
    //     // Clear old spawned objects if any
    //     foreach (var obj in listSpawnObj)
    //         Destroy(obj);
    //     listSpawnObj.Clear();

    //     // Get selected utility data
    //     var utility = ScreenManager.Instance.GameDataPrefab.UtilityDataInfo.Find(u => u.id == ScreenManager.Instance.GameDataPrefab.SelectedUtility);
    //     if (utility == null)
    //     {
    //         Debug.LogWarning("Selected utility not found!");
    //         return;
    //     }
    //      List<Transform> availablePositions = new List<Transform>(SpawnPositions);

    //     int scenarioCount = utility.scenarioDatas.Count;

    //     // Spawn each scenario object
    //     foreach (var scenario in utility.scenarioDatas)
    //     {
    //         if (availablePositions.Count == 0)
    //         {
    //             Debug.LogWarning("No more available positions!");
    //             break;
    //         }

    //         int randIndex = Random.Range(0, availablePositions.Count);
    //         Transform spawnPos = availablePositions[randIndex];
    //         // Spawn the prefab at the position
    //         ScenrioObj scenarioObj = Instantiate(spawnPrefab, spawnPos.position, spawnPos.rotation, parentTransform);

    //         // Optionally, assign scenario info to the spawned object
    //         scenarioObj.SetData(scenario); // your method to set scenario data


    //         // Add to list
    //         listSpawnObj.Add(scenarioObj);
    //         availablePositions.RemoveAt(randIndex);
    //     }
    // }
    // public void RotateParentToChild(int index)
    // {
    //     Debug.Log("RotateParentToChild"+index);
    //     if (listSpawnObj.Count == 0 || index < 0 || index >= listSpawnObj.Count) return;

    //     Transform targetChild = listSpawnObj[index].transform;

    //     // Direction from child to camera
    //     Vector3 direction = Camera.main.transform.position - targetChild.position;

    //     // Only consider horizontal direction
    //     direction.y = 0;

    //     if (direction.sqrMagnitude < 0.001f) return; // avoid zero-length

    //     // Rotate parent on Y-axis only
    //     Quaternion targetRotation = Quaternion.LookRotation(-direction);
    //     transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    // }

    // --------------------------
    // Editor Controls
    // --------------------------
    void HandleMouseInput()
    {
        // Move with left mouse drag
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(transform.position).z)
            );
            transform.position = Vector3.Lerp(transform.position, pos, dragSpeed * 100f);
        }

        // Rotate with right mouse drag
        if (Input.GetMouseButton(1))
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * 10f;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * 10f;

            transform.Rotate(Camera.main.transform.up, -rotX, Space.World);   // Y-axis
            transform.Rotate(Camera.main.transform.right, rotY, Space.World);  // X-axis
        }

        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            transform.localScale *= (1 + scroll * scaleSpeed * 50f);
        }
    }

        // --------------------------
    // Touch Controls
    // --------------------------
    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            // One finger drag = rotate only X-axis
            touchZero = Input.GetTouch(0);

            if (touchZero.phase == TouchPhase.Moved)
            {
                 float rotY = touchZero.deltaPosition.x * rotationSpeed;
                transform.Rotate(Vector3.up, -rotY, Space.World);
            }
        }
        else if (Input.touchCount == 2)
        {
            // Pinch to zoom
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

            float scaleFactor = 1 + (deltaMagnitudeDiff * scaleSpeed);
            scaleFactor = Mathf.Clamp(scaleFactor, 0.5f, 2f); // adjust min/max scale
            transform.localScale *= scaleFactor;
        }
    }


}
