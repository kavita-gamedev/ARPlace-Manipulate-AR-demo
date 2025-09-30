
using UnityEngine;


public class ScenrioObj : MonoBehaviour
{
    public int ID;
    public Material red;
    public Renderer rend;
    public Material green;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetData(GamePrefabs.ScenarioData data)
    {
        Debug.Log("SetData ScenrioObj id"+data.id + "correctAnswer" +data.correctAnswer);
        ID = data.id;
        if (data.correctAnswer)
        {
            rend.material = green;
        }
        else
        {
            rend.material = red;
        }
    }
}
