using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBlock : MonoBehaviour,IUseable
{
    public void Use(Player player)
    {
        Destroy(gameObject);
    }
}
