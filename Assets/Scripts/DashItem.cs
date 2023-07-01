using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashItem : MonoBehaviour,IUseable
{
    public Vector2 direction;
    public bool isOnce = true;

    public void Use(Player player)
    {
        player.rigid.useGravity = false;
        player.rigid.velocity = Vector3.zero;
        player.dashDirection = direction;
        if (isOnce)
        {
            Destroy(gameObject);
        }
    }
}
