using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Crédits : Marc-André Larouche

public class Load_Scene : MonoBehaviour
{
    private AsyncOperation async;
    public void BtnLoadScene(int i)
    {
       
        if (async == null)
        {
            //charge la future scene
            async = SceneManager.LoadSceneAsync(i);
            async.allowSceneActivation = true;//false= charge dans le backround
        }
    }

    public void BtnLoadScene(string s)
    {

         if (async == null)
        {
            //charge la future scene
            async = SceneManager.LoadSceneAsync(s);
            async.allowSceneActivation = true;
        }
    }


}
