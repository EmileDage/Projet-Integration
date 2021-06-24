using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Coffre))]
public class PrefillChest : MonoBehaviour
{
    [SerializeField] private List<Item> toFillChest;
    [SerializeField] private List<int> qtes;
    private List<ItemStack> listI_S;

    private void Start()
    {
        listI_S = new List<ItemStack>();
        for (int i = 0; i < toFillChest.Count; i++)
        {
            listI_S.Add(new ItemStack(toFillChest[i], qtes[i]));
        }
        
        GetComponent<Coffre>().Contenu = listI_S;
    }
}
