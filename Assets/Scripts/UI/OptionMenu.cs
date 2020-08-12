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
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class OptionMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean OpenOptionAction;
    public Transform SelectedObject;
    public bool OptionMenuIsVisible = false;

    public Transform headset;
    public Pointer pointerRef;
    public Button deleteButton;
    public Slider gravitySlider;

    private Quaternion orginalRotation;

    private void Awake() 
    {
        orginalRotation = transform.rotation;    
    }
    private void OnEnable()
    {
        if (OpenOptionAction == null)
        {
            Debug.LogError("<b>[StreamVR Interaction]</b> No open OptionMenu action assigned!");
            return;
        }
        OpenOptionAction.AddOnChangeListener(ActivateOptionMenu, SteamVR_Input_Sources.RightHand);
    }

    private void OnDisable()
    {
        if(OpenOptionAction != null)
            OpenOptionAction.RemoveOnChangeListener(ActivateOptionMenu,SteamVR_Input_Sources.RightHand);
    }

    private void Update()
    {
        // set rotation of option menu related to camera
        UpdateRotation();
    }

    // ================== CALL WHEN CLICK ON MENU BUTTON CONTROLLER ==================   
    private void ActivateOptionMenu(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSources, bool newValue)
    {
        SelectedObject = pointerRef.GetHitedObject();
        GetComponent<Canvas>().enabled = newValue;
        
        if (newValue)
        {
            SyncOptionToObject();
        }

    }
    // ================================================================================
    
    private void SyncOptionToObject()
    {
        // Bind Destroy function to delete button
        deleteButton.onClick.AddListener(DestroyObject);
        
        // set slider value from mass of selected object rigidbody.mass
        if (SelectedObject.GetComponent<Rigidbody>() != null)
        {
            gravitySlider.value = SelectedObject.GetComponent<Rigidbody>().mass;    
        }

        SetOptionMenuPos();
    }
    
    // ====================== OPTION MENU POSITION ======================  
    private void SetOptionMenuPos()
    {
        transform.position = SelectedObject.position + Vector3.right * 1.5f;
    }

    // ====================== OPTION MENU POSITION ======================  
    private void UpdateRotation()
    {
        transform.rotation = headset.rotation * orginalRotation;
    }

    
    private void DestroyObject()
    {
        var options = SelectedObject.GetComponent<Interactable>();
        if(options != null)
            options.DestroyObject();
    }

    
    
}
