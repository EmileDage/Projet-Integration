using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OnScreenMessage : MonoBehaviour
{
    private float timeAlive = 1;
    private float counter;
    public Text txt;
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
    }
    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if(counter <= 0)
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
