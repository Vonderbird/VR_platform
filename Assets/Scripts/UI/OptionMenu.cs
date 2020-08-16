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
    [Header("VR References")]
    public SteamVR_Action_Boolean OpenOptionAction;
    public SteamVR_Action_Boolean scalePadActionLeft;
    public SteamVR_Action_Boolean scalePadActionRight;
    
    public Transform leftController;
    public Transform rightController;
    public Transform headset;
    public Pointer pointerRef;
    
    [Header("UI References")]
    public Button deleteButton;
    public Slider massSlider;
    public Toggle gravityToggle;
    
    private Hand rightHand;
    private Hand leftHand;
    private Transform SelectedObject;
    private Quaternion orginalRotation;
    private float currentDisControllers;


    

    #region UnityEvents
    private void Awake() 
    {
        orginalRotation = transform.rotation;
        leftHand  = leftController.GetComponent<Hand>();
        rightHand = rightController.GetComponent<Hand>();
    }
    private void OnEnable()
    {
        if (OpenOptionAction == null)
        {
            Debug.LogError("<b>[StreamVR Interaction]</b> No open OptionMenu action assigned!");
            return;
        }
        OpenOptionAction.AddOnChangeListener(ActivateOptionMenu,  rightHand.handType);
        
        gravityToggle.onValueChanged.AddListener(delegate { UseGravity(gravityToggle);});
        massSlider.onValueChanged.AddListener(delegate { MassModifier();});
        
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
        // update selectedObject cached
    }
    #endregion

    // ================== CALL WHEN CLICK ON MENU BUTTON CONTROLLER ==================   
    private void ActivateOptionMenu(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSources, bool newValue)
    {
        GetComponent<Canvas>().enabled = newValue;
        SelectedObject = pointerRef.GetHitedObject();
        
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
        
        // sync object rigidbody values with UI
        var objRigid = SelectedObject.GetComponent<Rigidbody>();
        if (objRigid != null)
        {
            massSlider.value = objRigid.mass;
            gravityToggle.isOn = objRigid.useGravity;
        }
        
        //sync position of option menu on top of object
        SetOptionMenuPos();
    }
    
    // ====================== OPTION MENU POSITION ======================  
    private void SetOptionMenuPos()
    {
        transform.position = SelectedObject.position + Vector3.up * 1.5f;
    }

    // ====================== OPTION MENU ROTATION ======================  
    private void UpdateRotation()
    {
        transform.rotation = headset.rotation * orginalRotation;
    }

    
    // ====================== Gravity ======================  
    private void UseGravity(Toggle toggle)
    {
        var interations = SelectedObject.GetComponent<Interactable>();
        if (interations != null)
        {
            interations.Gravity(toggle.isOn);
            Debug.Log("object " + SelectedObject.name + "| gravity: " + SelectedObject.GetComponent<Rigidbody>().useGravity);
        }
    }

    private void MassModifier()
    {
        var interations = SelectedObject.GetComponent<Interactable>();
        if (interations != null)
        {
            
            Debug.Log("object " + SelectedObject.name + "| mass: " +SelectedObject.GetComponent<Rigidbody>().mass);
            interations.ModifyMass(massSlider.value);
        }
    }
    // ====================== Destroy Object ======================  
    private void DestroyObject()
    {
        var interations = SelectedObject.GetComponent<Interactable>();
        if(interations != null)
            interations.DestroyObject();
    }
    
        
    // ====================== Scale Object ======================  
    private void ScaleObject()
    {
        if (scalePadActionLeft.GetState(leftHand.handType) && scalePadActionRight.GetState(rightHand.handType))
        {
            Debug.Log("both trigger as scaling action pressed!");
        }
        // if both controller grab pressed
            // cash distance of controllers when grab on both controllers pressed
                // if cashedDistance < currentDisControllers
                    //scale object
    }
    
    private float UpdateDisControllers()
    {
        float distance = Vector3.SqrMagnitude(leftController.position - rightController.position);
        return distance;
    }
    
    
}
