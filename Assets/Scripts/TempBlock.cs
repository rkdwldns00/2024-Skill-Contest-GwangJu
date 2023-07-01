using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBlock : MonoBehaviour, IUseable, IResetable
{
    private void Start()
    {
        LevelManager.AddResetable(this);
    }

    public void Use(Player player)
    {
        gameObject.SetActive(false);
    }

    public void ResetObject()
    {
        gameObject.SetActive(true);
    }
}
