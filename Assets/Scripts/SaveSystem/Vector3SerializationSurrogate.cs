///////////////////////////////////////////////////////////////////////////
//
//    Desc   : This class is an interface to handle serialize/deserialize 
//             Vector3 type, because this data type is not serializable 
//             by default in Unity.
//
//    author : AmirArdroudi
//
///////////////////////////////////////////////////////////////////////////

using System.Runtime.Serialization;
using UnityEngine;

namespace saveSystem
{
    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 v3 = (Vector3) obj;
            
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            Vector3 v3 = (Vector3) obj;
            v3.x = (float) info.GetValue("x", typeof(float));
            v3.y = (float) info.GetValue("y", typeof(float));
            v3.z = (float) info.GetValue("z", typeof(float));
            obj = v3;
            return obj;
        }
    
        
    }
    
}// saveSystem