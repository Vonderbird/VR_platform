//////////////////////////////////////////////////////
//
//
//
//
//
//////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace saveSystem
{

    [System.Serializable]
    public enum ObjectType
    {
        Tree,
        Bush,
        Stone
    }

    [System.Serializable]
    public class ObjectData
    {
        public string id;
        
        public ObjectType objectType;
        
        public Vector3 position;
        
        public Quaternion rotation;
        
    }
    
}
