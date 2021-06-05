using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerUnityEvent : UnityEvent<Player>
{
}

[System.Serializable]
public class GameObjUnityEvent : UnityEvent<GameObject>
{
}
