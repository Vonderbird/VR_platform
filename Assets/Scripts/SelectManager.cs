//////////////////////////////////////////////////////
// desc:    Highlight object with selected with pointer
//
// author:  amirardroudi
//////////////////////////////////////////////////////
/// 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [SerializeField] private string interactableTag = "interactable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    
    public Pointer pointerRef;
    private Transform highlightedObject = null;


    void Update() 
    {
        HighlightObject();
    }

    void HighlightObject()
    {
        Transform obj = pointerRef.GetHitedObject();
        if (obj == null)
        {
            var renderer = highlightedObject.GetComponent<Renderer>();
            renderer.material = defaultMaterial;
            defaultMaterial = null;
            highlightedObject = null;
            return;
        }
        // dehighlight
        if(highlightedObject != null)
        {
            if (highlightedObject.GetInstanceID() == obj.GetInstanceID())
                return;
            else
            {
                
                var renderer = highlightedObject.GetComponent<Renderer>();
                renderer.material = defaultMaterial;
                defaultMaterial = null;
                highlightedObject = null;
            }
        }
        
        // highlight
        if(obj.CompareTag(interactableTag))
        {
            var Renderer = obj.GetComponent<Renderer>();

            if(Renderer == null)
                return;
                
            defaultMaterial = Renderer.material;
            Renderer.material = highlightMaterial;
                                                        
            highlightedObject = obj;
        }
        
    }
}
