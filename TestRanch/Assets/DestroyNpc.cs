using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNpc : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

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
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
