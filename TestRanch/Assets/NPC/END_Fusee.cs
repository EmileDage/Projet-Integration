using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class END_Fusee : MonoBehaviour, IInteractible
{
    //idealement tu voit la fusee monter dans lespcae puis credits
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject pannel;
    [SerializeField] private ParticleSystem engine_fire;
    private Animator anime;
    private GameManager gm;
    private Player joueur;
    private Camera cam_joueur;
    [SerializeField] private Camera cam_fusee;


    public void Interact(Player joueur)
    {

        joueur.gameObject.SetActive(false);
        cam_joueur.enabled = false;
        cam_fusee.enabled = true;
        anime.SetBool("GoingToSpace", true);
        engine_fire.gameObject.SetActive(true);

        
    }

    public void RollCredits() {
        pannel.SetActive(true);
        credits.SetActive(true);

    }

    private void Awake()
    {
        cam_fusee.enabled = false;//just to be safe to not fuck up normal cam

    }

    private void Start()
    {
        credits.SetActive(false);
        pannel.SetActive(false);
        //pannel.GetComponent<Ima>
        engine_fire.gameObject.SetActive(false);
        anime = this.gameObject.GetComponent<Animator>();
        gm = GameManager.gmInstance;
        joueur = gm.Joueur;
        cam_joueur = gm.Joueur.playerCam.GetComponent<Camera>();
    }


}
