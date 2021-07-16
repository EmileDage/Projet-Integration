using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int attack;
    [SerializeField] private float speedPro;

    public int Attack { get => attack; set => attack = value; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Death());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speedPro);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<HealthModule>().DecreaseHealth(attack);
            Debug.Log(other.GetComponent<HealthModule>().GetHealth().GetCurrentValue());
            Destroy(gameObject);
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
