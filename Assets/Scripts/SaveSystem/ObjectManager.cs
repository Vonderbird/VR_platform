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
    public class ObjectManager : MonoBehaviour
    {

        #region Public Fields
        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            
        }
        #endregion

        public void OnSave()
        {
            SerializationManager.Save("ObjectSave", SaveData.Current);
        }

        public void OnLoad()
        {
//            GameEvents.current.dispatchOnLoadEvent();

            SaveData.Current =
                (SaveData) SerializationManager.Load(Application.persistentDataPath + "/saves/objectSave.ema");

            for (int i = 0; i < SaveData.Current.objects.Count; i++)
            {
                ObjectData currentObj = SaveData.Current.objects[i];
            }
        }
        
        #region Private Methods
        
        #endregion
    }
}