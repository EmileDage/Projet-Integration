using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC_basicsRewards : Npc_Basic
{
    //rewards
    [SerializeField] protected Item[] rewards;
    [SerializeField] protected int[] rewardsQte;
    [SerializeField] protected Coffre chest;//le npc va juste drop un chest avec l


}
