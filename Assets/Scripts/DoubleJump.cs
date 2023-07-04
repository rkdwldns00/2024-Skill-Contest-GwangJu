using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour, IUseable, IResetable
{
    public bool isOnce;

    private void Start()
    {
        LevelManager.AddResetable(this);
    }

    public void Use(Player player)
    {
        player.haveSkill = Player.Skill.DoubleJump;
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
