using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emile
public class MyTimeManager : MonoBehaviour
{
    

    private int second=0;
    [SerializeField]private int hour = 5;//l'heure à laquelle le jeux commence -1
    private int gameHourLenght;//en secondes
    private readonly int nbHourinDay = 23;
    [SerializeField]float gameDayLenght;//en minutes

    public static MyTimeManager timeInstance;

    public int Second { get => second;}
    public int Hour { get => hour;}

    

    public int GetNbHourDay()
    {
        return nbHourinDay;
    }

    #region time events
    public delegate void SecondPassedEventHandler(object source);
    public event SecondPassedEventHandler SecondPassed;

    public delegate void GameHourPassedEventHandler(object source);
    public event GameHourPassedEventHandler GHourPassed;

    public delegate void GameDayPassedEventHandler(object source);
    public event GameDayPassedEventHandler GDayPassed;


    void Start()
    {
        gameHourLenght = Mathf.RoundToInt((gameDayLenght * 60)/nbHourinDay);
       // Debug.Log(gameHourLenght + " durée en seconde d'une heure en jeux");
        Invoke(nameof(OnGameHourPassed), 0.01f);
        StartCoroutine(SecondPassCoroutine());
    }

    private IEnumerator SecondPassCoroutine()
    {
        yield return new WaitForSeconds(1);

        OnSecondPassed();

        StartCoroutine(SecondPassCoroutine());
    }

    private void OnSecondPassed()
    {
        second++;
        if (SecondPassed != null)
        {
            SecondPassed(this);
        }
        if(second >= gameHourLenght)
        {
            second = 0;
         //   Debug.Log("Une heure est passée");
            OnGameHourPassed();    
        }
    }

    private void OnGameHourPassed()
    {
        hour++;
        //Debug.Log("l'heure est " + hour);
        if(GHourPassed != null)
        {           
            GHourPassed(this);
        }
        if(hour>= nbHourinDay)
        {
            hour = 0;
            OnGameDayPassed();
        }
    }

    private void OnGameDayPassed()
    {
        if(GDayPassed != null)
        {
            
            GDayPassed(this);
        }
    }
    #endregion


    void Awake()
    {
        if(timeInstance != null && timeInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timeInstance = this;
        }
    }


}
