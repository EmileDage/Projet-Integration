using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] float delayMin;
    [SerializeField] float delayMax;
    [SerializeField] List<AudioClip> tracks;
    private AudioSource source;
    int currentSong = 0;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(MusicCoroutine());
    }

    private IEnumerator MusicCoroutine()
    {
        float a = Random.Range(delayMin, delayMax) * 60;
        Debug.Log(a + " temps à attendre");
        yield return new WaitForSeconds(a);
        source.clip = tracks[currentSong];
        source.Play();

      yield return new WaitWhile(IsMusicOn);
        //
        Debug.Log("new song");
        currentSong++;
        currentSong %= (tracks.Count);
        StartCoroutine(MusicCoroutine());
    }

    private bool IsMusicOn() => source.isPlaying;

}
