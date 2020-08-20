//////////////////////////////////////////////////////
//
//
//
//
//
//////////////////////////////////////////////////////

using UnityEngine;

namespace saveSystem
{
    public class ObjectHandler : MonoBehaviour
    {
        
        #region Public Fields
        
        public ObjectType objectType;

        public ObjectData objectData;
        
        #endregion

        
        #region Unity Methods

        private void Start()
        {
            // tell our objects to add themselves to our savedata when they are spawned if the don't already exist

            if (string.IsNullOrEmpty(objectData.id))
            {
                // creating unique id for every objects
                objectData.id = System.DateTime.Now.ToLongDateString() + System.DateTime.Now.ToLongDateString() +
                                Random.Range(0, int.MaxValue).ToString();
                objectData.objectType = objectType;
                SaveData.Current.objects.Add(objectData);
            }

//            GameEvents.current.onloadEvent += DestroyMe();
        }

        private void Update()
        {
            Transform trans = transform;
            objectData.position = trans.position;
            objectData.rotation = trans.rotation;
        }
        
        #endregion


        #region Private Methods
        private void DestroyMe()
        {
//            GameEvents.current.onLoadEvent -= DestroyMe(); 
            Destroy(gameObject);
        }
        #endregion
    }

}
