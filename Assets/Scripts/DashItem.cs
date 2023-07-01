using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashItem : MonoBehaviour,IItem
{
    public void Use(Player player)
    {
        player.rigid.useGravity = true;

    }
}
