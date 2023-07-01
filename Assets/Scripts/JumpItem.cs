using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour,IItem
{
    public void Use(Player player)
    {
        player.rigid.velocity = new Vector3(player.rigid.   velocity.x, player.jumpPower);
        Destroy(gameObject);
    }
}

public interface IItem
{
    void Use(Player player);
}