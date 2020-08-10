//////////////////////////////////////////////////////
// desc:    Option Menu for object that is selected
//          will pops up on your vr controller
//
// author:  amirardroudi
//////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class OptionMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean OpenOptionAction;
    public GameObject SelectedObject;
    public bool OptionMenuIsVisible = false;

    private void OnEnable()
    {
        if (OpenOptionAction == null)
        {
            Debug.LogError("<b>[StreamVR Interaction]</b> No open OptionMenu action assigned!");
            return;
        }
        OpenOptionAction.AddOnChangeListener(ActiveOptionMenu,SteamVR_Input_Sources.Any);
    }

    private void OnDisable()
    {
        if(OpenOptionAction != null)
            OpenOptionAction.RemoveOnChangeListener(ActiveOptionMenu,SteamVR_Input_Sources.Any);
    }

    private void ActiveOptionMenu(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSources, bool newValue)
    {
        this.GetComponent<Canvas>().enabled = newValue;
        if (newValue)
        {
            SyncOptionToObject();
        }

        Debug.Log("showoption button status: " + newValue);
    }
    
    private void SyncOptionToObject()
    {
        
    }
    
    public void DestroyObject()
    {
        SelectedObject.GetComponent<Interactable>().DestroyObject();
    }
    
}
