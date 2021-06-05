using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



public class DayNightVisual : MonoBehaviour
{
    MyTimeManager TM;
    //[SerializeField] private Transform sunTransform;
    [SerializeField] private Text heur;
    //Vector3 sunRotation;
    private void Start()
    {
        TM = MyTimeManager.timeInstance;
       // sunRotation = new Vector3(-90,0 , 0);
       // sunTransform.rotation = Quaternion.Euler(sunRotation);
        TM.GHourPassed += OnGameHourChanged;
        
    }

    
    private void OnGameHourChanged(object source) 
    {
        //TM.Hour;
        //à 12h, le soleil est à 90 degree en x
        //à 0h, le soleil est a 270 degre en x
      //  sunRotation.x = (270 - (TM.Hour*15));
      //  Debug.Log(sunRotation.x);
       // sunTransform.rotation = Quaternion.Euler(sunRotation);
        heur.text = TM.Hour + " : 00";
    }
}
