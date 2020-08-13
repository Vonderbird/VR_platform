//////////////////////////////////////////////////////
// 
// desc:    Option Menu for object that is selected
//          will pops up on your vr controller
//
// author:  amirardroudi
//
//////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class OptionMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean OpenOptionAction;
    public Hand rightHand;
    public Hand leftHand;
    
    public Transform SelectedObject;
    public bool OptionMenuIsVisible = false;

    public Transform headset;
    public Pointer pointerRef;
    public Button deleteButton;
    public Slider gravitySlider;

    private Quaternion orginalRotation;

    private float currentDisControllers;
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
        OpenOptionAction.AddOnChangeListener(ActivateOptionMenu,  rightHand.handType);
    }

    private void OnDisable()
    {
        if(OpenOptionAction != null)
            OpenOptionAction.RemoveOnChangeListener(ActivateOptionMenu, rightHand.handType);
    }

    private void Update()
    {
        // set rotation of option menu related to camera
        UpdateRotation();
        SelectedObject = pointerRef.GetHitedObject();
        
        Debug.Log("Distance of controllers : " + UpdateDisControllers());
    }

    // ================== CALL WHEN CLICK ON MENU BUTTON CONTROLLER ==================   
    private void ActivateOptionMenu(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSources, bool newValue)
    {
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

    private float UpdateDisControllers()
    {
        float distance = Vector3.SqrMagnitude(leftHand.transform.position - rightHand.transform.position);
        
        return distance;
    }
    
    // ====================== Object functions ======================  
    
    private void DestroyObject()
    {
        var options = SelectedObject.GetComponent<Interactable>();
        if(options != null)
            options.DestroyObject();
    }

    private void ScaleObject()
    {
        // if both controller grab pressed
            // cash distance of controllers when grab on both controllers pressed
                // if cashedDistance < currentDisControllers
                    //scale object
                    
        
    }
    
    
    
}
