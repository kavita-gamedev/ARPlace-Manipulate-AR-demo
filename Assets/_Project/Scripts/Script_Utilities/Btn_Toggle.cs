using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UITween))]
public class Btn_Toggle : MonoBehaviour
{
    public Transform t_From;
    public Transform t_To;
    public GameObject obj_Btn;
    public GameObject btn_ToggleOn;
    public GameObject btn_ToggleOff;

    private UITween uiTween;

    private bool isOn;
    private int n_State = 0;
    private string s_StateName;

    void Awake()
    {
        uiTween = GetComponent<UITween>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateState()
    {

        Game.SetToggleStatus(s_StateName, n_State);
        Toggle();
    }

public void Toggle()
    {

        Debug.Log("Toggle state" + n_State);
        if (n_State == (int)Game.ToggleState.On)
        {
            Debug.Log("toggle ison true" + n_State);
            isOn = true;
            n_State = (int)Game.ToggleState.Off;

            btn_ToggleOn.SetActive(true); //login
            btn_ToggleOff.SetActive(false); // logout
            uiTween.posFrom = t_To;
            uiTween.posTo = t_From;
            LTDescr LTween = LeanTween.move(obj_Btn, uiTween.posTo, uiTween.tweenDuration).setEaseLinear();
        }
        else
        {
            Debug.Log("toggle ison false" + n_State);
            isOn = false;
            n_State = (int)Game.ToggleState.On;
            btn_ToggleOn.SetActive(false); //login
            btn_ToggleOff.SetActive(true); // logout
            uiTween.posFrom = t_From;
            uiTween.posTo = t_To;
            LTDescr soundLTween = LeanTween.move(obj_Btn, uiTween.posTo, uiTween.tweenDuration).setEaseLinear();
        }
    }

    public void Updateslider()
    {

        Game.SetToggleStatus(s_StateName, n_State);
        Toggle();
    }

    public void CheckState(string stateName)
    {
        s_StateName = stateName;

        n_State = Game.GetToggleStatus(s_StateName);
        Debug.Log("CheckState toggleStateName" + s_StateName + "state" + n_State);
        Toggle();
    }

    public void SetBtnStateOn()
    {
        n_State = (int)Game.ToggleState.On;
        Toggle();
    }

    public void SetBtnStateOff()
    {
        n_State = (int)Game.ToggleState.Off;
        Toggle();
    }
}
