using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class END_Fusee : MonoBehaviour, IInteractible
{
    //idealement tu voit la fusee monter dans lespcae puis credits
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject pannel;
    [SerializeField] private GameObject[] permanent_UI;
    [SerializeField] private ParticleSystem[] engine_fires;
    private Animator anime;
    private bool fadeIn;
    private GameManager gm;
    private Player joueur;
    private Camera cam_joueur;
    [SerializeField] private Camera cam_fusee;


    public void Interact(Player joueur)
    {
        foreach (GameObject ui in permanent_UI)
            ui.SetActive(false);
        joueur.gameObject.SetActive(false);
        cam_joueur.enabled = false;
        cam_fusee.enabled = true;
        anime.SetBool("GoingToSpace", true);

        foreach(ParticleSystem fire in engine_fires)
            fire.gameObject.SetActive(true);

        
    }

    public void RollCredits() {
        pannel.SetActive(true);
        fadeIn = true;
        //we wait until complety fadeIn to actually rollcredits
    }

    

    private void Awake()
    {
        cam_fusee.enabled = false;//just to be safe to not fuck up normal cam

    }

    private void Start()
    {
        credits.SetActive(false);
        pannel.SetActive(false);
        pannel.GetComponent<CanvasGroup>().alpha = 0;
        foreach (ParticleSystem fire in engine_fires)
            fire.gameObject.SetActive(true);
        anime = this.gameObject.GetComponent<Animator>();
        gm = GameManager.gmInstance;
        joueur = gm.Joueur;
        cam_joueur = gm.Joueur.playerCam.GetComponent<Camera>();
    }

    private void Update()
    {
        if (fadeIn)
        {
            pannel.GetComponent<CanvasGroup>().alpha += Time.deltaTime / 3;
            if (pannel.GetComponent<CanvasGroup>().alpha >= 1)
            {
                credits.SetActive(true);
                fadeIn = false;
            }
        }
    }
}
