using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCoffre : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkSiVide())
        {
            gameObject.SetActive(false);
        }
    }

    private bool checkSiVide()
    {
        foreach (ItemStack item in GetComponent<Coffre>().Contenu)
        {
            if (!item.isEmpty())
                return false;
        }
        return true;
    }
}
