using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Crédits : Marc-André Larouche

public class Loading : MonoBehaviour
{

    private AsyncOperation async;

    [SerializeField] private Image progressbar;
    [SerializeField] private Text txtPourcent;

    [SerializeField] private bool waitForUserInput = false;
    private bool ready = false;

    [SerializeField] private float delay = 0;

    [SerializeField] private int sceneToLoad = -1;

    void Start()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();//vas reinitialiser les entrées durant 1 frame
        //jette les trucs inutiles sur la ram
        System.GC.Collect();//garbage collector vide la ram des trucs inutiles
        //chargement de scene incremental
        Scene currentscene = SceneManager.GetActiveScene();

        if (sceneToLoad < 0)
        {//si la valeur negative
            async = SceneManager.LoadSceneAsync(currentscene.buildIndex + 1);
        }
        else//si la avleur positive
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        async.allowSceneActivation = false;//atttendree avant de passer a la scene suivante
        //il blocque a 90 donc on le turn true dans update

        if (waitForUserInput == false)
        {//invoke appel une fonction apres un delais
            Invoke("Activate", delay); //acrive la rpochaine scene apres un delais
        }
    }

    public void Activate()//appel cette fonction pour passer a la  scene suivante
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForUserInput && Input.anyKey)
        {
            ready = true;

        }
        if (progressbar)
        {
            progressbar.fillAmount = async.progress + 0.1f;//fill la barre verte avec un début de 10 pour compenser 
        }
        if (txtPourcent)
        {
            txtPourcent.text = ((async.progress + 0.1f) * 100).ToString("F2") + " %";//F2 = 2 deciamls ex 88.88 %
        }
        //massurer charger tout avant de changer scene et tout le slogos afficher
        if (async.progress > 0.89f && SplashScreen.isFinished && ready == true)
        {
            async.allowSceneActivation = true;
        }
    }
}
