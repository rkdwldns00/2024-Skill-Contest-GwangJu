using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashItem : MonoBehaviour,IUseable,IResetable
{
    public Vector2 direction;
    public bool isOnce = true;

    private void Start()
    {
        LevelManager.AddResetable(this);
    }

    public void Use(Player player)
    {
        player.rigid.useGravity = false;
        player.rigid.velocity = Vector3.zero;
        player.dashDirection = direction;
        if (isOnce)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetObject()
    {
        gameObject.SetActive(true);
    }
}
