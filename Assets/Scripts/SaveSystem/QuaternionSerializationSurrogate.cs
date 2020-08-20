///////////////////////////////////////////////////////////////////////////
//
//    Desc   : This class is an interface to handle serialize/deserialize 
//             rotation type, because this data type is not serializable 
//             by default in Unity.
//
//    author : AmirArdroudi
//
///////////////////////////////////////////////////////////////////////////

using System.Runtime.Serialization;
using UnityEngine;

namespace saveSystem
{
    public class QuaternionSerializationSurrogate : ISerializationSurrogate
    {

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Quaternion quaternion = (Quaternion) obj;
            info.AddValue("x", quaternion.x);
            info.AddValue("y", quaternion.y);
            info.AddValue("z", quaternion.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Quaternion quaternion = (Quaternion) obj;
            quaternion.x = (float) info.GetValue("x", typeof(float));
            quaternion.y = (float) info.GetValue("y", typeof(float));
            quaternion.z = (float) info.GetValue("z", typeof(float));
            quaternion.w = (float) info.GetValue("w", typeof(float));
            obj = quaternion;
            return obj; 
        }
    }
    
}// saveSystem
