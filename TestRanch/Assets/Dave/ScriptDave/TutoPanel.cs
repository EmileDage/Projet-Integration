using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    // Start is called before the first frame update
    void Start()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        StartCoroutine(RemovePanel1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RemovePanel1()
    {

        yield return new WaitForSeconds(10f);
        panel1.SetActive(false);
        panel2.SetActive(true);
        yield return new WaitForSeconds(10f);
        panel2.SetActive(false);
    }
}
