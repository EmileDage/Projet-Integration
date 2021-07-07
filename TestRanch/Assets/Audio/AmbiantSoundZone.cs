using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmbiantSoundZone : MonoBehaviour
{
    [SerializeField] AudioClip sonAmbiant;
    [SerializeField] private AudioSource source;

    [SerializeField] bool startZone;
    private void Start()
    {
        if (startZone) {
            Debug.Log("Le joueur est dans la zone" + gameObject);
            source.clip = sonAmbiant;
            source.loop = true;
            source.Play();
        }
    }
    //GameManager gm;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(source.clip != sonAmbiant) {
            Debug.Log("Le joueur est dans la zone" + gameObject);
            source.clip = sonAmbiant;
            source.loop = true;
            source.Play(); }
        }
    }


}
