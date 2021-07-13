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


    void Start()
    {
        Invoke("PanelToggle", 0.01f);
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
}
