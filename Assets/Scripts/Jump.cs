using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour, IUseable
{
    public float jumpPower;

    public void Use(Player player)
    {
        player.rigid.velocity = new Vector3(player.rigid.velocity.x, jumpPower);
        if (GetComponent<Collider>().isTrigger)
        {
            Destroy(gameObject);
        }
    }
}

public interface IUseable
{
    void Use(Player player);
}