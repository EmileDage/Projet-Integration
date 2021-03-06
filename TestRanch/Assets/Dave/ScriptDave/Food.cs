using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public CreatureBehavior creature;
    [SerializeField] private int happinessIncrease;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "produit")
        {
            WorldObjectMateriaux food = other.GetComponent<WorldObjectMateriaux>();
            if (Fonctions.produits_vegetaux.Equals(food.Item().Funct) && creature.CreatureInfo.hungry == "Yes" && food.Item().Nom.Equals(creature.CreatureInfo.FoodLikes))
            {
                creature.CreatureInfo.hungry = "No";
                creature.Happiness += happinessIncrease;
                Destroy(other.gameObject);
            }
        }
    }
}
