using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_prepFusée : Npc_Basic
{
    //Fetchthings
    [SerializeField] private Item[] fetchThis;//exemple bannane
    [SerializeField] private int[] fetchThisQte;//exemple 2 ainsi le npc veut 2 banane
    [SerializeField] private int ChronoCoinNeeded;

    private GameManager gm;
    private bool fusee;

    private void Start()
    {
        gm = GameManager.gmInstance;
        conversation = this.gameObject.GetComponent<DialogueTrigger>();

        //for testing
        talked = true;
        gm.ModifyChronoCoin(50, false);
    }

    public override void Interact(Player joueur)//quand joueur interagit avec NPC
    {
        if (!talked)
        { //le joueur na pas parler au npc une premiere fois yet  
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueStart();
                talked = true;
            }

        }
        else if (quest_completed)
        {//la quete est faite mais le joueur for some reason veut parler au npc
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueIdleChat();
            }
        }
        else if (fusee)//Check if you have what the NPC WANTS
        {
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueEnd();
                quest_completed = true;
            }

        }
        else
        {//tu nas pas ce que le npc veut
            if (!manager.FadeOut)
            {
                conversation.TriggerDialogueWaiting();

                //check money
                //this works
                if (ChronoCoinNeeded != 0)
                { //si 0 = joueur as payer donc pas besoin de check
                    if (gm.GetChronoCoin() > ChronoCoinNeeded)
                    { //si tu as plus d'argent que le doc a besoin pour la fusee
                        gm.ModifyChronoCoin(ChronoCoinNeeded, true);
                        ChronoCoinNeeded = 0;
                    }else {
                        ChronoCoinNeeded -= gm.GetChronoCoin();
                        gm.ModifyChronoCoin(gm.GetChronoCoin(), true);
                    }


                }


                    //check objects
                    for (int z = 0; z <= fetchThis.Length; z++) { //Check les objects necessaire pour la fusee
                        Debug.Log("Currently looking for" + fetchThis[z].Nom);


                        if (fetchThisQte[z] != 0)//si la qte nest pas a 0 il faut check si le joeur a des objects a donner
                        {
                            int QTE = fetchThisQte[z];
                            for (int a = 0; a < QTE; a++) {//je vais check si je peux enlever au moins 1 de l'item donc on a beosin pour le fusée
                                if (joueur.BarreInventaire.TryPayWithItemStack(new ItemStack(fetchThis[z], 1)))//est ce que le jouer a au moins 1 de cet object
                                {
                                    Debug.Log("One "+ fetchThis[z].Nom+ "is given");                                  
                                    fetchThisQte[z]--;

                                    if (fetchThisQte[z] == 0)
                                    {
                                    Debug.Log("Currently 0 of object");
                                        z++;
                                        break;
                                    }
                                }else{
                                    Debug.Log("Player doesnt have any "+ fetchThis[z].Nom + "on them");
                                    z++;
                                    break;
                                }
                            }
                        }

                    }
                
                CheckIfFuseeComplete();

            }
        }
    }


    public void CheckIfFuseeComplete() {
        int check = 0;
        for (int z = 0; z > fetchThis.Length; z++)
        {
            if (fetchThisQte[z] == 0)
            {
                check++;
            }
            else
            {
                //mettre message qui dit besoin de x
                Debug.Log("You need "+fetchThisQte[z]+" of "+fetchThis[z].Nom);

            }
        }

        if (ChronoCoinNeeded == 0)
        {           
            if (check == fetchThis.Length)
            {
                fusee = true;//La fusée est complété
            }

        }
        else {
            //mettre message qui dit besoin de x cc
            Debug.Log("You need "+ChronoCoinNeeded+"cc");
        }
    }
}
