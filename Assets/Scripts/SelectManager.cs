using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [SerializeField] private string interactableTag = "interactable";
    public Pointer pointerRef;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    private Transform selectedObj;


    void Update() 
    {
        
        HighlightObject();
    }

    void HighlightObject()
    {
        // dehighlight
        if(selectedObj != null)
        {
            var renderer = selectedObj.GetComponent<Renderer>();
            renderer.material = defaultMaterial;
            selectedObj = null;
        }
        // highlight
        Transform obj = pointerRef.GetHitedObject();
        if(obj.CompareTag(interactableTag))
        {
            var Renderer = obj.GetComponent<Renderer>();
            if(Renderer != null)
            {
                Renderer.material = highlightMaterial;
            }
            selectedObj = obj;
        }
    }
   
}
