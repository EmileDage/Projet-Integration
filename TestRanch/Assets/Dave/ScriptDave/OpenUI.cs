using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] private GameObject addUI;
    [SerializeField] private GameObject removeUI;

    private void Start()
    {
        addUI.SetActive(false);
        removeUI.SetActive(false);
    }

    public void AddOpenUI()
    {
        addUI.SetActive(true);
        removeUI.SetActive(false);
    }

    public void RemoveOpenUI()
    {
        addUI.SetActive(false);
        removeUI.SetActive(false);
    }

}
