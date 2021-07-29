using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Crédits : Marc-André Larouche

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultButtons;
    [SerializeField] private AudioSource a_s;
    [SerializeField] private Canvas credits;


    void Start()
    {
        Invoke("PanelToggle", 0.01f);
        if(credits != null)
        credits.gameObject.SetActive(false);
    }

    public void PanelToggle()
    {
        PanelToggle(0);
    }

    public void PanelToggle(int position)
    {
        a_s.Play();
        Input.ResetInputAxes();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);

            if (position == i)
            {
                defaultButtons[i].Select();
            }
        }

    }

    public void Exit_game()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }


    public void Credits_play() { 
        credits.gameObject.SetActive(true);
    }

    public void Credits_cancel()
    {
        credits.gameObject.SetActive(false);
    }
}
