using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Outils", menuName = "ScriptableItems/Outils", order = 3)]
public class Outils : Item
{

    public UnityEvent toolMethod;
    Player player;

    public override void UseThis(ItemStack itemStack, Player joueur)
    {
        base.UseThis(itemStack, joueur);
        player = joueur;
        toolMethod.Invoke();
    }

    public void MiningTool()
    {
        Debug.Log("using mining tool");

        if(player.HitScan(out RaycastHit hit)) {

            if (player.DistanceCheck(hit)) { 
                IMinable minable = hit.collider.GetComponent<IMinable>();
                if (minable != null)
                {
                    minable.Mine(player);
                    player.Item_AS.PlayOneShot(useSound);
                }
            }
        }
    }

    public void BecheHoe()
    {
        Debug.Log("b�che");
        if (player.HitScan(out RaycastHit hit))
        {
            if (player.DistanceCheck(hit))
            {
                IFarmable farmable= hit.collider.GetComponent<IFarmable>();
                if (farmable != null)
                {
                    farmable.FarmIt();
                    player.Item_AS.PlayOneShot(useSound);
                    
                }
            }
        }
    }

    public void BuilderTool()
    {
        Debug.Log("builder tool");
        if (player.HitScan(out RaycastHit hit))
        {

            if (player.DistanceCheck(hit))
            {
                IBuildable buildable = hit.collider.GetComponent<IBuildable>();
                if (buildable != null)
                {
                    buildable.Build();
                    player.Item_AS.PlayOneShot(useSound);
                }
            }
        }
    }

    public void WateringCan()
    {
        Debug.Log("water");
        if (player.HitScan(out RaycastHit hit))
        {

            if (player.DistanceCheck(hit))
            {
                IWaterable waterable = hit.collider.GetComponent<IWaterable>();
                if (waterable != null)
                {
                    waterable.Watering();
                }
            }
        }
    }

    public void CaptureTool()
    {
        Debug.Log("Capturing");
        if (player.HitScan(out RaycastHit hit))
        {

            if (player.DistanceCheck(hit))
            {
                ICapturable capture = hit.collider.GetComponent<ICapturable>();
                if (capture != null)
                {
                    capture.Capture();
                    player.Item_AS.PlayOneShot(useSound);
                }
            }
        }

    }

   
}
