using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISaveLoad : MonoBehaviour
{

    public void Save()
    {
        GameEvents.OnSaveInitiated();
    }

    public void Load()
    {
        GameEvents.OnLoadInitiated();
    }

    public void DeleteAll()
    {
        SaveSystem.DeleteAllSave();
    }
}
