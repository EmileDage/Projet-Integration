using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OnScreenMessage : MonoBehaviour
{
    private float timeAlive = 1;
    private float counter;
    [HideInInspector]public Text txt;
    Color colr;


   /* private float maxAlpha = 1;
    private float targetAlpha = 0;
    private float currentAlpha = 1;*/
    //private Text msg;
    private void Start()
    {
        //   msg = GetComponent<Text>();
        counter = timeAlive;
        this.gameObject.SetActive(false);
        txt = GetComponent<Text>();
        colr = txt.color;
    }
    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        txt.CrossFadeAlpha(0, 0.5f, false);
        if (counter <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartCounter(string msg)
    {
        counter = timeAlive;
        txt.text = msg;
        
        gameObject.SetActive(true);
    }
}
