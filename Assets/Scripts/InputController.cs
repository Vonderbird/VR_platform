using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputController : MonoBehaviour
{
    [Header("References")]
    public MenuManager menuManagerRef;
    public GameObject rightController;
    public Pointer pointerRef;

    public Transform generatedObj;
    public bool objectAttached;
    public string interactableObjTag;
    
    public SteamVR_Action_Boolean grabAction;
    private Transform hittedObj;
    public Hand hand;
    
    
    private void Awake()
    {
        objectAttached = false;
    }

    private void OnEnable()
    {
        if (grabAction == null)
        {
            Debug.LogError("<b>[StreamVR Interaction]</b> Grab action assigned!", this);
            return;
        }
        grabAction.AddOnChangeListener(GrabReleaseObject, hand.handType);
    }
    private void OnDisable()
    {
        if(grabAction != null)
            grabAction.RemoveOnChangeListener(GrabReleaseObject , hand.handType);
    }


    // TODO: change input system to events (Remove them from update method)
    void Update()
    {
        // show tool menu
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            menuManagerRef.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }

//        hittedObj = pointerRef.GetHitedObject();
    }

    public void InstantiateObject(Transform objectPrefab)
    {
        generatedObj = Instantiate(objectPrefab,
            pointerRef.GetEndPosition() - Vector3.up*1.3f,
            rightController.transform.rotation);
    }

    private void GrabReleaseObject(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources  inputSources, bool newValue)
    {
        var intractable = hittedObj.GetComponent<Interactable>();
        var rigid = intractable.GetRigidbody();
 
        if (newValue)
        {
            hittedObj = pointerRef.GetHitedObject();
            if (!hittedObj.CompareTag(interactableObjTag) && !objectAttached) return;
            
            //disable gravity when grabbing object
            if (rigid)
                rigid.useGravity = false;
            
            hittedObj.parent = rightController.transform;
            objectAttached = true;
            Debug.Log(hittedObj.name + " Grabbed!");
        }
        // if grab released
        else
        {
            //set gravity to previous state before grabbing
            if (rigid)
                rigid.useGravity = intractable.cachedRigid.gravityState;
            
            hittedObj.parent = null;
            objectAttached = false;
            Debug.Log(hittedObj.name + " released from controller!");
            hittedObj = null;
            
        }

    }
    
}
