using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class UpgradeSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        UpgradeSlot us = (UpgradeSlot)obj;
        info.AddValue("UpgradeSlot", us);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        UpgradeSlot us = (UpgradeSlot)obj;
        obj = us;
        return obj;
    }
}
