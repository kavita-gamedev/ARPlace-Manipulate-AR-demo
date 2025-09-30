using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RobotController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI tutorialText; // assign in inspector (text on capsule head)

    [Header("Tutorial Steps")]
    public List<string> tutorialSteps = new List<string>();
    private int currentIndex = 0;

    // Update is called once per frame
    void Update()
    {
        
    }
}
