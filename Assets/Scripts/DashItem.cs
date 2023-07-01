using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashItem : MonoBehaviour,IUseable,IResetable
{
    public bool isOnce = true;

    private void Start()
    {
        LevelManager.AddResetable(this);
    }

    public void Use(Player player)
    {
        player.haveDash = true;
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
