using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static System.Action LoadInitiated;
    public static System.Action SaveInitiated;

    public static void OnSaveInitiated()
    {
        if (SaveInitiated != null)
            SaveInitiated.Invoke();
    }

    public static void OnLoadInitiated()
    {
        if (LoadInitiated != null)
            LoadInitiated.Invoke();
    }


}
