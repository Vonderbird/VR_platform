﻿//////////////////////////////////////////////////////
//
//    Description : Singleton class 
//
//    Author      : AmirArdroudi
//
//////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using saveSystem;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;

    public static SaveData Current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }
        set { _current = value; }
    }

    public List<ObjectData> objects;

}