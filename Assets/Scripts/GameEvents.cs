//////////////////////////////////////////////////////
//
//
//
//
//
//////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    #region Public Fields
    public static GameEvents current;

    
    #endregion
    
    #region Unity Methods

    private void Awake()
    {
        current = this;
    }
    
    void Start()
    {
        
    }
    #endregion


    #region Private Methods

 
    #endregion
}
