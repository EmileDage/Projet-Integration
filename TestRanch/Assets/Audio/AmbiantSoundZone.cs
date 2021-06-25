using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class AmbiantSoundZone : MonoBehaviour
{
    [SerializeField] AudioClip sonAmbiant;
    private AudioSource source;
    //GameManager gm;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Le joueur est dans la zone" + gameObject);
            source.clip = sonAmbiant;
            source.loop = true;
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            source.Stop();
        }
    }

}
