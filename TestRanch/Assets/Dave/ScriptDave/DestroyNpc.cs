using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNpc : MonoBehaviour
{
    [SerializeField] private GameObject nextNPC;
    // Start is called before the first frame update
    void Start()
    {
        nextNPC.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<NPC_Fetch>().Quest_completed == true)
        {
            StartCoroutine(WaitForAnswer());
        }
    }

    private IEnumerator WaitForAnswer()
    {
        nextNPC.SetActive(true);
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
