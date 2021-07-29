using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextNPC : MonoBehaviour
{
    [SerializeField] private GameObject nextNPC;

    void Start()
    {
        nextNPC.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (this.gameObject.GetComponent<NPC_talking>().Talked == false)
        {
            StartCoroutine(WaitForAnswer());
        }
    }

    private IEnumerator WaitForAnswer()
    {
        nextNPC.SetActive(true);
        yield return new WaitForSeconds(10f);
    }
}
