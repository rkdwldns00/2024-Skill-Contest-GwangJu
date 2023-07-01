using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour, IUseable
{
    public bool isOnce;

    public void Use(Player player)
    {
        player.haveDoubleJump = true;
        if(isOnce)
        {
            Destroy(gameObject);
        }
    }
}
