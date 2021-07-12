using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Credits : Brackeys
//https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public static DialogueManager d_instance;

    [SerializeField]private GameObject dialogueBox;

    public Text name_txt;
    public Text dialogue_txt;

    //How long till another letter is showned for the sentence
    private float regular_speed = 0.05f;//trust me this is slow enough hopefully
    private float current_speed;

    private bool check;//regarde si le text a finit de s'écrire

    private bool fadeIn;
    private bool fadeOut;
    public bool FadeOut{ get => fadeOut;}

    //to lock cam during dialogue
    private GameManager gm;
    private Player joueur;
    private GameObject cam_joueur;

    private void Awake()
    {
        if (d_instance != null && d_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            d_instance = this;
        }

    }

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
        dialogueBox.GetComponent<CanvasGroup>().alpha = 0;

        //to lock cam during dialogue
        gm = GameManager.gmInstance;
        joueur = gm.Joueur;
        cam_joueur = gm.Joueur.playerCam;
    }

    private void Update()
    {
        if (fadeIn) {         
            dialogueBox.GetComponent<CanvasGroup>().alpha += Time.deltaTime /2 ;
            if (dialogueBox.GetComponent<CanvasGroup>().alpha >= 1) {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            dialogueBox.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            if (dialogueBox.GetComponent<CanvasGroup>().alpha <= 0)
            {
                   
                Cursor.visible = false;
                cam_joueur.GetComponent<CameraControl>().UnlockCamera();

                dialogueBox.SetActive(false);
                fadeOut = false;

            }
        }

    }

    public void StartDialogue(Dialogue dialogue)
    {

        Cursor.visible = true;
        cam_joueur.GetComponent<CameraControl>().LockCamera();

        dialogueBox.SetActive(true);
        fadeIn = true;

        name_txt.text = dialogue.name;

        sentences.Clear();//reset

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {//trigger en appuyant sur enter ou sur le text pour continuer
        if (sentences.Count ==0) { //end of queue

            if (!check)
            {                
                EndOfDialog();
                return;
            }
            else
            {
                Debug.Log("Please wait till the text finish displaying before proceeding to the end of the dialogue");
                current_speed = 0;//makes text display faster
            }
        
        }
        
        if (!check)
        {
            current_speed = regular_speed;
           string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(sentence));
        }
        else {

            Debug.Log("Please wait till the text finish displaying before proceeding to the next one");
            current_speed = 0;//makes text display faster
        }

    }


    IEnumerator TypeSentence(string sentence) {
        check = true;
        dialogue_txt.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogue_txt.text += letter;
            yield return new WaitForSeconds(current_speed) ;
        }
        check = false;
    }

    private void EndOfDialog()
    {
        if (fadeIn) {
            fadeIn = false;
        }
        fadeOut = true;
        Debug.Log("End of convo");

    }

   

  
}
